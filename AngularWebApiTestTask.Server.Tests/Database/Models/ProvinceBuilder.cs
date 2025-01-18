using AngularWebApiTestTask.Server.Database.Models;
using System.ComponentModel.DataAnnotations;

namespace AngularWebApiTestTask.Server.Tests.Database.Models;

public class ProvinceBuilder
{
    public int Id { get; set; }

    public string Name { get; set; }

    public int CountryId { get; set; }

    public Province Build() => new()
    {
        Id = Id,
        Name = Name,
        CountryId = CountryId
    };

    public static Province Any() => new ProvinceBuilder().Build();
}