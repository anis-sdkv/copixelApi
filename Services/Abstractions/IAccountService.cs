using System.Security.Claims;
using CopixelApi.Domain.Dto.Account;
using CopixelApi.Domain.Models;

namespace CopixelApi.Services.Abstractions;

public interface IAccountService
{
    public Task<LoginResponseDto> LoginAsync(LoginRequestDto loginRequestDto);
    public Task<RegisterResponseDto> RegisterAsync(RegisterRequestDto registerRequestDto);
    public Task<User?> GetUser(ClaimsPrincipal principal);
}