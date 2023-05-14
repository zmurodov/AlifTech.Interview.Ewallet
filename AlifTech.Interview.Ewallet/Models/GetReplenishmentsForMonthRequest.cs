using System.ComponentModel.DataAnnotations;

namespace AlifTech.Interview.Ewallet.Models;

public class GetReplenishmentsForMonthRequest
{
    [Required]
    public string WalletId { get; set; }
    
    [Required]
    public DateTime Month { get; set; }
}