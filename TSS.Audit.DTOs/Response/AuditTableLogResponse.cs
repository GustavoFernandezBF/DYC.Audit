// Decompiled with JetBrains decompiler
// Type: TSS.Audit.DTOs.Response.AuditTableLogResponse
// Assembly: TSS.Audit.DTOs, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8CA03111-1FE1-44F2-8494-9D295448C35A
// Assembly location: D:\Baufest\GIT\TSS.Audit.WebApi\TSS.Audit.DTOs.dll

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using TSS.Audit.Common;

#nullable disable
namespace TSS.Audit.DTOs.Response;

public class AuditTableLogResponse
{
  public string TableLogId { get; set; }

  public string TableName { get; set; }

  public string TableDescriptionFormat { get; set; }

  public bool IsMainTable { get; set; }

  [JsonConverter(typeof (StringEnumConverter))]
  public Constants.TableOperation Operation { get; set; }

  public string UpdateMask { get; set; }

  public string IdColumnValue { get; set; }

  public string KeyColumnValue { get; set; }

  public string RowVersion { get; set; }

  public DateTime Timestamp { get; set; }

  public List<AuditColumnLogResponse> Columns { get; set; } = new List<AuditColumnLogResponse>();
}
