using CabinCrew.Domain.Entities;
using CabinCrew.Domain.Enums;
using CabinCrew.Domain.ValueObjects;
using CabinCrew.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CabinCrew.Infrastructure.Seed
{
    public static class SeedService
    {
        public static void Seed(this CabinCrewDbContext context)
        {
            if (context.CabinAttendants.Any())
                return; 

            var attendant1 = new CabinAttendant(
                Guid.NewGuid(),
                new AttendantInfo(
                    name: "Beste Yasak",
                    age: 30,
                    gender: "Kadın",
                    nationality: "Türk",
                    languages: new List<string> { "Türkçe", "İngilizce" }
                ),
                AttendantType.Chief,
                new List<string> { "Boeing 737", "Airbus A320" }
            );

            var attendant2 = new CabinAttendant(
                Guid.NewGuid(),
                new AttendantInfo(
                    name: "Aysu Sever",
                    age: 25,
                    gender: "Kadın",
                    nationality: "Amerikan",
                    languages: new List<string> { "İngilizce", "İspanyolca" }
                ),
                AttendantType.Regular,
                new List<string> { "Boeing 777" }
            );

            var attendant3 = new CabinAttendant(
                Guid.NewGuid(),
                new AttendantInfo(
                    name: "Ercan Tanrıkulu",
                    age: 35,
                    gender: "Erkek",
                    nationality: "Türk",
                    languages: new List<string> { "Türkçe", "Almanca" }
                ),
                AttendantType.Chef,
                new List<string> { "Airbus A330" }
            );

            attendant3.AddRecipe("Izgara Tavuk");
            attendant3.AddRecipe("Mantı");

            context.CabinAttendants.AddRange(attendant1, attendant2, attendant3);

            context.SaveChanges();
        }
    }
}
