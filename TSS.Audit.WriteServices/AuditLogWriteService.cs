// Decompiled with JetBrains decompiler
// Type: TSS.Audit.WriteServices.AuditLogWriteService
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
using TSS.Audit.DTOs.Response;
using TSS.Audit.Persistence;
using TSS.Audit.Persistence.Commands;
using TSS.Audit.Resources;
using TSS.Audit.WriteServices.Settings;
using TSS.Audit.WriteServices.Validation;

#nullable disable
namespace TSS.Audit.WriteServices;

public class AuditLogWriteService : IDisposable
{
  private readonly IAuditUnitOfWork _unitOfWork;
  private readonly IAuditRepository<AuditProcessDescription> _auditProcessRepository;
  private readonly IAuditRepository<TSS.Audit.Domain.AuditEntity> _auditEntityRepository;
  private readonly IAuditRepository<AuditProcessLog> _auditProcessLogRepository;
  private readonly IValidationService _validationService;
  private readonly WriteServiceSettings _writeServiceSettings;

  public AuditLogWriteService(
    IAuditUnitOfWork unitOfWork,
    IAuditRepository<AuditProcessDescription> auditProcessRepository,
    IAuditRepository<TSS.Audit.Domain.AuditEntity> auditEntityRepository,
    IAuditRepository<AuditProcessLog> auditProcessLogRepository,
    IValidationService validationService,
    WriteServiceSettings writeServiceSettings)
  {
    this._unitOfWork = unitOfWork;
    this._auditProcessRepository = auditProcessRepository;
    this._auditEntityRepository = auditEntityRepository;
    this._auditProcessLogRepository = auditProcessLogRepository;
    this._validationService = validationService;
    this._writeServiceSettings = writeServiceSettings;
  }

  public async Task<WriteOperationResponse> RegisterAsync(string appCode, AuditLog newAuditAppLog)
  {
    WriteOperationResponse auditProcessLogAsync = await this.ValidateDataToRegisterNewAuditProcessLogAsync(appCode, newAuditAppLog);
    if (!auditProcessLogAsync.IsCorrect)
      return auditProcessLogAsync;
    await this.AddNewAuditProcessLogAsync(appCode, newAuditAppLog);
    await this.CommitTransactionAsync();
    return WriteOperationBuilder.CreateWriteOperationResponse();
  }

  public void Dispose() => this._unitOfWork?.Dispose();

  private async Task<WriteOperationResponse> ValidateDataToRegisterNewAuditProcessLogAsync(
    string appCode,
    AuditLog auditAppLog)
  {
    WriteOperationResponse newAuditProcessLog = this.ValidateParametersFormatToRegisterNewNewAuditProcessLog(appCode, auditAppLog);
    return !newAuditProcessLog.IsCorrect ? newAuditProcessLog : await this.ValidateDataIntegrityToRegisterNewNewAuditProcessLogAsync(appCode, auditAppLog);
  }

  private WriteOperationResponse ValidateParametersFormatToRegisterNewNewAuditProcessLog(
    string appCode,
    AuditLog auditAppLog)
  {
    if (auditAppLog == null || string.IsNullOrWhiteSpace(appCode))
      return WriteOperationBuilder.CreateWriteOperationResponse(Messages.BadParameter, OperationStatus.Invalid);
    OperationResponse operationResponse = this._validationService.Validate<AuditLog>(auditAppLog);
    return operationResponse.IsCorrect ? WriteOperationBuilder.CreateWriteOperationResponse() : operationResponse.WithStatus(OperationStatus.Invalid);
  }

  private async Task<WriteOperationResponse> ValidateDataIntegrityToRegisterNewNewAuditProcessLogAsync(
    string appCode,
    AuditLog auditAppLog)
  {
    return await this.GetAuditProcessDescriptionAsync(appCode, auditAppLog.ProcessName, auditAppLog.Module) == null ? WriteOperationBuilder.CreateWriteOperationResponse(Messages.ProcessNotExists, OperationStatus.NotFound) : await this.ValidateAuditEntitiesExistenceAsync(appCode, auditAppLog.Entities);
  }

