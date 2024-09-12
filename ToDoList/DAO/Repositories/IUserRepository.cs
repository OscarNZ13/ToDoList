using ToDoList.Models;

namespace ToDoList.DAO.Repositories
{
    public interface IUserRepository
    {
        User GetUserById(int id);
        Task<User> GetUserByName(string username);
        IEnumerable<User> GetAll();
        public void create(User user);
        public void Update(User user);
        public void Delete(int id);
    }
}
