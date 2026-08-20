// Decompiled with JetBrains decompiler
// Type: TSS.Audit.DTOs.Request.AuditLogSearch
// Assembly: TSS.Audit.DTOs, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8CA03111-1FE1-44F2-8494-9D295448C35A
// Assembly location: D:\Baufest\GIT\TSS.Audit.WebApi\TSS.Audit.DTOs.dll

using System;
using System.Collections.Generic;
using TSS.Audit.Common;
using TSS.Audit.DTOs.Core;

#nullable disable
namespace TSS.Audit.DTOs.Request;

public class AuditLogSearch
{
  public AuditLogSearch()
  {
  }

  public AuditLogSearch(
    DateTime? from,
    DateTime? to,
    string process,
    string processModule,
    string entity,
    string entityModule,
    string entityKeyValue,
    string by,
    Constants.TableOperation? actions,
    int? page,
    int? pageSize)
    : this()
  {
    this.From = from;
    this.To = to;
    this.Process = process;
    this.ProcessModule = processModule;
    this.Entity = entity;
    this.EntityModule = entityModule;
    this.EntityKeyValue = entityKeyValue;
    this.By = by;
    this.Actions = actions;
    this.Page = page;
    this.PageSize = pageSize;
  }

  public DateTime? From { get; set; }

  public DateTime? To { get; set; }

  public string Process { get; set; }

  public string ProcessModule { get; set; }

  public string Entity { get; set; }

  public string EntityModule { get; set; }

  public string EntityKeyValue { get; set; }

  public string By { get; set; }

  public Constants.TableOperation? Actions { get; set; }

  public int? Page { get; set; }

  public int? PageSize { get; set; }

  public List<AuditFieldSearch> Fields { get; set; } = new List<AuditFieldSearch>();

  public Guid? RequestId { get; set; }
}
