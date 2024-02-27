namespace CopixelApi.Api.Requests.Art;

public record CreateRequest(
    string UserId,
    IFormFile Image
);