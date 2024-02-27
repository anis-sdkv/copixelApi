using CopixelApi.Api.Requests;
using CopixelApi.Api.Requests.Account;
using CopixelApi.Data.Repositories;
using CopixelApi.Domain.Dto.Account;
using CopixelApi.Domain.Models;
using CopixelApi.Services.Abstractions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CopixelApi.Api.Controllers;

[Route("accounts")]
[ApiController]
public class AccountsController : Controller
{
    private readonly IAccountService _accountService;

    public AccountsController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        var result = await _accountService.RegisterAsync(new RegisterRequestDto()
        {
            UserName = request.UserName,
            Email = request.Email,
            Password = request.Password
        });

        if (result.Status == RegisterResponseStatus.Success)
            return Ok("Register successful");
        return BadRequest(result.RegistrationErrors);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var result = await _accountService.LoginAsync(new LoginRequestDto()
        {
            UserName = request.UserName,
            Password = request.Password
        });

        if (result.Status == LoginResponseStatus.Success)
            return Ok("Login successful");
        return BadRequest("Login failed");
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync();
        return Ok("Logout Successful.");
    }

    [HttpGet]
    public IEnumerable<User> GetApp() => UserRepository.UsersDb.ToArray();

    [Authorize]
    [HttpGet("my")]
    public async Task<User?> GetMe()
    {
        return await _accountService.GetUser(HttpContext.User);
    }
}