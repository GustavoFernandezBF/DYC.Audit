// Decompiled with JetBrains decompiler
// Type: TSS.Audit.WriteServices.AuditEntityWriteService
// Assembly: TSS.Audit.WriteServices, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA5016C0-9FEC-4A45-98E8-F75EBF083899
// Assembly location: D:\Baufest\GIT\TSS.Audit.WebApi\TSS.Audit.WriteServices.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TSS.Audit.Domain;
using TSS.Audit.DTOs.Core;
using TSS.Audit.DTOs.Request;
using TSS.Audit.DTOs.Response;
using TSS.Audit.Persistence;
using TSS.Audit.Persistence.Commands;
using TSS.Audit.Resources;
using TSS.Audit.WriteServices.Validation;

#nullable disable
namespace TSS.Audit.WriteServices;

public class AuditEntityWriteService : IDisposable
{
  private readonly IAuditUnitOfWork _unitOfWork;
  private readonly IAuditRepository<TSS.Audit.Domain.AuditEntity> _auditEntityRepository;
  private readonly IAuditRepository<AuditTransactionEntityLog> _auditTransactionEntityLogRepository;
  private readonly IAuditRepository<AuditEntityTable> _auditEntityTableRepository;
  private readonly IAuditRepository<AuditTransactionEntityTableLog> _auditTransactionEntityTableLogRepository;
  private readonly IAuditRepository<AuditEntityTableColumn> _auditEntityTableColumnRepository;
  private readonly IAuditRepository<AuditTransactionEntityTableColumnLog> _auditTransactionEntityTableColumnLogRepository;
  private readonly IValidationService _validationService;

  public AuditEntityWriteService(
    IAuditUnitOfWork unitOfWork,
    IAuditRepository<TSS.Audit.Domain.AuditEntity> auditEntityRepository,
    IAuditRepository<AuditTransactionEntityLog> auditTransactionEntityLogRepository,
    IAuditRepository<AuditEntityTable> auditEntityTableRepository,
    IAuditRepository<AuditTransactionEntityTableLog> auditTransactionEntityTableLogRepository,
    IAuditRepository<AuditEntityTableColumn> auditEntityTableColumnRepository,
    IAuditRepository<AuditTransactionEntityTableColumnLog> auditTransactionEntityTableColumnLogRepository,
    IValidationService validationService)
  {
    this._unitOfWork = unitOfWork;
    this._auditEntityRepository = auditEntityRepository;
    this._auditTransactionEntityLogRepository = auditTransactionEntityLogRepository;
    this._auditEntityTableRepository = auditEntityTableRepository;
    this._auditTransactionEntityTableLogRepository = auditTransactionEntityTableLogRepository;
    this._auditEntityTableColumnRepository = auditEntityTableColumnRepository;
    this._auditTransactionEntityTableColumnLogRepository = auditTransactionEntityTableColumnLogRepository;
    this._validationService = validationService;
  }

  public async Task<WriteOperationResponse> RegisterAsync(string appCode, TSS.Audit.DTOs.Core.AuditEntity newEntity)
  {
    WriteOperationResponse registerNewEntityAsync = await this.ValidateDataToRegisterNewEntityAsync(appCode, newEntity);
    if (!registerNewEntityAsync.IsCorrect)
      return registerNewEntityAsync;
    await this.AddNewEntityAsync(appCode, newEntity);
    await this.CommitTransactionAsync();
    return WriteOperationBuilder.CreateWriteOperationResponse();
  }

  public async Task<WriteOperationResponse> DeleteAsync(
    string appCode,
    AuditEntityDelete auditEntityDelete)
  {
    WriteOperationResponse deleteEntityAsync = await this.ValidateDataToDeleteEntityAsync(appCode, auditEntityDelete);
    if (!deleteEntityAsync.IsCorrect)
      return deleteEntityAsync;
    await this.DeleteAuditEntityAsync(appCode, auditEntityDelete);
    await this.CommitTransactionAsync();
    return WriteOperationBuilder.CreateWriteOperationResponse();
  }

