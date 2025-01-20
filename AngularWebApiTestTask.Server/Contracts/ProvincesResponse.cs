using AngularWebApiTestTask.Server.Database.Models;

namespace AngularWebApiTestTask.Server.Contracts;

public record ProvincesResponse(IEnumerable<Province> Provinces);