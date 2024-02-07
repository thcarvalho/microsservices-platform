namespace MSP.Clients.API.DTOs;

public record ClientResponseDTO
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string DocumentNumber { get; set; }
}