  public async Task<WriteOperationResponse> UpdateAsync(string appCode, TSS.Audit.DTOs.Core.AuditEntity entity)
  {
    WriteOperationResponse updateEntityAsync = await this.ValidateDataToUpdateEntityAsync(appCode, entity);
    if (!updateEntityAsync.IsCorrect)
      return updateEntityAsync;
    await this.UpdateEntityAsync(appCode, entity);
    await this.CommitTransactionAsync();
    return WriteOperationBuilder.CreateWriteOperationResponse();
  }

  public void Dispose() => this._unitOfWork?.Dispose();

  private async Task<WriteOperationResponse> ValidateDataToRegisterNewEntityAsync(
    string appCode,
    TSS.Audit.DTOs.Core.AuditEntity newEntitys)
  {
    WriteOperationResponse registerNewEntity = this.ValidateParametersFormatToRegisterNewEntity(appCode, newEntitys);
    return !registerNewEntity.IsCorrect ? registerNewEntity : await this.ValidateDataIntegrityToRegisterNewEntityAsync(appCode, newEntitys);
  }

  private WriteOperationResponse ValidateParametersFormatToRegisterNewEntity(
    string appName,
    TSS.Audit.DTOs.Core.AuditEntity newEntity)
  {
    if (newEntity == null || string.IsNullOrWhiteSpace(appName))
      return WriteOperationBuilder.CreateWriteOperationResponse(Messages.BadParameter, OperationStatus.Invalid);
    OperationResponse operationResponse = this._validationService.Validate<TSS.Audit.DTOs.Core.AuditEntity>(newEntity);
    return operationResponse.IsCorrect ? WriteOperationBuilder.CreateWriteOperationResponse() : operationResponse.WithStatus(OperationStatus.Invalid);
  }

  private async Task<WriteOperationResponse> ValidateDataIntegrityToRegisterNewEntityAsync(
    string appCode,
    TSS.Audit.DTOs.Core.AuditEntity newEntity)
  {
    return await this.ValidateAuditEntityExistenceAsync(appCode, newEntity.Name, newEntity.Module) ? WriteOperationBuilder.CreateWriteOperationResponse(Messages.EntityAlreadyExists, OperationStatus.Conflict) : WriteOperationBuilder.CreateWriteOperationResponse();
  }

  private Task<bool> ValidateAuditEntityExistenceAsync(
    string appCode,
    string entityName,
    string module)
  {
    return (Task<bool>) this._auditEntityRepository.AnyAsync((Expression<Func<TSS.Audit.Domain.AuditEntity, bool>>) (x => string.Equals(x.ApplicationCode, appCode, StringComparison.OrdinalIgnoreCase) && string.Equals(x.Name, entityName, StringComparison.OrdinalIgnoreCase) && string.Equals(x.Module, module, StringComparison.OrdinalIgnoreCase)));
  }

  private async Task AddNewEntityAsync(string appCode, TSS.Audit.DTOs.Core.AuditEntity newEntity)
  {
    if (string.IsNullOrWhiteSpace(appCode) || newEntity == null)
      return;
    if (await (Task<bool>) this._auditEntityRepository.AnyAsync((Expression<Func<TSS.Audit.Domain.AuditEntity, bool>>) (x => string.Equals(appCode, newEntity.Name, StringComparison.OrdinalIgnoreCase) && string.Equals(x.Name, newEntity.Name, StringComparison.OrdinalIgnoreCase) && string.Equals(x.Module, newEntity.Module, StringComparison.OrdinalIgnoreCase))))
      return;
    await (Task) this._auditEntityRepository.AddAsync(this.CreateAuditEntityFromDto(appCode, newEntity));
  }

  private TSS.Audit.Domain.AuditEntity CreateAuditEntityFromDto(
    string appCode,
    TSS.Audit.DTOs.Core.AuditEntity newEntity)
  {
    TSS.Audit.Domain.AuditEntity auditEntity = new TSS.Audit.Domain.AuditEntity(appCode, newEntity.Name, newEntity.Module);
    this.AddAuditEntityTablesToAuditEntity(auditEntity, newEntity.Tables);
    return auditEntity;
  }

