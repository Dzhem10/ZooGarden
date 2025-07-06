using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ZooGarden
{

    using static Constants;
    public class Data
    {
        public List<Animal> Animals { get; private set; }

        public Data()
        {
            LoadAnimals();
        }

        public void Save()
        {
            using StreamWriter writer = new StreamWriter(AnimalsFilePath);
            string jsonData = JsonSerializer.Serialize(Animals);
            writer.WriteLine(jsonData);
        }

        private void LoadAnimals()
        {
            Animals = new List<Animal>();

            try
            {
                using StreamReader reader = new StreamReader(AnimalsFilePath);
                string jsonData = reader.ReadToEnd();

                if (!string.IsNullOrEmpty(jsonData))
                {
                    Animals = JsonSerializer.Deserialize<List<Animal>>(jsonData)!;
                }
            }
            catch (FileNotFoundException)
            {
                Animals = new List<Animal>();
            }
        }

        public List<Animal> GetAvailableAnimals()
        {
            return Animals.Where(a => a.Availability).ToList();
        }

        public List<Animal> GetUnavailableAnimals()
        {
            return Animals.Where(a => !a.Availability).ToList();
        }
    }
}
