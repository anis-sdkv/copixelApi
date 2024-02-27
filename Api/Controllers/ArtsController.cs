using CopixelApi.Api.Requests.Art;
using CopixelApi.Domain.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CopixelApi.Api.Controllers;

[Route("arts")]
[ApiController]
public class ArtsController : Controller
{
    private readonly IArtRepository _artRepository;

    public ArtsController(IArtRepository artRepository)
    {
        _artRepository = artRepository;
    }

    [HttpPost("create")]
    public async Task<IActionResult> Upload(IFormFile image)
    {
        Console.Write(image);
        return Ok();
    }
}