  private void AddAuditEntityTablesToAuditEntity(
    TSS.Audit.Domain.AuditEntity auditEntity,
    List<AuditTable> auditTables)
  {
    if (auditTables == null)
      return;
    foreach (AuditTable auditTable1 in auditTables)
    {
      AuditTable auditTable = auditTable1;
      if (!auditEntity.AuditEntityTables.Any<AuditEntityTable>((Func<AuditEntityTable, bool>) (x => string.Equals(x.TableName, auditTable.Name, StringComparison.OrdinalIgnoreCase))))
      {
        AuditEntityTable entityTableFromDto = this.CreateAuditEntityTableFromDto(auditTable);
        if (entityTableFromDto != null)
          auditEntity.RegisterEntityTable(entityTableFromDto);
      }
    }
  }

  private AuditEntityTable CreateAuditEntityTableFromDto(AuditTable newTable)
  {
    if (newTable == null)
      return (AuditEntityTable) null;
    AuditEntityTable auditEntityTable = new AuditEntityTable(newTable.Name)
    {
      TableDescriptionFormat = newTable.DescriptionFormat,
      IdColumnName = newTable.IdColumnName,
      KeyFieldName = newTable.KeyFieldName,
      AuditByFieldName = newTable.AuditByFieldName,
      AuditDateFieldName = newTable.AuditDateFieldName
    };
    this.AddAuditEntityTableColumnsToAuditEntityTable(auditEntityTable, newTable.Columns);
    return auditEntityTable;
  }

  private void AddAuditEntityTableColumnsToAuditEntityTable(
    AuditEntityTable auditEntityTable,
    List<AuditTableColumn> auditColumns)
  {
    if (auditColumns == null)
      return;
    foreach (AuditTableColumn auditColumn1 in auditColumns)
    {
      AuditTableColumn auditColumn = auditColumn1;
      if (!auditEntityTable.AuditEntityTableColumns.Any<AuditEntityTableColumn>((Func<AuditEntityTableColumn, bool>) (x => string.Equals(x.ColumnName, auditColumn.Name, StringComparison.OrdinalIgnoreCase))))
      {
        AuditEntityTableColumn tableColumnFromDto = this.CreateAuditEntityTableColumnFromDto(auditColumn);
        if (tableColumnFromDto != null)
          auditEntityTable.RegisterTableColumn(tableColumnFromDto);
      }
    }
  }

  private AuditEntityTableColumn CreateAuditEntityTableColumnFromDto(AuditTableColumn newColumn)
  {
    if (newColumn == null)
      return (AuditEntityTableColumn) null;
    return new AuditEntityTableColumn(newColumn.Name)
    {
      ColumnDotNetType = newColumn.NetType,
      ColumnTsqltype = newColumn.SqlType,
      DisplayOrder = newColumn.DisplayOrder,
      ColumnLabel = newColumn.Label,
      MasterTableName = newColumn.MasterTableName,
      MasterTablePkname = newColumn.MasterTablePKName,
      MasterTableDescColumnName = newColumn.MasterTableDescColumnName
    };
  }

  private Task CommitTransactionAsync() => (Task) this._unitOfWork.CommitAsync();

  private async Task<WriteOperationResponse> ValidateDataToDeleteEntityAsync(
    string appCode,
    AuditEntityDelete auditEntityDelete)
  {
    WriteOperationResponse deleteEntity = this.ValidateParametersFormatToDeleteEntity(appCode, auditEntityDelete);
    return !deleteEntity.IsCorrect ? deleteEntity : await this.ValidateDataIntegrityToDeleteAsync(appCode, auditEntityDelete);
  }

  private WriteOperationResponse ValidateParametersFormatToDeleteEntity(
    string appCode,
    AuditEntityDelete auditEntityDelete)
  {
    if (string.IsNullOrWhiteSpace(appCode))
      return WriteOperationBuilder.CreateWriteOperationResponse(Messages.AppInvalid, OperationStatus.Invalid);
    OperationResponse operationResponse = this._validationService.Validate<AuditEntityDelete>(auditEntityDelete);
    return operationResponse.IsCorrect ? WriteOperationBuilder.CreateWriteOperationResponse() : operationResponse.WithStatus(OperationStatus.Invalid);
  }

