// Decompiled with JetBrains decompiler
// Type: TSS.Audit.ReadServices.AuditLogReadService
// Assembly: TSS.Audit.ReadServices, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A10258C3-6446-4BF1-813B-45DC267811FE
// Assembly location: D:\Baufest\GIT\TSS.Audit.WebApi\TSS.Audit.ReadServices.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TSS.Audit.Common;
using TSS.Audit.Common.Extensions;
using TSS.Audit.DTOs;
using TSS.Audit.DTOs.Core;
using TSS.Audit.DTOs.Request;
using TSS.Audit.DTOs.Response;
using TSS.Audit.Mapping;
using TSS.Audit.Persistence.Queries;
using TSS.Audit.QueryModel;
using TSS.Audit.ReadServices.Settings;
using TSS.Core.Persistence.Reading;

#nullable disable
namespace TSS.Audit.ReadServices;

public class AuditLogReadService
{
  private readonly IAuditMapper _mapper;
  private readonly IAuditQueryDataAccess<AuditLogData> _auditLogDataQuery;
  private readonly ReadServiceSettings _readServiceSettings;
  private const bool DEFAULT_AUDIT_LOG_ASCENDING_SORT_DIRECTION = false;
  private const string DEFAULT_AUDIT_LOG_SORT_FIELD_NAME_IN_DATABASE = "EndProcessTimestamp";
  private const string DEFAULT_AUDIT_LOG_SORT_FIELD_NAME_IN_ENTITY = "EndProcess";

  public AuditLogReadService(
    IAuditQueryDataAccess<AuditLogData> auditLogDataQuery,
    IAuditMapper mapper,
    ReadServiceSettings readServiceSettings)
  {
    this._mapper = mapper;
    this._auditLogDataQuery = auditLogDataQuery;
    this._readServiceSettings = readServiceSettings;
  }

  public async Task<PaginatedResponse<AuditLogResponse>> FindAuditDataLogAsync(
    Guid tenantId,
    string appCode,
    AuditLogSearch auditLogSearch,
    SortDescriptor sortDescriptor)
  {
    IPagedResult<string> auditProcessLogIdStringList = await this.GetAuditProcessLogIdsForFindAuditLogAsync(this.CreateSqlStatementForFindAuditLog(), this.CreateSqlWhereStamentForFindAuditLog(tenantId, appCode, auditLogSearch), this.CreateSqlOrderByStamentForFindAuditLog(sortDescriptor), (int?) auditLogSearch?.Page, (int?) auditLogSearch?.PageSize);
    if (!((IEnumerable<string>) auditProcessLogIdStringList.Items).Any<string>())
      return new PaginatedResponse<AuditLogResponse>((int?) auditLogSearch?.Page, (int?) auditLogSearch?.PageSize, new int?(0));
    List<AuditLogData> list = (await this.GetAuditLogData((IList<string>) auditProcessLogIdStringList.Items)).ToList<AuditLogData>();
    return list.Any<AuditLogData>() ? this.CreatePaginatedResponseFromAuditLogData(list, sortDescriptor, new int?(auditProcessLogIdStringList.Page), new int?(auditProcessLogIdStringList.PageSize), new int?(auditProcessLogIdStringList.Total)) : new PaginatedResponse<AuditLogResponse>((int?) auditLogSearch?.Page, (int?) auditLogSearch?.PageSize, new int?(0));
  }

  public async Task<IList<AuditLogDataExport>> GetAuditLogDataToExportAsync(
    Guid tenantId,
    string appCode,
    AuditLogSearch auditLogSearch)
  {
    IPagedResult<string> findAuditLogAsync = await this.GetAuditProcessLogIdsForFindAuditLogAsync(this.CreateSqlStatementForFindAuditLog(), this.CreateSqlWhereStamentForFindAuditLog(tenantId, appCode, auditLogSearch), this.CreateSqlOrderByStamentForFindAuditLog(), new int?(1), this._readServiceSettings.MaxAuditLogRowsQuantityToExport);
    return !((IEnumerable<string>) findAuditLogAsync.Items).Any<string>() ? (IList<AuditLogDataExport>) new List<AuditLogDataExport>() : (IList<AuditLogDataExport>) this._mapper.Map<List<AuditLogData>, List<AuditLogDataExport>>((await this.GetAuditLogData((IList<string>) findAuditLogAsync.Items)).ToList<AuditLogData>());
  }

