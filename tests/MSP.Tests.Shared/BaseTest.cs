using Microsoft.Extensions.Configuration;
using Moq;
using Moq.AutoMock;
using MSP.Core.Utils;

namespace MSP.Tests.Shared;


public class BaseTest : IClassFixture<MSPFixture>
{
    protected readonly AutoMocker _autoMocker;
    
    public BaseTest(MSPFixture fixture)
    {
        _autoMocker = fixture.AutoMocker;
        ResetMockCalls();
    }


    public void AddConfiguration(string service) =>
        _autoMocker.Use<IConfiguration>(Configuration.GetConfiguration($"MSP.{service}.Api", "Test"));

    public T GetService<T>() where T : class
        => _autoMocker.CreateInstance<T>();

    public Mock<T> GetMock<T>() where T : class
        => _autoMocker.GetMock<T>();

    private void ResetMockCalls()
    {
        foreach (var resolvedObject in _autoMocker.ResolvedObjects)
            (resolvedObject.Value as Mock)?.Invocations.Clear();
    }
}