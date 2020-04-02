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

            // openapi2tsclient 

            var settings = new TypeScriptClientGeneratorSettings
            {
                // ClientBaseClass = "RootApi",
                UseGetBaseUrlMethod = true,
            };

            settings.TypeScriptGeneratorSettings.TypeStyle = 
                NJsonSchema.CodeGeneration.TypeScript.TypeScriptTypeStyle.Interface;

            settings.TypeScriptGeneratorSettings.MarkOptionalProperties = true;

            var generator = new TypeScriptClientGenerator(doc, settings);

            var code = generator.GenerateFile();

            await File.WriteAllTextAsync("output.ts", code);
        }
    }
}
