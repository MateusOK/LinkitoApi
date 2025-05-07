using Linkito.Domain.Common;

namespace Linkito.Domain.Entities;

public class ShortUrl : BaseEntity
{
    public string OriginalUrl { get; set; } = string.Empty;
    public string ShortCode { get; set; } = string.Empty;
    public DateTime? ExpirationDate { get; set; }
}