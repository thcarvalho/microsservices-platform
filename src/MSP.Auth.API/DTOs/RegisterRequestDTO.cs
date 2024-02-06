namespace MSP.Auth.API.DTOs;

public record RegisterRequestDTO
{
    public string Name { get; set; }
    public string DocumentNumber { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}