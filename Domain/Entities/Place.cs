using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Place
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string Description { get; set; } = null!;

        // Зв’язки
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
        public ICollection<Question> Questions { get; set; } = new List<Question>();
        public ICollection<MediaFile> MediaFiles { get; set; } = new List<MediaFile>();
        public ICollection<Visit> Visits { get; set; } = new List<Visit>();
    }
}
