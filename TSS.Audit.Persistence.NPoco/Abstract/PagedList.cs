// Decompiled with JetBrains decompiler
// Type: TSS.Audit.Persistence.NPoco.Abstract.PagedList`1
// Assembly: TSS.Audit.Persistence.NPoco, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F1D411E7-D536-4883-86F6-699D5668BAB4
// Assembly location: D:\Baufest\GIT\TSS.Audit.WebApi\TSS.Audit.Persistence.NPoco.dll

using System.Collections.Generic;
using TSS.Core.Persistence.Reading;

#nullable disable
namespace TSS.Audit.Persistence.NPoco.Abstract;

public class PagedList<T> : IPagedResult<T> where T : class
{
  public int Page { get; set; }

  public int Total { get; set; }

  public int PageSize { get; set; }

  public IList<T> Items { get; set; }
}
