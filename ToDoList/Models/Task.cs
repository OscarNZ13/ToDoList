using System.ComponentModel.DataAnnotations;

namespace ToDoList.Models
{
    public class Task
    {
        [Key]
        public int TaskId { get; set; }

        [Required]
        [MaxLength(25)]
        public string TaskTitle { get; set; } = null!;

        [Required]
        [MaxLength(255)]
        public string? TaskDescription { get; set; }

        [Required]
        public DateTime TaskTimeCreate { get; set; }

        [Required]
        public DateTime TaskTimeLimit { get; set; }

        public int UserId { get; set; } // clave foránea hacia User
        public User User { get; set; } = null!; // relación con User

        public int StateId { get; set; } // clave foránea hacia State
        public State State { get; set; } = null!; // relación con State
    }
}
