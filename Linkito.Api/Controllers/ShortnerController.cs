using Linkito.Application.Dtos.Request;
using Linkito.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Linkito.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ShortnerController : ControllerBase
{
    private readonly IShortUrlService _shortUrlService;
    
    public ShortnerController(IShortUrlService shortUrlService)
    {
        _shortUrlService = shortUrlService;
    }

    [HttpPost]
    public async Task<IActionResult> ShortenUrl([FromBody] ShortUrlRequest request)
    {
        var result = await _shortUrlService.CreateShortUrlAsync(request.OriginalUrl, request.ExpirationDate);
        return Ok(result);
    }
    
    
    
}