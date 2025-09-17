using BankingApp.Models;

namespace BankingApp.Repositories
{
    public class UserRepository:IUserRepository
    {
        private readonly BankingContext _context;

        public UserRepository(BankingContext context)
        {
            _context = context;
        }

        User IUserRepository.Add(User User)
        {
                _context.Users.Add(User);
            _context.SaveChanges();
            return User;
        }

        void IUserRepository.Delete(int id)
        {
            var user = _context.Users.Find(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
        }
        IEnumerable<User> IUserRepository.GetAll()
        {
            return _context.Users.ToList();
        }
        User IUserRepository.GetById(int id)
        {
            return _context.Users.Find(id);
        }
        User IUserRepository.Update(User User)
        {
            var existingUser = _context.Users.Find(User.UserId);
            if (existingUser != null)
            {
                existingUser.Username = User.Username;
                existingUser.Password = User.Password;
                existingUser.Email = User.Email;
                existingUser.PhoneNumber = User.PhoneNumber;
                _context.SaveChanges();
            }
            return existingUser;
        }

    }
}
