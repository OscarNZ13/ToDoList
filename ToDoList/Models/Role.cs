using System.ComponentModel.DataAnnotations;

namespace ToDoList.Models
{
    public class Role
    {
        [Key]
        public int RoleId { get; set; }

        [Required]
        public string RoleName { get; set; } = null!;

        public ICollection<User> UsersList { get; set; } = new List<User>(); // Relacion con entidad/tabla usuarios
    }
}
