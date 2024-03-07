
using WildFarm.Core;
using WildFarm.Factories;
using WildFarm.Factories.Interfaces;

IAnimalFactory animalFactory = new AnimalFactory();
IFoodFactory foodFactory = new FoodFactory();

Engine engine = new Engine(animalFactory, foodFactory);
engine.Run();