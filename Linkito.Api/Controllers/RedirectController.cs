using Linkito.Domain.Entities;
using Linkito.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Linkito.Api.Controllers;

[ApiController]
[Route("{shortCode}")]
public class RedirectController : ControllerBase
{
    private readonly IGenericRepository<ShortUrl> _repository;

    public RedirectController(IGenericRepository<ShortUrl> repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<IActionResult> RedirectToOriginalUrl(string shortCode)
    {
        var shortUrl = await _repository.GetFirstOrDefaultAsync(s => s.ShortCode == shortCode);

        if (shortUrl == null || (shortUrl.ExpirationDate != null && shortUrl.ExpirationDate < DateTime.Now))
        {
            return NotFound();
        }

        return Redirect(shortUrl.OriginalUrl);

    }
    
    
}