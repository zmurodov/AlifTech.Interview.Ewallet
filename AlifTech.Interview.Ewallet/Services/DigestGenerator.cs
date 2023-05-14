using System.Security.Cryptography;
using System.Text;
using AlifTech.Interview.Ewallet.Services.Interfaces;

namespace AlifTech.Interview.Ewallet.Services;

public class DigestGenerator : IDigestGenerator
{
    public Task<string> GenerateDigestAsync(string message, string secret)
    {
        var key = Encoding.UTF8.GetBytes(secret);
        var messageBytes = Encoding.UTF8.GetBytes(message);
        
        using var hmac = new HMACSHA1(key);
        
        var hash = hmac.ComputeHash(messageBytes);
        return Task.FromResult(Convert.ToBase64String(hash));
    }
}