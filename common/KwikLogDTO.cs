namespace common;

public record KwikLogDTO
{
    public DateTime TimestampUTC { get; set; }
    public string? Entry { get; set; }
    public string? OwnerId { get; set; }
}
