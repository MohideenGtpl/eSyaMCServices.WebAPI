using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using eSyaMCServices.IF;
using eSyaMCServices.DO;
namespace eSyaMCServices.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SpecialtyUnitsController : ControllerBase
    {
        private readonly ISpecialtyUnitsRepository _SpecialtyUnitsRepository;
        public SpecialtyUnitsController(ISpecialtyUnitsRepository SpecialtyUnitsRepository)
        {
            _SpecialtyUnitsRepository = SpecialtyUnitsRepository;
        }
        /// <summary>
        /// Get Specialty Units By Business Key.
        /// UI Reffered - Specialty Units
        /// </summary>
        /// <param name="BusinessKey"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetSpecialtyUnitsbyBusinessKey(int Businesskey)
        {
            var sp_units =  _SpecialtyUnitsRepository.GetSpecialtyUnitsbyBusinessKey(Businesskey);
            return Ok(sp_units);
        }
        /// <summary>
        /// Insert Or Update Specialty Units.
        /// UI Reffered -Specialty Units
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> InsertOrUpdateSpecialtyUnits(DO_SpecialtUnits obj)
        {
            var msg = await _SpecialtyUnitsRepository.InsertOrUpdateSpecialtyUnits(obj);
            return Ok(msg);

        }
    }
}
