using System.ComponentModel.DataAnnotations;

namespace AlifTech.Interview.Ewallet.Models;

public class GetBalanceRequest
{
    [Required]
    public string WalletId { get; set; }
}