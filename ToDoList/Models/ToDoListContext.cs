using Microsoft.EntityFrameworkCore;

namespace ToDoList.Models
{
    public class ToDoListContext : DbContext
    {
        public ToDoListContext(DbContextOptions<ToDoListContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<Task> Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuración de los ID para que la base de datos los genere automáticamente
            modelBuilder.Entity<User>()
                .Property(i => i.UserId)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Task>()
                .Property(i => i.TaskId)
                .ValueGeneratedOnAdd();

            // Ingresando datos iniciales a las tablas de Roles y States
            modelBuilder.Entity<Role>()
                .HasData(
                    new Role { RoleId = 1, RoleName = "Admin" },
                    new Role { RoleId = 2, RoleName = "User" }
                );

            modelBuilder.Entity<State>()
                .HasData(
                    new State { StateId = 1, StateName = "Active" },
                    new State { StateId = 2, StateName = "Inactive" }
                );

            // Relaciones entre entidades
            modelBuilder.Entity<User>()
                .HasOne(r => r.Roles)           // Un usuario tiene solo un rol
                .WithMany(r => r.UsersList)     // Un rol puede tener muchos usuarios
                .HasForeignKey(r => r.RoleId);  // Definimos la FK hacia Role

            modelBuilder.Entity<User>()
                .HasOne(r => r.States)          // Un usuario tiene solo un estado
                .WithMany(r => r.UsersList)     // Un estado puede tener muchos usuarios
                .HasForeignKey(r => r.StateId); // Definimos la FK hacia State

            modelBuilder.Entity<Task>()
                .HasOne(s => s.States)          // Una tarea tiene solo un estado
                .WithMany(s => s.TasksList)     // Un estado puede tener muchas tareas
                .HasForeignKey(r => r.StateId)
                .OnDelete(DeleteBehavior.Restrict); // Cambiado a Restrict para evitar cascada

            modelBuilder.Entity<Task>()
                .HasOne(t => t.Users)           // Una tarea tiene un usuario
                .WithMany(u => u.TasksList)     // Un usuario puede tener muchas tareas
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Restrict); // Cambiado a Restrict para evitar cascada

            base.OnModelCreating(modelBuilder);
        }
    }
}
