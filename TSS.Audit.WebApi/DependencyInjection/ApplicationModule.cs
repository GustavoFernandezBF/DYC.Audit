// Decompiled with JetBrains decompiler
// Type: TSS.Audit.WebApi.DependencyInjection.ApplicationModule
// Assembly: TSS.Audit.WebApi, Version=0.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9CA87E92-3DDD-413F-AD9B-DAF8395B6F1D
// Assembly location: D:\Baufest\GIT\TSS.Audit.WebApi\TSS.Audit.WebApi.dll
// XML documentation location: D:\Baufest\GIT\TSS.Audit.WebApi\TSS.Audit.WebApi.xml

using Autofac;
using Autofac.Builder;
using Autofac.Features.Scanning;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using System;
using System.Reflection;
using TSS.Audit.ExternalServicesClients.Abstractions.Security.Services;
using TSS.Audit.ExternalServicesClients.Security.Services;
using TSS.Audit.ExternalServicesClients.Security.Settings;
using TSS.Audit.ReadServices.Settings;
using TSS.Audit.WriteServices.Settings;
using TSS.Audit.WriteServices.Validation;

#nullable disable
namespace TSS.Audit.WebApi.DependencyInjection;

public class ApplicationModule : Autofac.Module
{
  private readonly IConfiguration _configuration;

  public ApplicationModule(IConfiguration configuration) => this._configuration = configuration;

  protected override void Load(ContainerBuilder builder)
  {
    builder.RegisterInstance<ReadServiceSettings>(this._configuration.GetSection("ReadServiceSettings").Get<ReadServiceSettings>());
    builder.RegisterInstance<WriteServiceSettings>(this._configuration.GetSection("WriteServiceSettings").Get<WriteServiceSettings>());
    builder.RegisterInstance<SecurityServiceSettings>(this._configuration.GetSection("ExternalServiceSettings:Security").Get<SecurityServiceSettings>());
    builder.RegisterType<SecurityService>().As<ISecurityService>().InstancePerLifetimeScope();
    builder.RegisterAssemblyTypes((Assembly[]) new Assembly[2]
    {
      Assembly.Load("TSS.Audit.WriteServices"),
      Assembly.Load("TSS.Audit.ReadServices")
    }).Where<object, ScanningActivatorData, DynamicRegistrationStyle>((Func<Type, bool>) (x => x.Name.EndsWith("Service"))).AsSelf<object>().InstancePerLifetimeScope();
    builder.RegisterAssemblyTypes((Assembly[]) new Assembly[1]
    {
      Assembly.Load("TSS.Audit.WriteServices")
    }).Where<object, ScanningActivatorData, DynamicRegistrationStyle>((Func<Type, bool>) (x => x.Name.EndsWith("Validator") && x.IsClass && !x.IsAbstract)).AsClosedTypesOf<object, ScanningActivatorData, DynamicRegistrationStyle>((Type) typeof (AbstractValidator<>)).SingleInstance();
    builder.RegisterType<AutofacValidatorProvider>().As<IValidatorProvider>().SingleInstance();
    builder.RegisterType<FluentValidationService>().As<IValidationService>().SingleInstance();
  }
}
