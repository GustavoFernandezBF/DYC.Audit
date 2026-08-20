// Decompiled with JetBrains decompiler
// Type: TSS.Audit.DTOs.Core.AuditEntityLog
// Assembly: TSS.Audit.DTOs, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8CA03111-1FE1-44F2-8494-9D295448C35A
// Assembly location: D:\Baufest\GIT\TSS.Audit.WebApi\TSS.Audit.DTOs.dll

using System;
using System.Collections.Generic;

#nullable disable
namespace TSS.Audit.DTOs.Core;

public class AuditEntityLog
{
  private string _entity;
  private string _module;
  private string _entityKey;
  private string _auditBy;

  public string Entity
  {
    get => this._entity?.Trim();
    set => this._entity = value;
  }

  public string Module
  {
    get => this._module?.Trim();
    set => this._module = value;
  }

  public string EntityKey
  {
    get => this._entityKey?.Trim();
    set => this._entityKey = value;
  }

  public bool IsMainEntity { get; set; }

  public string AuditBy
  {
    get => this._auditBy?.Trim();
    set => this._auditBy = value;
  }

  public DateTime AuditDate { get; set; }

  public List<AuditTableLog> Tables { get; set; } = new List<AuditTableLog>();
}
