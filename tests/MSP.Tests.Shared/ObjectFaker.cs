using Bogus;

namespace MSP.Tests.Shared;

public class ObjectFaker<T> : Faker<T> where T : class
{
    public ObjectFaker<T> UsePrivateConstructor()
        => base.CustomInstantiator(f => Activator.CreateInstance(typeof(T), nonPublic: true) as T) as ObjectFaker<T>;
}