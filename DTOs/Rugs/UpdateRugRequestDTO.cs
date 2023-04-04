namespace VintageRugsApi.DTOs.Rugs;

public class UpdateRugRequestDTO
{
    public int Id { get; set; }
    public string? Name { get; set; }
    
    public string? MadeIn { get; set; }

    public string? ImageUrl { get; set; }
    public string? Description { get; set; }
    
    public decimal? Price { get; set; }
}