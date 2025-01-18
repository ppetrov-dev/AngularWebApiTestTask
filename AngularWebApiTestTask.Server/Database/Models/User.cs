using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AngularWebApiTestTask.Server.Database.Models;

public class User
{
    public int Id { get; set; }

    [Required]
    [EmailAddress]
    public string Login { get; set; } = string.Empty;

    [Required]
    [MinLength(8)]
    [RegularExpression(@"^(?=.*\d)(?=.*[a-zA-Z]).+$")]
    public string Password { get; set; } = string.Empty;

    [Required]
    public bool AgreeToTerms { get; set; }

    [Required]
    public int ProvinceId { get; set; }

    [ForeignKey(nameof(ProvinceId))]
    public virtual Province? Province { get; set; } 
}