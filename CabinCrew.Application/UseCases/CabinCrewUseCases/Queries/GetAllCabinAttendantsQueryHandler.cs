using CabinCrew.Application.Abstractions;
using CabinCrew.Domain.Enums;
using CabinCrew.Domain.ValueObjects;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CabinCrew.Application.UseCases.CabinCrewUseCases.Queries
{
    public class GetAllCabinAttendantsQueryRequest : IRequest<List<GetAllCabinAttendantsQueryResponse>>
    {

    }

    public class GetAllCabinAttendantsQueryResponse
    {
        public AttendantInfoResponse Info { get; set; }
        public string AttendantType { get; set; }
        public List<string> VehicleRestrictions { get; set; }
        public List<string> Recipes { get; set; }

    }

    public class AttendantInfoResponse
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string Nationality { get; set; }
        public List<string> KnownLanguages { get; set; }
    }

    public class GetAllCabinAttendantsQueryHandler : IRequestHandler<GetAllCabinAttendantsQueryRequest, List<GetAllCabinAttendantsQueryResponse>>
    {
        private readonly ICrewRepository _crewRepository;

        public GetAllCabinAttendantsQueryHandler(ICrewRepository crewRepository)
        {
            _crewRepository = crewRepository;
        }

        public async Task<List<GetAllCabinAttendantsQueryResponse>> Handle(GetAllCabinAttendantsQueryRequest request, CancellationToken cancellationToken)
        {
            var crew = await _crewRepository.GetAllCabinCrewAsync(cancellationToken);
            var response = crew.Select(c => new GetAllCabinAttendantsQueryResponse
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

            }).ToList();
            return response;
        }
    }
}
