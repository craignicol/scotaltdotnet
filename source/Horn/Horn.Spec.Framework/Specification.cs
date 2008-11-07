using System;
using Rhino.Mocks;

public abstract class Specification : IDisposable
{
    protected Specification()
    {
        Before_each_spec();
        Because();
    }

    protected abstract void Because();

    protected virtual void Before_each_spec() { }

    protected virtual void After_each_spec() { }

    protected static T CreateStub<T>() where T : class
    {
        return MockRepository.GenerateStub<T>();
    }


    public void Dispose()
    {
        After_each_spec();
    }
}