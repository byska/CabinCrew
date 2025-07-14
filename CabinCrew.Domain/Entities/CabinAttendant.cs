using CabinCrew.Domain.Enums;
using CabinCrew.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CabinCrew.Domain.Entities
{
    public class CabinAttendant : EntityBase
    {
        public AttendantInfo Info { get; private set; }
        public AttendantType AttendantType { get; private set; }
        public IReadOnlyCollection<string> VehicleRestrictions => _vehicleRestrictions.AsReadOnly();
        public IReadOnlyCollection<string> Recipes => _recipes.AsReadOnly();

        private readonly List<string> _vehicleRestrictions = new();
        private readonly List<string> _recipes = new();

        protected CabinAttendant() { }

        public CabinAttendant(Guid id, AttendantInfo info, AttendantType type, IEnumerable<string> vehicleRestrictions)
        {
            if (info == null)
                throw new ArgumentNullException(nameof(info));
            if (vehicleRestrictions == null || !vehicleRestrictions.Any())
                throw new ArgumentException("Vehicle restrictions cannot be empty.");

            Id = id == Guid.Empty ? Guid.NewGuid() : id;
            Info = info;
            AttendantType = type;

            _vehicleRestrictions.AddRange(vehicleRestrictions);
        }

        public void AddRecipe(string recipe)
        {
            if (AttendantType != AttendantType.Chef)
                throw new InvalidOperationException("Only chefs can have recipes.");
            if (_recipes.Count >= 4)
                throw new InvalidOperationException("Chef cannot have more than 4 recipes.");
            if (string.IsNullOrWhiteSpace(recipe))
                throw new ArgumentException("Recipe cannot be empty.");
            _recipes.Add(recipe);
        }

        public void RemoveRecipe(string recipe)
        {
            if (AttendantType != AttendantType.Chef)
                throw new InvalidOperationException("Only chefs have recipes.");
            if (!_recipes.Contains(recipe))
                throw new InvalidOperationException("Recipe not found.");
            _recipes.Remove(recipe);
        }

        public void ChangeType(AttendantType type)
        {
            AttendantType = type;

            if (type != AttendantType.Chef)
            {
                _recipes.Clear();
            }
        }

        public void AddVehicleRestriction(string vehicle)
        {
            if (!_vehicleRestrictions.Contains(vehicle))
                _vehicleRestrictions.Add(vehicle);
        }

        public void RemoveVehicleRestriction(string vehicle)
        {
            _vehicleRestrictions.Remove(vehicle);
        }

        public void UpdateInfo(AttendantInfo newInfo)
        {
            if (newInfo == null) throw new ArgumentNullException(nameof(newInfo));
            Info = newInfo;
        }
    }
}
