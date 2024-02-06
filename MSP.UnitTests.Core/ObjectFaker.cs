using Bogus;

namespace MSP.UnitTests.Core;

public class ObjectFaker<T> : Faker<T> where T : class
{
    public ObjectFaker<T> UsePrivateConstructor()
        => base.CustomInstantiator(f => Activator.CreateInstance(typeof(T), nonPublic: true) as T) as ObjectFaker<T>;
}