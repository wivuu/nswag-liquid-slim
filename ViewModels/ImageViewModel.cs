using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;

namespace nswag_liquid_slim.ViewModels
{
    public class ImageViewModel
    {
        public string? Url { get; internal set; }

        [DataType(DataType.Upload), JsonIgnore]
        public IFormFile? ImageData { get; set; }
    }
}