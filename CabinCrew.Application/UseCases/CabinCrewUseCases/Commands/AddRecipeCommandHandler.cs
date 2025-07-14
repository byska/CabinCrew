using CabinCrew.Application.Abstractions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CabinCrew.Application.UseCases.CabinCrewUseCases.Commands
{
    public class AddRecipeCommand : IRequest<AddRecipeCommandResponse>
    {
        public Guid AttendantId { get; set; }
        public string Recipe { get; set; }

        public AddRecipeCommand(Guid attendantId, string recipe)
        {
            AttendantId = attendantId;
            Recipe = recipe;
        }
    }

    public class AddRecipeCommandResponse
    {
        public bool IsAdded { get; set; }
        public string? Message { get; set; }

        public AddRecipeCommandResponse(bool ısAdded, string? message)
        {
            IsAdded = ısAdded;
            Message = message;
        }
    }
    public class AddRecipeCommandHandler : IRequestHandler<AddRecipeCommand, AddRecipeCommandResponse>
    {
        private readonly ICrewRepository _crewRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AddRecipeCommandHandler(ICrewRepository crewRepository, IUnitOfWork unitOfWork)
        {
            _crewRepository = crewRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<AddRecipeCommandResponse> Handle(AddRecipeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var attendant = await _crewRepository.GetByIdCabinCrewAsync(request.AttendantId, cancellationToken);

                if (attendant == null)
                    throw new InvalidOperationException($"Cabin attendant with ID {request.AttendantId} not found.");

                attendant.AddRecipe(request.Recipe);

                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return new AddRecipeCommandResponse(true, null);
            }
            catch (Exception ex)
            {
                return new AddRecipeCommandResponse(false, ex.Message);

            }
            
        }
    }

}
