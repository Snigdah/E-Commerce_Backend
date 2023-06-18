using E_Commerce_Backend.Data;
using E_Commerce_Backend.Dto;
using E_Commerce_Backend.Interfaces;
using E_Commerce_Backend.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace E_Commerce_Backend.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }

        //Get All Users List
        public async Task<IEnumerable<User>> GetUsers(int pageSize, int pageNumber)
        {
            var users = await _context.Users
             .Skip((pageNumber - 1) * pageSize)
             .Take(pageSize)
             .ToListAsync();

            return users;
        }

        //Get Product By ID
        public async Task<User> GetById(int id)
        {
            var user = await _context.Users
                .Include(i => i.Cart)
                .FirstOrDefaultAsync(i => i.UserId == id);

            return user;
        }

        //Add User
        public async Task AddUser(UserDto userDto)
        {
            CreatePasswordHash(userDto.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var user = new User();
            user.UserId = userDto.UserId;
            user.UserName = userDto.UserName;
            user.Email = userDto.Email;
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            user.Orders = null;
            user.Cart = null;

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        //Update User
        public async Task UpdateUser(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        //Delete User
        public async Task DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if(user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }

        //Create Password hash
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}
