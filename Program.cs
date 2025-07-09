using System;
using System.Collections.Generic;
using System.IO;


namespace ZooGarden // Пространство от имена за тази част на проекта (интерфейс/входна точка).
{
    public class Program
    {
        // Създаваме инстанция на класа Data, който отговаря за:
        // - списъка с животни (Animals)
        // - зареждането и записа във файл
        private static Data data = new Data();

        static void Main(string[] args)
        {
            // Задаваме енкодинга на конзолата, за да може правилно да показва/чете Unicode символи (напр. кирилица).
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            Console.InputEncoding = System.Text.Encoding.Unicode;

            DisplayMenu(); // Показваме началното меню

            string choice;
            while ((choice = Console.ReadLine()!.ToLower()) != "x") // Четем избор, докато потребителят не въведе 'x'
            {
                switch (choice)
                {
                    case "1": // Добавяне на ново животно
                        try
                        {
                            Animal newAnimal = DisplayAddNewAnimal(); // Събиране на информация за новото животно
                            data.Animals.Add(newAnimal); // Добавяне към списъка
                            data.Save(); // Записване във файл
                            ShowSuccess("Животното е добавено успешно.");
                        }
                        catch (InvalidDataException e)
                        {
                            ShowError(e.Message); // Грешка при въвеждане
                        }
                        BackToMenu();
                        break;

                    case "2": // Отбелязване на животно като заето (недостъпно)
                        DisplayMarkUnavailableAnimal(data.Animals);
                        data.Save();
                        BackToMenu();
                        break;

                    case "3": // Отбелязване като отново налично
                        DisplayMarkAvailableAnimal(data.GetUnavailableAnimals());
                        data.Save();
                        BackToMenu();
                        break;

                    case "4": // Показване на всички животни
                        DisplayAllAnimals(data.Animals);
                        BackToMenu();
                        break;

                    case "5": // Справка само за заетите животни
                        DisplayUnavailableAnimals(data.GetUnavailableAnimals());
                        BackToMenu();
                        break;

                    default: // Невалиден избор
                        ShowError("Невалиден избор!");
                        BackToMenu();
                        break;
                }
            }

            Environment.Exit(0); // Изход от програмата
        }

        // Метод за добавяне на ново животно чрез вход от потребителя
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
            if (!int.TryParse(Console.ReadLine(), out int age))
            {
                throw new InvalidDataException("Невалидна стойност за възраст.");
            }

            Console.Write("Въведи хабитат: ");
            string habitat = Console.ReadLine();

            // Създаваме и връщаме нов обект Animal
            return new Animal(species, name, age, habitat);
        }

        // Отбелязване на животно като заето (недостъпно)
        private static void DisplayMarkUnavailableAnimal(List<Animal> animals)
        {
            Console.Clear();
            Console.WriteLine("Отбележи животно като заето");
            Console.WriteLine("===========================");

            var available = animals.FindAll(a => a.Availability); // Филтрираме само наличните
            if (available.Count == 0)
            {
                Console.WriteLine("[ Няма налични животни ]");
                return;
            }

            // Извеждаме списък с номерация
            for (int i = 0; i < available.Count; i++)
            {
                Console.WriteLine($"{i + 1:d3}. {available[i]}");
            }

            Console.Write("Избери номер: ");
            if (int.TryParse(Console.ReadLine(), out int index) && index >= 1 && index <= available.Count)
            {
                available[index - 1].Availability = false; // Променяме статуса
                ShowSuccess("Животното е отбелязано като заето.");
            }
            else
            {
                ShowError("Невалиден избор.");
            }
        }

        // Отбелязване на животно като налично (свободно)
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

            for (int i = 0; i < unavailable.Count; i++)
            {
                Console.WriteLine($"{i + 1:d3}. {unavailable[i]}");
            }

            Console.Write("Избери номер: ");
            if (int.TryParse(Console.ReadLine(), out int index) && index >= 1 && index <= unavailable.Count)
            {
                unavailable[index - 1].Availability = true;
                ShowSuccess("Животното е отбелязано като налично.");
            }
            else
            {
                ShowError("Невалиден избор.");
            }
        }

        // Показва списък с всички животни
        private static void DisplayAllAnimals(List<Animal> animals)
        {
            Console.Clear();
            Console.WriteLine("Всички животни");
            Console.WriteLine("==============");
            if (animals.Count == 0)
            {
                Console.WriteLine("[ Няма добавени животни ]");
                return;
            }

            foreach (Animal animal in animals)
            {
                Console.WriteLine($"▶ {animal}");
            }
        }

        // Показва само заетите животни
        private static void DisplayUnavailableAnimals(List<Animal> unavailable)
        {
            Console.Clear();
            Console.WriteLine("Списък на заетите животни");
            Console.WriteLine("==========================");

            if (unavailable.Count == 0)
            {
                Console.WriteLine("[ Няма заети животни ]");
                return;
            }

            foreach (var animal in unavailable)
            {
                Console.WriteLine($"▶ {animal}");
            }
        }

        // Метод за връщане към менюто след всяка операция
        private static void BackToMenu()
        {
            Console.WriteLine();
            Console.Write("Натисни ENTER за връщане към меню...");
            Console.ReadLine();
            Console.Clear();
            DisplayMenu();
        }

        // Показва началното меню
        private static void DisplayMenu()
        {
            Console.WriteLine("=========================");
            Console.WriteLine("★     М Е Н Ю - ЗОО    ★");
            Console.WriteLine("=========================");
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


        // Показва съобщение за грешка в червено
        private static void ShowError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\nГрешка: {message}");
            Console.ResetColor();
        }

        // Показва съобщение за успех в зелено
        private static void ShowSuccess(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n{message}");
            Console.ResetColor();
        }
    }
}