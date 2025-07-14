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
        private readonly IUnitOfWork _unitOfWork;

        public CrewRepository(CabinCrewDbContext dbContext, IUnitOfWork unitOfWork)
        {
            _dbContext = dbContext;
            _unitOfWork = unitOfWork;
        }
        public async Task AddAsync(CabinAttendant request, CancellationToken ct)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));
            request.CreateDate = DateTime.Now;
            await _dbContext.CabinAttendants.AddAsync(request, ct);
        }

        public async Task<IReadOnlyList<CabinAttendant>> GetAllCabinCrewAsync(CancellationToken ct)
        {
            return await _dbContext.CabinAttendants.Where(x=>x.IsActive).AsNoTracking().ToListAsync(ct);

        }

        public async Task<CabinAttendant> GetByIdCabinCrewAsync(Guid id, CancellationToken ct)
        {
            return await _dbContext.CabinAttendants.Where(x => x.Id == id && x.IsActive).FirstOrDefaultAsync(ct);

        }
        public void Delete(CabinAttendant attendant)
        {
            if (attendant == null) throw new ArgumentNullException(nameof(attendant));

            attendant.DeleteDate = DateTime.Now;
            attendant.IsActive = false; 
            _dbContext.Update(attendant);
            _unitOfWork.SaveChangesAsync();

        }
    }
}
