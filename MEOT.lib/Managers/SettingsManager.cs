using System.Collections.Generic;
using System.Linq;

using MEOT.lib.DAL.Base;
using MEOT.lib.Objects;

namespace MEOT.lib.Managers
{
    public class SettingsManager
    {
        private readonly IDAL _db;

        public SettingsManager(IDAL db)
        {
            _db = db;
        }

        public void SaveSettings(Settings setting)
        {
            _db.Update(setting);
        }

        public Settings GetSettings()
        {
            var settings = _db.SelectOne<Settings>(a => a != null);

            if (settings != null)
            {
                return settings;
            }

            _db.Insert(new Settings());
            
            return _db.SelectOne<Settings>(a => a != null);
        }

        public void UpdateSources(List<string> sources)
        {
            var isNew = false;

            var settings = _db.SelectFirstOrDefault<Settings>();

            if (settings == null)
            {
                isNew = true;

                settings = new Settings();
            }

            settings.Sources ??= new Dictionary<string, string>();

            foreach (var source in sources.Where(source => !settings.Sources.ContainsKey(source)))
            {
                settings.Sources.Add(source, string.Empty);
            }

            if (isNew)
            {
                _db.Insert(settings);
            }
            else
            {
                _db.Update(settings);
            }
        }
    }
}