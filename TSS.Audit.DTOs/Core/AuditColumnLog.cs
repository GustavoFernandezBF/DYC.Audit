// Decompiled with JetBrains decompiler
// Type: TSS.Audit.DTOs.Core.AuditColumnLog
// Assembly: TSS.Audit.DTOs, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8CA03111-1FE1-44F2-8494-9D295448C35A
// Assembly location: D:\Baufest\GIT\TSS.Audit.WebApi\TSS.Audit.DTOs.dll

#nullable disable
namespace TSS.Audit.DTOs.Core;

public class AuditColumnLog
{
  private string _columnName;
  private string _previous;
  private string _current;

  public string ColumnName
  {
    get => this._columnName?.Trim();
    set => this._columnName = value;
  }

  public string Previous
  {
    get => this._previous?.Trim();
    set => this._previous = value;
  }

  public string Current
  {
    get => this._current?.Trim();
    set => this._current = value;
  }
}
