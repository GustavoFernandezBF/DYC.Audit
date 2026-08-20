// Decompiled with JetBrains decompiler
// Type: TSS.Audit.WebApi.DependencyInjection.InfrastructureModule
// Assembly: TSS.Audit.WebApi, Version=0.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9CA87E92-3DDD-413F-AD9B-DAF8395B6F1D
// Assembly location: D:\Baufest\GIT\TSS.Audit.WebApi\TSS.Audit.WebApi.dll
// XML documentation location: D:\Baufest\GIT\TSS.Audit.WebApi\TSS.Audit.WebApi.xml

using Autofac;
using Autofac.Builder;
using Autofac.Core;
using ExpressMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NPoco;
using System;
using System.Collections.Generic;
using System.Reflection;
using TSS.Audit.Common.Helpers.IdsHasher;
using TSS.Audit.Mapping;
using TSS.Audit.Mapping.ExpressMapper;
using TSS.Audit.Mapping.ExpressMapper.Mapping;
using TSS.Audit.Persistence;
using TSS.Audit.Persistence.Commands;
using TSS.Audit.Persistence.EFCore;
using TSS.Audit.Persistence.NPoco;
using TSS.Audit.Persistence.NPoco.Abstract;
using TSS.Audit.Persistence.Queries;
using TSS.Core.Persistence.Reading;

#nullable disable
namespace TSS.Audit.WebApi.DependencyInjection;

public class InfrastructureModule : Autofac.Module
{
  private readonly IConfiguration _configuration;
  private const string ProviderName = "System.Data.SqlClient";

  public InfrastructureModule(IConfiguration configuration) => this._configuration = configuration;

  protected override void Load(ContainerBuilder builder)
  {
    string parameterValue = this._configuration.GetSection("Storage:Command").GetValue<string>("Database");
    string connectionSring = this._configuration.GetSection("Storage:Query").GetValue<string>("Database");
    builder.Register<NPoco.Database>((Func<IComponentContext, NPoco.Database>) (x => NPocoDatabaseFactory.DbFactory.GetDatabase())).As<IDatabase>().InstancePerLifetimeScope();
    builder.RegisterGeneric((Type) typeof (NPocoDataAccess<>)).As((Type[]) new Type[1]
    {
      typeof (IRelationalQueryDataAccess<>)
    }).As((Type[]) new Type[1]
    {
      typeof (IQueryCoreDataAccess<>)
    }).As((Type[]) new Type[1]
    {
      typeof (IAuditQueryDataAccess<>)
    }).InstancePerLifetimeScope();
    builder.RegisterGeneric((Type) typeof (EntityFrameworkAuditRepository<>)).As((Type[]) new Type[1]
    {
      typeof (IAuditRepository<>)
    }).InstancePerLifetimeScope();
    builder.RegisterType<AuditDbContext>().As<DbContext>().WithParameter<AuditDbContext, ConcreteReflectionActivatorData, SingleRegistrationStyle>("connectionString", (object) parameterValue).InstancePerLifetimeScope();
    builder.RegisterType<AuditUnitOfWork>().As<IAuditUnitOfWork>().InstancePerLifetimeScope();
    builder.RegisterInstance<IdsHasherSettings>(this._configuration.GetSection("IdsHasherSettings").Get<IdsHasherSettings>());
    builder.RegisterType<TSS.Audit.Common.Helpers.IdsHasher.IdsHasher>();
    builder.RegisterType<ExternalSecurityContractsToDtoMapperConfig>().As<IMapperConfiguration>().InstancePerLifetimeScope();
    builder.RegisterType<QueryModelToDtoMapperConfig>().As<IMapperConfiguration>().InstancePerLifetimeScope();
    builder.Register<MappingServiceProvider>((Func<IComponentContext, MappingServiceProvider>) (c =>
    {
      MappingServiceProvider mappingServiceProvider = new MappingServiceProvider();
      foreach (IMapperConfiguration mapperConfiguration in c.Resolve<IEnumerable<IMapperConfiguration>>((Parameter) new TypedParameter((Type) typeof (MappingServiceProvider), (object) mappingServiceProvider), (Parameter) new ResolvedParameter((Func<ParameterInfo, IComponentContext, bool>) ((pi, ctx) => pi.ParameterType == typeof (TSS.Audit.Common.Helpers.IdsHasher.IdsHasher)), (Func<ParameterInfo, IComponentContext, object>) ((pi, ctx) => (object) ctx.Resolve<TSS.Audit.Common.Helpers.IdsHasher.IdsHasher>()))))
        mapperConfiguration.Configure();
      mappingServiceProvider.Compile();
      return mappingServiceProvider;
    })).As<IMappingServiceProvider>().SingleInstance();
    builder.RegisterType<AuditMapper>().As<IAuditMapper>().InstancePerLifetimeScope();
    NPocoDatabaseFactory.Setup(connectionSring, "System.Data.SqlClient");
  }
}
