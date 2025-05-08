namespace Linkito.Application.Services.Interfaces;

public interface IShortUrlService
{
    Task CreateShortUrl(string originalUrl, DateTime? expirationDate);
}