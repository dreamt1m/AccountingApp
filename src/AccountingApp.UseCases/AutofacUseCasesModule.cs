﻿using System.Reflection;
using Autofac;
using MediatR.Pipeline;
using MediatR;

namespace AccountingApp.UseCases;

public class AutofacUseCasesModule : Autofac.Module
{
    protected override void Load(ContainerBuilder builder)
    {
        RegisterMediatR(builder);
    }

    private void RegisterMediatR(ContainerBuilder builder)
    {
        builder
            .RegisterType<Mediator>()
            .As<IMediator>()
            .InstancePerLifetimeScope();

        var mediatrOpenTypes = new[]
        {
            typeof(IRequestHandler<,>),
            typeof(IRequestExceptionHandler<,,>),
            typeof(IRequestExceptionAction<,>),
            typeof(INotificationHandler<>),
        };

        foreach (var mediatrOpenType in mediatrOpenTypes)
        {
            builder
                .RegisterAssemblyTypes(Assembly.GetCallingAssembly())
                .AsClosedTypesOf(mediatrOpenType)
                .AsImplementedInterfaces();
        }
    }
}