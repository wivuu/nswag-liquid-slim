
using System.ComponentModel.DataAnnotations;

namespace nswag_liquid_slim.ViewModels
{
    public class AddressViewModel
    {
        [Required]
        [StringLength(200)]
        [Display(Name = "Address")]
        public string Line1 { get; set; } = null!;

        [StringLength(200)]
        [Display(Name = "Line 2")]
        public string Line2 { get; set; } = null!;

        [Required]
        [StringLength(200)]
        [Display(Name = "City")]
        public string City { get; set; } = null!;

        [Required]
        [StringLength(100)]
        [Display(Name = "State")]
        public string State { get; set; } = null!;

        [Required]
        [StringLength(5, MinimumLength = 5)]
        [RegularExpression(@"^\d{5}$")]
        [DataType(DataType.PostalCode)]
        [Display(Name = "Zip")]
        public string Zip { get; set; } = null!;
    }
}