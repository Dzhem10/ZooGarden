using System;
using System.IO;

namespace ZooGarden // Пространство от имена, използвано за тази версия на класа Animal.
{
#nullable disable // Изключва проверките за null стойности (за да не се изисква инициализация или null проверки за string полета)

    /// <summary>
    /// Модел на животно в зоологическа система.
    /// Съдържа данни и поведение, свързани с конкретно животно.
    /// </summary>
    public class Animal
    {
        // Частни полета – използвани за съхранение на данните вътрешно в класа.
        private string species;
        private string name;
        private int age;
        private string habitat;

        // Конструкторът приема стойности за всички ключови свойства на животното
        // и автоматично създава уникално AnimalID.
        public Animal(string species, string name, int age, string habitat)
        {
            AnimalID = Guid.NewGuid().ToString(); // Създаване на уникален идентификатор
            Species = species;
            Name = name;
            Age = age;
            Habitat = habitat;
        }

        // Уникално ID на животното, което не може да се променя 
        public string AnimalID { get; set; }

        // Свойство за вид (species)
        public string Species
        {
            get { return species; }
            set
            {
                // Проверка за невалиден вход – празен, null или само интервали
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new InvalidDataException("Невалиден вид на животното.");
                }
                species = value.Trim(); // Премахва излишни интервали
            }
        }

        // Свойство за име (name)
        public string Name
        {
            get { return name; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new InvalidDataException("Невалидно име на животното.");
                }
                name = value.Trim();
            }
        }

        // Свойство за възраст (age)
        public int Age
        {
            get { return age; }
            set
            {
                if (value < 0 || value > 200) // Ограничения за допустима възраст
                {
                    throw new InvalidDataException("Невалидна възраст за животно.");
                }
                age = value;
            }
        }

        // Свойство за местообитание (habitat)
        public string Habitat
        {
            get { return habitat; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new InvalidDataException("Невалидно местообитание.");
                }
                habitat = value.Trim();
            }
        }

        // Наличност на животното – по подразбиране е true (налично).
        // Може да се използва, за да се отбележи дали животното е "в клетка", "на разходка", "заето" и т.н.
        public bool Availability { get; set; } = true;

        // Презаписване на ToString() – за по-удобно показване на животното в текстов формат.
        public override string ToString()
        {
            string status = Availability ? "Налично" : "Заето";
            return $"{Name} ({Species}), {Age} г. - {Habitat} [{status}]";
        }

        // Представя животното като ред за запис във файл, като стойностите са разделени със `;`.
        public string ToFileFormat()
        {
            return $"{AnimalID};{Species};{Name};{Age};{Habitat};{Availability}";
        }
    }
}
