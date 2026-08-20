// Decompiled with JetBrains decompiler
// Type: TSS.Audit.Persistence.EFCore.EntityFrameworkPagedList`1
// Assembly: TSS.Audit.Persistence.EFCore, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F067C905-29AB-47FD-BDFA-B984FDF57185
// Assembly location: D:\Baufest\GIT\TSS.Audit.WebApi\TSS.Audit.Persistence.EFCore.dll

using System.Collections.Generic;
using TSS.Core.Persistence.Reading;

#nullable disable
namespace TSS.Audit.Persistence.EFCore;

internal class EntityFrameworkPagedList<T> : IPagedResult<T> where T : class
{
  public EntityFrameworkPagedList(IList<T> items, int page, int total, int pageSize)
  {
    this.Items = items;
    this.Page = page;
    this.Total = total;
    this.PageSize = pageSize;
  }

  public int Page { get; private set; }

  public int Total { get; private set; }

  public int PageSize { get; private set; }

  public IList<T> Items { get; private set; }
}
