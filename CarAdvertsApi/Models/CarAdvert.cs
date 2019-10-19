using System;
using System.ComponentModel.DataAnnotations;
using CarAdvertsApi.Models.Enums;

namespace CarAdvertsApi.Models
{
    public class CarAdvert
    {
        /// <summary>
        /// Id of the adverts.
        /// Required field.
        /// </summary>
        [Required]
        public Guid? Id { get; set; }

        /// <summary>
        /// The title of the advert.
        /// Required field.
        /// </summary>
        [Required]
        [MaxLength(30)]
        public string Title { get; set; }

        /// <summary>
        /// Car fuel type.
        /// Required field.
        /// </summary>
        [Required]
        public CarFuelTypes? Fuel { get; set; }

        /// <summary>
        /// The price of the car proposed by the advert.
        /// Required field.
        /// </summary>
        [Required]
        public int? Price { get; set; }

        /// <summary>
        /// Indicates if the car is new.
        /// Required field.
        /// </summary>
        [Required]
        public bool? New { get; set; }

        /// <summary>
        /// The mileage of the car in kilometers.
        /// Set only if the car is used.
        /// </summary>
        public int? Mileage { get; set; }

        /// <summary>
        /// The date of the first registration of the car.
        /// Set only if the car is used.
        /// </summary>
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? FirstRegistrationDate { get; set; }
    }
}
