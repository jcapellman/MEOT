using System.Collections.Generic;

using MEOT.lib.Sources.Objects;

namespace MEOT.lib.Sources.Base
{
    public abstract class BaseSource
    {
        public abstract string Name { get; }

        public virtual bool Enabled => true;

        public abstract bool RequiresKey { get; }

        public abstract void Initialize(string licenseKey);

        public abstract Dictionary<string, SourceItem> QueryHash(string sha1);
    }
}