using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace nswag_liquid_slim.ViewModels.Account
{
    /// <example>
    /// { "username": "test@crfusa.com", "password": "Password1" }
    /// </example>
    public class LoginViewModel
    {
        [Required, BindRequired, EmailAddress]
        public string Username { get; set; } = "";

        [Required, BindRequired, DataType(DataType.Password)]
        public string Password { get; set; } = "";
        
        [BindRequired]
        public string RecaptchaToken { get; set; } = "";
    }
}
