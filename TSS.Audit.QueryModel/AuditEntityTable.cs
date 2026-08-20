// Decompiled with JetBrains decompiler
// Type: TSS.Audit.QueryModel.AuditEntityTable
// Assembly: TSS.Audit.QueryModel, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DF217321-56CA-450D-84E4-3813C3160EAD
// Assembly location: D:\Baufest\GIT\TSS.Audit.WebApi\TSS.Audit.QueryModel.dll

using System.Collections.Generic;

#nullable disable
namespace TSS.Audit.QueryModel;

public class AuditEntityTable
{
  public int AuditEntityTableId { get; set; }

  public string TableName { get; set; }

  public int? AuditEntityId { get; set; }

  public string KeyFieldName { get; set; }

  public string IdColumnName { get; set; }

  public string AuditByFieldName { get; set; }

  public string AuditDateFieldName { get; set; }

  public bool Enabled { get; set; }

  public string TableDescriptionFormat { get; set; }

  public List<AuditEntityTableColumn> AuditEntityTableColumns { get; set; } = new List<AuditEntityTableColumn>();
}
