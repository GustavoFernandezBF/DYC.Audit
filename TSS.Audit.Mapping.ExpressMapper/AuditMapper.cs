// Decompiled with JetBrains decompiler
// Type: TSS.Audit.Mapping.ExpressMapper.AuditMapper
// Assembly: TSS.Audit.Mapping.ExpressMapper, Version=1.2.1.22, Culture=neutral, PublicKeyToken=null
// MVID: 71DC9CF9-D03F-4B28-8468-3DCC7914BCDB
// Assembly location: D:\Baufest\GIT\TSS.Audit.WebApi\TSS.Audit.Mapping.ExpressMapper.dll

using ExpressMapper;
using TSS.Core.Mapping;
using TSS.Core.Mapping.ExpressMapper;

#nullable disable
namespace TSS.Audit.Mapping.ExpressMapper;

public class AuditMapper(IMappingServiceProvider mapper) : TSSExpressMapper(mapper), IAuditMapper, IMapper
{
}
