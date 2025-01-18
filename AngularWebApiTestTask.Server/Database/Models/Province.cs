using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AngularWebApiTestTask.Server.Database.Models;

public class Province
{
    [Required]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public int CountryId { get; set; }

    [ForeignKey(nameof(CountryId))]
    [JsonIgnore]
    public virtual Country? Country { get; set; } 
}
