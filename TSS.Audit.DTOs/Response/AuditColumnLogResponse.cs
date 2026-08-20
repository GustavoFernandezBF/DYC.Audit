// Decompiled with JetBrains decompiler
// Type: TSS.Audit.DTOs.Response.AuditColumnLogResponse
// Assembly: TSS.Audit.DTOs, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8CA03111-1FE1-44F2-8494-9D295448C35A
// Assembly location: D:\Baufest\GIT\TSS.Audit.WebApi\TSS.Audit.DTOs.dll

#nullable disable
namespace TSS.Audit.DTOs.Response;

public class AuditColumnLogResponse
{
  public string ColumnLogId { get; set; }

  public string ColumnName { get; set; }

  public string ColumnLabel { get; set; }

  public string ColumnDotNetType { get; set; }

  public string Previous { get; set; }

  public string Current { get; set; }
}
