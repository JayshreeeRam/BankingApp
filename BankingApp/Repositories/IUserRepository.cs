using BankingApp.Models;

namespace BankingApp.Repositories
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAll();
        User GetById(int id);
        User Add(User User);
        User Update(User User);
        void Delete(int id);



    }
}