  public async Task<IList<AuditLogDataExport>> GetAuditLogDataToExportAsync(long auditLogProcessId)
  {
    return (IList<AuditLogDataExport>) this._mapper.Map<List<AuditLogData>, List<AuditLogDataExport>>((await (Task<IEnumerable<AuditLogData>>) this._auditLogDataQuery.ListAsync((Expression<Func<AuditLogData, bool>>) (x => x.AuditProcessLogId == auditLogProcessId))).ToList<AuditLogData>());
  }

  public async Task<AuditTableLogSnapshotResponse> GetLastAuditTableLogSnapshotAsync(
    string appCode,
    AuditTableLogSnapshotRequest auditLogSnapshotRequest)
  {
    return !this.ValidateGetLastAuditTableLogSnapshotParameters(appCode, auditLogSnapshotRequest) ? (AuditTableLogSnapshotResponse) null : await this.GetLastAuditTableLogsSnapshotAsync(appCode, auditLogSnapshotRequest);
  }

  private string CreateSqlStatementForFindAuditLog()
  {
    return "\r\n                    SELECT DISTINCT\r\n\t                    APL.[AuditProcessLogId],\r\n                        APD.[Name],\r\n                        APL.[AuditUserIdentifier],\r\n                        APL.[EndProcessTimestamp]\r\n                    FROM \r\n\t                    dbo.AuditProcessLog APL\r\n                    INNER JOIN\r\n\t                    dbo.AuditProcessDescription APD ON APD.[AuditProcessDescriptionId]=APL.[AuditProcessDescriptionId] AND APD.[Enabled]=1\r\n                    INNER JOIN\r\n\t                    dbo.AuditTransactionEntityLog ATEL ON ATEL.[AuditProcessLogId]=APL.[AuditProcessLogId]\r\n                    INNER JOIN\r\n\t                    dbo.AuditEntity AE ON ATEL.[AuditEntityId]=AE.[AuditEntityId] AND AE.[Enabled]=1\r\n                    INNER JOIN\r\n\t                    dbo.AuditTransactionEntityTableLog ATETL ON ATETL.[AuditTransactionEntityId]=ATEL.[AuditTransactionEntityId]\r\n                    INNER JOIN\r\n\t                    dbo.AuditEntityTable AET ON AET.[AuditEntityTableId]=ATETL.[AuditEntityTableId] AND AET.[Enabled]=1\r\n                    INNER JOIN\r\n\t                    dbo.AuditTransactionEntityTableColumnLog ATETCL ON ATETCL.[AuditTransactionEntityTableId]=ATETL.[AuditTransactionEntityTableId]\r\n                    INNER JOIN\r\n\t                    dbo.AuditEntityTableColumn AETC ON AETC.[AuditEntityTableColumnId]=ATETCL.[AuditEntityTableColumnId] AND AETC.[Enabled]=1";
  }

