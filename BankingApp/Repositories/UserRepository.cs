using System.Collections.Generic;
using System.Linq;
using BankingApp.Models;
using BankingApp.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BankingApp.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly BankingContext _context;

        public UserRepository(BankingContext context)
        {
            _context = context;
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users
                .Include(u => u.Documents)
                .Include(u => u.Reports)
                .ToList();
        }

        public User? GetById(int id)
        {
            return _context.Users
                //.Include(u => u.Client)
                .Include(u => u.Documents)
                .Include(u => u.Reports)
                .FirstOrDefault(u => u.UserId == id);
        }

        public User Add(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return user;
        }

        public User Update(int id, User user)
        {
            var existing = _context.Users.Find(id);
            if (existing == null) return null!;

            _context.Entry(existing).CurrentValues.SetValues(user);
            _context.SaveChanges();
            return existing;
        }

        public bool Delete(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null) return false;

            _context.Users.Remove(user);
            _context.SaveChanges();
            return true;
        }
    }
}
