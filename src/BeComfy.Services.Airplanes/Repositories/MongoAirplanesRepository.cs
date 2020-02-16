using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BeComfy.Common.Mongo;
using BeComfy.Services.Airplanes.Domain;

namespace BeComfy.Services.Airplanes.Repositories
{
    public class _AirplanesRepository : IAirplanesRepository
    {
        private readonly IMongoRepository<Airplane> _collection;

        public _AirplanesRepository(IMongoRepository<Airplane> collection)
        {
            _collection = collection;
        }

        public async Task AddAsync(Airplane entity)
            => await _collection.AddAsync(entity);

        public async Task<Airplane> GetAsync(Guid id)
            => await _collection.GetAsync(x => x.Id == id);

        public async Task<Airplane> GetAsync(string airplaneRegistrationNumber)
            => await _collection.GetAsync(x => x.AirplaneRegistrationNumber == airplaneRegistrationNumber);

        public async Task<IEnumerable<Airplane>> BrowseAsync(int pageSize, int page)
            => await _collection.BrowseAsync(pageSize, page);

        public async Task<IEnumerable<Airplane>> BrowseAsync(int pageSize, int page, Expression<Func<Airplane, bool>> predicate)
            => await _collection.BrowseAsync(pageSize, page, predicate);

        public async Task UpdateAsync(Airplane entity)
            => await _collection.UpdateAsync(entity);
    
        public async Task DeleteAsync(Guid id)
            => await _collection.DeleteAsync(id);
    }
}