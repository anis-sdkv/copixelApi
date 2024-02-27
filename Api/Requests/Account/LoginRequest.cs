using System.ComponentModel.DataAnnotations;

namespace CopixelApi.Api.Requests;

public class LoginRequest
{
    public string UserName { get; set; }
        
    [DataType(DataType.Password)]
    public string Password { get; set; }
}