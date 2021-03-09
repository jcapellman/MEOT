using System.ComponentModel.DataAnnotations;

using MEOT.lib.Enums;
using MEOT.lib.Objects.Base;

namespace MEOT.lib.Objects
{
    public class User : BaseObject
    {
        [Required]
        public string EmailAddress { get; set; }

        [Required]
        public string Password { get; set; }

        public Roles Role { get; set; }

        public User()
        {
            EmailAddress = string.Empty;

            Password = string.Empty;

            Role = Roles.Viewer;
        }
    }
}