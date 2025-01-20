using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AngularWebApiTestTask.Server.Database.Models;

public class Country
{
    [Required]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; } = string.Empty;

    [JsonIgnore]
    public virtual ICollection<Province> Provinces { get; set; } = new List<Province>();
}