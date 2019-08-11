using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OEEAPI.Models;

namespace OEEAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OEEController : ControllerBase
    {
        private IHostingEnvironment _hostingEnvironment;
        private CATContext _context;
        public OEEController(IHostingEnvironment hostingEnvironment, CATContext context)
        {
            _hostingEnvironment = hostingEnvironment;
            _context = context;
        }

        // POST: api/OEE
        [HttpPost]
        public ActionResult<APIResponse> Post([FromBody] OEEViewModel oEEViewModel)
        {
            APIResponse result = new APIResponse("Success", "Data saved successfully.");
            try
            {
                OEE.SaveOEEData(_context, oEEViewModel);
            }
            catch(Exception ex)
            {
                result.Status = "Failed";
                result.Message = ex.Message;
            }
            return result;
        }
       
    }
}
