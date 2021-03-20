using MEOT.lib.Containers;
using MEOT.lib.Managers;

using MEOT.WebAPI.Controllers.Base;

using Microsoft.AspNetCore.Mvc;

namespace MEOT.WebAPI.Controllers
{
    public class VendorBreakdownController : BaseController
    {
        private MalwareManager _manager;

        public VendorBreakdownController(MalwareManager manager)
        {
            _manager = manager;
        }

        [HttpGet]
        public VendorContainer Get(string vendorName) => _manager.GetVendorData(vendorName);
    }
}