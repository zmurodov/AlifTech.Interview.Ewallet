namespace AlifTech.Interview.Ewallet.Entities;

public class Wallet
{
    public Wallet()
    {
        Replenishments = new HashSet<Replenishment>();
    }

    public string Id { get; set; }
    public int WalletTypeId { get; set; }
    public decimal Balance { get; set; }
    public string UserId { get; set; }
    public User User { get; set; }

    public ICollection<Replenishment> Replenishments { get; set; }
}