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

        static async Task CompileFile(string input, string output, TypeScriptClientGeneratorSettings settings)
        {
            var doc = await OpenApiDocument.FromFileAsync(input);

            var generator = new TypeScriptClientGenerator(doc, settings);

            await File.WriteAllTextAsync(output, generator.GenerateFile());
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _ = Task.Run(async () => 
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    Logger.LogInformation("Compiling output.ts...");
                    
                    try
                    {
                        var settings = new TypeScriptClientGeneratorSettings
                        {
                            ClassName           = "{controller}",
                            Template            = TypeScriptTemplate.Fetch,
                            UseGetBaseUrlMethod = true,
                        };

                        settings.TypeScriptGeneratorSettings.EnumStyle =
                            NJsonSchema.CodeGeneration.TypeScript.TypeScriptEnumStyle.StringLiteral;
                        settings.TypeScriptGeneratorSettings.TemplateDirectory = "./Templates";
                        settings.TypeScriptGeneratorSettings.TypeStyle = 
                            NJsonSchema.CodeGeneration.TypeScript.TypeScriptTypeStyle.Interface;

                        await CompileFile("./Client/openapi.json", "./Client/output.ts", settings);
                    }
                    catch (Exception e)
                    {
                        Logger.LogError(e, "Unable to write output");
                    }

                    using var fsw = new FileSystemWatcher("Templates");
                    fsw.WaitForChanged(WatcherChangeTypes.Changed);
                }
            });
            
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}