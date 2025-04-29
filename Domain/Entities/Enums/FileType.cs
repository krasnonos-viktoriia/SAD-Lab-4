using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Enums
{
    // Перерахування, що визначає типи файлів, що можуть бути представлені в системі.
    public enum FileType
    {
        Photo, // Фото
        Video, // Відео
        Audio, // Аудіо
        Link, // Посилання
        Other // Інший тип
    }
}
