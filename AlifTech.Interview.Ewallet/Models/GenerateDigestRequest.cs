using System.ComponentModel.DataAnnotations;

namespace AlifTech.Interview.Ewallet.Models;

public class GenerateDigestRequest
{
    [Required] public string UserId { get; set; }
    [Required] public string Body { get; set; }
}