using System.Collections.Generic;

using MEOT.lib.Objects.Base;

namespace MEOT.lib.Objects
{
    public class Settings : BaseObject
    {
        public int HoursBetweenChecks { get; set; }

        public string SMTPAddress { get; set; }

        public int SMTPPort { get; set; }

        public string SMTPUser { get; set; }

        public string SMTPPassword { get; set; }

        public Dictionary<string, string> Sources { get; set; }
    }
}