namespace AlifTech.Interview.Ewallet.Entities;

public class Replenishment
{
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public string WalletId { get; set; }
    public string UserId { get; set; }

    public Wallet Wallet { get; set; }
}