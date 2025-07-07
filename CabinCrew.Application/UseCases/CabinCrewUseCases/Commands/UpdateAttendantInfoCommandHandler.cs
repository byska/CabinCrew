using CabinCrew.Application.Abstractions;
using CabinCrew.Domain.ValueObjects;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CabinCrew.Application.UseCases.CabinCrewUseCases.Commands
{
    public class UpdateAttendantInfoCommand : IRequest<UpdateAttendantInfoCommandResponse>
    {
        public Guid AttendantId { get; set; }

        public AttendantInfoRequest Info { get; set; }

        public UpdateAttendantInfoCommand() { }

        public UpdateAttendantInfoCommand(Guid attendantId, AttendantInfoRequest info)
        {
            AttendantId = attendantId;
            Info = info;
        }
    }

    public class UpdateAttendantInfoCommandResponse
    {
        public bool IsUpdated { get; set; }

        public UpdateAttendantInfoCommandResponse(bool ısUpdated)
        {
            IsUpdated = ısUpdated;
        }
    }

    public class UpdateAttendantInfoCommandHandler : IRequestHandler<UpdateAttendantInfoCommand, UpdateAttendantInfoCommandResponse>
    {
        private readonly ICrewRepository _crewRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateAttendantInfoCommandHandler(ICrewRepository crewRepository, IUnitOfWork unitOfWork)
        {
            _crewRepository = crewRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<UpdateAttendantInfoCommandResponse> Handle(UpdateAttendantInfoCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var attendant = await _crewRepository.GetByIdCabinCrewAsync(request.AttendantId, cancellationToken);

                if (attendant == null)
                    throw new InvalidOperationException($"Cabin attendant with ID {request.AttendantId} not found.");

                var newInfo = new AttendantInfo(
                    request.Info.Name,
                    request.Info.Age,
                    request.Info.Gender,
                    request.Info.Nationality,
                    request.Info.KnownLanguages
                );

                attendant.UpdateInfo(newInfo);

                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return new UpdateAttendantInfoCommandResponse(true);
            }
            catch (Exception)
            {

                return new UpdateAttendantInfoCommandResponse(false);
            }

        }
    }

}
