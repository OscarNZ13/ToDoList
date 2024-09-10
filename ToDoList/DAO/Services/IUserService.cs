using ToDoList.Models;

namespace ToDoList.DAO.Services
{
    public interface IUserService
    {
        User GetById(int id);
        IEnumerable<User> GetAll();
        public Boolean Update(User user);
        public Boolean Delete(int id);
    }
}
