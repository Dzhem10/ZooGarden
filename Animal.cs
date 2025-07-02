using System;

namespace ZooGarden
{
    public class Animal
    {

        // Свойства за всяко животно
        public int AnimalID { get; set; }
        public string Species { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Habitat { get; set; }
        public bool Availability { get; set; }

        // Конструктор за създаване на животно
        public Animal(int id, string species, string name, int age, string habitat, bool availability)
        {
            AnimalID = id;
            Species = species;
            Name = name;
            Age = age;
            Habitat = habitat;
            Availability = availability;
        }

        // Метод за преобразуване на животното към формат за запис във файл
        public string ToFileFormat()
        {
            return $"{AnimalID};{Species};{Name};{Age};{Habitat};{Availability}";
        }
    }
}