  public Dictionary<string, object[]> CreateSqlWhereStamentForFindAuditLog(
    Guid tenantId,
    string appCode,
    AuditLogSearch auditLogSearch)
  {
    Dictionary<string, object[]> whereStatements = new Dictionary<string, object[]>()
    {
      {
        "(APL.[TenantId]=@0)",
        new object[1]{ (object) tenantId }
      },
      {
        "(APD.[ApplicationCode]=@0)",
        new object[1]{ (object) appCode }
      }
    };
    if (auditLogSearch == null)
      return whereStatements;
    if (!string.IsNullOrWhiteSpace(auditLogSearch.Process))
      whereStatements.Add("(APD.[Name]=@0)", new object[1]
      {
        (object) auditLogSearch.Process.Trim()
      });
    if (!string.IsNullOrWhiteSpace(auditLogSearch.ProcessModule))
      whereStatements.Add("(APD.[Module]=@0)", new object[1]
      {
        (object) auditLogSearch.ProcessModule.Trim()
      });
    if (!string.IsNullOrWhiteSpace(auditLogSearch.Entity))
      whereStatements.Add("(AE.[Name]=@0)", new object[1]
      {
        (object) auditLogSearch.Entity.Trim()
      });
    if (!string.IsNullOrWhiteSpace(auditLogSearch.EntityModule))
      whereStatements.Add("(AE.[Module]=@0)", new object[1]
      {
        (object) auditLogSearch.EntityModule.Trim()
      });
    if (!string.IsNullOrWhiteSpace(auditLogSearch.EntityKeyValue))
      whereStatements.Add("(ATETL.[IsMain]=1 AND ATETL.[KeyFieldValue]=@0)", new object[1]
      {
        (object) auditLogSearch.EntityKeyValue.Trim()
      });
    Constants.TableOperation? actions = auditLogSearch.Actions;
    if (actions.HasValue)
    {
      Dictionary<string, object[]> dictionary = whereStatements;
      object[] objArray = new object[1];
      actions = auditLogSearch.Actions;
      objArray[0] = (object) (byte) actions.Value;
      dictionary.Add("(ATETL.[operation]=@0)", objArray);
    }
    if (!string.IsNullOrWhiteSpace(auditLogSearch.By))
      whereStatements.Add("(APL.[AuditUserDescription]=@0 OR APL.[AuditUserIdentifier]=@0)", new object[1]
      {
        (object) auditLogSearch.By.Trim()
      });
    DateTime? nullable = auditLogSearch.From;
    if (nullable.HasValue)
    {
      Dictionary<string, object[]> dictionary = whereStatements;
      object[] objArray = new object[1];
      nullable = auditLogSearch.From;
      objArray[0] = (object) nullable.Value;
      dictionary.Add("(APL.[EndProcessTimeStamp]>=@0)", objArray);
    }
    nullable = auditLogSearch.To;
    if (nullable.HasValue)
    {
      Dictionary<string, object[]> dictionary = whereStatements;
      object[] objArray = new object[1];
      nullable = auditLogSearch.To;
      objArray[0] = (object) nullable.Value;
      dictionary.Add("(APL.[EndProcessTimeStamp]<=@0)", objArray);
    }
    if (auditLogSearch.Fields != null && auditLogSearch.Fields.Any<AuditFieldSearch>())
      this.AddFieldsWhereStatements(whereStatements, auditLogSearch.Fields);
    return whereStatements;
  }

  public string CreateSqlOrderByStamentForFindAuditLog(SortDescriptor sortDescriptor = null)
  {
    string str = this.GetAuditLogFieldName(sortDescriptor?.FieldName, true);
    bool flag = sortDescriptor != null && sortDescriptor.Ascending;
    if (string.IsNullOrWhiteSpace(str))
      str = "EndProcessTimestamp";
    return $"{$"{str}"} {(flag ? "ASC" : "DESC")}";
  }

  private string GetAuditLogFieldName(string fieldName, bool getFieldNameInDatabase = false)
  {
    if (string.IsNullOrWhiteSpace(fieldName) || this._readServiceSettings?.AuditLogFieldNameEquivalence == null || !this._readServiceSettings.AuditLogFieldNameEquivalence.ContainsKey(fieldName))
      return (string) null;
    AuditLogFieldNameEquivalence fieldNameEquivalence = this._readServiceSettings.AuditLogFieldNameEquivalence[fieldName];
    return !getFieldNameInDatabase ? (!string.IsNullOrWhiteSpace(fieldNameEquivalence.NameInEntity) ? fieldNameEquivalence.NameInEntity : fieldName) : (!string.IsNullOrWhiteSpace(fieldNameEquivalence.NameInDababase) ? fieldNameEquivalence.NameInDababase : fieldName);
  }

