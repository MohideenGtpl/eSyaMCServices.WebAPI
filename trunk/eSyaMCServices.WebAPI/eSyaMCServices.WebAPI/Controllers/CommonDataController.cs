using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eSyaMCServices.IF;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eSyaMCServices.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CommonDataController : ControllerBase
    {
        private readonly ICommonDataRepository _CommonDataRepository;
        public CommonDataController(ICommonDataRepository CommonDataRepository)
        {
            _CommonDataRepository = CommonDataRepository;
        }
        /// <summary>
        /// Get Business Key
        /// UI Reffered - Specialty Units
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetBusinessKey()
        {
            var pa_rm = await _CommonDataRepository.GetBusinessKey();
            return Ok(pa_rm);
        }
    }
}