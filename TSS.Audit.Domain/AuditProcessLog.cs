// Decompiled with JetBrains decompiler
// Type: TSS.Audit.Domain.AuditProcessLog
// Assembly: TSS.Audit.Domain, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9D95FB3D-318C-4872-B305-85847E8E57E6
// Assembly location: D:\Baufest\GIT\TSS.Audit.WebApi\TSS.Audit.Domain.dll

using System;
using System.Collections.Generic;

#nullable disable
namespace TSS.Audit.Domain;

public class AuditProcessLog
{
  public AuditProcessLog()
  {
  }

  public AuditProcessLog(
    Guid tenantId,
    int auditProcessDescriptionId,
    DateTime endProcessTimestamp)
    : this()
  {
    this.TenantId = tenantId;
    this.AuditProcessDescriptionId = auditProcessDescriptionId;
    this.EndProcessTimestamp = endProcessTimestamp;
  }

  public int AuditProcessLogId { get; protected set; }

  public Guid TenantId { get; protected set; }

  public DateTime? BeginProcessTimestamp { get; set; }

  public DateTime EndProcessTimestamp { get; protected set; }

  public int AuditProcessDescriptionId { get; protected set; }

  public string AuditUserDescription { get; set; }

  public string AuditUserIdentifier { get; set; }

  public virtual AuditProcessDescription AuditProcessDescription { get; protected set; }

  public virtual ICollection<AuditTransactionEntityLog> AuditTransactionEntityLogs { get; protected set; } = (ICollection<AuditTransactionEntityLog>) new List<AuditTransactionEntityLog>();

  public void RegisterTransactionEntity(
    AuditTransactionEntityLog auditTransactionEntityLog)
  {
    this.AuditTransactionEntityLogs.Add(auditTransactionEntityLog);
  }
}
