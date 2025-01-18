using AngularWebApiTestTask.Server.Database.Models;

namespace AngularWebApiTestTask.Server.Tests.Database.Models;

public class CountryBuilder
{
    public List<Province> _provinces = new();

    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public IEnumerable<Province> Provinces => _provinces;

    public Country Build() => new()
    {
        Id = Id,
        Name = Name,
        Provinces = _provinces
    };

    public CountryBuilder AddProvinces(params Province[] provinces)
    {
        _provinces.AddRange(provinces);
        return this;
    }

    public static Country Any() => new CountryBuilder().Build();
}