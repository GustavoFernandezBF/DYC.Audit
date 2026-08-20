// Decompiled with JetBrains decompiler
// Type: TSS.Audit.Persistence.EFCore.EntityFrameworkCoreRepository`1
// Assembly: TSS.Audit.Persistence.EFCore, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F067C905-29AB-47FD-BDFA-B984FDF57185
// Assembly location: D:\Baufest\GIT\TSS.Audit.WebApi\TSS.Audit.Persistence.EFCore.dll

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using TSS.Core.Persistence.Reading;

#nullable disable
namespace TSS.Audit.Persistence.EFCore;

public class EntityFrameworkCoreRepository<T> where T : class
{
  private readonly DbContext _context;

  public EntityFrameworkCoreRepository(DbContext context) => this._context = context;

  protected DbSet<T> Table => this._context.Set<T>();

  public virtual async Task<long> CountAsync(Expression<Func<T, bool>> whereExpression = null)
  {
    return (long) await (Task<int>) this.Table.CountAsync<T>((Expression<Func<T, bool>>) whereExpression, (CancellationToken) new CancellationToken());
  }

  public virtual Task<double> SumAsync<TValue>(
    Expression<Func<T, double>> selector,
    Expression<Func<T, bool>> whereExpression = null)
    where TValue : struct
  {
    return whereExpression != null ? (Task<double>) ((IQueryable<T>) ((IQueryable<T>) this.Table).Where<T>(whereExpression)).SumAsync<T>((Expression<Func<T, double>>) selector, (CancellationToken) new CancellationToken()) : (Task<double>) this.Table.SumAsync<T>((Expression<Func<T, double>>) selector, (CancellationToken) new CancellationToken());
  }

  public virtual Task<double> AvgAsync<TValue>(
    Expression<Func<T, double>> selector,
    Expression<Func<T, bool>> whereExpression = null)
    where TValue : struct
  {
    return whereExpression != null ? (Task<double>) ((IQueryable<T>) ((IQueryable<T>) this.Table).Where<T>(whereExpression)).AverageAsync<T>((Expression<Func<T, double>>) selector, (CancellationToken) new CancellationToken()) : (Task<double>) this.Table.AverageAsync<T>((Expression<Func<T, double>>) selector, (CancellationToken) new CancellationToken());
  }

  public virtual Task<bool> AnyAsync(Expression<Func<T, bool>> whereExpression)
  {
    return (Task<bool>) this.Table.AnyAsync<T>((Expression<Func<T, bool>>) whereExpression, (CancellationToken) new CancellationToken());
  }

  public virtual Task<T> FirstAsync(
    Expression<Func<T, bool>> whereExpression,
    Sort<T> sortExpression = null,
    string[] includes = null)
  {
    IQueryable<T> queryable1 = (IQueryable<T>) this.Table;
    if (includes != null && ((IEnumerable<string>) includes).Any<string>())
      queryable1 = ((IEnumerable<string>) includes).Aggregate<string, IQueryable<T>>(queryable1, (Func<IQueryable<T>, string, IQueryable<T>>) ((current, include) => (IQueryable<T>) ((IQueryable<T>) current).Include<T>(include)));
    IQueryable<T> queryable2 = queryable1.Where<T>(whereExpression);
    if (sortExpression == null)
      return (Task<T>) ((IQueryable<T>) queryable2).FirstAsync<T>((CancellationToken) new CancellationToken());
    if (sortExpression.Expressions.Count<SortExpressionInner<T>>() <= 1)
      return (Task<T>) ((IQueryable<T>) this.ApplyOrderExpression(queryable2, sortExpression.Expressions.First<SortExpressionInner<T>>())).FirstAsync<T>((CancellationToken) new CancellationToken());
    IQueryable<T> seed = this.ApplyOrderExpression(queryable2, sortExpression.Expressions.First<SortExpressionInner<T>>());
    return (Task<T>) ((IQueryable<T>) sortExpression.Expressions.Skip<SortExpressionInner<T>>(1).Aggregate<SortExpressionInner<T>, IQueryable<T>>(seed, new Func<IQueryable<T>, SortExpressionInner<T>, IQueryable<T>>(this.ApplyOrderExpression))).FirstAsync<T>((CancellationToken) new CancellationToken());
  }

