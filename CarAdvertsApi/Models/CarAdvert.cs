using System;
using CarAdvertsApi.Models.Enums;

namespace CarAdvertsApi.Models
{
    public class CarAdvert
    {
        /// <summary>
        /// Id of the adverts.
        /// Required field.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The title of the advert.
        /// Required field.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Car fuel type.
        /// Required field.
        /// </summary>
        public CarFuelTypes Fuel { get; set; }

        /// <summary>
        /// The price of the car proposed by the advert.
        /// Required field.
        /// </summary>
        public int Price { get; set; }

        /// <summary>
        /// Indicates if the car is new.
        /// Required field.
        /// </summary>
        public bool New { get; set; }

        /// <summary>
        /// The mileage of the car in kilometers.
        /// Set only if the car is used.
        /// </summary>
        public int Mileage { get; set; }

        /// <summary>
        /// The date of the first registration of the car.
        /// Set only if the car is used.
        /// </summary>
        public DateTime FirstRegistrationDate { get; set; }
    }
}
