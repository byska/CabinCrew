using CabinCrew.Application.Abstractions;
using CabinCrew.Domain.Entities;
using CabinCrew.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CabinCrew.Infrastructure.Repositories
{
    public class CrewRepository : ICrewRepository
    {
        private readonly CabinCrewDbContext _dbContext;

        public CrewRepository(CabinCrewDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddAsync(CabinAttendant request, CancellationToken ct)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));
            await _dbContext.CabinAttendants.AddAsync(request, ct);
        }

        public Task<IReadOnlyList<CabinAttendant>> GetAllCabinCrewAsync(CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public Task<CabinAttendant> GetByIdCabinCrewAsync(Guid id, CancellationToken ct)
        {
            throw new NotImplementedException();
        }
        public void Delete(CabinAttendant attendant)
        {
            if (attendant == null) throw new ArgumentNullException(nameof(attendant));
            _dbContext.CabinAttendants.Remove(attendant);
        }
    }
}