  public virtual Task<TOut> FirstAsync<TOut>(
    Expression<Func<T, bool>> whereExpression,
    Expression<Func<T, TOut>> projectionExpression,
    Sort<T> sortExpression = null,
    string[] includes = null)
  {
    IQueryable<T> queryable1 = (IQueryable<T>) this.Table;
    if (includes != null && ((IEnumerable<string>) includes).Any<string>())
      queryable1 = ((IEnumerable<string>) includes).Aggregate<string, IQueryable<T>>(queryable1, (Func<IQueryable<T>, string, IQueryable<T>>) ((current, include) => (IQueryable<T>) ((IQueryable<T>) current).Include<T>(include)));
    IQueryable<T> queryable2 = queryable1.Where<T>(whereExpression);
    if (sortExpression == null)
      return (Task<TOut>) ((IQueryable<TOut>) queryable2.Select<T, TOut>(projectionExpression)).FirstAsync<TOut>((CancellationToken) new CancellationToken());
    if (!sortExpression.IsComplex)
      return (Task<TOut>) ((IQueryable<TOut>) this.ApplyOrderExpression(queryable2, sortExpression.Expressions.First<SortExpressionInner<T>>()).Select<T, TOut>(projectionExpression)).FirstAsync<TOut>((CancellationToken) new CancellationToken());
    IQueryable<T> seed = this.ApplyOrderExpression(queryable2, sortExpression.Expressions.First<SortExpressionInner<T>>());
    return (Task<TOut>) ((IQueryable<TOut>) sortExpression.Expressions.Skip<SortExpressionInner<T>>(1).Aggregate<SortExpressionInner<T>, IQueryable<T>>(seed, new Func<IQueryable<T>, SortExpressionInner<T>, IQueryable<T>>(this.ApplyOrderExpression)).Select<T, TOut>(projectionExpression)).FirstAsync<TOut>((CancellationToken) new CancellationToken());
  }

  public virtual async Task<IEnumerable<T>> ListAllAsync(Sort<T> sortExpression = null, string[] includes = null)
  {
    IQueryable<T> seed1 = (IQueryable<T>) this.Table;
    if (includes != null && ((IEnumerable<string>) includes).Any<string>())
      seed1 = ((IEnumerable<string>) includes).Aggregate<string, IQueryable<T>>(seed1, (Func<IQueryable<T>, string, IQueryable<T>>) ((current, include) => (IQueryable<T>) ((IQueryable<T>) current).Include<T>(include)));
    IQueryable<T> includedExpression = seed1;
    if (sortExpression == null)
      return (IEnumerable<T>) await (Task<List<T>>) ((IQueryable<T>) includedExpression).ToListAsync<T>((CancellationToken) new CancellationToken());
    if (!sortExpression.IsComplex)
      return (IEnumerable<T>) await (Task<List<T>>) ((IQueryable<T>) this.ApplyOrderExpression(includedExpression, sortExpression.Expressions.First<SortExpressionInner<T>>())).ToListAsync<T>((CancellationToken) new CancellationToken());
    IQueryable<T> seed2 = this.ApplyOrderExpression(includedExpression, sortExpression.Expressions.First<SortExpressionInner<T>>());
    return (IEnumerable<T>) await (Task<List<T>>) ((IQueryable<T>) sortExpression.Expressions.Skip<SortExpressionInner<T>>(1).Aggregate<SortExpressionInner<T>, IQueryable<T>>(seed2, new Func<IQueryable<T>, SortExpressionInner<T>, IQueryable<T>>(this.ApplyOrderExpression))).ToListAsync<T>((CancellationToken) new CancellationToken());
  }

  public virtual async Task<IEnumerable<T>> ListAsync(
    Expression<Func<T, bool>> whereExpression,
    Sort<T> sortExpression = null,
    string[] includes = null)
  {
    IQueryable<T> queryable = (IQueryable<T>) this.Table;
    if (includes != null && ((IEnumerable<string>) includes).Any<string>())
      queryable = ((IEnumerable<string>) includes).Aggregate<string, IQueryable<T>>(queryable, (Func<IQueryable<T>, string, IQueryable<T>>) ((current, include) => (IQueryable<T>) ((IQueryable<T>) current).Include<T>(include)));
    IQueryable<T> includedExpression = queryable.Where<T>(whereExpression);
    if (sortExpression == null)
      return (IEnumerable<T>) await (Task<List<T>>) ((IQueryable<T>) includedExpression).ToListAsync<T>((CancellationToken) new CancellationToken());
    if (!sortExpression.IsComplex)
      return (IEnumerable<T>) await (Task<List<T>>) ((IQueryable<T>) this.ApplyOrderExpression(includedExpression, sortExpression.Expressions.First<SortExpressionInner<T>>())).ToListAsync<T>((CancellationToken) new CancellationToken());
    IQueryable<T> seed = this.ApplyOrderExpression(includedExpression, sortExpression.Expressions.First<SortExpressionInner<T>>());
    return (IEnumerable<T>) await (Task<List<T>>) ((IQueryable<T>) sortExpression.Expressions.Skip<SortExpressionInner<T>>(1).Aggregate<SortExpressionInner<T>, IQueryable<T>>(seed, new Func<IQueryable<T>, SortExpressionInner<T>, IQueryable<T>>(this.ApplyOrderExpression))).ToListAsync<T>((CancellationToken) new CancellationToken());
  }

