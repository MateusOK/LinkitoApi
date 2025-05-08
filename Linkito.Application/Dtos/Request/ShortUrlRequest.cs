namespace Linkito.Application.Dtos.Request;

public record ShortUrlRequest()
{
    public string OriginalUrl { get; init; }
    public DateTime? ExpirationDate { get; init; }
};