using System.Collections.Generic;

namespace MEOT.lib.Sources.Base
{
    public abstract class BaseSource
    {
        public abstract string Name { get; }

        public abstract bool RequiresKey { get; }

        public abstract void Initialize(string licenseKey);

        public abstract Dictionary<string, bool> QueryHash(string sha1);
    }
}