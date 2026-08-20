// Decompiled with JetBrains decompiler
// Type: TSS.Audit.DTOs.PaginatedResponse`1
// Assembly: TSS.Audit.DTOs, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8CA03111-1FE1-44F2-8494-9D295448C35A
// Assembly location: D:\Baufest\GIT\TSS.Audit.WebApi\TSS.Audit.DTOs.dll

using System.Collections.Generic;

#nullable disable
namespace TSS.Audit.DTOs;

public class PaginatedResponse<T> where T : class
{
  public PaginatedResponse()
  {
  }

  public PaginatedResponse(int? page, int? pageSize, int? totalItems)
    : this()
  {
    this.Page = page;
    this.PageSize = pageSize;
    this.TotalItems = totalItems;
  }

  public int? Page { get; set; }

  public int? PageSize { get; set; }

  public int? TotalItems { get; set; }

  public List<T> Items { get; set; } = new List<T>();
}