  private Task<AuditProcessDescription> GetAuditProcessDescriptionAsync(
    string appCode,
    string processName,
    string module)
  {
    return this._auditProcessRepository.FirstOrDefaultAsync((Expression<Func<AuditProcessDescription, bool>>) (x => string.Equals(x.ApplicationCode, appCode, StringComparison.OrdinalIgnoreCase) && string.Equals(x.Name, processName, StringComparison.OrdinalIgnoreCase) && string.Equals(x.Module, module, StringComparison.OrdinalIgnoreCase)));
  }

  private Task CommitTransactionAsync() => (Task) this._unitOfWork.CommitAsync();

  private async Task<WriteOperationResponse> ValidateAuditEntitiesExistenceAsync(
    string appCode,
    List<AuditEntityLog> auditEntityLogs)
  {
    foreach (AuditEntityLog auditEntityLog in auditEntityLogs)
    {
      WriteOperationResponse operationResponse = await this.ValidateAuditEntityExistenceAsync(appCode, auditEntityLog);
      if (!operationResponse.IsCorrect)
        return operationResponse;
    }
    return WriteOperationBuilder.Correct();
  }

  private async Task<WriteOperationResponse> ValidateAuditEntityExistenceAsync(
    string appCode,
    AuditEntityLog auditEntityLog)
  {
    TSS.Audit.Domain.AuditEntity auditEntityAsync = await this.GetAuditEntityAsync(appCode, auditEntityLog.Entity, auditEntityLog.Module, new string[1]
    {
      "AuditEntityTables.AuditEntityTableColumns"
    });
    return auditEntityAsync != null ? (this._writeServiceSettings.ValidateAuditEntityTablesExistence ? this.ValidateAuditEntityTablesExistence(auditEntityAsync, auditEntityLog) : WriteOperationBuilder.Correct()) : WriteOperationBuilder.WithMessage(string.Format(Messages.EntityNotExistsFormat, (object) auditEntityLog.Entity, (object) auditEntityLog.Module), OperationStatus.NotFound);
  }

  private Task<TSS.Audit.Domain.AuditEntity> GetAuditEntityAsync(
    string appCode,
    string entityName,
    string module,
    string[] includes = null)
  {
    return this._auditEntityRepository.FirstOrDefaultAsync((Expression<Func<TSS.Audit.Domain.AuditEntity, bool>>) (x => x.ApplicationCode.Equals(appCode, StringComparison.OrdinalIgnoreCase) && x.Name.Equals(entityName, StringComparison.OrdinalIgnoreCase) && x.Module.Equals(module, StringComparison.OrdinalIgnoreCase)), includes: includes);
  }

  private WriteOperationResponse ValidateAuditEntityTablesExistence(
    TSS.Audit.Domain.AuditEntity auditEntity,
    AuditEntityLog auditEntityLog)
  {
    foreach (AuditTableLog table in auditEntityLog.Tables)
    {
      WriteOperationResponse operationResponse = this.ValidateAuditEntityTableExistence(auditEntity, table);
      if (!operationResponse.IsCorrect)
        return operationResponse;
    }
    return WriteOperationBuilder.Correct();
  }

  private WriteOperationResponse ValidateAuditEntityTableExistence(
    TSS.Audit.Domain.AuditEntity auditEntity,
    AuditTableLog tableLog)
  {
    AuditEntityTable auditEntityTable = this.GetAuditEntityTable(auditEntity, tableLog.TableName);
    if (auditEntityTable == null)
      return WriteOperationBuilder.WithMessage(string.Format(Messages.TableNotExistsFormat, (object) tableLog.TableName), OperationStatus.NotFound);
    return !this._writeServiceSettings.ValidateAuditEntityTableColumnsExistence ? WriteOperationBuilder.Correct() : this.ValidateAuditEntityTableColumnsExistence(auditEntityTable, tableLog);
  }

  private AuditEntityTable GetAuditEntityTable(TSS.Audit.Domain.AuditEntity auditEntity, string tableName)
  {
    if (auditEntity == null)
      return (AuditEntityTable) null;
    ICollection<AuditEntityTable> auditEntityTables = auditEntity.AuditEntityTables;
    return auditEntityTables == null ? (AuditEntityTable) null : auditEntityTables.FirstOrDefault<AuditEntityTable>((Func<AuditEntityTable, bool>) (x => string.Equals(x.TableName, tableName, StringComparison.OrdinalIgnoreCase)));
  }

