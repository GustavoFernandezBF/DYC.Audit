// Decompiled with JetBrains decompiler
// Type: TSS.Audit.DTOs.Core.AuditLog
// Assembly: TSS.Audit.DTOs, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8CA03111-1FE1-44F2-8494-9D295448C35A
// Assembly location: D:\Baufest\GIT\TSS.Audit.WebApi\TSS.Audit.DTOs.dll

using System;
using System.Collections.Generic;

#nullable disable
namespace TSS.Audit.DTOs.Core;

public class AuditLog
{
  private string _applicationCode;
  private string _processName;
  private string _module;
  private string _auditUserDescription;
  private string _auditUserIdentifier;

  public Guid TenantId { get; set; }

  public string ApplicationCode
  {
    get => this._applicationCode?.Trim();
    set => this._applicationCode = value;
  }

  public string ProcessName
  {
    get => this._processName?.Trim();
    set => this._processName = value;
  }

  public string Module
  {
    get => this._module?.Trim();
    set => this._module = value;
  }

  public DateTime? BeginProcess { get; set; }

  public DateTime EndProcess { get; set; }

  public string AuditUserDescription
  {
    get => this._auditUserDescription?.Trim();
    set => this._auditUserDescription = value;
  }

  public string AuditUserIdentifier
  {
    get => this._auditUserIdentifier?.Trim();
    set => this._auditUserIdentifier = value;
  }

  public List<AuditEntityLog> Entities { get; set; } = new List<AuditEntityLog>();
}
