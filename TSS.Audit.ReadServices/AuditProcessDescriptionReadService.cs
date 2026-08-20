// Decompiled with JetBrains decompiler
// Type: TSS.Audit.ReadServices.AuditProcessDescriptionReadService
// Assembly: TSS.Audit.ReadServices, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A10258C3-6446-4BF1-813B-45DC267811FE
// Assembly location: D:\Baufest\GIT\TSS.Audit.WebApi\TSS.Audit.ReadServices.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TSS.Audit.DTOs.Core;
using TSS.Audit.Mapping;
using TSS.Audit.Persistence.Queries;
using TSS.Audit.QueryModel;

#nullable disable
namespace TSS.Audit.ReadServices;

public class AuditProcessDescriptionReadService
{
  private readonly IAuditMapper _mapper;
  private readonly IAuditQueryDataAccess<AuditProcessDescription> _auditProcessDescriptionQuery;

  public AuditProcessDescriptionReadService(
    IAuditQueryDataAccess<AuditProcessDescription> auditProcessDescriptionQuery,
    IAuditMapper mapper)
  {
    this._mapper = mapper;
    this._auditProcessDescriptionQuery = auditProcessDescriptionQuery;
  }

  public async Task<List<AuditProcess>> FindByApplicationAsync(string appCode)
  {
    return string.IsNullOrWhiteSpace(appCode) ? new List<AuditProcess>() : this.CreateAuditProcessDtoObjects(await this.FindAuditProcessDescriptionByApplicationAsync(appCode));
  }

  private async Task<List<AuditProcessDescription>> FindAuditProcessDescriptionByApplicationAsync(
    string appCode)
  {
    return (await (Task<IEnumerable<AuditProcessDescription>>) this._auditProcessDescriptionQuery.ListAsync((Expression<Func<AuditProcessDescription, bool>>) (x => x.ApplicationCode == appCode && x.Enabled))).ToList<AuditProcessDescription>();
  }

  private List<AuditProcess> CreateAuditProcessDtoObjects(
    List<AuditProcessDescription> auditProcessDescriptions)
  {
    return this._mapper.Map<List<AuditProcessDescription>, List<AuditProcess>>(auditProcessDescriptions);
  }
}
