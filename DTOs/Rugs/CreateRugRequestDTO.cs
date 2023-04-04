namespace VintageRugsApi.DTOs.Rugs;

public class CreateRugRequestDTO
{
    public string? Name { get; set; }
    
    public string? MadeIn { get; set; }

    public string? ImageUrl { get; set; }
    public string? Description { get; set; }
    
    public decimal? Price { get; set; }

    public int? RugmakerId { get; set; }
}