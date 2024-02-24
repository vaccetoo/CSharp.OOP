
namespace Animals;

public class StartUp
{
    public static void Main()
    {
        string input = string.Empty;

        while ((input = Console.ReadLine()) != "Beast!")
        {
            string animalType = input;
            string[] animalInfo = Console.ReadLine()
                .Split(" ", StringSplitOptions.RemoveEmptyEntries);

            string name = animalInfo[0];
            int age = int.Parse(animalInfo[1]);
            string gender = animalInfo[2];

            try
            {
                if (animalType == "Dog")
                {
                    Dog dog = new Dog(name, age, gender);
                    Console.WriteLine(animalType);
                    Console.WriteLine(dog);
                    dog.ProduceSound();
                }
                else if (animalType == "Frog")
                {
                    Frog frog = new Frog(name, age, gender);
                    Console.WriteLine(animalType);
                    Console.WriteLine(frog);
                }
                else if (animalType == "Cat")
                {
                    Cat cat = new Cat(name, age, gender);
                    Console.WriteLine(animalType);
                    Console.WriteLine(cat);
                }
                else if (animalType == "Tomcat")
                {
                    Tomcat tomcat = new Tomcat(name, age);
                    Console.WriteLine(animalType);
                    Console.WriteLine(tomcat);
                }
                else if (animalType == "Kitten")
                {
                    Kitten kitten = new Kitten(name, age);
                    Console.WriteLine(animalType);
                    Console.WriteLine(kitten);
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}