using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NSwag;
using NSwag.CodeGeneration.TypeScript;

namespace nswag_liquid_slim
{
    public class RecompileWatcher : IHostedService
    {
        public RecompileWatcher(ILogger<RecompileWatcher> logger)
        {
            Logger = logger;
        }

        public ILogger<RecompileWatcher> Logger { get; }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var doc = await OpenApiDocument.FromFileAsync("./Client/openapi.json");

            var settings = new TypeScriptClientGeneratorSettings
            {
                ClassName = "{controller}",
                Template  = TypeScriptTemplate.Fetch,
                UseGetBaseUrlMethod = true,
            };

            settings.TypeScriptGeneratorSettings.EnumStyle =
                NJsonSchema.CodeGeneration.TypeScript.TypeScriptEnumStyle.Enum;
            settings.TypeScriptGeneratorSettings.TemplateDirectory = "./Templates";
            settings.TypeScriptGeneratorSettings.TypeStyle = 
                NJsonSchema.CodeGeneration.TypeScript.TypeScriptTypeStyle.Interface;

            var generator = new TypeScriptClientGenerator(doc, settings);

            var fsw = new FileSystemWatcher("Templates");

            _ = Task.Run(async () => 
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    Logger.LogInformation("Compiling output.ts...");
                    
                    try
                    {
                        await File.WriteAllTextAsync("./Client/output.ts", generator.GenerateFile());
                    }
                    catch (Exception e)
                    {
                        Logger.LogError(e, "Unable to write output");
                    }

                    fsw.WaitForChanged(WatcherChangeTypes.Changed);
                }
            });
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}