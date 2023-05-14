namespace AlifTech.Interview.Ewallet.Exceptions;

public class WalletNotFoundException : Exception
{
    public WalletNotFoundException()
    {
    }

    public WalletNotFoundException(string? message) : base(message)
    {
    }

    public WalletNotFoundException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}