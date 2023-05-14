using AlifTech.Interview.Ewallet.Entities;

namespace AlifTech.Interview.Ewallet.Models;

public class ReplenishmentsForMonthResponse
{
    public string WalletId { get; set; }
    public DateTime Month { get; set; }
    public int TotalCount { get; set; }
    public decimal TotalSum { get; set; }
    public List<Replenishment> Replenishments { get; set; }
}