using System;
using System.Collections.Generic;
using System.Linq;

using MEOT.lib.Objects;
using MEOT.lib.Sources.Base;

namespace MEOT.lib.Managers
{
    public class SourceManager
    {
        private readonly List<BaseSource> _sources;

        public SourceManager(Settings settings)
        {
            _sources = this.GetType().Assembly.GetTypes().Where(a => a.BaseType == typeof(BaseSource) && !a.IsAbstract)
                .Select(a => (BaseSource) Activator.CreateInstance(a)).ToList();

            foreach (var source in _sources)
            {
                if (settings == null || !settings.Sources.ContainsKey(source.Name))
                {
                    continue;
                }

                var key = settings.Sources[source.Name];
                
                source.Initialize(key);
            }
        }

        public List<string> SourceNames => _sources.Select(a => a.Name).ToList();
        
        public Dictionary<string, Dictionary<string, bool>> CheckSources(string hash)
        {
            var result = new Dictionary<string, Dictionary<string, bool>>();

            foreach (var source in _sources)
            {
                var sourceResult = source.QueryHash(hash);

                result.Add(source.Name, sourceResult);
            }

            return result;
        }
    }
}