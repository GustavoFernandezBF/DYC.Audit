// Decompiled with JetBrains decompiler
// Type: TSS.Audit.Persistence.EFCore.EntityFrameworkAuditRepository`1
// Assembly: TSS.Audit.Persistence.EFCore, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F067C905-29AB-47FD-BDFA-B984FDF57185
// Assembly location: D:\Baufest\GIT\TSS.Audit.WebApi\TSS.Audit.Persistence.EFCore.dll

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TSS.Audit.Persistence.Commands;
using TSS.Core.Persistence.Reading;
using TSS.Core.Persistence.Writing;

#nullable disable
namespace TSS.Audit.Persistence.EFCore;

public class EntityFrameworkAuditRepository<T>(DbContext context) : 
  EntityFrameworkCoreRepository<T>(context),
  IAuditRepository<T>,
  IRelationalCoreRepository<T>,
  IRelationalCorePersistant<T>,
  ICorePersistant<T>,
  IQueryCoreDataAccess<T>
  where T : class
{
  public Task<T> FirstAsync(
    Expression<Func<T, bool>> whereExpression,
    Sort<T> sortExpression = null,
    string[] includes = null)
  {
    return base.FirstAsync(whereExpression, sortExpression, includes);
  }

  public Task<TOut> FirstAsync<TOut>(
    Expression<Func<T, bool>> whereExpression,
    Expression<Func<T, TOut>> projectionExpression,
    Sort<T> sortExpression = null,
    string[] includes = null)
  {
    return base.FirstAsync<TOut>(whereExpression, projectionExpression, sortExpression, includes);
  }

  public Task<T> FirstOrDefaultAsync(
    Expression<Func<T, bool>> whereExpression,
    Sort<T> sortExpression = null,
    string[] includes = null)
  {
    return base.FirstOrDefaultAsync(whereExpression, sortExpression, includes);
  }

  public Task<IEnumerable<T>> ListAsync(
    Expression<Func<T, bool>> whereExpression,
    Sort<T> sortExpression = null,
    string[] includes = null)
  {
    return base.ListAsync(whereExpression, sortExpression, includes);
  }

  public Task<IEnumerable<TOut>> ListAsync<TOut>(
    Expression<Func<T, bool>> whereExpression,
    Expression<Func<T, TOut>> projectionExpression,
    Sort<T> sortExpression = null,
    string[] includes = null)
  {
    return base.ListAsync<TOut>(whereExpression, projectionExpression, sortExpression, includes);
  }
}
