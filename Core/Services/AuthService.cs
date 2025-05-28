using System.Security.Claims;
using Condorcet.B2.AspnetCore.MVC.Application.Core.Domain;
using Condorcet.B2.AspnetCore.MVC.Application.Core.Repository;
using Condorcet.B2.AspnetCore.MVC.Application.Models;
using Condorcet.B2.ProjectManagement.WebApplication.Core.Domain;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Condorcet.B2.AspnetCore.MVC.Application.Core.Services;

public class AuthService : IAuthService
{
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUserRepository _userRepository;

    public AuthService(IPasswordHasher passwordHasher, IUserRepository userRepository)
    {
        _passwordHasher = passwordHasher;
        _userRepository = userRepository;
    }

    public async Task<bool> RegisterUserAsync(RegisterUserViewModel registerDto)
    {
        var existingUser = await _userRepository.UsernameExist(registerDto.Username);

        if (existingUser)
            return false;

        var (hash, salt) = _passwordHasher.Hash(registerDto.Password);

        var user = new User
        {
            Username = registerDto.Username,
            Email = registerDto.Email,
            PasswordHash = hash,
            Salt = salt,
            FirstName = registerDto.FirstName,
            LastName = registerDto.LastName,
            IsActive = true,
            Role = UserRoles.User
        };

        await _userRepository.CreateUserAsync(user);

        return true;
    }

    public async Task<User?> AuthenticateAsync(string username, string password)
    {
        var user = await _userRepository.GetByUsernameAsync(username);
        if (user == null || !user.IsActive)
            return null;

        bool verified = _passwordHasher.VerifyPassword(password, user.PasswordHash, user.Salt);
        if (!verified)
            return null;

        return user;
    }

    public ClaimsPrincipal CreateClaimsPrincipalAsync(User user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role)
        };

        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        return new ClaimsPrincipal(identity);
    }
}