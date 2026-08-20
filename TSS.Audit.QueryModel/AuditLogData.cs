// Decompiled with JetBrains decompiler
// Type: TSS.Audit.QueryModel.AuditLogData
// Assembly: TSS.Audit.QueryModel, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DF217321-56CA-450D-84E4-3813C3160EAD
// Assembly location: D:\Baufest\GIT\TSS.Audit.WebApi\TSS.Audit.QueryModel.dll

using System;
using TSS.Audit.Common;

#nullable disable
namespace TSS.Audit.QueryModel;

public class AuditLogData
{
  public long AuditProcessLogId { get; set; }

  public Guid TenantId { get; set; }

  public string ApplicationCode { get; set; }

  public string ProcessName { get; set; }

  public string ProcessDescription { get; set; }

  public string ProcessModule { get; set; }

  public DateTime? BeginProcessTimestamp { get; set; }

  public DateTime EndProcessTimestamp { get; set; }

  public string AuditUserDescription { get; set; }

  public string AuditUserIdentifier { get; set; }

  public long AuditTransactionEntityId { get; set; }

  public string EntityName { get; set; }

  public string EntityModule { get; set; }

  public bool IsMainEntity { get; set; }

  public string AuditByFieldValue { get; set; }

  public DateTime AuditDateFieldValue { get; set; }

  public long AuditTransactionEntityTableId { get; set; }

  public string TableName { get; set; }

  public string TableDescriptionFormat { get; set; }

  public bool IsMainTable { get; set; }

  public Constants.TableOperation Operation { get; set; }

  public string UpdateMask { get; set; }

  public string IdColumnValue { get; set; }

  public string KeyFieldValue { get; set; }

  public DateTime Timestamp { get; set; }

  public string RowVersion { get; set; }

  public long AuditTransactionEntityTableColumnId { get; set; }

  public string ColumnName { get; set; }

  public string ColumnLabel { get; set; }

  public string ColumnDotNetType { get; set; }

  public string PreviousValue { get; set; }

  public string CurrentValue { get; set; }
}