  public virtual async Task<IEnumerable<TOut>> ListAsync<TOut>(
    Expression<Func<T, bool>> whereExpression,
    Expression<Func<T, TOut>> projectionExpression,
    Sort<T> sortExpression = null,
    string[] includes = null)
  {
    IQueryable<T> queryable = (IQueryable<T>) this.Table;
    if (includes != null && ((IEnumerable<string>) includes).Any<string>())
      queryable = ((IEnumerable<string>) includes).Aggregate<string, IQueryable<T>>(queryable, (Func<IQueryable<T>, string, IQueryable<T>>) ((current, include) => (IQueryable<T>) ((IQueryable<T>) current).Include<T>(include)));
    IQueryable<T> includedExpression = queryable.Where<T>(whereExpression);
    if (sortExpression == null)
      return (IEnumerable<TOut>) await (Task<List<TOut>>) ((IQueryable<TOut>) includedExpression.Select<T, TOut>(projectionExpression)).ToListAsync<TOut>((CancellationToken) new CancellationToken());
    if (!sortExpression.IsComplex)
      return (IEnumerable<TOut>) await (Task<List<TOut>>) ((IQueryable<TOut>) this.ApplyOrderExpression(includedExpression, sortExpression.Expressions.First<SortExpressionInner<T>>()).Select<T, TOut>(projectionExpression)).ToListAsync<TOut>((CancellationToken) new CancellationToken());
    IQueryable<T> seed = this.ApplyOrderExpression(includedExpression, sortExpression.Expressions.First<SortExpressionInner<T>>());
    return (IEnumerable<TOut>) await (Task<List<TOut>>) ((IQueryable<TOut>) sortExpression.Expressions.Skip<SortExpressionInner<T>>(1).Aggregate<SortExpressionInner<T>, IQueryable<T>>(seed, new Func<IQueryable<T>, SortExpressionInner<T>, IQueryable<T>>(this.ApplyOrderExpression)).Select<T, TOut>(projectionExpression)).ToListAsync<TOut>((CancellationToken) new CancellationToken());
  }

  public virtual async Task<IPagedResult<T>> ListPagedAsync(
    Expression<Func<T, bool>> whereExpression,
    int page,
    int pageSize,
    Sort<T> sortExpression = null,
    string[] includes = null)
  {
    IQueryable<T> queryable = (IQueryable<T>) this.Table;
    if (includes != null && ((IEnumerable<string>) includes).Any<string>())
      queryable = ((IEnumerable<string>) includes).Aggregate<string, IQueryable<T>>(queryable, (Func<IQueryable<T>, string, IQueryable<T>>) ((current, include) => (IQueryable<T>) ((IQueryable<T>) current).Include<T>(include)));
    IQueryable<T> includedExpression = queryable.Where<T>(whereExpression);
    int total = 0;
    if (sortExpression == null)
    {
      total = await (Task<int>) ((IQueryable<T>) includedExpression).CountAsync<T>((CancellationToken) new CancellationToken());
      return (IPagedResult<T>) new EntityFrameworkPagedList<T>((IList<T>) await (Task<List<T>>) ((IQueryable<T>) includedExpression.Skip<T>((page - 1) * pageSize).Take<T>(pageSize)).ToListAsync<T>((CancellationToken) new CancellationToken()), page, total, pageSize);
    }
    if (sortExpression.IsComplex)
    {
      IQueryable<T> ordered = this.ApplyOrderExpression(includedExpression, sortExpression.Expressions.First<SortExpressionInner<T>>());
      ordered = sortExpression.Expressions.Skip<SortExpressionInner<T>>(1).Aggregate<SortExpressionInner<T>, IQueryable<T>>(ordered, new Func<IQueryable<T>, SortExpressionInner<T>, IQueryable<T>>(this.ApplyOrderExpression));
      total = await (Task<int>) ((IQueryable<T>) ordered).CountAsync<T>((CancellationToken) new CancellationToken());
      return (IPagedResult<T>) new EntityFrameworkPagedList<T>((IList<T>) await (Task<List<T>>) ((IQueryable<T>) ordered.Skip<T>((page - 1) * pageSize).Take<T>(pageSize)).ToListAsync<T>((CancellationToken) new CancellationToken()), page, total, pageSize);
    }
    IQueryable<T> ordered1 = this.ApplyOrderExpression(includedExpression, sortExpression.Expressions.First<SortExpressionInner<T>>());
    total = await (Task<int>) ((IQueryable<T>) ordered1).CountAsync<T>((CancellationToken) new CancellationToken());
    return (IPagedResult<T>) new EntityFrameworkPagedList<T>((IList<T>) await (Task<List<T>>) ((IQueryable<T>) ordered1.Skip<T>((page - 1) * pageSize).Take<T>(pageSize)).ToListAsync<T>((CancellationToken) new CancellationToken()), page, total, pageSize);
  }

