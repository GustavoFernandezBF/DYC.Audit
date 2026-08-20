// Decompiled with JetBrains decompiler
// Type: TSS.Audit.Domain.AuditEntityTable
// Assembly: TSS.Audit.Domain, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9D95FB3D-318C-4872-B305-85847E8E57E6
// Assembly location: D:\Baufest\GIT\TSS.Audit.WebApi\TSS.Audit.Domain.dll

using System.Collections.Generic;

#nullable disable
namespace TSS.Audit.Domain;

public class AuditEntityTable
{
  public AuditEntityTable() => this.Enabled = true;

  public AuditEntityTable(string tableName)
    : this()
  {
    this.TableName = tableName;
    this.Enabled = true;
  }

  public int AuditEntityTableId { get; protected set; }

  public string TableName { get; protected set; }

  public int? AuditEntityId { get; protected set; }

  public string KeyFieldName { get; set; }

  public string IdColumnName { get; set; }

  public string AuditByFieldName { get; set; }

  public string AuditDateFieldName { get; set; }

  public bool Enabled { get; set; }

  public string TableDescriptionFormat { get; set; }

  public virtual AuditEntity AuditEntity { get; protected set; }

  public virtual ICollection<AuditEntityTableColumn> AuditEntityTableColumns { get; protected set; } = (ICollection<AuditEntityTableColumn>) new List<AuditEntityTableColumn>();

  public virtual ICollection<AuditTransactionEntityTableLog> AuditTransactionEntityTableLogs { get; protected set; } = (ICollection<AuditTransactionEntityTableLog>) new List<AuditTransactionEntityTableLog>();

  public void RegisterTableColumn(AuditEntityTableColumn newEntityTableColumn)
  {
    this.AuditEntityTableColumns.Add(newEntityTableColumn);
  }

  public void RemoveTableColumn(AuditEntityTableColumn newEntityTableColumn)
  {
    this.AuditEntityTableColumns.Remove(newEntityTableColumn);
  }
}
