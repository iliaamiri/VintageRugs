namespace VintageRugsApi.Models;

public class Rugmaker
{
    public int Id { get; set; }
    
    public string? FullName { get; set; }
    
    public string? Country { get; set; }
    
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    
    public virtual ICollection<Rug> Rugs { get; set; } = new List<Rug>();
}