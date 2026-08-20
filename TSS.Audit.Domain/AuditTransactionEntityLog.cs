// Decompiled with JetBrains decompiler
// Type: TSS.Audit.Domain.AuditTransactionEntityLog
// Assembly: TSS.Audit.Domain, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9D95FB3D-318C-4872-B305-85847E8E57E6
// Assembly location: D:\Baufest\GIT\TSS.Audit.WebApi\TSS.Audit.Domain.dll

using System;
using System.Collections.Generic;

#nullable disable
namespace TSS.Audit.Domain;

public class AuditTransactionEntityLog
{
  public AuditTransactionEntityLog()
  {
  }

  public AuditTransactionEntityLog(
    int? auditEntityId,
    string auditByFieldValue,
    DateTime auditDateFieldValue,
    bool isMain)
    : this()
  {
    this.AuditEntityId = auditEntityId;
    this.AuditByFieldValue = auditByFieldValue;
    this.AuditDateFieldValue = auditDateFieldValue;
    this.IsMain = isMain;
  }

  public int AuditTransactionEntityId { get; protected set; }

  public int? AuditEntityId { get; protected set; }

  public string AuditByFieldValue { get; protected set; }

  public DateTime AuditDateFieldValue { get; protected set; }

  public int? AuditProcessLogId { get; protected set; }

  public bool IsMain { get; protected set; }

  public virtual AuditEntity AuditEntity { get; protected set; }

  public virtual AuditProcessLog AuditProcessLog { get; protected set; }

  public virtual ICollection<AuditTransactionEntityTableLog> AuditTransactionEntityTableLogs { get; protected set; } = (ICollection<AuditTransactionEntityTableLog>) new List<AuditTransactionEntityTableLog>();

  public void RegisterTransactionEntityTable(
    AuditTransactionEntityTableLog auditTransactionEntityTableLog)
  {
    this.AuditTransactionEntityTableLogs.Add(auditTransactionEntityTableLog);
  }
}
