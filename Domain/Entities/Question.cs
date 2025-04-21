using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Question
    {
        public int Id { get; set; }
        public string Text { get; set; } = null!;
        public string? Answer { get; set; }

        // Зв’язки
        public int UserId { get; set; } // тільки менеджер може створити
        public User User { get; set; } = null!;

        public int PlaceId { get; set; }
        public Place Place { get; set; } = null!;
    }
}