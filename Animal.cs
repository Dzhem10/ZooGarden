using System;

namespace ZooGarden
{
    public class Animal
    {

        // Свойства за всяко животно
        public string AnimalID { get; set; }
        public string Species { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Habitat { get; set; }
        public bool Availability { get; set; } = true;

        // Конструктор за създаване на животно
        public Animal(string species, string name, int age, string habitat)
        {
            AnimalID = Guid.NewGuid().ToString();
            Species = species;
            Name = name;
            Age = age;
            Habitat = habitat;
        }

        // Метод за преобразуване на животното към формат за запис във файл
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
