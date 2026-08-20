// Decompiled with JetBrains decompiler
// Type: TSS.Audit.DTOs.Core.AuditTable
// Assembly: TSS.Audit.DTOs, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8CA03111-1FE1-44F2-8494-9D295448C35A
// Assembly location: D:\Baufest\GIT\TSS.Audit.WebApi\TSS.Audit.DTOs.dll

using System.Collections.Generic;

#nullable disable
namespace TSS.Audit.DTOs.Core;

public class AuditTable
{
  private string _name;
  private string _descriptionFormat;
  private string _idColumnName;
  private string _keyFieldName;
  private string _auditByFieldName;
  private string _auditDateFieldName;

  public string Name
  {
    get => this._name?.Trim();
    set => this._name = value;
  }

  public string DescriptionFormat
  {
    get => this._descriptionFormat?.Trim();
    set => this._descriptionFormat = value;
  }

  public string IdColumnName
  {
    get => this._idColumnName?.Trim();
    set => this._idColumnName = value;
  }

  public string KeyFieldName
  {
    get => this._keyFieldName?.Trim();
    set => this._keyFieldName = value;
  }

  public string AuditByFieldName
  {
    get => this._auditByFieldName?.Trim();
    set => this._auditByFieldName = value;
  }

  public string AuditDateFieldName
  {
    get => this._auditDateFieldName?.Trim();
    set => this._auditDateFieldName = value;
  }

  public List<AuditTableColumn> Columns { get; set; } = new List<AuditTableColumn>();
}
