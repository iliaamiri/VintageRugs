namespace VintageRugsApi.DTOs.Rugs;

public class UpdateRugmakerRequestDTO
{
    public int Id { get; set; }
    public string? FullName { get; set; }
    
    public string? Country { get; set; }
}