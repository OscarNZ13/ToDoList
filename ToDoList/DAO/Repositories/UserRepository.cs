using Microsoft.EntityFrameworkCore;
using ToDoList.Models;

namespace ToDoList.DAO.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ToDoListContext _toDoListContext;

        public UserRepository(ToDoListContext toDoListContext)
        {
            _toDoListContext = toDoListContext;
        }

        //------------------------------------------------------ Metodo para traer todos los usuarios ------------------------------------------------------
        public IEnumerable<User> GetAll()
        {
            return _toDoListContext.Users.ToList();
        }

        //------------------------------------------------------ Metodo para traer usuarios por id ------------------------------------------------------
        public User GetUserById(int id)
        {
            return _toDoListContext.Users.Find(id);
        }

        //------------------------------------------------------ Metodo para traer usuarios por username ------------------------------------------------------
        public async Task<User> GetUserByName(string UserName)
        {
            var SerchUser = await _toDoListContext.Users.FirstOrDefaultAsync(x => x.UserName == UserName);
            return SerchUser;
        }

        //------------------------------------------------------ Metodo para crear usuarios ------------------------------------------------------
        public void create(User user)
        {
            _toDoListContext.Add(user);
            _toDoListContext.SaveChanges();
        }

        //------------------------------------------------------ Metodo para actualizar usuarios ------------------------------------------------------
        public void Update(User user)
        {
            _toDoListContext.Update(user);
            _toDoListContext.SaveChanges();
        }

        //------------------------------------------------------ Metodo para eliminar usuarios por id ------------------------------------------------------
        public void Delete(int id)
        {
            _toDoListContext.Remove(id);
            _toDoListContext.SaveChanges();
        }
    }
}
