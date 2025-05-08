using Linkito.Application.Dtos.Response;
using Linkito.Application.Services.Interfaces;
using Linkito.Domain.Entities;
using Linkito.Domain.Repositories;
using Microsoft.Extensions.Configuration;

namespace Linkito.Application.Services;

public class ShortUrlService : IShortUrlService
{

    private readonly IGenericRepository<ShortUrl> _shortUrlRepository;
    private readonly IConfiguration _configuration;

    public ShortUrlService(IGenericRepository<ShortUrl> shortUrlRepository, IConfiguration configuration)
    {
        _shortUrlRepository = shortUrlRepository;
        _configuration = configuration;
    }


    public async Task<ShortUrlResponse> CreateShortUrlAsync(string originalUrl, DateTime? expirationDate)
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
        
        var baseUrl = _configuration["AppSettings:BaseUrl"];
        return new ShortUrlResponse { ShortUrl = $"{baseUrl}{shortCode}" };

    }

    private static string GenerateShortCode()
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        var random = new Random();
        return new string(Enumerable.Repeat(chars, 6)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }

}