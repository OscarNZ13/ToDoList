using System.ComponentModel.DataAnnotations;

namespace ToDoList.Models
{
    public class State
    {
        [Key]
        public int StateId { get; set; }

        [Required]
        public string? StateName { get; set; }

        public ICollection <User> UsersList { get; set; } = new List<User> (); // Relacion con entidad/tabla
        public ICollection<Task> TasksList { get; set; } = new List<Task>(); // Relacion con entidad/tabla tareas
    }
}
