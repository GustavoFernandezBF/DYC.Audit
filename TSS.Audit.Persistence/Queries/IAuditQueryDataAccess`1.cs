// Decompiled with JetBrains decompiler
// Type: TSS.Audit.Persistence.Queries.IAuditQueryDataAccess`1
// Assembly: TSS.Audit.Persistence, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F095A8AF-792F-4104-B44E-0AE2DABC28F9
// Assembly location: D:\Baufest\GIT\TSS.Audit.WebApi\TSS.Audit.Persistence.dll

using System.Collections.Generic;
using System.Threading.Tasks;
using TSS.Core.Persistence.Reading;

#nullable disable
namespace TSS.Audit.Persistence.Queries;

public interface IAuditQueryDataAccess<T> : IRelationalQueryDataAccess<T>, IQueryCoreDataAccess<T> where T : class
{
  Task<IPagedResult<TOut>> ListPagedAsync<TOut>(
    string sqlSentence,
    Dictionary<string, object[]> whereStatements = null,
    string orderByStatement = null,
    int? page = null,
    int? pageSize = null)
    where TOut : class;
}
