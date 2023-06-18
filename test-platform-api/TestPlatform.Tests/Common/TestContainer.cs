using Autofac;
using TestPlatform.Application.Modules;

namespace TestPlatform.Tests.Common;

internal static class TestContainer
{
    private static readonly IContainer Container;

    static TestContainer()
    {
        var builder = new ContainerBuilder();
        
        builder.RegisterModule<TestModule>();
        builder.RegisterModule<ApplicationModule>();
        
        Container = builder.Build();
    }

    public static T Resolve<T>() where T : notnull => Container.Resolve<T>();
}