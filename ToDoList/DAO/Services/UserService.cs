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
        public bool Delete(int id) => _userRepository.Delete(id);

        public IEnumerable<User> GetAll() => _userRepository.GetAll();  

        public User GetById(int id) => _userRepository.GetById(id);

        public bool Update(User user) => _userRepository.Update(user);
    }
}
