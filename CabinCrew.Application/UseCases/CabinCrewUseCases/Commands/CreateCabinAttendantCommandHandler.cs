using CabinCrew.Application.Abstractions;
using CabinCrew.Domain.Entities;
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
    public class CreateCabinAttendantCommand : IRequest<Guid>
    {
        public AttendantInfoRequest Info { get; set; }
        public string AttendantType { get; set; }
        public List<string> VehicleRestrictions { get; set; }
        public List<string> Recipes { get; set; }
    }

    public class AttendantInfoRequest
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string Nationality { get; set; }
        public List<string> KnownLanguages { get; set; }
    }
    public class CreateCabinAttendantCommandHandler : IRequestHandler<CreateCabinAttendantCommand, Guid>
    {
        private readonly ICrewRepository _crewRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateCabinAttendantCommandHandler(ICrewRepository crewRepository, IUnitOfWork unitOfWork)
        {
            _crewRepository = crewRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(CreateCabinAttendantCommand request, CancellationToken cancellationToken)
        {
            var attendant = new CabinAttendant(
                Guid.NewGuid(),
                new AttendantInfo(
                    request.Info.Name,
                    request.Info.Age,
                    request.Info.Gender,
                    request.Info.Nationality,
                    request.Info.KnownLanguages
                ),
                Enum.Parse<AttendantType>(request.AttendantType),
                request.VehicleRestrictions
            );

            if (attendant.AttendantType == AttendantType.Chef && request.Recipes != null)
            {
                foreach (var recipe in request.Recipes.Take(4)) 
                {
                    attendant.AddRecipe(recipe);
                }
            }

            await _crewRepository.AddAsync(attendant, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return attendant.Id;
        }
    }

}