  private async Task<WriteOperationResponse> ValidateDataIntegrityToDeleteAsync(
    string appCode,
    AuditEntityDelete auditEntityDelete)
  {
    return !await this.ValidateAuditEntityExistenceAsync(appCode, auditEntityDelete.Name, auditEntityDelete.Module) ? WriteOperationBuilder.CreateWriteOperationResponse(Messages.EntityNotExists, OperationStatus.NotFound) : WriteOperationBuilder.CreateWriteOperationResponse();
  }

  private async Task DeleteAuditEntityAsync(string appCode, AuditEntityDelete auditEntityDelete)
  {
    TSS.Audit.Domain.AuditEntity auditEntity = await this.GetAuditEntityAsync(appCode, auditEntityDelete.Name, auditEntityDelete.Module, new string[1]
    {
      "AuditEntityTables.AuditEntityTableColumns"
    });
    if (auditEntity == null)
      return;
    if (await this.ValidateExistenceOfAuditDataLogLinkedToEntityAsync(auditEntity))
      this.UpdateAuditEntityEnabledProperty(auditEntity, false);
    else
      await this.DeleteEntityPhysicallyAsync(auditEntity);
  }

  private Task<bool> ValidateExistenceOfAuditDataLogLinkedToEntityAsync(TSS.Audit.Domain.AuditEntity auditEntity)
  {
    return (Task<bool>) this._auditTransactionEntityLogRepository.AnyAsync((Expression<Func<AuditTransactionEntityLog, bool>>) (x => x.AuditEntityId == (int?) auditEntity.AuditEntityId));
  }

  private Task<TSS.Audit.Domain.AuditEntity> GetAuditEntityAsync(
    string appCode,
    string entityName,
    string module,
    string[] includes = null)
  {
    return this._auditEntityRepository.FirstOrDefaultAsync((Expression<Func<TSS.Audit.Domain.AuditEntity, bool>>) (x => string.Equals(x.ApplicationCode, appCode, StringComparison.OrdinalIgnoreCase) && string.Equals(x.Name, entityName, StringComparison.OrdinalIgnoreCase) && string.Equals(x.Module, module, StringComparison.OrdinalIgnoreCase)), includes: includes);
  }

  private void UpdateAuditEntityEnabledProperty(TSS.Audit.Domain.AuditEntity auditEntity, bool enabled)
  {
    if (auditEntity == null)
      return;
    auditEntity.Enabled = enabled;
  }

  private async Task DeleteEntityPhysicallyAsync(TSS.Audit.Domain.AuditEntity auditEntity)
  {
    if (auditEntity == null)
      return;
    await this.DeleteAuditEntityChildrenDataPhysicallyAsync(auditEntity);
    await (Task) this._auditEntityRepository.DeleteAsync(auditEntity);
  }

  private async Task DeleteAuditEntityChildrenDataPhysicallyAsync(TSS.Audit.Domain.AuditEntity auditEntity)
  {
    await this.DeleteAuditEntityTablesPhysicallyAsync(auditEntity.AuditEntityTables.ToList<AuditEntityTable>());
  }

  private async Task DeleteAuditEntityTablesPhysicallyAsync(List<AuditEntityTable> auditEntityTables)
  {
    if (auditEntityTables == null)
      return;
    foreach (AuditEntityTable auditEntityTable in auditEntityTables)
      await this.DeleteAuditEntityTablePhysicallyAsync(auditEntityTable);
  }

  private async Task DeleteAuditEntityTablePhysicallyAsync(AuditEntityTable auditEntityTable)
  {
    await this.DeleteAuditEntityTableColumnsPhysicallyAsync(auditEntityTable.AuditEntityTableColumns.ToList<AuditEntityTableColumn>());
    await (Task) this._auditEntityTableRepository.DeleteAsync(auditEntityTable);
  }

