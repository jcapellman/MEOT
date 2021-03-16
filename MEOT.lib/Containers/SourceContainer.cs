using System.Collections.Generic;

using MEOT.lib.Sources.Objects;

namespace MEOT.lib.Containers
{
    public class SourceContainer
    {
        public string MD5 { get; set; }

        public Dictionary<string, SourceItem> SourceData { get; set; }
    }
}