  private Task<IPagedResult<string>> GetAuditProcessLogIdsForFindAuditLogAsync(
    string sqlStatement,
    Dictionary<string, object[]> whereStatements = null,
    string orderByStatement = null,
    int? page = null,
    int? pageSize = null)
  {
    return this._auditLogDataQuery.ListPagedAsync<string>(sqlStatement, whereStatements, orderByStatement, page, pageSize);
  }

  private Task<IEnumerable<AuditLogData>> GetAuditLogData(IList<string> auditProcessLogIdList)
  {
    IList<long> auditProcessLogIds = this.ConvertStringListToLongList(auditProcessLogIdList);
    return (Task<IEnumerable<AuditLogData>>) this._auditLogDataQuery.ListAsync((Expression<Func<AuditLogData, bool>>) (x => auditProcessLogIds.Contains(x.AuditProcessLogId)));
  }

  private IList<long> ConvertStringListToLongList(IList<string> stringList)
  {
    return (IList<long>) stringList.Select<string, long>(new Func<string, long>(long.Parse)).ToList<long>();
  }

  public PaginatedResponse<AuditLogResponse> CreatePaginatedResponseFromAuditLogData(
    List<AuditLogData> auditLogsData,
    SortDescriptor sortDescriptor,
    int? page,
    int? pageSize,
    int? totalItems)
  {
    List<AuditLogResponse> auditLogResponseList = this.SortAuditLog(this.CreateAuditLogsFromAuditLogsData(auditLogsData), sortDescriptor);
    return new PaginatedResponse<AuditLogResponse>(page, pageSize, new int?(totalItems ?? auditLogResponseList.Count))
    {
      Items = auditLogResponseList
    };
  }

  private List<AuditLogResponse> SortAuditLog(
    List<AuditLogResponse> auditLogResponseList,
    SortDescriptor sortDescriptor)
  {
    string propertyName = this.GetAuditLogFieldName(sortDescriptor?.FieldName);
    int num = sortDescriptor != null ? (sortDescriptor.Ascending ? 1 : 0) : 0;
    if (string.IsNullOrWhiteSpace(propertyName))
      propertyName = "EndProcess";
    return num != 0 ? auditLogResponseList.AsQueryable<AuditLogResponse>().OrderBy<AuditLogResponse>(propertyName).ToList<AuditLogResponse>() : auditLogResponseList.AsQueryable<AuditLogResponse>().OrderByDescending<AuditLogResponse>(propertyName).ToList<AuditLogResponse>();
  }

  private List<AuditLogResponse> CreateAuditLogsFromAuditLogsData(List<AuditLogData> auditLogsData)
  {
    return this.ConvertAuditProcessLogGroupsToAuditLogResponses(this.GroupAuditProcessLog(auditLogsData));
  }

  private IEnumerable<IGrouping<long, AuditLogData>> GroupAuditProcessLog(
    List<AuditLogData> auditLogsData)
  {
    return auditLogsData.GroupBy<AuditLogData, long>((Func<AuditLogData, long>) (auditLogDataRow => auditLogDataRow.AuditProcessLogId)).Select<IGrouping<long, AuditLogData>, IGrouping<long, AuditLogData>>((Func<IGrouping<long, AuditLogData>, IGrouping<long, AuditLogData>>) (auditProcessLogGroup => auditProcessLogGroup));
  }

  private List<AuditLogResponse> ConvertAuditProcessLogGroupsToAuditLogResponses(
    IEnumerable<IGrouping<long, AuditLogData>> auditProcessLogGroups)
  {
    List<AuditLogResponse> auditLogResponses = new List<AuditLogResponse>();
    foreach (IGrouping<long, AuditLogData> auditProcessLogGroup in auditProcessLogGroups)
    {
      AuditLogResponse auditLogResponse = this.ConvertAuditProcessLogGroupToAuditLogResponse(auditProcessLogGroup);
      if (auditLogResponse != null)
        auditLogResponses.Add(auditLogResponse);
    }
    return auditLogResponses;
  }

