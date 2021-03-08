using System.Collections.Generic;
using System.Diagnostics;

using MEOT.lib.Sources.Base;

namespace MEOT.lib.Sources
{
    public class MLSource : BaseSource
    {
        public override string Name => "ML";

        public override bool RequiresKey => false;

        public override void Initialize(string licenseKey)
        {
            // Not needed
        }

        public override Dictionary<string, bool> QueryHash(string sha1)
        {
            var result = new Dictionary<string, bool>();

            using var process = new Process
            {
                StartInfo =
                {
                    FileName = "ml.exe", 
                    UseShellExecute = false, 
                    RedirectStandardOutput = true
                }
            };

            process.Start();

            var reader = process.StandardOutput;
            var output = reader.ReadToEnd();

            result.Add(Name, (output == "true"));

            process.WaitForExit();

            return result;
        }
    }
}