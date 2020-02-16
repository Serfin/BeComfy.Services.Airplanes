using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BeComfy.Services.Airplanes.Domain;

namespace BeComfy.Services.Airplanes.Repositories
{
    public interface IAirplanesRepository
    {
        Task AddAsync(Airplane airplane);
        Task<Airplane> GetAsync(Guid id);
        Task<Airplane> GetAsync(string airplaneRegistrationNumber);
        Task<IEnumerable<Airplane>> BrowseAsync(int pageSize, int page);
        Task<IEnumerable<Airplane>> BrowseAsync(int pageSize, int page, Expression<Func<Airplane, bool>> predicate);
        Task UpdateAsync(Airplane airplane);
        Task DeleteAsync(Guid id);
    }
}