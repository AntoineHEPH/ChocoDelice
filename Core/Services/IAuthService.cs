using System.Security.Claims;
using Condorcet.B2.AspnetCore.MVC.Application.Core.Domain;
using Condorcet.B2.AspnetCore.MVC.Application.Models;

namespace Condorcet.B2.AspnetCore.MVC.Application.Core.Services;

public interface IAuthService
{
    Task<bool> RegisterUserAsync(RegisterUserViewModel model);
    Task<User?> AuthenticateAsync(string username, string password);
    ClaimsPrincipal CreateClaimsPrincipalAsync(User user);
}