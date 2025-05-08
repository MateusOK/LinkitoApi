using Linkito.Application.Services.Interfaces;
using Linkito.Domain.Entities;
using Linkito.Domain.Repositories;

namespace Linkito.Application.Services;

public class ShortUrlService : IShortUrlService
{

    private readonly IGenericRepository<ShortUrl> _shortUrlRepository;

    public ShortUrlService(IGenericRepository<ShortUrl> shortUrlRepository)
    {
        _shortUrlRepository = shortUrlRepository;
    }


    public async Task CreateShortUrl(string originalUrl, DateTime? expirationDate)
    {
        var shortCode = GenerateShortCode();

        var existing = await _shortUrlRepository.GetAllAsync();
        while (existing.Any(x => x.ShortCode == shortCode))
        {
            shortCode = GenerateShortCode();
        }

        var shortUrl = new ShortUrl
        {
            Id = Guid.NewGuid(),
            OriginalUrl = originalUrl,
            ShortCode = shortCode,
            ExpirationDate = expirationDate
        };

        await _shortUrlRepository.AddAsync(shortUrl);

    }

    private static string GenerateShortCode()
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        var random = new Random();
        return new string(Enumerable.Repeat(chars, 6)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }

}