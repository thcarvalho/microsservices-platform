namespace MSP.Auth.API.ViewModels;

public class RegisterRequestDTO
{
    public string Name { get; set; }
    public string DocumentNumber { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}