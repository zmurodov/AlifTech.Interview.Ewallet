using System.ComponentModel.DataAnnotations;

namespace AlifTech.Interview.Ewallet.Models;

public class UpdateBalanceRequest
{
    [Required]
    public string WalletId { get; set; }
    
    [Required]
    public decimal Amount { get; set; }
}