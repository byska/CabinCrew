using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CabinCrew.Domain.ValueObjects
{
    public class AttendantInfo
    {
        public string Name { get; }
        public int Age { get; }
        public string Gender { get; }
        public string Nationality { get; }
        public IReadOnlyCollection<string> KnownLanguages => _knownLanguages.AsReadOnly();

        private readonly List<string> _knownLanguages;

        protected AttendantInfo() { }

        public AttendantInfo(string name, int age, string gender, string nationality, IEnumerable<string> languages)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name required.");
            if (age <= 0) throw new ArgumentException("Age must be positive.");
            if (string.IsNullOrWhiteSpace(gender)) throw new ArgumentException("Gender required.");
            if (string.IsNullOrWhiteSpace(nationality)) throw new ArgumentException("Nationality required.");
            if (languages == null || !languages.Any()) throw new ArgumentException("Languages required.");

            Name = name;
            Age = age;
            Gender = gender;
            Nationality = nationality;
            _knownLanguages = new List<string>(languages);
        }
    }
}