  private async Task DeleteAuditEntityTableColumnsPhysicallyAsync(
    List<AuditEntityTableColumn> auditEntityTableColumns)
  {
    if (auditEntityTableColumns == null)
      return;
    foreach (AuditEntityTableColumn entityTableColumn in auditEntityTableColumns)
      await this.DeleteAuditEntityTableColumnPhysicallyAsync(entityTableColumn);
  }

  private Task DeleteAuditEntityTableColumnPhysicallyAsync(
    AuditEntityTableColumn auditEntityTableColumn)
  {
    return (Task) this._auditEntityTableColumnRepository.DeleteAsync(auditEntityTableColumn);
  }

  private async Task<WriteOperationResponse> ValidateDataToUpdateEntityAsync(
    string appCode,
    TSS.Audit.DTOs.Core.AuditEntity newEntitys)
  {
    WriteOperationResponse updateEntity = this.ValidateParametersFormatToUpdateEntity(appCode, newEntitys);
    return !updateEntity.IsCorrect ? updateEntity : await this.ValidateDataIntegrityToUpdateEntityAsync(appCode, newEntitys);
  }

  private WriteOperationResponse ValidateParametersFormatToUpdateEntity(
    string appCode,
    TSS.Audit.DTOs.Core.AuditEntity entity)
  {
    if (entity == null || string.IsNullOrWhiteSpace(appCode))
      return WriteOperationBuilder.CreateWriteOperationResponse(Messages.BadParameter, OperationStatus.Invalid);
    OperationResponse operationResponse = this._validationService.Validate<TSS.Audit.DTOs.Core.AuditEntity>(entity);
    return operationResponse.IsCorrect ? WriteOperationBuilder.CreateWriteOperationResponse() : operationResponse.WithStatus(OperationStatus.Invalid);
  }

  private async Task<WriteOperationResponse> ValidateDataIntegrityToUpdateEntityAsync(
    string appCode,
    TSS.Audit.DTOs.Core.AuditEntity entity)
  {
    return !await this.ValidateAuditEntityExistenceAsync(appCode, entity.Name, entity.Module) ? WriteOperationBuilder.CreateWriteOperationResponse(Messages.EntityAlreadyExists, OperationStatus.NotFound) : WriteOperationBuilder.CreateWriteOperationResponse();
  }

  private async Task UpdateEntityAsync(string appCode, TSS.Audit.DTOs.Core.AuditEntity entity)
  {
    TSS.Audit.Domain.AuditEntity auditEntity = await this.GetAuditEntityAsync(appCode, entity.Name, entity.Module, new string[1]
    {
      "AuditEntityTables.AuditEntityTableColumns"
    });
    await this.UpdateAuditEntityChildrenDataAsync(auditEntity, entity);
    await (Task) this._auditEntityRepository.UpdateAsync(auditEntity);
  }

  private async Task UpdateAuditEntityChildrenDataAsync(TSS.Audit.Domain.AuditEntity auditEntity, TSS.Audit.DTOs.Core.AuditEntity entity)
  {
    if (auditEntity == null || entity == null)
      return;
    await this.DeleteAuditEntityTablesAsync(auditEntity, entity.Tables);
    await this.UpdateAuditEntityTablesAsync(auditEntity, entity.Tables);
    this.AddNewAuditEntityTables(auditEntity, entity.Tables);
  }

  private async Task DeleteAuditEntityTablesAsync(
    TSS.Audit.Domain.AuditEntity auditEntity,
    List<AuditTable> auditTables)
  {
    IEnumerable<string> auditTableNames = auditTables.Select<AuditTable, string>((Func<AuditTable, string>) (x => x.Name));
    foreach (AuditEntityTable auditEntityTableToDelete in auditEntity.AuditEntityTables.ToList<AuditEntityTable>().Where<AuditEntityTable>((Func<AuditEntityTable, bool>) (x => !auditTableNames.Contains<string>(x.TableName))))
    {
      if (await this.ValidateExistenceOfAuditDataLogLinkedToTableAsync(auditEntityTableToDelete))
        this.UpdateAuditEntityTableEnabledProperty(auditEntityTableToDelete, false);
      else
        await this.DeleteAuditEntityTablePhysicallyAsync(auditEntityTableToDelete);
    }
  }

