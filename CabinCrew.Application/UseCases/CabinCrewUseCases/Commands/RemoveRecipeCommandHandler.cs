using CabinCrew.Application.Abstractions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CabinCrew.Application.UseCases.CabinCrewUseCases.Commands
{
    public class RemoveRecipeCommand : IRequest<RemoveRecipeCommandResponse>
    {
        public Guid AttendantId { get; }
        public string RecipeName { get; }

        public RemoveRecipeCommand(Guid attendantId, string recipeName)
        {
            AttendantId = attendantId;
            RecipeName = recipeName;
        }
    }

    public class RemoveRecipeCommandResponse
    {
        public bool IsDeleted { get; set; }
        public string? Message { get; set; }

        public RemoveRecipeCommandResponse(bool ısDeleted, string? message)
        {
            IsDeleted = ısDeleted;
            Message = message;
        }
    }

        public class RemoveRecipeCommandHandler : IRequestHandler<RemoveRecipeCommand, RemoveRecipeCommandResponse>
    {
        private readonly ICrewRepository _crewRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RemoveRecipeCommandHandler(ICrewRepository crewRepository, IUnitOfWork unitOfWork)
        {
            _crewRepository = crewRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<RemoveRecipeCommandResponse> Handle(RemoveRecipeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var attendant = await _crewRepository.GetByIdCabinCrewAsync(request.AttendantId, cancellationToken);

                if (attendant == null)
                    throw new InvalidOperationException($"Cabin attendant with ID {request.AttendantId} not found.");

                attendant.RemoveRecipe(request.RecipeName);

                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return new RemoveRecipeCommandResponse(true, null);
            }
            catch (Exception ex)
            {
                return new RemoveRecipeCommandResponse(false, ex.Message);
                throw;
            }
           
        }
    }

}
