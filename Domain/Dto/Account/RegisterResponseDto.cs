namespace CopixelApi.Domain.Dto.Account;

public class RegisterResponseDto 
{
    public IEnumerable<string>? RegistrationErrors { get; init; }
    public RegisterResponseStatus Status { get; init; }

    public RegisterResponseDto(RegisterResponseStatus status, IEnumerable<string>? errors = null)
    {
        Status = status;
        if (errors != null) RegistrationErrors = errors.ToArray();
    }
}