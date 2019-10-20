using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CarAdvertsApi.Models.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CarAdvertsApi.Models
{
    public class CarAdvert : IValidatableObject
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
        [JsonConverter(typeof(StringEnumConverter))]
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
        [Column(TypeName = "Date")]
        public DateTime? FirstRegistrationDate { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (New == false)
            {
                if (Mileage == null) 
                    yield return new ValidationResult(
                        $"Not enough parameters for used car. Specify mileage.",
                        new[] { "Mileage" });
                if (FirstRegistrationDate == null)
                    yield return new ValidationResult(
                        $"Not enough parameters for used car. Specify year of registration",
                        new[] { "FirstRegistrationDate" });
            }
            else
            {
                if (Mileage != null)
                    yield return new ValidationResult(
                        $"Mileage parameter is not allowed for a new car",
                        new[] { "Mileage" });
                if (FirstRegistrationDate != null)
                    yield return new ValidationResult(
                        $"FirstRegistration parameter is not allowed for a new car",
                        new[] { "FirstRegistrationDate" });
            }
        }
    }
}
