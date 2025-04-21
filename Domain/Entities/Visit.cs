using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Visit
    {
        public int Id { get; set; }
        public DateTime VisitDate { get; set; }

        public int UserId { get; set; }
        public User User { get; set; } = null!;

        public int PlaceId { get; set; }
        public Place Place { get; set; } = null!;
    }
}
