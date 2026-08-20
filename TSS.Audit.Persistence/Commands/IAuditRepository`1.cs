// Decompiled with JetBrains decompiler
// Type: TSS.Audit.Persistence.Commands.IAuditRepository`1
// Assembly: TSS.Audit.Persistence, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F095A8AF-792F-4104-B44E-0AE2DABC28F9
// Assembly location: D:\Baufest\GIT\TSS.Audit.WebApi\TSS.Audit.Persistence.dll

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TSS.Core.Persistence.Reading;
using TSS.Core.Persistence.Writing;

#nullable disable
namespace TSS.Audit.Persistence.Commands;

public interface IAuditRepository<T> : 
  IRelationalCoreRepository<T>,
  IRelationalCorePersistant<T>,
  ICorePersistant<T>,
  IQueryCoreDataAccess<T>
  where T : class
{
  Task<T> FirstAsync(
    Expression<Func<T, bool>> whereExpression,
    Sort<T> sortExpression = null,
    string[] includes = null);

  Task<TOut> FirstAsync<TOut>(
    Expression<Func<T, bool>> whereExpression,
    Expression<Func<T, TOut>> projectionExpression,
    Sort<T> sortExpression = null,
    string[] includes = null);

  Task<T> FirstOrDefaultAsync(
    Expression<Func<T, bool>> whereExpression,
    Sort<T> sortExpression = null,
    string[] includes = null);

  Task<IEnumerable<T>> ListAllAsync(Sort<T> sortExpression = null, string[] includes = null);

  Task<IEnumerable<TOut>> ListAsync<TOut>(
    Expression<Func<T, bool>> whereExpression,
    Expression<Func<T, TOut>> projectionExpression,
    Sort<T> sortExpression = null,
    string[] includes = null);

  Task<IEnumerable<T>> ListAsync(
    Expression<Func<T, bool>> whereExpression,
    Sort<T> sortExpression = null,
    string[] includes = null);
}
