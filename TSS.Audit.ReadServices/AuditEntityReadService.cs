// Decompiled with JetBrains decompiler
// Type: TSS.Audit.ReadServices.AuditEntityReadService
// Assembly: TSS.Audit.ReadServices, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A10258C3-6446-4BF1-813B-45DC267811FE
// Assembly location: D:\Baufest\GIT\TSS.Audit.WebApi\TSS.Audit.ReadServices.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TSS.Audit.Mapping;
using TSS.Audit.Persistence.Queries;
using TSS.Audit.QueryModel;

#nullable disable
namespace TSS.Audit.ReadServices;

public class AuditEntityReadService
{
  private readonly IAuditMapper _mapper;
  private readonly IAuditQueryDataAccess<TSS.Audit.QueryModel.AuditEntity> _auditEntityQuery;
  private readonly IAuditQueryDataAccess<AuditEntityTable> _auditEntityTableQuery;
  private readonly IAuditQueryDataAccess<AuditEntityTableColumn> _auditEntityTableColumnQuery;

  public AuditEntityReadService(
    IAuditQueryDataAccess<TSS.Audit.QueryModel.AuditEntity> auditEntityQuery,
    IAuditQueryDataAccess<AuditEntityTable> auditEntityTableQuery,
    IAuditQueryDataAccess<AuditEntityTableColumn> auditEntityTableColumnQuery,
    IAuditMapper mapper)
  {
    this._mapper = mapper;
    this._auditEntityQuery = auditEntityQuery;
    this._auditEntityTableQuery = auditEntityTableQuery;
    this._auditEntityTableColumnQuery = auditEntityTableColumnQuery;
  }

  public async Task<List<TSS.Audit.DTOs.Core.AuditEntity>> FindByApplicationAsync(
    string appCode,
    bool includeAuditEntityTables)
  {
    return string.IsNullOrWhiteSpace(appCode) ? new List<TSS.Audit.DTOs.Core.AuditEntity>() : this.CreateAuditEntityDtoObjects(await this.FindAuditEntitiesByApplicationAsync(appCode, includeAuditEntityTables));
  }

  private async Task<List<TSS.Audit.QueryModel.AuditEntity>> FindAuditEntitiesByApplicationAsync(
    string appCode,
    bool includeAuditEntityTables)
  {
    List<TSS.Audit.QueryModel.AuditEntity> auditEntities = (await (Task<IEnumerable<TSS.Audit.QueryModel.AuditEntity>>) this._auditEntityQuery.ListAsync((Expression<Func<TSS.Audit.QueryModel.AuditEntity, bool>>) (x => x.ApplicationCode == appCode && x.Enabled))).ToList<TSS.Audit.QueryModel.AuditEntity>();
    if (includeAuditEntityTables && auditEntities.Any<TSS.Audit.QueryModel.AuditEntity>())
      await this.FindAuditEntityTablesAsync(auditEntities);
    return auditEntities;
  }

  private async Task FindAuditEntityTablesAsync(List<TSS.Audit.QueryModel.AuditEntity> auditEntities)
  {
    List<AuditEntityTable> tableByEntitiesAsync = await this.FindAuditEntityTableByEntitiesAsync(this.GetAuditEntityIds(auditEntities));
    if (tableByEntitiesAsync == null || !tableByEntitiesAsync.Any<AuditEntityTable>())
      return;
    foreach (TSS.Audit.QueryModel.AuditEntity auditEntity1 in auditEntities)
    {
      TSS.Audit.QueryModel.AuditEntity auditEntity = auditEntity1;
      auditEntity.AuditEntityTables = tableByEntitiesAsync.Where<AuditEntityTable>((Func<AuditEntityTable, bool>) (x =>
      {
        int? auditEntityId1 = x.AuditEntityId;
        int auditEntityId2 = auditEntity.AuditEntityId;
        return auditEntityId1.GetValueOrDefault() == auditEntityId2 && auditEntityId1.HasValue;
      })).ToList<AuditEntityTable>();
    }
  }

  private List<int> GetAuditEntityIds(List<TSS.Audit.QueryModel.AuditEntity> auditEntities)
  {
    return auditEntities.Select<TSS.Audit.QueryModel.AuditEntity, int>((Func<TSS.Audit.QueryModel.AuditEntity, int>) (x => x.AuditEntityId)).Distinct<int>().ToList<int>();
  }

  private async Task<List<AuditEntityTable>> FindAuditEntityTableByEntitiesAsync(
    List<int> auditEntityIds)
  {
    List<AuditEntityTable> auditEntityTables = (await (Task<IEnumerable<AuditEntityTable>>) this._auditEntityTableQuery.ListAsync((Expression<Func<AuditEntityTable, bool>>) (x => x.AuditEntityId.HasValue && auditEntityIds.Contains(x.AuditEntityId.Value) && x.Enabled))).ToList<AuditEntityTable>();
    if (auditEntityTables.Any<AuditEntityTable>())
      await this.FindAuditEntityTableColumnsAsync(auditEntityTables);
    return auditEntityTables;
  }

  private async Task FindAuditEntityTableColumnsAsync(List<AuditEntityTable> auditEntityTables)
  {
    List<AuditEntityTableColumn> entityTablesAsync = await this.FindAuditEntityTableColumnByEntityTablesAsync(this.GetAuditEntityTableIds(auditEntityTables));
    if (!entityTablesAsync.Any<AuditEntityTableColumn>())
      return;
    foreach (AuditEntityTable auditEntityTable1 in auditEntityTables)
    {
      AuditEntityTable auditEntityTable = auditEntityTable1;
      auditEntityTable.AuditEntityTableColumns = entityTablesAsync.Where<AuditEntityTableColumn>((Func<AuditEntityTableColumn, bool>) (x => x.AuditEntityTableId == auditEntityTable.AuditEntityTableId)).ToList<AuditEntityTableColumn>();
    }
  }

  private List<int> GetAuditEntityTableIds(List<AuditEntityTable> auditEntityTables)
  {
    return auditEntityTables.Select<AuditEntityTable, int>((Func<AuditEntityTable, int>) (x => x.AuditEntityTableId)).Distinct<int>().ToList<int>();
  }

  private async Task<List<AuditEntityTableColumn>> FindAuditEntityTableColumnByEntityTablesAsync(
    List<int> auditEntityTableIds)
  {
    return (await (Task<IEnumerable<AuditEntityTableColumn>>) this._auditEntityTableColumnQuery.ListAsync((Expression<Func<AuditEntityTableColumn, bool>>) (x => auditEntityTableIds.Contains(x.AuditEntityTableId) && x.Enabled))).ToList<AuditEntityTableColumn>();
  }

  private List<TSS.Audit.DTOs.Core.AuditEntity> CreateAuditEntityDtoObjects(
    List<TSS.Audit.QueryModel.AuditEntity> auditEntities)
  {
    return this._mapper.Map<List<TSS.Audit.QueryModel.AuditEntity>, List<TSS.Audit.DTOs.Core.AuditEntity>>(auditEntities);
  }
}
