// Decompiled with JetBrains decompiler
// Type: TSS.Audit.DTOs.Core.AuditApp
// Assembly: TSS.Audit.DTOs, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8CA03111-1FE1-44F2-8494-9D295448C35A
// Assembly location: D:\Baufest\GIT\TSS.Audit.WebApi\TSS.Audit.DTOs.dll

#nullable disable
namespace TSS.Audit.DTOs.Core;

public class AuditApp
{
  private string _code;
  private string _name;
  private string _logoUri;

  public string Code
  {
    get => this._code?.Trim();
    set => this._code = value;
  }

  public string Name
  {
    get => this._name?.Trim();
    set => this._name = value;
  }

  public string LogoUri
  {
    get => this._logoUri?.Trim();
    set => this._logoUri = value;
  }
}
