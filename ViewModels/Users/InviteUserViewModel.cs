using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace nswag_liquid_slim.ViewModels.Users
{
    /// <example>
    /// { "email": "test.user@gmail.com", "firstName": "John", "lastName": "Smith", "inviteAsAdmin": true }
    /// </example>
    public class InviteUserViewModel
    {
        [DataType(DataType.EmailAddress), MaxLength(150)]
        [Required, BindRequired]
        public string Email { get; set; } = "";

        [MaxLength(50)]
        [Required, BindRequired]
        public string FirstName { get; set; } = "";

        [MaxLength(50)]
        [Required, BindRequired]
        public string LastName { get; set; } = "";
    }
}