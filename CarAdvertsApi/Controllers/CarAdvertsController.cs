using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CarAdvertsApi.Models.Enums;
using CarAdvertsApi.Models;
using CarAdvertsApi.Repositories;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CarAdvertApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarAdvertsController : Controller {

        readonly ICarAdvertsRepository _carAdvertsRepository;

        public CarAdvertsController(ICarAdvertsRepository carAdvertService)
        {
            _carAdvertsRepository = carAdvertService;
        }

        /// <summary>
        /// Return the list of car advents.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            try
            {
                return Ok(await _carAdvertsRepository.FindCarAdvertsAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        /// <summary>
        /// Create Car Advert resource
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CarAdvert carAdvert)
        {
            try
            {
                await _carAdvertsRepository.SaveCarAdvertAsync(carAdvert);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
