using System.ComponentModel.DataAnnotations;

namespace AlifTech.Interview.Ewallet.Models;

public class WalletExistsRequest
{
    [Required]
    public string WalletId { get; set; }
}