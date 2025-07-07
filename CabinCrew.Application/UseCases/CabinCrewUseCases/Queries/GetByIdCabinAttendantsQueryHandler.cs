using CabinCrew.Application.Abstractions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CabinCrew.Application.UseCases.CabinCrewUseCases.Queries
{
    public class GetByIdCabinAttendantQueryRequest : IRequest<GetByIdCabinAttendantsQueryResponse>
    {
        public Guid Id { get; }

        public GetByIdCabinAttendantQueryRequest(Guid id)
        {
            Id = id;
        }
    }

    public class GetByIdCabinAttendantsQueryResponse
    {
        public AttendantInfoResponse Info { get; set; }
        public string AttendantType { get; set; }
        public List<string> VehicleRestrictions { get; set; }
        public List<string> Recipes { get; set; }

    }

    public class GetByIdCabinAttendantQueryHandler : IRequestHandler<GetByIdCabinAttendantQueryRequest, GetByIdCabinAttendantsQueryResponse>
    {
        private readonly ICrewRepository _crewRepository;

        public GetByIdCabinAttendantQueryHandler(ICrewRepository crewRepository)
        {
            _crewRepository = crewRepository;
        }

        public async Task<GetByIdCabinAttendantsQueryResponse> Handle(GetByIdCabinAttendantQueryRequest request, CancellationToken cancellationToken)
        {
            var c = await _crewRepository.GetByIdCabinCrewAsync(request.Id, cancellationToken);
            if (c == null) return null; 

            var response = new GetByIdCabinAttendantsQueryResponse
            {
                Info = new AttendantInfoResponse
                {
                    Name = c.Info.Name,
                    Age = c.Info.Age,
                    Gender = c.Info.Gender,
                    Nationality = c.Info.Nationality,
                    KnownLanguages = c.Info.KnownLanguages.ToList()
                },
                AttendantType = c.AttendantType.ToString(),
                VehicleRestrictions = c.VehicleRestrictions.ToList(),
                Recipes = c.Recipes.ToList()
            };

            return response;
        }
    }

}
