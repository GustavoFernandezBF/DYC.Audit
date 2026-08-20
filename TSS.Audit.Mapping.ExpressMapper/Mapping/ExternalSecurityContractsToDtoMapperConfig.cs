// Decompiled with JetBrains decompiler
// Type: TSS.Audit.Mapping.ExpressMapper.Mapping.ExternalSecurityContractsToDtoMapperConfig
// Assembly: TSS.Audit.Mapping.ExpressMapper, Version=1.2.1.22, Culture=neutral, PublicKeyToken=null
// MVID: 71DC9CF9-D03F-4B28-8468-3DCC7914BCDB
// Assembly location: D:\Baufest\GIT\TSS.Audit.WebApi\TSS.Audit.Mapping.ExpressMapper.dll

using ExpressMapper;
using TSS.Audit.DTOs.Core;
using TSS.Audit.ExternalServicesClients.Abstractions.Security.Contracts;

#nullable disable
namespace TSS.Audit.Mapping.ExpressMapper.Mapping;

public class ExternalSecurityContractsToDtoMapperConfig : IMapperConfiguration
{
  private readonly MappingServiceProvider _mappingServiceProvider;

  public ExternalSecurityContractsToDtoMapperConfig(MappingServiceProvider mappingServiceProvider)
  {
    this._mappingServiceProvider = mappingServiceProvider;
  }

  public void Configure() => this._mappingServiceProvider.Register<ApplicationDto, AuditApp>();
}
