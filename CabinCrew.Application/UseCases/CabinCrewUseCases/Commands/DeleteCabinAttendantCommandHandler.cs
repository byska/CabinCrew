using CabinCrew.Application.Abstractions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CabinCrew.Application.UseCases.CabinCrewUseCases.Commands
{
    public class DeleteCabinAttendantCommand : IRequest<DeleteCabinAttendantCommandResponse>
    {
        public Guid Id { get; }

        public DeleteCabinAttendantCommand(Guid id)
        {
            Id = id;
        }
    }

    public class DeleteCabinAttendantCommandResponse
    {
        public bool IsDeleted { get; set; }
        public string? Message { get; set; }

        public DeleteCabinAttendantCommandResponse(bool ısDeleted, string? message)
        {
            IsDeleted = ısDeleted;
            Message = message;
        }
    }

    public class DeleteCabinAttendantCommandHandler : IRequestHandler<DeleteCabinAttendantCommand, DeleteCabinAttendantCommandResponse>
    {
        private readonly ICrewRepository _crewRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCabinAttendantCommandHandler(ICrewRepository crewRepository, IUnitOfWork unitOfWork)
        {
            _crewRepository = crewRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<DeleteCabinAttendantCommandResponse> Handle(DeleteCabinAttendantCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var attendant = await _crewRepository.GetByIdCabinCrewAsync(request.Id, cancellationToken);

                if (attendant == null)
                    throw new InvalidOperationException($"Cabin attendant with ID {request.Id} not found.");

                _crewRepository.Delete(attendant);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return new DeleteCabinAttendantCommandResponse(true, null);
            }
            catch (Exception ex)
            {
                return new DeleteCabinAttendantCommandResponse(false, ex.Message);

            }

        }
    }

}
