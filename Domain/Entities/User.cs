using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.Enums;

namespace Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public Role Role { get; set; }

        // Зв’язки
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
        public ICollection<Question> Questions { get; set; } = new List<Question>();
        public ICollection<Visit> Visits { get; set; } = new List<Visit>();
    }
}
