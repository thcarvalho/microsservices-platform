namespace MSP.Auth.API.Application.Settings;

public class TokenGeneratorSettings
{
    public string SecurityKey { get; set; }
    public int ExpirationMinutes { get; set; }
}