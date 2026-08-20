// Decompiled with JetBrains decompiler
// Type: TSS.Audit.Domain.AuditTransactionEntityTableColumnLog
// Assembly: TSS.Audit.Domain, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9D95FB3D-318C-4872-B305-85847E8E57E6
// Assembly location: D:\Baufest\GIT\TSS.Audit.WebApi\TSS.Audit.Domain.dll

#nullable disable
namespace TSS.Audit.Domain;

public class AuditTransactionEntityTableColumnLog
{
  public AuditTransactionEntityTableColumnLog()
  {
  }

  public AuditTransactionEntityTableColumnLog(
    int auditEntityTableColumnId,
    string previousValue,
    string currentValue)
    : this()
  {
    this.AuditEntityTableColumnId = auditEntityTableColumnId;
    this.PreviousValue = previousValue;
    this.CurrentValue = currentValue;
  }

  public int AuditLogId { get; protected set; }

  public string PreviousValue { get; protected set; }

  public string CurrentValue { get; protected set; }

  public int AuditEntityTableColumnId { get; protected set; }

  public int AuditTransactionEntityTableId { get; protected set; }

  public virtual AuditEntityTableColumn AuditEntityTableColumn { get; protected set; }

  public virtual AuditTransactionEntityTableLog AuditTransactionEntityTable { get; protected set; }
}
