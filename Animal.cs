using System;
using System.IO;

namespace ZooGarden
{
    public class Animal
    {
        private string species;
        private string name;
        private int age;
        private string habitat;
        private bool availability;

        public string AnimalID { get; set; }

        public string Species
        {
            get => species;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new InvalidDataException("Невалиден вид на животното.");
                species = value.Trim();
            }
        }

        public string Name
        {
            get => name;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new InvalidDataException("Невалидно име на животно.");
                name = value.Trim();
            }
        }

        public int Age
        {
            get => age;
            set
            {
                if (value <= 0 || value > 200)
                    throw new InvalidDataException("Възрастта трябва да е по-голяма от 0 и не повече от 200.");
                age = value;
            }
        }

        public string Habitat
        {
            get => habitat;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new InvalidDataException("Невалидно местообитание.");
                habitat = value.Trim();
            }
        }

        public bool Availability
        {
            get => availability;
            set
            {
                availability = value;
              
            }
        }

        public string StatusTag { get; set; }

        public Animal(string species, string name, int age, string habitat)
        {
            AnimalID = Guid.NewGuid().ToString();
            Species = species;
            Name = name;
            Age = age;
            Habitat = habitat;
            Availability = true;
           
        }

        public string ToFileFormat()
        {
            return $"{AnimalID};{Species};{Name};{Age};{Habitat};{Availability}";
        }

        public override string ToString()
        {
            string status = Availability ? "Налично" : "Заето";
            return $"{Name} ({Species}), {Age} г. - {Habitat} [{status}]";
        }
    }
}
