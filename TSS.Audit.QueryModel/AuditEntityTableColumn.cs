// Decompiled with JetBrains decompiler
// Type: TSS.Audit.QueryModel.AuditEntityTableColumn
// Assembly: TSS.Audit.QueryModel, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DF217321-56CA-450D-84E4-3813C3160EAD
// Assembly location: D:\Baufest\GIT\TSS.Audit.WebApi\TSS.Audit.QueryModel.dll

#nullable disable
namespace TSS.Audit.QueryModel;

public class AuditEntityTableColumn
{
  public int AuditEntityTableColumnId { get; set; }

  public int AuditEntityTableId { get; set; }

  public string ColumnName { get; set; }

  public string ColumnDotNetType { get; set; }

  public string ColumnTsqltype { get; set; }

  public int? DisplayOrder { get; set; }

  public bool Enabled { get; set; }

  public string ColumnLabel { get; set; }

  public string MasterTableName { get; set; }

  public string MasterTablePkname { get; set; }

  public string MasterTableDescColumnName { get; set; }
}
