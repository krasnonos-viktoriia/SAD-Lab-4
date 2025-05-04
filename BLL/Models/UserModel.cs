using Domain.Entities.Enums;

namespace BLL.Models
{
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

/*using Domain.Entities.Enums;

namespace BLL.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        //public Role Role { get; set; } //= null!;
        public RoleEnum Role { get; set; }

        public enum RoleEnum
        {
            Regular,
            Manager
        }
    }
}*/