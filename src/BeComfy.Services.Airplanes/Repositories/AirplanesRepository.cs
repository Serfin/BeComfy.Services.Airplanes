using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeComfy.Services.Airplanes.Domain;
using BeComfy.Services.Airplanes.EF;
using Microsoft.EntityFrameworkCore;

namespace BeComfy.Services.Airplanes.Repositories
{
    public class AirplanesRepository : IAirplanesRepository
    {
        private readonly AirplanesContext _context;

        public AirplanesRepository(AirplanesContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Airplane airplane)
        {
            await _context.AddAsync(airplane);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Airplane>> BrowseAsync(int pageSize, int page, AirplaneStatus status)
            => await _context.Airplanes
                .OrderBy(x => x.FlightsCarriedOut)
                .Where(y => y.AirplaneStatus == status)
                .Skip((page - 1) * pageSize)
                .Take(pageSize).ToListAsync();
        
        public async Task DeleteAsync(Guid id)
        {
            var airplane = await GetAsync(id);
            _context.Remove(airplane);
            await _context.SaveChangesAsync();
        }

        public async Task<Airplane> GetAsync(Guid id)
            => await _context.Airplanes.FindAsync(id);

        public async Task UpdateAsync(Airplane airplane)
        {
            _context.Update(airplane);
            await _context.SaveChangesAsync();
        }
    }
}