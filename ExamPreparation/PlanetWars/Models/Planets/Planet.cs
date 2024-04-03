using PlanetWars.Models.MilitaryUnits;
using PlanetWars.Models.MilitaryUnits.Contracts;
using PlanetWars.Models.Planets.Contracts;
using PlanetWars.Models.Weapons;
using PlanetWars.Models.Weapons.Contracts;
using PlanetWars.Repositories;
using PlanetWars.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlanetWars.Models.Planets
{
    public class Planet : IPlanet
    {
        private IRepository<IMilitaryUnit> units;
        private IRepository<IWeapon> weapons;
        private string name;
        private double budget;

        public Planet(string name, double budget)
        {
            Name = name;
            Budget = budget;

            units = new UnitRepository();
            weapons = new WeaponRepository();
        }

        public string Name
        {
            get => name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException("Planet name cannot be null or empty.");
                }
                name = value;
            }
        }

        public double Budget
        {
            get => budget;
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Budget's amount cannot be negative.");
                }
                budget = value;
            }
        }

        public double MilitaryPower
            => CalculatemilitaryPower();

        public IReadOnlyCollection<IMilitaryUnit> Army
            => units.Models;

        public IReadOnlyCollection<IWeapon> Weapons
            => weapons.Models;

        public void AddUnit(IMilitaryUnit unit)
            => units.AddItem(unit);

        public void AddWeapon(IWeapon weapon)
            => weapons.AddItem(weapon);

        public string PlanetInfo()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"Planet: {Name}");
            sb.AppendLine($"--Budget: {Budget} billion QUID");

            if (units.Models.Any())
            {
                string result = string.Join(", ", units.Models.Select(u => u.GetType().Name));
                sb.AppendLine($"--Forces: {result}");
            }
            else
            {
                sb.AppendLine($"--Forces: No units");
            }

            if (weapons.Models.Any())
            {
                string result = string.Join(", ", weapons.Models.Select(w => w.GetType().Name));
                sb.AppendLine($"--Combat equipment: {result}");
            }
            else
            {
                sb.AppendLine($"--Combat equipment: No weapons");
            }

            sb.AppendLine($"--Military Power: {MilitaryPower}");

            return sb.ToString().Trim();
        }

        public void Profit(double amount)
        {
            Budget += amount;
        }

        public void Spend(double amount)
        {
            if (amount > Budget)
            {
                throw new ArgumentException("Budget too low!");
            }

            Budget -= amount;
        }

        public void TrainArmy()
        {
            foreach (var itemUnit in units.Models)
            {
                itemUnit.IncreaseEndurance();
            }
        }

        private double CalculatemilitaryPower()
        {
            //double totalAmount = weapons
            //    .Models
            //    .Sum(w => w.DestructionLevel) + units.Models.Sum(u => u.EnduranceLevel);

            double totalAmount = 0;

            foreach (var itemUnit in units.Models)
            {
                totalAmount += itemUnit.EnduranceLevel;
            }

            foreach (var weapon in weapons.Models)
            {
                totalAmount += weapon.DestructionLevel;
            }

            if (units.Models.Any(u => u.GetType().Name == nameof(AnonymousImpactUnit)))
            {
                totalAmount += totalAmount * 0.3;
            }

            if (weapons.Models.Any(w => w.GetType().Name == nameof(NuclearWeapon))) 
            {
                totalAmount += totalAmount * 0.45;
            }

            return Math.Round(totalAmount, 3);
        }
    }
}
