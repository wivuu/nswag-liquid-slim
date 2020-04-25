using System;
using System.Collections.Generic;
using System.Linq;

namespace nswag_liquid_slim.ViewModels.Users
{
    public class UserViewModel
    {
        public Guid Id { get; set; }

        public string FirstName { get; internal set; } = "";

        public string LastName { get; internal set; } = "";

        public string Email { get; internal set; } = "";

        public string Phone { get; internal set; } = "";

        public string CreatedBy { get; internal set; } = "";

        public DateTimeOffset DateCreated { get; internal set; }

        public ImageViewModel Image { get; internal set; } = new ImageViewModel();

        public List<UserRoleViewModel> Roles { get; internal set; } = new List<UserRoleViewModel>();

        public string FullName => $"{FirstName} {LastName}";

        public bool IsActive { get; set; }
    }
}