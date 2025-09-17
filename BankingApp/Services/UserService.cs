using BankingApp.Models;
using BankingApp.Repositories;

namespace BankingApp.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public IEnumerable<User> GetAllUsers()
        {
            return _userRepository.GetAll();
        }
        public User GetUserById(int id)
        {
            return _userRepository.GetById(id);
        }
        public User CreateUser(User user)
        {
            return _userRepository.Add(user);
        }
        public User UpdateUser(int id, User user)
        {
            return _userRepository.Update(id, user);
        }

        public bool DeleteUser(int id)
        {
            return _userRepository.Delete(id);
        }
    }
}
