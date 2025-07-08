using System;
using System.Collections.Generic;
using System.IO;

namespace ZooGarden
{
    public class Program
    {
        private static Data data = new Data();

        static void Main(string[] args)
        {

            // Настройка на конзолата за правилна работа с Unicode символи (български букви и др.)
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            Console.InputEncoding = System.Text.Encoding.Unicode;

            DisplayMenu();

            string choice = string.Empty;
            while ((choice = Console.ReadLine()!.ToLower()) != "x")
            {
                switch (choice)
                {
                    case "1":
                        Animal newAnimal = DisplayAddNewAnimal();
                        data.Animals.Add(newAnimal);
                        data.Save();
                        BackToMenu();
                        break;

                    case "2":
                        DisplayMarkUnavailableAnimal(data.Animals);
                        data.Save();
                        BackToMenu();
                        break;

                    case "3":
                        DisplayMarkAvailableAnimal(data.GetUnavailableAnimals());
                        data.Save();
                        BackToMenu();
                        break;

                    case "4":
                        DisplayAllAnimals(data.Animals);
                        BackToMenu();
                        break;

                    case "5":
                        DisplayUnavailableAnimals(data.GetUnavailableAnimals());
                        BackToMenu();
                        break;

                    default:
                        break;
                }
            }

            Environment.Exit(0);
        }

        private static Animal DisplayAddNewAnimal()
        {
            Console.Clear();
            Console.WriteLine("Добавяне на ново животно");
            Console.WriteLine("=========================");
            Console.Write("Въведи вид: ");
            string species = Console.ReadLine();
            Console.Write("Въведи име: ");
            string name = Console.ReadLine();
            Console.Write("Въведи възраст: ");
            int age = int.Parse(Console.ReadLine());
            Console.Write("Въведи хабитат: ");
            string habitat = Console.ReadLine();

            return new Animal(species, name, age, habitat);
        }

        private static void DisplayMarkUnavailableAnimal(List<Animal> animals)
        {
            Console.Clear();
            Console.WriteLine("Отбележи животно като заето");
            Console.WriteLine("===========================");

            var available = animals.Where(a => a.Availability).ToList();
            if (available.Count == 0)
            {
                Console.WriteLine("[ Няма налични животни ]");
                return;
            }

            int i = 1;
            foreach (var animal in available)
            {
                Console.WriteLine($"{i++:d3}. {animal}");
            }

            Console.Write("Избери номер: ");
            int index = int.Parse(Console.ReadLine());

            available[index - 1].Availability = false;

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Животното е отбелязано като заето.");
            Console.ResetColor();
        }

        private static void DisplayMarkAvailableAnimal(List<Animal> unavailable)
        {
            Console.Clear();
            Console.WriteLine("Отбележи животно като налично");
            Console.WriteLine("=============================");

            if (unavailable.Count == 0)
            {
                Console.WriteLine("[ Няма заети животни ]");
                return;
            }

            int i = 1;
            foreach (var animal in unavailable)
            {
                Console.WriteLine($"{i++:d3}. {animal}");
            }

            Console.Write("Избери номер: ");
            int index = int.Parse(Console.ReadLine());

            unavailable[index - 1].Availability = true;

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Животното е отбелязано като налично.");
            Console.ResetColor();
        }

        private static void DisplayAllAnimals(List<Animal> animals)
        {
            Console.Clear();
            Console.WriteLine("Всички животни");
            Console.WriteLine("==============");
            Console.WriteLine();
            foreach (Animal animal in animals)
            {
                Console.WriteLine($"▶ {animal}");
            }
        }

        private static void DisplayUnavailableAnimals(List<Animal> unavailable)
        {
            Console.Clear();
            Console.WriteLine("Списък на заетите животни");
            Console.WriteLine("==========================");
            Console.WriteLine();
            foreach (var animal in unavailable)
            {
                Console.WriteLine($"▶ {animal}");
            }
        }

        private static void BackToMenu()
        {
            Console.WriteLine();
            Console.Write("Натисни ENTER за връщане към меню...");
            Console.ReadLine();
            Console.Clear();
            DisplayMenu();
        }

        private static void DisplayMenu()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("=========================");
            Console.WriteLine("★     М Е Н Ю - ЗОО    ★");
            Console.WriteLine("=========================");
            Console.WriteLine();
            Console.WriteLine("1. ▶ Добавяне на ново животно");
            Console.WriteLine("2. ▶ Отбелязване като заето");
            Console.WriteLine("3. ▶ Отбелязване като налично");
            Console.WriteLine("4. ▶ Справка за всички животни");
            Console.WriteLine("5. ▶ Справка за заетите животни");
            Console.WriteLine();
            Console.WriteLine("x. ❌ Изход");
            Console.WriteLine("=========================");
            Console.Write("Твоят избор: ");
        }
    }
}
