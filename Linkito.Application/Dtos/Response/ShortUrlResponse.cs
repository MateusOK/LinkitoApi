namespace Linkito.Application.Dtos.Response;

public record ShortUrlResponse()
{
    public string ShortUrl { get; init; } = string.Empty;
};