  private AuditLogResponse ConvertAuditProcessLogGroupToAuditLogResponse(
    IGrouping<long, AuditLogData> auditProcessLogGroup)
  {
    AuditLogResponse auditLogResponse = this._mapper.Map<AuditLogData, AuditLogResponse>(auditProcessLogGroup != null ? auditProcessLogGroup.FirstOrDefault<AuditLogData>() : (AuditLogData) null);
    if (auditLogResponse == null)
      return (AuditLogResponse) null;
    auditLogResponse.Entities = this.CreateAuditEntityLogsFromAuditLogsData(auditProcessLogGroup);
    return auditLogResponse;
  }

  private List<AuditEntityLogResponse> CreateAuditEntityLogsFromAuditLogsData(
    IGrouping<long, AuditLogData> auditProcessLogGroup)
  {
    return this.ConvertAuditTransactionEntityLogGroupsToAuditEntityLogs(this.GroupAuditTransactionEntityLog(auditProcessLogGroup));
  }

  private IEnumerable<IGrouping<long, AuditLogData>> GroupAuditTransactionEntityLog(
    IGrouping<long, AuditLogData> auditProcessLogGroup)
  {
    return auditProcessLogGroup.GroupBy<AuditLogData, long>((Func<AuditLogData, long>) (auditLogDataRow => auditLogDataRow.AuditTransactionEntityId)).Select<IGrouping<long, AuditLogData>, IGrouping<long, AuditLogData>>((Func<IGrouping<long, AuditLogData>, IGrouping<long, AuditLogData>>) (auditEntityLogGroup => auditEntityLogGroup));
  }

  private List<AuditEntityLogResponse> ConvertAuditTransactionEntityLogGroupsToAuditEntityLogs(
    IEnumerable<IGrouping<long, AuditLogData>> auditTransactionEntityLogGroups)
  {
    List<AuditEntityLogResponse> auditEntityLogs = new List<AuditEntityLogResponse>();
    foreach (IGrouping<long, AuditLogData> transactionEntityLogGroup in auditTransactionEntityLogGroups)
    {
      AuditEntityLogResponse auditEntityLog = this.ConvertAuditTransactionEntityLogGroupToAuditEntityLog(transactionEntityLogGroup);
      if (auditEntityLog != null)
        auditEntityLogs.Add(auditEntityLog);
    }
    return auditEntityLogs;
  }

  private AuditEntityLogResponse ConvertAuditTransactionEntityLogGroupToAuditEntityLog(
    IGrouping<long, AuditLogData> auditTransactionEntityLogGroup)
  {
    AuditEntityLogResponse auditEntityLog = this._mapper.Map<AuditLogData, AuditEntityLogResponse>(auditTransactionEntityLogGroup != null ? auditTransactionEntityLogGroup.FirstOrDefault<AuditLogData>() : (AuditLogData) null);
    if (auditEntityLog == null)
      return (AuditEntityLogResponse) null;
    auditEntityLog.Tables = this.CreateAuditTableLogsFromAuditLogsData(auditTransactionEntityLogGroup);
    auditEntityLog.EntityKey = this.GetEntityKeyfromTableLogList(auditEntityLog.Tables);
    auditEntityLog.EntityId = this.GetEntityIdfromTableLogList(auditEntityLog.Tables);
    return auditEntityLog;
  }

  private string GetEntityKeyfromTableLogList(List<AuditTableLogResponse> auditTableLogs)
  {
    List<string> list = auditTableLogs.Where<AuditTableLogResponse>((Func<AuditTableLogResponse, bool>) (auditTableLog => auditTableLog.IsMainTable)).Select<AuditTableLogResponse, string>((Func<AuditTableLogResponse, string>) (auditTableLog => auditTableLog.KeyColumnValue)).ToList<string>();
    return list.Any<string>() ? string.Join("|", list.ToArray()) : (string) null;
  }

