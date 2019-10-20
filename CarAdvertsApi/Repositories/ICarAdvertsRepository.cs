using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CarAdvertsApi.Models;
using CarAdvertsApi.Repositories.impl;

namespace CarAdvertsApi.Repositories
{
    /// <summary>
    /// Repository for basic operations with CarAdvert entities.
    /// </summary>
    public interface ICarAdvertsRepository
    {
        /// <summary>
        /// Returms all curent car adverts in the database without sorting.
        /// </summary>
        Task<IEnumerable<CarAdvert>> FindCarAdvertsAsync();

        /// <summary>
        /// Returms all curent car adverts sorted by sorting key and given direction.
        /// </summary>
        Task<IEnumerable<CarAdvert>> FindCarAdvertsAsync(string sortKey,
            SortOrder sortOrder);

        /// <summary>
        /// Find Acr Advert by Id.
        /// </summary>
        Task<CarAdvert> GetCarAdvertAsync(Guid id);

        /// <summary>
        /// Saves new car adverts in the database or overrides an existing
        /// one if the car advert with the current ID already exists.
        /// </summary>
        Task SaveCarAdvertAsync(CarAdvert carAdvert);

        /// <summary>
        /// Update existing entity by ID.
        /// </summary>
        Task UpdateCarAdvertAsync(CarAdvert carAdvert);

        /// <summary>
        /// Delete car advert by Id
        /// </summary>
        Task DeleteCarAdvertAsync(Guid id);
    }
}
