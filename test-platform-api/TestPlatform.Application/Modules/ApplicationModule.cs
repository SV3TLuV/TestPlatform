using System.Reflection;
using Autofac;
using AutoMapper.Contrib.Autofac.DependencyInjection;
using MediatR.Extensions.Autofac.DependencyInjection;
using MediatR.Extensions.Autofac.DependencyInjection.Builder;
using TestPlatform.Application.Common.Interfaces;
using TestPlatform.Application.Common.Mapping;
using TestPlatform.Application.Services;
using Module = Autofac.Module;

namespace TestPlatform.Application.Modules;

public class ApplicationModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<PasswordHasherService>()
            .As<IPasswordHasher>()
            .AsSelf();

        builder.RegisterType<TokenService>()
            .As<ITokenService>()
            .AsSelf();
        
        builder.RegisterAutoMapper(options =>
            options.AddProfile(new AssemblyMappingProfile(ThisAssembly)));

        builder.RegisterMediatR(MediatRConfigurationBuilder
            .Create(ThisAssembly)
            .WithAllOpenGenericHandlerTypesRegistered()
            .Build());
    }
}