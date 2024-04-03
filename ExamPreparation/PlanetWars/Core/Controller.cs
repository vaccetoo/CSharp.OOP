using PlanetWars.Core.Contracts;
using PlanetWars.Models.MilitaryUnits;
using PlanetWars.Models.MilitaryUnits.Contracts;
using PlanetWars.Models.Planets;
using PlanetWars.Models.Planets.Contracts;
using PlanetWars.Models.Weapons;
using PlanetWars.Models.Weapons.Contracts;
using PlanetWars.Repositories;
using PlanetWars.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlanetWars.Core
{
    public class Controller : IController
    {
        private IRepository<IPlanet> planets;
        private bool isWinner = true;

        public Controller()
        {
            planets = new PlanetRepository();
        }

        public string AddUnit(string unitTypeName, string planetName)
        {
            if (planets.FindByName(planetName) == null)
            {
                throw new InvalidOperationException($"Planet {planetName} does not exist!");
            }

            if (unitTypeName != nameof(AnonymousImpactUnit) &&
                unitTypeName != nameof(SpaceForces) &&
                unitTypeName != nameof(StormTroopers))
            {
                throw new InvalidOperationException($"{unitTypeName} still not available!");
            }

            IPlanet planet = planets.FindByName(planetName);

            if (planet.Army.Any(u => u.GetType().Name == unitTypeName))
            {
                throw new InvalidOperationException($"{unitTypeName} already added to the Army of {planetName}!");
            }

            IMilitaryUnit militaryUnit = unitTypeName switch
            {
                "AnonymousImpactUnit" => new AnonymousImpactUnit(),
                "SpaceForces" => new SpaceForces(),
                "StormTroopers" => new StormTroopers()
            };

            planet.Spend(militaryUnit.Cost);
            planet.AddUnit(militaryUnit);

            return $"{unitTypeName} added successfully to the Army of {planetName}!";
        }

        public string AddWeapon(string planetName, string weaponTypeName, int destructionLevel)
        {
            if (planets.FindByName(planetName) == null)
            {
                throw new InvalidOperationException("Planet {planetName} does not exist!");
            }

            IPlanet planet = planets.FindByName(planetName);

            if (planet.Weapons.Any(w => w.GetType().Name == weaponTypeName))
            {
                throw new InvalidOperationException($"{weaponTypeName} already added to the Weapons of {planetName}!");
            }

            if (weaponTypeName != nameof(BioChemicalWeapon) &&
                weaponTypeName != nameof(NuclearWeapon) &&
                weaponTypeName != nameof(SpaceMissiles))
            {
                throw new InvalidOperationException($"{weaponTypeName} still not available!");
            }

            IWeapon weapon = weaponTypeName switch
            {
                "BioChemicalWeapon" => new BioChemicalWeapon(destructionLevel),
                "NuclearWeapon" => new NuclearWeapon(destructionLevel),
                "SpaceMissiles" => new SpaceMissiles(destructionLevel)
            };

            planet.Spend(weapon.Price);
            planet.AddWeapon(weapon);

            return $"{planetName} purchased {weaponTypeName}!";
        }

        public string CreatePlanet(string name, double budget)
        {
            if (planets.FindByName(name) != null)
            {
                return $"Planet {name} is already added!";
            }

            IPlanet planet = new Planet(name, budget);
            planets.AddItem(planet);

            return $"Successfully added Planet: {name}";
        }

        public string ForcesReport()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("***UNIVERSE PLANET MILITARY REPORT***");

            foreach (IPlanet planet in planets.Models.OrderByDescending(p => p.MilitaryPower).ThenBy(p => p.Name))
            {
                sb.AppendLine(planet.PlanetInfo());
            }

            return sb.ToString().TrimEnd();
        }

        public string SpaceCombat(string planetOne, string planetTwo)
        {
            IPlanet firstPlanet = planets.FindByName(planetOne);
            IPlanet secondPlanet = planets.FindByName(planetTwo);

            IPlanet winner = null;
            IPlanet lostPlanet = null;

            if (firstPlanet.MilitaryPower > secondPlanet.MilitaryPower)
            {
                winner = firstPlanet;
                lostPlanet = secondPlanet;
            }
            else if (firstPlanet.MilitaryPower < secondPlanet.MilitaryPower)
            {
                winner = secondPlanet;
                lostPlanet = firstPlanet;
            }
            else
            {
                if ((firstPlanet.Weapons.Any(w => w.GetType().Name == nameof(NuclearWeapon)) &&
                    secondPlanet.Weapons.Any(w => w.GetType().Name == nameof(NuclearWeapon))) ||
                    (!firstPlanet.Weapons.Any(w => w.GetType().Name == nameof(NuclearWeapon)) &&
                    !secondPlanet.Weapons.Any(w => w.GetType().Name == nameof(NuclearWeapon))))
                {
                    isWinner = false;

                    firstPlanet.Spend(firstPlanet.Budget * 0.5);
                    secondPlanet.Spend(secondPlanet.Budget * 0.5);

                }
                else if ((firstPlanet.Weapons.Any(w => w.GetType().Name == nameof(NuclearWeapon)) &&
                    !secondPlanet.Weapons.Any(w => w.GetType().Name == nameof(NuclearWeapon))))
                {
                    winner = firstPlanet;
                    lostPlanet = secondPlanet;
                }
                else if ((!firstPlanet.Weapons.Any(w => w.GetType().Name == nameof(NuclearWeapon)) &&
                    secondPlanet.Weapons.Any(w => w.GetType().Name == nameof(NuclearWeapon))))
                {
                    winner = secondPlanet;
                    lostPlanet = firstPlanet;
                }
            }

            if (isWinner)
            {
                winner.Spend(winner.Budget * 0.5);
                winner.Profit(lostPlanet.Budget * 0.5);
                winner.Profit(lostPlanet.Weapons.Sum(w => w.Price) + lostPlanet.Army.Sum(u => u.Cost));

                planets.RemoveItem(lostPlanet.Name);

                return $"{winner.Name} destructed {lostPlanet.Name}!";
            }
            else
            {
                return "The only winners from the war are the ones who supply the bullets and the bandages!";
            }

        }

        public string SpecializeForces(string planetName)
        {
            if (planets.FindByName(planetName) == null)
            {
                throw new InvalidOperationException($"Planet {planetName} does not exist!");
            }

            IPlanet planet = planets.FindByName(planetName);

            if (!planet.Army.Any())
            {
                throw new InvalidOperationException("No units available for upgrade!");
            }

            planet.TrainArmy();
            planet.Spend(1.25);

            return $"{planetName} has upgraded its forces!";
        }
    }
}
