using MEOT.lib.Enums;
using MEOT.lib.Objects.Base;

namespace MEOT.lib.Objects
{
    public class User : BaseObject
    {
        public string EmailAddress { get; set; }

        public string Password { get; set; }

        public Roles Role { get; set; }
    }
}