using System.ComponentModel.DataAnnotations;

namespace ToDoList.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [MinLength(5, ErrorMessage = "El nombre de usuario debe contener minimo 5 caracteres")]
        [MaxLength(30, ErrorMessage = "El nombre de usuario debe contener maximo 30 caracteres")]
        public string? UserName { get; set; }

        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,25}$", ErrorMessage = "La contrasena debe estar entre 8 y 25 caracteres, tambien contener minimo una letra y un numero")]
        public string? UserPassword { get; set; }

        public int RoleId { get; set; } // llave foranea
        public Role Role { get; set; } = null!; // Relacion con entidad/tabla roles

        public int StateId { get; set; } // llave foranea
        public State State { get; set; } = null!; // Relacion con entidad/tabla estados 

        public ICollection<Task> TasksList { get; set; } = new List<Task>(); // Relacion con entidad/tabla tareas
    }
}
