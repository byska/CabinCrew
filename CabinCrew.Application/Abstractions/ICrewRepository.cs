using CabinCrew.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CabinCrew.Application.Abstractions
{
    public interface ICrewRepository
    {
        Task<IReadOnlyList<CabinAttendant>> GetAllCabinCrewAsync(CancellationToken ct);
        Task<CabinAttendant> GetByIdCabinCrewAsync(Guid id, CancellationToken ct);
        Task AddAsync(CabinAttendant request, CancellationToken ct);
        void Delete(CabinAttendant attendant);



    }
}
