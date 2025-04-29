using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    // Клас, що представляє візит користувача до певного місця із зазначенням дати та зв'язків із користувачем і місцем.
    public class Visit
    {
        // Ідентифікатор візиту.
        public int Id { get; set; }

        // Дата та час візиту.
        public DateTime VisitDate { get; set; }

        // Ідентифікатор користувача, який здійснив візит.
        public int UserId { get; set; }

        // Зв'язок з користувачем, який здійснив візит.
        public virtual User User { get; set; } = null!;

        // Ідентифікатор місця, яке відвідав користувач.
        public int PlaceId { get; set; }

        // Зв'язок із місцем, яке було відвідано.
        public virtual Place Place { get; set; } = null!;
    }
}