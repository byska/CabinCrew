using CabinCrew.Application.Abstractions;
using CabinCrew.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CabinCrew.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CabinCrewDbContext _context;

        public UnitOfWork(CabinCrewDbContext context)
        {
            _context = context;
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
