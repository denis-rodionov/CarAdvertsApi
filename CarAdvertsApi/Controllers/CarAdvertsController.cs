using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CarAdvertsApi.Models.Enums;
using CarAdvertsApi.Models;
using CarAdvertsApi.Repositories;
using System.Net;
using CarAdvertsApi.Repositories.impl;

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
        /// Return the list of car advents sorted based on the given parameters.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] string sortKey = "Id",
            [FromQuery] string sortOrder = "ASK")
        {
            try
            {
                var parsedSortOrder = Enum.Parse<SortOrder>(sortOrder.ToUpper());
                return Ok(await _carAdvertsRepository.FindCarAdvertsAsync(sortKey, parsedSortOrder));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            try
            {
                var carAdvert = await _carAdvertsRepository.GetCarAdvertAsync(id);
                if (carAdvert == null)
                    return NotFound();
                return Ok(carAdvert);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
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

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] Guid id, [FromBody] CarAdvert carAdvert)
        {
            if (carAdvert.Id != id)
                return BadRequest();

            try
            {
                var existingAdvert = await _carAdvertsRepository.GetCarAdvertAsync(id);
                if (existingAdvert == null)
                    return NotFound();

                await _carAdvertsRepository.UpdateCarAdvertAsync(carAdvert);
                return Ok();
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
