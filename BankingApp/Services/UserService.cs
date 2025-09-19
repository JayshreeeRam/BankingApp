using System.Collections.Generic;
using BankingApp.DTOs;
using BankingApp.Models;
using BankingApp.Repositories;
using BankingApp.Repository;

namespace BankingApp.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repo;

        public UserService(IUserRepository repo)
        {
            _repo = repo;
        }

        public IEnumerable<User> GetAll()
        {
            return _repo.GetAll();
        }

        public User? GetById(int id)
        {
            return _repo.GetById(id);
        }

        public User Add(UserDto dto)
        {
            var user = new User
            {
                Username = dto.Username,
                Password = dto.Password,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                UserRole = dto.UserRole,
               
            };
            return _repo.Add(user);
        }

        public User? Update(int id, UserDto dto)
        {
            var existing = _repo.GetById(id);
            if (existing == null) return null;

            existing.Username = dto.Username;
            existing.Password = dto.Password;
            existing.Email = dto.Email;
            existing.PhoneNumber = dto.PhoneNumber;
            existing.UserRole = dto.UserRole;

            return _repo.Update(id, existing);
        }

        public bool Delete(int id)
        {
            return _repo.Delete(id);
        }
    }
}
