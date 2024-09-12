using ToDoList.DAO.Repositories;
using ToDoList.Models;

namespace ToDoList.DAO.Services
{
    public class UserService : IUserService
    {

        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void create(User user) => _userRepository.create(user);

        public void Delete(int id) => _userRepository.Delete(id);

        public IEnumerable<User> GetAll() => _userRepository.GetAll();

        public User GetUserById(int id) => _userRepository.GetUserById(id);

        public Task<User> GetUserByName(string username) => _userRepository.GetUserByName(username);

        public void Update(User user) => _userRepository.Update(user);
    }
}
