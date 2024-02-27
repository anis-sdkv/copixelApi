namespace CopixelApi.Domain.Dto.Account;

public class LoginResponseDto
{
    public LoginResponseStatus Status { get; init; }
    public LoginResponseDto(LoginResponseStatus status)
    {
        Status = status;
    }
}