using System.Text;

namespace MSP.Tests.Shared;

public class RandomDocumentNumberGenerator
{
    public static string Generate(int? length = 11)
    {
        var chars = "123456789";
        var stringChars = new StringBuilder();
        var random = new Random();

        for (int i = 0; i < length; i++)
        {
            stringChars.Append(chars[random.Next(chars.Length)]);
        }

        return stringChars.ToString();
    }
}