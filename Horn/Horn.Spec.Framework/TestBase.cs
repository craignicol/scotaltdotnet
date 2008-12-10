using System;

public abstract class TestBase : IDisposable
{
    protected TestBase()
    {
        Before_each_spec();
        Because();
    }

    protected abstract void Because();
    protected virtual void Before_each_spec() { }
    protected virtual void After_each_spec() { }

    public void Dispose()
    {
        After_each_spec();
    }
}