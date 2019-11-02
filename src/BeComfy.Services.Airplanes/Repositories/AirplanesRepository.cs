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

        public async Task<IEnumerable<Airplane>> BrowseAirplaneAsync(int pageSize, int page)
            => await _context.Airplanes.OrderBy(x => x.FlightsCarriedOut).Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();


        public async Task DeleteAirplaneAsync(Guid id)
        {
            var airplane = await GetAirplaneAsync(id);
            _context.Remove(airplane);
            await _context.SaveChangesAsync();
        }

        public async Task<Airplane> GetAirplaneAsync(Guid id)
            => await _context.Airplanes.FindAsync(id);

        public async Task UpdateAirplaneAsync(Airplane airplane)
        {
            _context.Update(airplane);
            await _context.SaveChangesAsync();
        }
    }
}