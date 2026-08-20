// Decompiled with JetBrains decompiler
// Type: TSS.Audit.DTOs.Response.AuditEntityLogResponse
// Assembly: TSS.Audit.DTOs, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8CA03111-1FE1-44F2-8494-9D295448C35A
// Assembly location: D:\Baufest\GIT\TSS.Audit.WebApi\TSS.Audit.DTOs.dll

using System;
using System.Collections.Generic;

#nullable disable
namespace TSS.Audit.DTOs.Response;

public class AuditEntityLogResponse
{
  public string EntityLogId { get; set; }

  public string Entity { get; set; }

  public string Module { get; set; }

  public string EntityKey { get; set; }

  public string EntityId { get; set; }

  public bool IsMainEntity { get; set; }

  public string AuditBy { get; set; }

  public DateTime AuditDate { get; set; }

  public List<AuditTableLogResponse> Tables { get; set; } = new List<AuditTableLogResponse>();
}
