using Condorcet.B2.AspnetCore.MVC.Application.Core.Domain;

namespace Condorcet.B2.AspnetCore.MVC.Application.Core.Repository;

public interface IUserRepository
{
    public Task<int> CreateUserAsync(User user);
    public Task<bool> UsernameExist(string username);
    public Task<User?> GetByUsernameAsync(string username);
    
    public Task<List<User>> GetAll();
}