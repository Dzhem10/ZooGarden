using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZooGarden
{

    // Класът Constants е статичен, защото съдържа само константи,
    // които не трябва да се променят и не изискват създаване на обект.
    public static class Constants
    {
        // Пътят до файла с животните.
        // Използва се за запис/четене на информация за животните от файл.
        // Относителният път "../../../" означава, че файлът се намира три папки
        // нагоре спрямо изходната директория (bin/Debug или bin/Release).
        public const string AnimalsFilePath = "../../../animals.txt";
    }

}
