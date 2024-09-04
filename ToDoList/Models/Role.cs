using System.ComponentModel.DataAnnotations;

namespace ToDoList.Models
{
    public class Role
    {
        [Key]
        public int RoleId { get; set; }

        [Required]
        public required string RoleName { get; set; }

        public ICollection<User> UsersList { get; set; } = new List<User>(); // Relacion con entidad/tabla usuarios
    }
}
