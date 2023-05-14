namespace AlifTech.Interview.Ewallet.Services.Interfaces;

public interface IDigestGenerator
{
    public Task<string> GenerateDigestAsync(string message, string secret);
}