  private Task<bool> ValidateExistenceOfAuditDataLogLinkedToTableAsync(
    AuditEntityTable auditEntityTable)
  {
    return (Task<bool>) this._auditTransactionEntityTableLogRepository.AnyAsync((Expression<Func<AuditTransactionEntityTableLog, bool>>) (x => x.AuditEntityTableId == (int?) auditEntityTable.AuditEntityTableId));
  }

  private void UpdateAuditEntityTableEnabledProperty(
    AuditEntityTable auditEntityTable,
    bool enabled)
  {
    if (auditEntityTable == null)
      return;
    auditEntityTable.Enabled = enabled;
  }

  private void AddNewAuditEntityTables(TSS.Audit.Domain.AuditEntity auditEntity, List<AuditTable> auditTables)
  {
    IEnumerable<string> auditEntityTableNames = auditEntity.AuditEntityTables.Select<AuditEntityTable, string>((Func<AuditEntityTable, string>) (x => x.TableName));
    List<AuditTable> list = auditTables.Where<AuditTable>((Func<AuditTable, bool>) (x => !auditEntityTableNames.Contains<string>(x.Name))).ToList<AuditTable>();
    this.AddAuditEntityTablesToAuditEntity(auditEntity, list);
  }

  private async Task UpdateAuditEntityTablesAsync(
    TSS.Audit.Domain.AuditEntity auditEntity,
    List<AuditTable> auditTables)
  {
    foreach (AuditTable auditTable in auditTables)
    {
      AuditEntityTable auditEntityTable = this.GetAuditEntityTable(auditEntity.AuditEntityTables.ToList<AuditEntityTable>(), auditTable.Name);
      if (auditEntityTable != null)
        await this.UpdateAuditEntityTableAsync(auditEntityTable, auditTable);
    }
  }

  private AuditEntityTable GetAuditEntityTable(
    List<AuditEntityTable> auditEntityTables,
    string name)
  {
    return auditEntityTables.FirstOrDefault<AuditEntityTable>((Func<AuditEntityTable, bool>) (x => string.Equals(x.TableName, name, StringComparison.OrdinalIgnoreCase)));
  }

  private Task UpdateAuditEntityTableAsync(AuditEntityTable auditEntityTable, AuditTable auditTable)
  {
    this.UpdateAuditEntityTableData(auditEntityTable, auditTable);
    return this.UpdateAuditEntityTableColumnsAsync(auditEntityTable, auditTable.Columns);
  }

  private void UpdateAuditEntityTableData(AuditEntityTable auditEntityTable, AuditTable auditTable)
  {
    if (auditEntityTable == null)
      return;
    auditEntityTable.TableDescriptionFormat = auditTable?.DescriptionFormat;
    auditEntityTable.IdColumnName = auditTable?.IdColumnName;
    auditEntityTable.KeyFieldName = auditTable?.KeyFieldName;
    auditEntityTable.AuditByFieldName = auditTable?.AuditByFieldName;
    auditEntityTable.AuditDateFieldName = auditTable?.AuditDateFieldName;
  }

  private async Task UpdateAuditEntityTableColumnsAsync(
    AuditEntityTable auditEntityTable,
    List<AuditTableColumn> auditTableColumns)
  {
    await this.DeleteAuditEntityTableColumnsAsync(auditEntityTable, auditTableColumns);
    this.UpdateAuditEntityTableColumnsData(auditEntityTable, auditTableColumns);
    this.AddNewAuditEntityTableColumns(auditEntityTable, auditTableColumns);
  }

