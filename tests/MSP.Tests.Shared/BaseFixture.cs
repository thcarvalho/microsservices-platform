using Moq.AutoMock;

namespace MSP.Tests.Shared;


[CollectionDefinition(nameof(MSPFixtureCollection))]
public class MSPFixtureCollection : ICollectionFixture<MSPFixture>
{
}

public class MSPFixture : ICollectionFixture<MSPFixture>, IDisposable
{
    public AutoMocker AutoMocker { get; private set; }


    public MSPFixture()
    {
        AutoMocker = new AutoMocker();
    }

    public void Dispose()
    {
        AutoMocker.AsDisposable().Dispose();
    }

}