  private WriteOperationResponse ValidateAuditEntityTableColumnsExistence(
    AuditEntityTable auditEntityTable,
    AuditTableLog auditTableLog)
  {
    foreach (AuditColumnLog column in auditTableLog.Columns)
    {
      WriteOperationResponse operationResponse = this.ValidateAuditEntityTableColumnExistence(auditEntityTable, column);
      if (!operationResponse.IsCorrect)
        return operationResponse;
    }
    return WriteOperationBuilder.Correct();
  }

  private WriteOperationResponse ValidateAuditEntityTableColumnExistence(
    AuditEntityTable auditEntityTable,
    AuditColumnLog auditColumnLog)
  {
    return this.GetAuditEntityTableColumn(auditEntityTable, auditColumnLog.ColumnName) == null ? WriteOperationBuilder.WithMessage(string.Format(Messages.ColumnNotExistsFormat, (object) auditColumnLog.ColumnName), OperationStatus.NotFound) : WriteOperationBuilder.Correct();
  }

  private AuditEntityTableColumn GetAuditEntityTableColumn(
    AuditEntityTable auditEntityTable,
    string columnName)
  {
    if (auditEntityTable == null)
      return (AuditEntityTableColumn) null;
    ICollection<AuditEntityTableColumn> entityTableColumns = auditEntityTable.AuditEntityTableColumns;
    return entityTableColumns == null ? (AuditEntityTableColumn) null : entityTableColumns.FirstOrDefault<AuditEntityTableColumn>((Func<AuditEntityTableColumn, bool>) (x => string.Equals(x.ColumnName, columnName, StringComparison.OrdinalIgnoreCase)));
  }

  private async Task AddNewAuditProcessLogAsync(string appCode, AuditLog auditLog)
  {
    AuditProcessLog processLogFromDtoAsync = await this.CreateAuditProcessLogFromDtoAsync(appCode, auditLog);
    if (processLogFromDtoAsync == null)
      return;
    await (Task) this._auditProcessLogRepository.AddAsync(processLogFromDtoAsync);
  }

  private async Task<AuditProcessLog> CreateAuditProcessLogFromDtoAsync(
    string appCode,
    AuditLog auditLog)
  {
    AuditProcessDescription descriptionAsync = await this.GetAuditProcessDescriptionAsync(appCode, auditLog.ProcessName, auditLog.Module);
    AuditProcessLog auditProcessLog = new AuditProcessLog(auditLog.TenantId, descriptionAsync.AuditProcessDescriptionId, auditLog.EndProcess)
    {
      BeginProcessTimestamp = auditLog.BeginProcess,
      AuditUserDescription = auditLog.AuditUserDescription,
      AuditUserIdentifier = auditLog.AuditUserIdentifier
    };
    await this.AddAuditTransactionEntitiesToAuditProcessLogAsync(descriptionAsync.ApplicationCode, auditProcessLog, auditLog.Entities);
    return auditProcessLog;
  }

  private async Task AddAuditTransactionEntitiesToAuditProcessLogAsync(
    string appCode,
    AuditProcessLog auditProcessLog,
    List<AuditEntityLog> auditEntityLogs)
  {
    foreach (AuditEntityLog auditEntityLog in auditEntityLogs)
      await this.AddAuditTransactionEntityToAuditProcessLogAsync(appCode, auditProcessLog, auditEntityLog);
  }

  private async Task AddAuditTransactionEntityToAuditProcessLogAsync(
    string appCode,
    AuditProcessLog auditProcessLog,
    AuditEntityLog auditEntityLog)
  {
    AuditTransactionEntityLog entityLogFromDtoAsync = await this.CreateAuditTransactionEntityLogFromDtoAsync(appCode, auditEntityLog);
    if (entityLogFromDtoAsync == null)
      return;
    auditProcessLog.RegisterTransactionEntity(entityLogFromDtoAsync);
  }

  private async Task<AuditTransactionEntityLog> CreateAuditTransactionEntityLogFromDtoAsync(
    string appCode,
    AuditEntityLog auditEntityLog)
  {
    TSS.Audit.Domain.AuditEntity auditEntityAsync = await this.GetAuditEntityAsync(appCode, auditEntityLog.Entity, auditEntityLog.Module, new string[1]
    {
      "AuditEntityTables.AuditEntityTableColumns"
    });
    AuditTransactionEntityLog auditTransactionEntityLog = new AuditTransactionEntityLog(auditEntityAsync?.AuditEntityId, auditEntityLog.AuditBy, auditEntityLog.AuditDate, auditEntityLog.IsMainEntity);
    this.AddAuditTransactionEntityTablesToAuditTransactionEntity(auditTransactionEntityLog, auditEntityLog.Tables, auditEntityAsync);
    return auditTransactionEntityLog;
  }

