using Rhino.Mocks;

public abstract class Specification : TestBase
{
    protected static T CreateStub<T>() where T : class
    {
        return MockRepository.GenerateStub<T>();
    }
}