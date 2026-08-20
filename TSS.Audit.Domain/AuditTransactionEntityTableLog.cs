// Decompiled with JetBrains decompiler
// Type: TSS.Audit.Domain.AuditTransactionEntityTableLog
// Assembly: TSS.Audit.Domain, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9D95FB3D-318C-4872-B305-85847E8E57E6
// Assembly location: D:\Baufest\GIT\TSS.Audit.WebApi\TSS.Audit.Domain.dll

using System;
using System.Collections.Generic;
using TSS.Audit.Common;

#nullable disable
namespace TSS.Audit.Domain;

public class AuditTransactionEntityTableLog
{
  public AuditTransactionEntityTableLog()
  {
  }

  public AuditTransactionEntityTableLog(
    int? auditEntityTableId,
    Constants.TableOperation operation,
    DateTime timestamp,
    bool isMain)
    : this()
  {
    this.AuditEntityTableId = auditEntityTableId;
    this.Operation = operation;
    this.Timestamp = timestamp;
    this.IsMain = isMain;
  }

  public int AuditTransactionEntityTableId { get; protected set; }

  public int? AuditTransactionEntityId { get; protected set; }

  public int? AuditEntityTableId { get; protected set; }

  public Constants.TableOperation Operation { get; protected set; }

  public string UpdateMask { get; set; }

  public string IdColumnValue { get; set; }

  public string KeyFieldValue { get; set; }

  public DateTime Timestamp { get; protected set; }

  public string RowVersion { get; set; }

  public bool IsMain { get; protected set; }

  public virtual AuditEntityTable AuditEntityTable { get; protected set; }

  public virtual AuditTransactionEntityLog AuditTransactionEntity { get; protected set; }

  public virtual ICollection<AuditTransactionEntityTableColumnLog> AuditTransactionEntityTableColumnLogs { get; protected set; } = (ICollection<AuditTransactionEntityTableColumnLog>) new List<AuditTransactionEntityTableColumnLog>();

  public void RegisterTransactionEntityTableColumn(
    AuditTransactionEntityTableColumnLog auditTransactionEntityTableColumnLog)
  {
    this.AuditTransactionEntityTableColumnLogs.Add(auditTransactionEntityTableColumnLog);
  }
}
