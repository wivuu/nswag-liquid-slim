using System;
using System.IO;
using System.Threading.Tasks;
using NSwag;
using NSwag.CodeGeneration.TypeScript;

namespace nswag_test
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var doc = await OpenApiDocument.FromFileAsync("./openapi.json");

            var settings = new TypeScriptClientGeneratorSettings
            {
                // ClientBaseClass = "RootApi",
                UseGetBaseUrlMethod = true,
                ClassName = "{controller}",
                Template            = TypeScriptTemplate.Fetch
            };

            settings.TypeScriptGeneratorSettings.TemplateDirectory = "./Templates";
            settings.TypeScriptGeneratorSettings.TypeStyle = 
                NJsonSchema.CodeGeneration.TypeScript.TypeScriptTypeStyle.Interface;

            var generator = new TypeScriptClientGenerator(doc, settings);

            var fsw = new FileSystemWatcher("Templates");

            while (true)
            {
                Console.WriteLine("Compiling output.ts...");
                
                try
                {
                    await File.WriteAllTextAsync("output.ts", generator.GenerateFile());
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }

                fsw.WaitForChanged(WatcherChangeTypes.Changed);
            }
        }
    }
}
