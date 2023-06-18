using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper.Contrib.Autofac.DependencyInjection;
using MediatR.Extensions.Autofac.DependencyInjection;
using MediatR.Extensions.Autofac.DependencyInjection.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using TestPlatform.Application.Common.Mapping;
using TestPlatform.Core.Common.Interfaces;
using TestPlatform.Persistence.Context;

namespace TestPlatform.Tests.Common;

internal class TestModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.Register(_ =>
            {
                var mock = new Mock<IConfiguration>()
                    .As<IConfiguration>();

                mock.SetupGet(x => x["Jwt:Key"])
                    .Returns(Guid.NewGuid().ToString());
                mock.SetupGet(x => x["Jwt:Issuer"])
                    .Returns("https://test-platform.com/");
                mock.SetupGet(x => x["Jwt:Audience"])
                    .Returns("https://test-platform.com/");
                
                return mock.Object;
            })
            .As<IConfiguration>();
        
        var services = new ServiceCollection();

        services.AddDbContext<ITestDbContext, TestDbContext>(config =>
            config.UseInMemoryDatabase(Guid.NewGuid().ToString()), ServiceLifetime.Transient);
        
        builder.Populate(services);
    }
}