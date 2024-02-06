namespace MSP.Auth.API.DTOs;

public record RegisterResponseDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string DocumentNumber { get; set; }
    public string Email { get; set; }
}