using System;
using System.Collections.Generic;
using System.IO;

namespace ZooGarden
{
    class Program
    {
        // Input Output Encoding
        static string filePath = "animals.txt";
        static List<Animal> animals = new List<Animal>();

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8; // За кирилица в конзолата
            LoadAnimals();

            while (true)
            {
                Console.WriteLine("\n--- Zoo Management ---");
                Console.WriteLine("1. Добавяне на животно");
                Console.WriteLine("2. Промяна на статус на наличност");
                Console.WriteLine("3. Проверка на животно по ID");
                Console.WriteLine("4. Показване на всички животни");
                Console.Write("Избор: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddAnimal();
                        break;
                    case "2":
                        UpdateAvailability();
                        break;
                    case "3":
                        CheckAnimalByID();
                        break;
                    case "4":
                        ShowAllAnimals();
                        break;
                    case "5":
                        return;
                    default:
                        Console.WriteLine("Невалиден избор!");
                        break;
                }

                if (!ReturnToMenu())
                {
                    break;
                }
            }
        }

        static bool ReturnToMenu()
        {
            while (true)
            {
                Console.WriteLine("\nИскаш ли да се върнеш в главното меню? (да/не)");
                string input = Console.ReadLine().Trim().ToLower();

                if (input == "да" || input == "д" || input == "yes" || input == "y")
                    return true;
                else if (input == "не" || input == "н" || input == "no" || input == "n")
                    return false;
                else
                    Console.WriteLine("Моля, въведи 'да' или 'не'!");
            }
        }

        static void LoadAnimals()
        {
            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath, System.Text.Encoding.UTF8);
                foreach (var line in lines)
                {
                    string[] parts = line.Split(';');
                    if (parts.Length == 6)
                    {
                        int id = int.Parse(parts[0]);
                        string species = parts[1];
                        string name = parts[2];
                        int age = int.Parse(parts[3]);
                        string habitat = parts[4];
                        bool availability = bool.Parse(parts[5]);

                        animals.Add(new Animal(id, species, name, age, habitat, availability));
                    }
                }
            }
        }

        static void SaveAnimals()
        {
            List<string> lines = new List<string>();
            foreach (var animal in animals)
            {
                lines.Add(animal.ToFileFormat());
            }
            File.WriteAllLines(filePath, lines, System.Text.Encoding.UTF8);
        }

        static void AddAnimal()
        {
            int id;
            while (true)
            {
                Console.Write("ID (цяло число): ");
                if (int.TryParse(Console.ReadLine(), out id))
                    break;
                Console.WriteLine("Моля, въведете валидно цяло число за ID!");
            }

            Console.Write("Вид: ");
            string species = Console.ReadLine();

            Console.Write("Име: ");
            string name = Console.ReadLine();

            int age;
            while (true)
            {
                Console.Write("Възраст (цяло число): ");
                if (int.TryParse(Console.ReadLine(), out age))
                    break;
                Console.WriteLine("Моля, въведете валидно цяло число за възраст!");
            }

            Console.Write("Местообитание: ");
            string habitat = Console.ReadLine();

            bool availability;
            while (true)
            {
                Console.Write("Налично за разглеждане (true/false): ");
                string availInput = Console.ReadLine().ToLower();

                if (availInput == "true" || availInput == "false")
                {
                    availability = availInput == "true";
                    break;
                }
                Console.WriteLine("Моля, въведете 'true' или 'false'!");
            }

            animals.Add(new Animal(id, species, name, age, habitat, availability));
            SaveAnimals();
            Console.WriteLine("Животното е добавено успешно!");
        }

        static void UpdateAvailability()
        {
            int id;
            while (true)
            {
                Console.Write("Въведи ID: ");
                string input = Console.ReadLine();

                if (int.TryParse(input, out id))
                    break;
                else
                    Console.WriteLine("Моля, въведете валидно цяло число за ID!");
            }

            foreach (var animal in animals)
            {
                if (animal.AnimalID == id)
                {
                    animal.Availability = !animal.Availability;
                    SaveAnimals();
                    Console.WriteLine($"Статусът на животното с ID {id} е променен.");
                    return;
                }
            }
            Console.WriteLine("Животно с такова ID не е намерено!");
        }

        static void CheckAnimalByID()
        {
            int id;
            while (true)
            {
                Console.Write("Въведи ID на животното: ");
                if (int.TryParse(Console.ReadLine(), out id))
                    break;
                Console.WriteLine("Моля, въведете валиден ID (цяло число)!");
            }

            foreach (var animal in animals)
            {
                if (animal.AnimalID == id)
                {
                    Console.WriteLine($"\nВид: {animal.Species}");
                    Console.WriteLine($"Име: {animal.Name}");
                    Console.WriteLine($"Възраст: {animal.Age}");
                    Console.WriteLine($"Местообитание: {animal.Habitat}");
                    Console.WriteLine($"Налично за разглеждане: {animal.Availability}");
                    return;
                }
            }
            Console.WriteLine("Животно с такова ID не е намерено!");
        }

        static void ShowAllAnimals()
        {
            Console.WriteLine("\n--- Списък с всички животни ---");
            foreach (var animal in animals)
            {
                Console.WriteLine($"ID: {animal.AnimalID}, Вид: {animal.Species}, Име: {animal.Name}, Възраст: {animal.Age}, Местообитание: {animal.Habitat}, Наличност: {animal.Availability}");
            }
        }
    }
}