  private string GetEntityIdfromTableLogList(List<AuditTableLogResponse> auditTableLogs)
  {
    List<string> list = auditTableLogs.Where<AuditTableLogResponse>((Func<AuditTableLogResponse, bool>) (auditTableLog => auditTableLog.IsMainTable)).Select<AuditTableLogResponse, string>((Func<AuditTableLogResponse, string>) (auditTableLog => auditTableLog.IdColumnValue)).ToList<string>();
    return list.Any<string>() ? string.Join("|", list.ToArray()) : (string) null;
  }

  private List<AuditTableLogResponse> CreateAuditTableLogsFromAuditLogsData(
    IGrouping<long, AuditLogData> auditTransactionEntityLogGroup)
  {
    return this.ConvertAuditTransactionEntityTableLogGroupsToAuditTableLogs(this.GroupAuditTableLog(auditTransactionEntityLogGroup));
  }

  private IEnumerable<IGrouping<long, AuditLogData>> GroupAuditTableLog(
    IGrouping<long, AuditLogData> auditTransactionEntityLogGroup)
  {
    return auditTransactionEntityLogGroup.GroupBy<AuditLogData, long>((Func<AuditLogData, long>) (auditLogDataRow => auditLogDataRow.AuditTransactionEntityTableId)).Select<IGrouping<long, AuditLogData>, IGrouping<long, AuditLogData>>((Func<IGrouping<long, AuditLogData>, IGrouping<long, AuditLogData>>) (auditTableLogGroup => auditTableLogGroup));
  }

  private List<AuditTableLogResponse> ConvertAuditTransactionEntityTableLogGroupsToAuditTableLogs(
    IEnumerable<IGrouping<long, AuditLogData>> auditTransactionEntityTableLogGroups)
  {
    List<AuditTableLogResponse> auditTableLogs = new List<AuditTableLogResponse>();
    foreach (IGrouping<long, AuditLogData> entityTableLogGroup in auditTransactionEntityTableLogGroups)
    {
      AuditTableLogResponse auditTableLog = this.ConvertAuditTransactionEntityTableLogGroupToAuditTableLog(entityTableLogGroup);
      if (auditTableLog != null)
        auditTableLogs.Add(auditTableLog);
    }
    return auditTableLogs;
  }

  private AuditTableLogResponse ConvertAuditTransactionEntityTableLogGroupToAuditTableLog(
    IGrouping<long, AuditLogData> auditTransactionEntityTableLogGroup)
  {
    AuditTableLogResponse auditTableLog = this._mapper.Map<AuditLogData, AuditTableLogResponse>(auditTransactionEntityTableLogGroup != null ? auditTransactionEntityTableLogGroup.FirstOrDefault<AuditLogData>() : (AuditLogData) null);
    if (auditTableLog == null)
      return (AuditTableLogResponse) null;
    auditTableLog.Columns = this.CreateAuditColumnLogsFromAuditLogsData(auditTransactionEntityTableLogGroup);
    return auditTableLog;
  }

  private List<AuditColumnLogResponse> CreateAuditColumnLogsFromAuditLogsData(
    IGrouping<long, AuditLogData> auditTransactionEntityTableLogGroup)
  {
    List<AuditColumnLogResponse> fromAuditLogsData1 = new List<AuditColumnLogResponse>();
    foreach (AuditLogData auditLogData in (IEnumerable<AuditLogData>) auditTransactionEntityTableLogGroup)
    {
      AuditColumnLogResponse fromAuditLogsData2 = this.CreateAuditColumnsLogFromAuditLogsData(auditLogData);
      if (fromAuditLogsData2 != null)
        fromAuditLogsData1.Add(fromAuditLogsData2);
    }
    return fromAuditLogsData1;
  }

  private AuditColumnLogResponse CreateAuditColumnsLogFromAuditLogsData(AuditLogData auditLogData)
  {
    return this._mapper.Map<AuditLogData, AuditColumnLogResponse>(auditLogData);
  }

