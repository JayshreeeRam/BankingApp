using System.Collections.Generic;
using System.Linq;
using BankingApp.Models;
using BankingApp.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BankingApp.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly BankingContext _repo;

        public UserRepository(BankingContext context)
        {
            _repo = context;
        }

        public IEnumerable<User> GetAll()
        {
            return _repo.Users
                .Include(u => u.Documents)
                .Include(u => u.Reports)
                .ToList();
        }

        public User? GetById(int id)
        {
            return _repo.Users
                //.Include(u => u.Client)
                .Include(u => u.Documents)
                .Include(u => u.Reports)
                .FirstOrDefault(u => u.UserId == id);
        }

        public User Add(User user)
        {
            _repo.Users.Add(user);
            _repo.SaveChanges();
            return user;
        }

        public User Update(int id, User user)
        {
            var existing = _repo.Users.Find(id);
            if (existing == null) return null!;

            _repo.Entry(existing).CurrentValues.SetValues(user);
            _repo.SaveChanges();
            return existing;
        }

        //public bool Delete(int id)
        //{
        //    var user = _repo.Users.Find(id);
        //    if (user == null) return false;

        //    _repo.Users.Remove(user);
        //    _repo.SaveChanges();
        //    return true;
        //}
    }
}
