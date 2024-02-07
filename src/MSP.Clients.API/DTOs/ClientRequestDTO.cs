namespace MSP.Clients.API.DTOs;

public record ClientRequestDTO
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string DocumentNumber { get; set; }
}