using AngularWebApiTestTask.Server.Database;
using Microsoft.EntityFrameworkCore;

namespace AngularWebApiTestTask.Server.Tests.Infrastructure;

public abstract class RepositoryBaseTests: IDisposable
{
    protected RepositoryBaseTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: $"Database-{Guid.NewGuid()}")
            .Options;
        Context = new ApplicationDbContext( options );

        Context.Database.EnsureCreated();
    }

    protected ApplicationDbContext Context { get; }

    public void Dispose()
    {
        Context.Database.EnsureDeleted();
    }
}