using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BeComfy.Services.Airplanes.Domain;

namespace BeComfy.Services.Airplanes.Repositories
{
    public interface IAirplanesRepository
    {
        Task AddAsync(Airplane airplane);
        Task<Airplane> GetAirplaneAsync(Guid id);
        Task<IEnumerable<Airplane>> BrowseAirplaneAsync(int pageSize, int page);
        Task UpdateAirplaneAsync(Airplane airplane);
        Task DeleteAirplaneAsync(Guid id);
    }
}