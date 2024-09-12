using ToDoList.Models;

namespace ToDoList.DAO.Services
{
    public interface IUserService
    {
        User GetUserById(int id);
        Task<User> GetUserByName(string username);
        IEnumerable<User> GetAll();
        public void create(User user);
        public void Update(User user);
        public void Delete(int id);
    }
}
