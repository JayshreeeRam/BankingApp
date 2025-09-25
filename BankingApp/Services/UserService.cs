using System.Collections.Generic;
using BankingApp.DTOs;
using BankingApp.Models;
using BankingApp.Repositories;
using BankingApp.Repository;
using Microsoft.AspNetCore.Identity;

namespace BankingApp.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repo;
        private readonly PasswordHasher<User> _passwordHasher;

        public UserService(IUserRepository repo)
        {
            _repo = repo;
            _passwordHasher = new PasswordHasher<User>();
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
            user.Password = _passwordHasher.HashPassword(user, dto.Password);

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

            if (!string.IsNullOrWhiteSpace(dto.Password))
            {
                existing.Password = _passwordHasher.HashPassword(existing, dto.Password);
            }

            return _repo.Update(id, existing);
        }

        //public bool Delete(int id)
        //{
        //    return _repo.Delete(id);
        //}
    }
}
