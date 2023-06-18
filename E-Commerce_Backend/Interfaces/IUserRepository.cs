using E_Commerce_Backend.Dto;
using E_Commerce_Backend.Models;

namespace E_Commerce_Backend.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetUsers(int pageSize, int pageNumber);
        Task<User> GetById(int id);
        Task AddUser(UserDto userDto);
        Task UpdateUser(User user);
        Task DeleteUser(int id);
    }
}
