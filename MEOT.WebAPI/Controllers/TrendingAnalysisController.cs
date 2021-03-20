using MEOT.lib.Containers;
using MEOT.lib.Managers;

using MEOT.WebAPI.Controllers.Base;

using Microsoft.AspNetCore.Mvc;

namespace MEOT.WebAPI.Controllers
{
    public class TrendingAnalysisController : BaseController
    {
        private TrendingAnalysisManager _manager;

        public TrendingAnalysisController(TrendingAnalysisManager manager)
        {
            _manager = manager;
        }

        [HttpGet]
        public TrendingAnalysisContainer Get()
        {
            return new()
            {
                TopVendors = _manager.GetTopVendorsFromDuration(),
                WorstVendors = _manager.GetWorstVendorsFromDuration()
            };
        }
    }
}