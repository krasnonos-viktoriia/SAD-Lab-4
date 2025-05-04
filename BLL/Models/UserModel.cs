namespace BLL.Models
{
    //Модель для представлення користувача в системі
    public class UserModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public RoleEnum Role { get; set; }

        public enum RoleEnum
        {
            Regular,
            Manager
        }
    }
}
