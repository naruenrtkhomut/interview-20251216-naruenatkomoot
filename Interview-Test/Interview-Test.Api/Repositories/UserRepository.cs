using Interview_Test.Infrastructure;
using Interview_Test.Models;
using Interview_Test.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Interview_Test.Repositories;

public class UserRepository : IUserRepository
{
    private readonly InterviewTestDbContext _interviewTestDbContext;
    public UserRepository(InterviewTestDbContext interviewTestDbContext)
    {
        _interviewTestDbContext = interviewTestDbContext;
    }
    public async Task<dynamic> GetUserById(string id)
    {
        return await _interviewTestDbContext.UserTb
                .AsNoTracking()
                .Include(u => u.UserProfile)
                .Include(u => u.UserRoleMappings)
                    .ThenInclude(urm => urm.Role)
                    .ThenInclude(urm => urm.Permissions)
                .FirstOrDefaultAsync(dbUser => dbUser.Id.ToString() == id) ?? new UserModel();
    }

    public async Task<int> CreateUser(UserModel user)
    {
        if (await _interviewTestDbContext.UserTb.CountAsync(dbUser => dbUser.Id == user.Id) > 0)
        {
            return 100;
        }
        else
        {
            await _interviewTestDbContext.UserTb.AddAsync(user);
            return await _interviewTestDbContext.SaveChangesAsync();
        }
    }

    public async Task<dynamic> GetUsers()
    {
        return await _interviewTestDbContext.UserTb
                .AsNoTracking()
                .Include(u => u.UserProfile)
                .Include(u => u.UserRoleMappings)
                    .ThenInclude(urm => urm.Role)
                    .ThenInclude(urm => urm.Permissions)
                 .ToListAsync();
    }
}