  private void AddFieldsWhereStatements(
    Dictionary<string, object[]> whereStatements,
    List<AuditFieldSearch> auditFieldSearches)
  {
    List<object> parameterList = new List<object>();
    List<string> stringList = new List<string>();
    foreach (AuditFieldSearch auditFieldSearch in auditFieldSearches)
    {
      if (!string.IsNullOrWhiteSpace(auditFieldSearch?.Entity) && !string.IsNullOrWhiteSpace(auditFieldSearch?.EntityModule) && !string.IsNullOrWhiteSpace(auditFieldSearch?.Table) && !string.IsNullOrWhiteSpace(auditFieldSearch?.FieldName))
        stringList.Add(this.CreateFieldQuery(auditFieldSearch, ref parameterList));
    }
    if (!stringList.Any<string>())
      return;
    whereStatements.Add($"({string.Join(" AND ", (IEnumerable<string>) stringList)})", parameterList.ToArray());
  }

  private string CreateFieldQuery(AuditFieldSearch auditFieldSearch, ref List<object> parameterList)
  {
    List<string> values = new List<string>();
    values.Add($"AE.[Name]=@{parameterList.Count}");
    parameterList.Add((object) auditFieldSearch.Entity.Trim());
    values.Add($"AE.[Module]=@{parameterList.Count}");
    parameterList.Add((object) auditFieldSearch.EntityModule.Trim());
    values.Add($"AET.[TableName]=@{parameterList.Count}");
    parameterList.Add((object) auditFieldSearch.Table.Trim());
    values.Add($"AETC.[ColumnName]=@{parameterList.Count}");
    parameterList.Add((object) auditFieldSearch.FieldName.Trim());
    if (auditFieldSearch.OldValue != null)
    {
      values.Add($"ATETCL.[PreviousValue] LIKE '%'+@{parameterList.Count}+'%'");
      parameterList.Add((object) auditFieldSearch.OldValue.Trim());
    }
    if (auditFieldSearch.NewValue != null)
    {
      values.Add($"ATETCL.[CurrentValue] LIKE '%'+@{parameterList.Count}+'%'");
      parameterList.Add((object) auditFieldSearch.NewValue.Trim());
    }
    return $"({string.Join(" AND ", (IEnumerable<string>) values)})";
  }

  private bool ValidateGetLastAuditTableLogSnapshotParameters(
    string appCode,
    AuditTableLogSnapshotRequest auditLogSnapshotRequest)
  {
    Guid? tenantId = auditLogSnapshotRequest?.TenantId;
    return !string.IsNullOrWhiteSpace(appCode) && !string.IsNullOrWhiteSpace(auditLogSnapshotRequest?.ApplicationCode) && tenantId.HasValue && auditLogSnapshotRequest?.Tables != null && auditLogSnapshotRequest.Tables.Any<AuditTableSnapshot>();
  }

  private async Task<AuditTableLogSnapshotResponse> GetLastAuditTableLogsSnapshotAsync(
    string appCode,
    AuditTableLogSnapshotRequest auditLogSnapshotRequest)
  {
    List<AuditTableSnapshotResponse> auditTableSnapshotResponseList = new List<AuditTableSnapshotResponse>();
    foreach (AuditTableSnapshot table in auditLogSnapshotRequest.Tables)
    {
      AuditTableSnapshot auditTableSnapshot = table;
      if (auditTableSnapshot != null && !string.IsNullOrWhiteSpace(auditTableSnapshot.Name) && !string.IsNullOrWhiteSpace(auditTableSnapshot.IdColumnValue) && !auditTableSnapshotResponseList.Any<AuditTableSnapshotResponse>((Func<AuditTableSnapshotResponse, bool>) (x => string.Equals(x.Name, auditTableSnapshot.Name, StringComparison.OrdinalIgnoreCase) && string.Equals(x.IdColumnValue, auditTableSnapshot.IdColumnValue, StringComparison.OrdinalIgnoreCase))))
      {
        AuditTableSnapshotResponse tableSnapshotAsync = await this.GetLastAuditTableSnapshotAsync(auditLogSnapshotRequest.TenantId, appCode, auditTableSnapshot.Name, auditTableSnapshot.IdColumnValue);
        if (tableSnapshotAsync != null)
          auditTableSnapshotResponseList.Add(tableSnapshotAsync);
      }
    }
    AuditTableLogSnapshotResponse logsSnapshotAsync;
    if (!auditTableSnapshotResponseList.Any<AuditTableSnapshotResponse>())
    {
      logsSnapshotAsync = (AuditTableLogSnapshotResponse) null;
    }
    else
    {
      logsSnapshotAsync = new AuditTableLogSnapshotResponse();
      logsSnapshotAsync.TenantId = auditLogSnapshotRequest.TenantId;
      logsSnapshotAsync.ApplicationCode = appCode;
      logsSnapshotAsync.Tables = auditTableSnapshotResponseList;
    }
    return logsSnapshotAsync;
  }

