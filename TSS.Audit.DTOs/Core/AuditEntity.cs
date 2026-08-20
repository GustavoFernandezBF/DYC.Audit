// Decompiled with JetBrains decompiler
// Type: TSS.Audit.DTOs.Core.AuditEntity
// Assembly: TSS.Audit.DTOs, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8CA03111-1FE1-44F2-8494-9D295448C35A
// Assembly location: D:\Baufest\GIT\TSS.Audit.WebApi\TSS.Audit.DTOs.dll

using System.Collections.Generic;

#nullable disable
namespace TSS.Audit.DTOs.Core;

public class AuditEntity
{
  private string _applicationCode;
  private string _name;
  private string _module;

  public string ApplicationCode
  {
    get => this._applicationCode?.Trim();
    set => this._applicationCode = value;
  }

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

  public List<AuditTable> Tables { get; set; } = new List<AuditTable>();
}
