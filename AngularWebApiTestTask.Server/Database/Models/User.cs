using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AngularWebApiTestTask.Server.Database.Models;

public class User
{
    public int Id { get; set; }

    [Required]
    [EmailAddress]
    public string Login { get; set; } = string.Empty;

    [Required]
    [MinLength(2)]
    [RegularExpression(@"^(?=.*\d)(?=.*[a-zA-Z]).+$")]
    public string Password { get; set; } = string.Empty;

    [Required]
    public int ProvinceId { get; set; }

    [JsonIgnore]
    [ForeignKey(nameof(ProvinceId))]
    public virtual Province? Province { get; set; } 
}