  private async Task DeleteAuditEntityTableColumnsAsync(
    AuditEntityTable auditEntityTable,
    List<AuditTableColumn> auditTableColumns)
  {
    IEnumerable<string> auditTableColumnNames = auditTableColumns.Select<AuditTableColumn, string>((Func<AuditTableColumn, string>) (x => x.Name));
    foreach (AuditEntityTableColumn auditEntityTableColumnToDelete in auditEntityTable.AuditEntityTableColumns.ToList<AuditEntityTableColumn>().Where<AuditEntityTableColumn>((Func<AuditEntityTableColumn, bool>) (x => !auditTableColumnNames.Contains<string>(x.ColumnName))))
    {
      if (await this.ValidateExistenceOfAuditDataLogLinkedToTableColumnAsync(auditEntityTableColumnToDelete))
        this.UpdateAuditEntityTableColumnEnabledProperty(auditEntityTableColumnToDelete, false);
      else
        await this.DeleteAuditEntityTableColumnPhysicallyAsync(auditEntityTableColumnToDelete);
    }
  }

  private Task<bool> ValidateExistenceOfAuditDataLogLinkedToTableColumnAsync(
    AuditEntityTableColumn auditEntityTableColumn)
  {
    return (Task<bool>) this._auditTransactionEntityTableColumnLogRepository.AnyAsync((Expression<Func<AuditTransactionEntityTableColumnLog, bool>>) (x => x.AuditEntityTableColumnId == auditEntityTableColumn.AuditEntityTableColumnId));
  }

  private void UpdateAuditEntityTableColumnEnabledProperty(
    AuditEntityTableColumn auditEntityTableColumn,
    bool enabled)
  {
    if (auditEntityTableColumn == null)
      return;
    auditEntityTableColumn.Enabled = enabled;
  }

  private void AddNewAuditEntityTableColumns(
    AuditEntityTable auditEntityTable,
    List<AuditTableColumn> auditTableColumns)
  {
    IEnumerable<string> auditEntityTableColumnNames = auditEntityTable.AuditEntityTableColumns.Select<AuditEntityTableColumn, string>((Func<AuditEntityTableColumn, string>) (x => x.ColumnName));
    List<AuditTableColumn> list = auditTableColumns.Where<AuditTableColumn>((Func<AuditTableColumn, bool>) (x => !auditEntityTableColumnNames.Contains<string>(x.Name))).ToList<AuditTableColumn>();
    this.AddAuditEntityTableColumnsToAuditEntityTable(auditEntityTable, list);
  }

  private void UpdateAuditEntityTableColumnsData(
    AuditEntityTable auditEntityTable,
    List<AuditTableColumn> auditTableColumns)
  {
    foreach (AuditTableColumn auditTableColumn in auditTableColumns)
    {
      AuditEntityTableColumn entityTableColumn = this.GetAuditEntityTableColumn(auditEntityTable.AuditEntityTableColumns.ToList<AuditEntityTableColumn>(), auditTableColumn.Name);
      if (entityTableColumn != null)
        this.UpdateAuditEntityColumnData(entityTableColumn, auditTableColumn);
    }
  }

  private AuditEntityTableColumn GetAuditEntityTableColumn(
    List<AuditEntityTableColumn> auditTableColumns,
    string name)
  {
    return auditTableColumns.FirstOrDefault<AuditEntityTableColumn>((Func<AuditEntityTableColumn, bool>) (x => string.Equals(x.ColumnName, name, StringComparison.OrdinalIgnoreCase)));
  }

  private void UpdateAuditEntityColumnData(
    AuditEntityTableColumn auditEntityTableColumn,
    AuditTableColumn auditTableColumn)
  {
    if (auditEntityTableColumn == null)
      return;
    auditEntityTableColumn.ColumnDotNetType = auditTableColumn?.NetType;
    auditEntityTableColumn.ColumnTsqltype = auditTableColumn?.SqlType;
    auditEntityTableColumn.DisplayOrder = (int?) auditTableColumn?.DisplayOrder;
    auditEntityTableColumn.ColumnLabel = auditTableColumn?.Label;
    auditEntityTableColumn.MasterTableName = auditTableColumn?.MasterTableName;
    auditEntityTableColumn.MasterTablePkname = auditTableColumn?.MasterTablePKName;
    auditEntityTableColumn.MasterTableDescColumnName = auditTableColumn?.MasterTableDescColumnName;
  }
}
