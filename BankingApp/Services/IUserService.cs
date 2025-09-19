using BankingApp.DTOs;
using BankingApp.Models;

namespace BankingApp.Services
{
    public interface IUserService
    {
        IEnumerable<User> GetAll();
        User? GetById(int id);
        User Add(UserDto dto);
        User? Update(int id, UserDto dto);
        bool Delete(int id);
    }
}
