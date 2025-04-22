using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.Enums;

namespace Domain.Entities
{
    public class MediaFile
    {
        public int Id { get; set; }
        public FileType FileType { get; set; }
        public string FilePath { get; set; } = null!;

        // Зв’язки
        public int PlaceId { get; set; }
        public virtual Place Place { get; set; } = null!;
    }
}
