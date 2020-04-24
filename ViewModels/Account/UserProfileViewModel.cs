using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace nswag_liquid_slim.ViewModels.Account
{
    public class UserProfileViewModel
    {
        public Guid Id { get; set; }

        [Required, BindRequired]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = "";

        [Required, BindRequired]
        public string FirstName { get; set; } = "";

        [Required, BindRequired]
        public string LastName { get; set; } = "";

        [Required, BindRequired]
        public string Title { get; set; } = "";

        [Required, BindRequired]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; } = "";

        public ImageViewModel Image { get; set; }
            = new ImageViewModel();

        [StringLength(200)]
        public string? AboutMe { get; set; }

        [StringLength(200)]
        public string? CustomQuote { get; set; }
    }
}