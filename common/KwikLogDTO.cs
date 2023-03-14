namespace common;

public record KwikLogDTO
{
    public DateTimeOffset TicksUTC { get; set; }
    public string Entry { get; set; }
}