  private async Task<AuditTableSnapshotResponse> GetLastAuditTableSnapshotAsync(
    Guid tenantId,
    string appCode,
    string tableName,
    string tableIdColumnValue)
  {
    AuditLogData lastAuditLogData = await (Task<AuditLogData>) this._auditLogDataQuery.FirstOrDefaultAsync((Expression<Func<AuditLogData, bool>>) (x => x.TenantId == tenantId && x.ApplicationCode == appCode && x.TableName == tableName && x.IdColumnValue == tableIdColumnValue), Sort<AuditLogData>.ByDesc((Expression<Func<AuditLogData, object>>) (y => (object) y.AuditTransactionEntityId)));
    if (lastAuditLogData == null)
      return (AuditTableSnapshotResponse) null;
    return this.GetAuditTableSnapshotResponseFromAuditLogData((await (Task<IEnumerable<AuditLogData>>) this._auditLogDataQuery.ListAsync((Expression<Func<AuditLogData, bool>>) (x => x.AuditTransactionEntityTableId == lastAuditLogData.AuditTransactionEntityTableId))).ToList<AuditLogData>());
  }

  private AuditTableSnapshotResponse GetAuditTableSnapshotResponseFromAuditLogData(
    List<AuditLogData> auditLogDataList)
  {
    if (!auditLogDataList.Any<AuditLogData>())
      return (AuditTableSnapshotResponse) null;
    List<IGrouping<string, AuditLogData>> list = auditLogDataList.GroupBy<AuditLogData, string>((Func<AuditLogData, string>) (auditLogDataRow => auditLogDataRow.ColumnName)).Select<IGrouping<string, AuditLogData>, IGrouping<string, AuditLogData>>((Func<IGrouping<string, AuditLogData>, IGrouping<string, AuditLogData>>) (auditColumnLogGroup => auditColumnLogGroup)).ToList<IGrouping<string, AuditLogData>>();
    AuditTableSnapshotResponse fromAuditLogData = (AuditTableSnapshotResponse) null;
    if (list.Any<IGrouping<string, AuditLogData>>())
    {
      AuditTableSnapshotResponse snapshotResponse = new AuditTableSnapshotResponse();
      snapshotResponse.Name = auditLogDataList[0].TableName;
      snapshotResponse.IdColumnValue = auditLogDataList[0].IdColumnValue;
      snapshotResponse.Columns = list.Select<IGrouping<string, AuditLogData>, AuditColumnSnapshot>((Func<IGrouping<string, AuditLogData>, AuditColumnSnapshot>) (x => new AuditColumnSnapshot()
      {
        Name = x.Key,
        Value = x.First<AuditLogData>().CurrentValue
      })).OrderBy<AuditColumnSnapshot, string>((Func<AuditColumnSnapshot, string>) (x => x.Name)).ToList<AuditColumnSnapshot>();
      fromAuditLogData = snapshotResponse;
    }
    return fromAuditLogData;
  }
}
