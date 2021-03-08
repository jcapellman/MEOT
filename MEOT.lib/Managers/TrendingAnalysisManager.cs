using System;
using System.Collections.Generic;
using System.Linq;

using MEOT.lib.Containers;
using MEOT.lib.DAL.Base;
using MEOT.lib.Objects;

namespace MEOT.lib.Managers
{
    public class TrendingAnalysisManager
    {
        private const int TopLimit = 5;

        private readonly IDAL _db;

        public TrendingAnalysisManager(IDAL db)
        {
            _db = db;
        }

        public List<VendorAnalysis> GetTopVendorsFromDuration() =>
            _db.SelectAll<MalwareVendorCheckpoint>().Where(a => a.Detected).OrderBy(a => a.HoursToDetection).Take(TopLimit).Select(a =>
                new VendorAnalysis
                {
                    HoursToDetection = Convert.ToInt32(a.HoursToDetection),
                    Name = a.VendorName
                }).ToList();

        public List<VendorAnalysis> GetWorstVendorsFromDuration() =>
            _db.SelectAll<MalwareVendorCheckpoint>().Where(a => a.Detected).OrderByDescending(a => a.HoursToDetection).Take(TopLimit).Select(a =>
                new VendorAnalysis
                {
                    HoursToDetection = Convert.ToInt32(a.HoursToDetection),
                    Name = a.VendorName
                }).ToList();
    }
}