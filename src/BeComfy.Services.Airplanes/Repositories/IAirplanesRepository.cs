using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BeComfy.Services.Airplanes.Domain;

namespace BeComfy.Services.Airplanes.Repositories
{
    public interface IAirplanesRepository
    {
        Task AddAsync(Airplane airplane);
        Task<Airplane> GetAsync(Guid id);
        Task<IEnumerable<Airplane>> BrowseAsync(int pageSize, int page, AirplaneStatus status);
        Task UpdateAsync(Airplane airplane);
        Task DeleteAsync(Guid id);
    }
}