using Linkito.Application.Dtos.Response;

namespace Linkito.Application.Services.Interfaces;

public interface IShortUrlService
{
    Task<ShortUrlResponse> CreateShortUrlAsync(string originalUrl, DateTime? expirationDate);
}