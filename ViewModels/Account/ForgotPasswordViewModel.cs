using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace nswag_liquid_slim.ViewModels.Account
{
    /// <example>
    /// { "email": "test@crfusa.com" }
    /// </example>
    public class ForgotPasswordViewModel
    {
        [Required, BindRequired, EmailAddress]
        public string Email { get; set; } = "";

        [BindRequired]
        public string RecaptchaToken { get; set; } = "";
    }
}
