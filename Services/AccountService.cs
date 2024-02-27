using System.Security.Claims;
using CopixelApi.Domain.Dto.Account;
using CopixelApi.Domain.Models;
using CopixelApi.Services.Abstractions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;

namespace CopixelApi.Services;

public class AccountService : IAccountService
{
    private readonly UserManager<User> _userManager;
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly ILogger<AccountService> _logger;

    public AccountService(ILogger<AccountService> logger, UserManager<User> userManager, IHttpContextAccessor contextAccessor)
    {
        _logger = logger;
        _userManager = userManager;
        _contextAccessor = contextAccessor;
    }

    public async Task<LoginResponseDto> LoginAsync( LoginRequestDto loginRequestDto)
    {
        var signedUser = await _userManager.FindByNameAsync(loginRequestDto.UserName);

        if (signedUser == null || !await _userManager.CheckPasswordAsync(signedUser, loginRequestDto.Password))
            return new LoginResponseDto(LoginResponseStatus.Fail);

        var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
        identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, signedUser.Id));
        identity.AddClaim(new Claim(ClaimTypes.Name, signedUser.UserName));

        if (_contextAccessor.HttpContext == null) throw new NullReferenceException();
        await _contextAccessor.HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(identity)
        );

        _logger.LogInformation("User logged in.");
        return new LoginResponseDto(LoginResponseStatus.Success);
    }

    public async Task<RegisterResponseDto> RegisterAsync(RegisterRequestDto registerRequestDto)
    {
        var user = new User
        {
            Id = Guid.NewGuid().ToString(),
            UserName = registerRequestDto.UserName,
            Email = registerRequestDto.Email,
        };

        var result = await _userManager.CreateAsync(user, registerRequestDto.Password);

        if (!result.Succeeded)
            return new RegisterResponseDto(RegisterResponseStatus.Fail, result.Errors.Select(x => x.Description));

        _logger.LogInformation("User created a new account with password.");
        return new RegisterResponseDto(RegisterResponseStatus.Success);
    }

    public Task<User> GetUser(ClaimsPrincipal principal)
    {
      var id =  _userManager.GetUserId(principal);
      return _userManager.FindByIdAsync(id);
    }
}