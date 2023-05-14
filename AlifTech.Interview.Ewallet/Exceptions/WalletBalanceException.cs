namespace AlifTech.Interview.Ewallet.Exceptions;

public class WalletBalanceException : Exception
{
    public WalletBalanceException()
    {
    }

    public WalletBalanceException(string message) : base(message)
    {
    }

    public WalletBalanceException(string message, Exception innerException) : base(message, innerException)
    {
    }
}