  public virtual Task<T> FirstOrDefaultAsync(
    Expression<Func<T, bool>> whereExpression,
    Sort<T> sortExpression = null,
    string[] includes = null)
  {
    IQueryable<T> queryable1 = (IQueryable<T>) this.Table;
    if (includes != null && ((IEnumerable<string>) includes).Any<string>())
      queryable1 = ((IEnumerable<string>) includes).Aggregate<string, IQueryable<T>>(queryable1, (Func<IQueryable<T>, string, IQueryable<T>>) ((current, include) => (IQueryable<T>) ((IQueryable<T>) current).Include<T>(include)));
    IQueryable<T> queryable2 = queryable1.Where<T>(whereExpression);
    if (sortExpression == null)
      return (Task<T>) ((IQueryable<T>) queryable2).FirstOrDefaultAsync<T>((CancellationToken) new CancellationToken());
    if (!sortExpression.IsComplex)
      return (Task<T>) ((IQueryable<T>) this.ApplyOrderExpression(queryable2, sortExpression.Expressions.First<SortExpressionInner<T>>())).FirstOrDefaultAsync<T>((CancellationToken) new CancellationToken());
    IQueryable<T> seed = this.ApplyOrderExpression(queryable2, sortExpression.Expressions.First<SortExpressionInner<T>>());
    return (Task<T>) ((IQueryable<T>) sortExpression.Expressions.Skip<SortExpressionInner<T>>(1).Aggregate<SortExpressionInner<T>, IQueryable<T>>(seed, new Func<IQueryable<T>, SortExpressionInner<T>, IQueryable<T>>(this.ApplyOrderExpression))).FirstOrDefaultAsync<T>((CancellationToken) new CancellationToken());
  }

  protected virtual IQueryable<T> ApplyOrderExpression(
    IQueryable<T> query,
    SortExpressionInner<T> orderExpression)
  {
    return orderExpression.Type != SortDirection.Asc ? (IQueryable<T>) query.OrderByDescending<T, object>(orderExpression.Expression) : (IQueryable<T>) query.OrderBy<T, object>(orderExpression.Expression);
  }

  protected virtual IQueryable<T> ApplyOrderExpression(
    IOrderedQueryable<T> query,
    SortExpressionInner<T> orderExpression)
  {
    return orderExpression.Type != SortDirection.Asc ? (IQueryable<T>) query.ThenByDescending<T, object>(orderExpression.Expression) : (IQueryable<T>) query.ThenBy<T, object>(orderExpression.Expression);
  }

  public virtual Task AddAsync(T entity)
  {
    this.Table.Add(entity);
    return Task.CompletedTask;
  }

  public virtual Task UpdateAsync(T entityToUpdate)
  {
    if (this._context.Entry<T>(entityToUpdate).State == EntityState.Detached)
      this.Table.Attach(entityToUpdate);
    this._context.Entry<T>(entityToUpdate).State = EntityState.Modified;
    return Task.CompletedTask;
  }

  public virtual Task UpdatePartialAsync(T entityToUpdate, params string[] changedPropertyNames)
  {
    EntityEntry<T> entityEntry = this._context.Entry<T>(entityToUpdate);
    if (entityEntry.State != EntityState.Detached)
      entityEntry.State = EntityState.Detached;
    this.Table.Attach(entityToUpdate);
    foreach (string changedPropertyName in changedPropertyNames)
      this._context.Entry<T>(entityToUpdate).Property(changedPropertyName).IsModified = true;
    return Task.CompletedTask;
  }

  public virtual Task DeleteAsync(T entityToDelete)
  {
    if (this._context.Entry<T>(entityToDelete).State == EntityState.Detached)
      this.Table.Attach(entityToDelete);
    this._context.Entry<T>(entityToDelete).State = EntityState.Deleted;
    return Task.CompletedTask;
  }
}
