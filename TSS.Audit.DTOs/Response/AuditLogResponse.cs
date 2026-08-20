// Decompiled with JetBrains decompiler
// Type: TSS.Audit.DTOs.Response.AuditLogResponse
// Assembly: TSS.Audit.DTOs, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8CA03111-1FE1-44F2-8494-9D295448C35A
// Assembly location: D:\Baufest\GIT\TSS.Audit.WebApi\TSS.Audit.DTOs.dll

using System;
using System.Collections.Generic;

#nullable disable
namespace TSS.Audit.DTOs.Response;

public class AuditLogResponse
{
  public string TransactionCode { get; set; }

  public Guid TenantId { get; set; }

  public string ApplicationCode { get; set; }

  public string ProcessDescription { get; set; }

  public string ProcessName { get; set; }

  public string Module { get; set; }

  public DateTime? BeginProcess { get; set; }

  public DateTime EndProcess { get; set; }

  public string AuditUserDescription { get; set; }

  public string AuditUserIdentifier { get; set; }

  public List<AuditEntityLogResponse> Entities { get; set; } = new List<AuditEntityLogResponse>();
}
