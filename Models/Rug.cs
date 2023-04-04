namespace VintageRugsApi.Models;

public class Rug
{
    public int Id { get; set; }
    
    public string? Name { get; set; }
    
    public string? MadeIn { get; set; }

    public string? ImageUrl { get; set; }
    public string? Description { get; set; }
    
    public decimal? Price { get; set; }

    public bool IsAvailableInMarket => Price != null;

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    
    public virtual Rugmaker? Rugmaker { get; set; }
}