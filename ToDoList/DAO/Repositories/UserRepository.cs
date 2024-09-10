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

        public IEnumerable<User> GetAll()
        {
            return _toDoListContext.Users.ToList();
        }

        public User GetById(int id)
        {
            return _toDoListContext.Users.Find(id);
        }

        public Boolean Update(User user)
        {
            User FindUser = _toDoListContext.Users.Find(user.UserId);

            if (FindUser != null)
            {
                _toDoListContext.Update(user);
                _toDoListContext.SaveChanges();
                return true;
            }
            return false;
        }

        public Boolean Delete(int id)
        {
            User FindUser = _toDoListContext.Users.Find(id);

            if (FindUser != null) 
            {
                _toDoListContext.Remove(id);
                _toDoListContext.SaveChanges();

                return true;
            }

            return false;
        }
    }
}
