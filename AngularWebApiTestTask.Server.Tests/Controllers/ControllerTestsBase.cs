namespace AngularWebApiTestTask.Server.Tests.Controllers;

public abstract class ControllerTestsBase: IDisposable
{
    protected CancellationTokenSource CancellationTokenSource { get; } = new();

    public void Dispose()
    {
        CancellationTokenSource.Cancel();
        CancellationTokenSource.Dispose();
    }
}