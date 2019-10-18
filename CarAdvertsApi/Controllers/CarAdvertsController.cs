using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CarAdvertsApi.Models.Enums;
using CarAdvertsApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CarAdvertApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarAdvertsController : Controller {

        // GET: api/values
        /// <summary>
        /// Return the list of car advents.
        /// </summary>
        [HttpGet]
        public IEnumerable<CarAdvert> Get()
        {   
            return new CarAdvert[] { new CarAdvert {
                Id = Guid.NewGuid(),
                Title = "Opel Astra",
                Fuel = CarFuelTypes.Gasoline,
                Price = 5000,
                New = false,
                Mileage = 90000,
                FirstRegistrationDate = new DateTime(2011, 6, 23)
            } };
        }
    }
}
