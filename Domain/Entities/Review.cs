using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Review
    {
        public int Id { get; set; }
        public string Comment { get; set; } = null!;
        public int Rating { get; set; } // від 1 до 5
        public DateTime CreatedAt { get; set; }

        // Зв’язки
        public int UserId { get; set; }
        public virtual User User { get; set; } = null!;

        public int PlaceId { get; set; }
        public virtual Place Place { get; set; } = null!;
    }
}
