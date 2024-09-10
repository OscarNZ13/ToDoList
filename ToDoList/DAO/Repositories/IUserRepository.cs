using ToDoList.Models;

namespace ToDoList.DAO.Repositories
{
    public interface IUserRepository
    {
        User GetById(int id);
        IEnumerable<User> GetAll();
        public Boolean Update(User user);
        public Boolean Delete(int id);
    }
}
