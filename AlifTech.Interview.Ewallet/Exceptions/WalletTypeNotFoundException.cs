namespace AlifTech.Interview.Ewallet.Exceptions;

public class WalletTypeNotFoundException : Exception
{
    public WalletTypeNotFoundException()
    {
    }

    public WalletTypeNotFoundException(string message) : base(message)
    {
    }

    public WalletTypeNotFoundException(string message, Exception innerException) : base(message, innerException)
    {
    }
}