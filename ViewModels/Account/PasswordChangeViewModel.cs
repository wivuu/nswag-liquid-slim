using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace nswag_liquid_slim.ViewModels.Account
{
    public class PasswordChangeViewModel : PasswordResetViewModel
    {
        [Required, BindRequired, DataType(DataType.Password)]
        public string CurrentPassword { get; set; } = "";
    }
}