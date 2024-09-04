using System.ComponentModel.DataAnnotations;

namespace ToDoList.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [MaxLength(25)]
        public string? UserName { get; set; }

        [MinLength(8)]
        public string? UserPasword { get; set; }

        public int RoleId { get; set; } // llave foranea
        public Role Roles { get; set; } = null!; // Relacion con entidad/tabla roles

        public int StateId { get; set; } // llave foranea
        public State States { get; set; } = null!; // Relacion con entidad/tabla estados 

        public ICollection<Task> TasksList { get; set; } = new List<Task> (); // Relacion con entidad/tabla tareas
    }
}
