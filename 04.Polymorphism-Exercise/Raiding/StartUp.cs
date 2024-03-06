
using Raiding.Core;
using Raiding.Factories;
using Raiding.Factories.Interfaces;

IHeroFactory heroFactory = new HeroFactory();

Engine engine = new Engine(heroFactory);
engine.Run();