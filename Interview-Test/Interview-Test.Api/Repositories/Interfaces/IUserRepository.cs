using Interview_Test.Models;

namespace Interview_Test.Repositories.Interfaces;

public interface IUserRepository
{
    Task<dynamic> GetUserById(string id);
    Task<int> CreateUser(UserModel user);
    Task<dynamic> GetUsers();
}