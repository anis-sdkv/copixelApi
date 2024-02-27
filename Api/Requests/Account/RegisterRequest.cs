using System.ComponentModel.DataAnnotations;

namespace CopixelApi.Api.Requests.Account;

public class RegisterRequest
{
    public string UserName { get; set; }
        
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }
        
    [DataType(DataType.Password)]
    public string Password { get; set; }
}