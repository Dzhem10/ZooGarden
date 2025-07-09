using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;


namespace ZooGarden // Пространство от имена за потребителската част на приложението (интерфейс + данни).
{
    // Импортираме константите от класа Constants директно (статично),
    // така че да можем да използваме AnimalsFilePath, без да пишем Constants.AnimalsFilePath.
    using static Constants;

    public class Data
    {
        // Публичен списък с животни, достъпен само за четене отвън (с private set),
        // за да не може да се презаписва от други класове директно.
        public List<Animal> Animals { get; private set; }

        // Конструктор – при създаване на обект от тип Data, автоматично зареждаме животните от файл.
        public Data()
        {
            LoadAnimals();
        }

        // Метод за записване на текущия списък с животни във файл.
        public void Save()
        {
            // Създаваме StreamWriter за писане във файла, определен в Constants.AnimalsFilePath.
            using StreamWriter writer = new StreamWriter(AnimalsFilePath);

            // Сериализираме списъка с животни в JSON формат.
            string jsonData = JsonSerializer.Serialize(Animals);

            // Записваме JSON текста във файла.
            writer.WriteLine(jsonData);
        }

        // Частен метод за зареждане на животни от файла при стартиране.
        private void LoadAnimals()
        {
            // Инициализираме празен списък по подразбиране (ако файлът липсва или е празен).
            Animals = new List<Animal>();

            try
            {
                // Опитваме се да прочетем съдържанието на файла.
                using StreamReader reader = new StreamReader(AnimalsFilePath);
                string jsonData = reader.ReadToEnd();

                // Ако файлът не е празен, десериализираме JSON текста обратно в списък от Animal.
                if (!string.IsNullOrEmpty(jsonData))
                {
                    Animals = JsonSerializer.Deserialize<List<Animal>>(jsonData)!;
                }
            }
            catch (FileNotFoundException)
            {
                // Ако файлът не съществува – оставяме списъка празен.
                Animals = new List<Animal>();
            }
        }

        // Метод за връщане на всички налични животни (филтриране по Availability == true).
        public List<Animal> GetAvailableAnimals()
        {
            return Animals.Where(a => a.Availability).ToList();
        }

        // Метод за връщане на всички заети животни (Availability == false).
        public List<Animal> GetUnavailableAnimals()
        {
            return Animals.Where(a => !a.Availability).ToList();
        }
    }
}