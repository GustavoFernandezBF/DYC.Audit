// Decompiled with JetBrains decompiler
// Type: TSS.Audit.DTOs.Request.AuditProcessDelete
// Assembly: TSS.Audit.DTOs, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8CA03111-1FE1-44F2-8494-9D295448C35A
// Assembly location: D:\Baufest\GIT\TSS.Audit.WebApi\TSS.Audit.DTOs.dll

#nullable disable
namespace TSS.Audit.DTOs.Request;

public class AuditProcessDelete
{
  private string _name;
  private string _module;

  public string Name
  {
    get => this._name?.Trim();
    set => this._name = value;
  }

  public string Module
  {
    get => this._module?.Trim();
    set => this._module = value;
  }
}
