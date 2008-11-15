
public static class IoC
{
    private static IDependencyResolver dependencyResolver;

    public static void InitializeWith(IDependencyResolver resolver)
    {
        dependencyResolver = resolver;
    }

    public static T Resolve<T>()
    {
        return dependencyResolver.Resolve<T>();
    }
}

public interface IDependencyResolver
{
    T Resolve<T>();
}