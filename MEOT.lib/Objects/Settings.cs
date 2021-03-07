using System.Collections.Generic;

using MEOT.lib.Objects.Base;

namespace MEOT.lib.Objects
{
    public class Settings : BaseObject
    {
        public int HoursBetweenChecks { get; set; }

        public Dictionary<string, string> Sources { get; set; }
    }
}