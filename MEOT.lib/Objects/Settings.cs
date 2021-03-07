using MEOT.lib.Objects.Base;

namespace MEOT.lib.Objects
{
    public class Settings : BaseObject
    {
        public string VTKey { get; set; }

        public int HoursBetweenChecks { get; set; }
    }
}