  private void AddAuditTransactionEntityTablesToAuditTransactionEntity(
    AuditTransactionEntityLog auditTransactionEntityLog,
    List<AuditTableLog> auditTableLogs,
    TSS.Audit.Domain.AuditEntity auditEntity)
  {
    foreach (AuditTableLog auditTableLog in auditTableLogs)
      this.AddAuditTransactionEntityTableToAuditTransactionEntity(auditTransactionEntityLog, auditTableLog, auditEntity);
  }

  private void AddAuditTransactionEntityTableToAuditTransactionEntity(
    AuditTransactionEntityLog auditTransactionEntityLog,
    AuditTableLog auditTableLog,
    TSS.Audit.Domain.AuditEntity auditEntity)
  {
    AuditTransactionEntityTableLog entityTableLogFromDto = this.CreateAuditTransactionEntityTableLogFromDto(auditTableLog, auditEntity);
    if (entityTableLogFromDto == null)
      return;
    auditTransactionEntityLog.RegisterTransactionEntityTable(entityTableLogFromDto);
  }

  private AuditTransactionEntityTableLog CreateAuditTransactionEntityTableLogFromDto(
    AuditTableLog auditTableLog,
    TSS.Audit.Domain.AuditEntity auditEntity)
  {
    AuditEntityTable auditEntityTable = this.GetAuditEntityTable(auditEntity, auditTableLog.TableName);
    if (auditEntityTable == null)
      return (AuditTransactionEntityTableLog) null;
    AuditTransactionEntityTableLog auditTransactionEntityTableLog = new AuditTransactionEntityTableLog(auditEntityTable?.AuditEntityTableId, auditTableLog.Operation, auditTableLog.Timestamp, auditTableLog.IsMainTable)
    {
      IdColumnValue = auditTableLog.IdColumnValue,
      KeyFieldValue = auditTableLog.KeyColumnValue,
      UpdateMask = auditTableLog.UpdateMask,
      RowVersion = auditTableLog.RowVersion
    };
    this.AddAuditTransactionEntityTableColumnsToAuditTransactionEntityTable(auditTransactionEntityTableLog, auditTableLog.Columns, auditEntityTable);
    return auditTransactionEntityTableLog;
  }

  private void AddAuditTransactionEntityTableColumnsToAuditTransactionEntityTable(
    AuditTransactionEntityTableLog auditTransactionEntityTableLog,
    List<AuditColumnLog> auditColumnLogs,
    AuditEntityTable auditEntityTable)
  {
    foreach (AuditColumnLog auditColumnLog in auditColumnLogs)
      this.AddAuditTransactionEntityTableColumnToAuditTransactionEntityTable(auditTransactionEntityTableLog, auditColumnLog, auditEntityTable);
  }

  private void AddAuditTransactionEntityTableColumnToAuditTransactionEntityTable(
    AuditTransactionEntityTableLog auditTransactionEntityTableLog,
    AuditColumnLog auditColumnLog,
    AuditEntityTable auditEntityTable)
  {
    AuditTransactionEntityTableColumnLog entityTableColumnLog = this.GetAuditTransactionEntityTableColumnLog(auditColumnLog, auditEntityTable);
    if (entityTableColumnLog == null)
      return;
    auditTransactionEntityTableLog.RegisterTransactionEntityTableColumn(entityTableColumnLog);
  }

  private AuditTransactionEntityTableColumnLog GetAuditTransactionEntityTableColumnLog(
    AuditColumnLog auditColumnLog,
    AuditEntityTable auditEntityTable)
  {
    AuditEntityTableColumn entityTableColumn = this.GetAuditEntityTableColumn(auditEntityTable, auditColumnLog.ColumnName);
    return entityTableColumn != null ? new AuditTransactionEntityTableColumnLog(entityTableColumn.AuditEntityTableColumnId, auditColumnLog.Previous, auditColumnLog.Current) : (AuditTransactionEntityTableColumnLog) null;
  }
}
