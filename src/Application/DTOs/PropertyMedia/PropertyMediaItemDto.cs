namespace Application.DTOs.PropertyMedia;

public class PropertyMediaItemDto
{
    public int Id { get; set; }
    public string ObjectKey { get; set; } = null!;
    public string? MediaType { get; set; }
    public int Order { get; set; }
}
