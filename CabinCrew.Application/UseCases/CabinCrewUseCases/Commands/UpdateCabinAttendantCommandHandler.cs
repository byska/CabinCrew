using CabinCrew.Application.Abstractions;
using CabinCrew.Domain.Enums;
using CabinCrew.Domain.ValueObjects;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CabinCrew.Application.UseCases.CabinCrewUseCases.Commands
{
    public class UpdateCabinAttendantCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
        public AttendantInfoRequest Info { get; set; }
        public string AttendantType { get; set; }
        public List<string> VehicleRestrictions { get; set; }
        public List<string> Recipes { get; set; }
    }

    public class UpdateCabinAttendantCommandHandler : IRequestHandler<UpdateCabinAttendantCommand,Unit>
    {
        private readonly ICrewRepository _crewRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateCabinAttendantCommandHandler(ICrewRepository crewRepository, IUnitOfWork unitOfWork)
        {
            _crewRepository = crewRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(UpdateCabinAttendantCommand request, CancellationToken cancellationToken)
        {
            var attendant = await _crewRepository.GetByIdCabinCrewAsync(request.Id, cancellationToken);
            if (attendant == null)
                throw new InvalidOperationException($"Cabin attendant with ID {request.Id} not found.");

            var newInfo = new AttendantInfo(
                request.Info.Name,
                request.Info.Age,
                request.Info.Gender,
                request.Info.Nationality,
                request.Info.KnownLanguages
            );
            attendant.UpdateInfo(newInfo);

            var newType = Enum.Parse<AttendantType>(request.AttendantType);
            attendant.ChangeType(newType);

            foreach (var vr in attendant.VehicleRestrictions.ToList())
                attendant.RemoveVehicleRestriction(vr);

            if (request.VehicleRestrictions != null)
            {
                foreach (var vr in request.VehicleRestrictions)
                    attendant.AddVehicleRestriction(vr);
            }

            if (newType == AttendantType.Chef)
            {
                foreach (var r in attendant.Recipes.ToList())
                    attendant.RemoveRecipe(r);

                if (request.Recipes != null)
                {
                    foreach (var r in request.Recipes.Take(4))
                        attendant.AddRecipe(r);
                }
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }

}
