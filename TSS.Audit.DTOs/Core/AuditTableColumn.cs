// Decompiled with JetBrains decompiler
// Type: TSS.Audit.DTOs.Core.AuditTableColumn
// Assembly: TSS.Audit.DTOs, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8CA03111-1FE1-44F2-8494-9D295448C35A
// Assembly location: D:\Baufest\GIT\TSS.Audit.WebApi\TSS.Audit.DTOs.dll

#nullable disable
namespace TSS.Audit.DTOs.Core;

public class AuditTableColumn
{
  private string _name;
  private string _label;
  private string _netType;
  private string _sqlType;
  private string _masterTableName;
  private string _masterTablePKName;
  private string _masterTableDescColumnName;

  public string Name
  {
    get => this._name?.Trim();
    set => this._name = value;
  }

  public string Label
  {
    get => this._label?.Trim();
    set => this._label = value;
  }

  public string NetType
  {
    get => this._netType?.Trim();
    set => this._netType = value;
  }

  public string SqlType
  {
    get => this._sqlType?.Trim();
    set => this._sqlType = value;
  }

  public int? DisplayOrder { get; set; }

  public string MasterTableName
  {
    get => this._masterTableName?.Trim();
    set => this._masterTableName = value;
  }

  public string MasterTablePKName
  {
    get => this._masterTablePKName?.Trim();
    set => this._masterTablePKName = value;
  }

  public string MasterTableDescColumnName
  {
    get => this._masterTableDescColumnName?.Trim();
    set => this._masterTableDescColumnName = value;
  }
}
