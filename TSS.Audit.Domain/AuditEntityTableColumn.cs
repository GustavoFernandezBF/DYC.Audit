// Decompiled with JetBrains decompiler
// Type: TSS.Audit.Domain.AuditEntityTableColumn
// Assembly: TSS.Audit.Domain, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9D95FB3D-318C-4872-B305-85847E8E57E6
// Assembly location: D:\Baufest\GIT\TSS.Audit.WebApi\TSS.Audit.Domain.dll

using System.Collections.Generic;

#nullable disable
namespace TSS.Audit.Domain;

public class AuditEntityTableColumn
{
  public AuditEntityTableColumn() => this.Enabled = true;

  public AuditEntityTableColumn(string columnName)
    : this()
  {
    this.ColumnName = columnName;
  }

  public int AuditEntityTableColumnId { get; protected set; }

  public int AuditEntityTableId { get; protected set; }

  public string ColumnName { get; protected set; }

  public string ColumnDotNetType { get; set; }

  public string ColumnTsqltype { get; set; }

  public int? DisplayOrder { get; set; }

  public bool Enabled { get; set; }

  public string ColumnLabel { get; set; }

  public string MasterTableName { get; set; }

  public string MasterTablePkname { get; set; }

  public string MasterTableDescColumnName { get; set; }

  public virtual AuditEntityTable AuditEntityTable { get; protected set; }

  public virtual ICollection<AuditTransactionEntityTableColumnLog> AuditTransactionEntityTableColumnLogs { get; protected set; } = (ICollection<AuditTransactionEntityTableColumnLog>) new HashSet<AuditTransactionEntityTableColumnLog>();
}
