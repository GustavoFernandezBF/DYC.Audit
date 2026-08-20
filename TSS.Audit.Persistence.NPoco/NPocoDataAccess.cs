// Decompiled with JetBrains decompiler
// Type: TSS.Audit.Persistence.NPoco.NPocoDataAccess`1
// Assembly: TSS.Audit.Persistence.NPoco, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F1D411E7-D536-4883-86F6-699D5668BAB4
// Assembly location: D:\Baufest\GIT\TSS.Audit.WebApi\TSS.Audit.Persistence.NPoco.dll

using NPoco;
using NPoco.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TSS.Audit.Persistence.NPoco.Abstract;
using TSS.Audit.Persistence.Queries;
using TSS.Core.Persistence.Reading;

#nullable disable
namespace TSS.Audit.Persistence.NPoco;

public class NPocoDataAccess<T> : 
  IAuditQueryDataAccess<T>,
  IRelationalQueryDataAccess<T>,
  IQueryCoreDataAccess<T>
  where T : class
{
  private readonly IDatabase _database;

  public NPocoDataAccess(IDatabase database)
  {
    this._database = database ?? throw new ArgumentNullException(nameof (database));
  }

  public async Task<long> CountAsync(Expression<Func<T, bool>> whereExpression = null)
  {
    return (long) await (Task<int>) this._database.Query<T>().Where((Expression<Func<T, bool>>) whereExpression).CountAsync();
  }

  public Task<double> SumAsync<TValue>(
    Expression<Func<T, double>> selector,
    Expression<Func<T, bool>> whereExpression = null)
    where TValue : struct
  {
    throw new NotImplementedException("not implemented yet");
  }

  public Task<double> AvgAsync<TValue>(
    Expression<Func<T, double>> selector,
    Expression<Func<T, bool>> whereExpression = null)
    where TValue : struct
  {
    throw new NotImplementedException("not implemented yet");
  }

  public Task<bool> AnyAsync(Expression<Func<T, bool>> whereExpression)
  {
    return (Task<bool>) this._database.Query<T>().Where((Expression<Func<T, bool>>) whereExpression).AnyAsync();
  }

  public Task<T> FirstAsync(
    Expression<Func<T, bool>> whereExpression,
    Sort<T> sortExpression = null,
    string[] includes = null)
  {
    return (Task<T>) NPocoDataAccess<T>.ApplySort(this._database.Query<T>().Where((Expression<Func<T, bool>>) whereExpression), sortExpression).FirstAsync();
  }

  public async Task<TOut> FirstAsync<TOut>(
    Expression<Func<T, bool>> whereExpression,
    Expression<Func<T, TOut>> projectionExpression,
    Sort<T> sortExpression = null,
    string[] includes = null)
  {
    return (await (Task<List<TOut>>) NPocoDataAccess<T>.ApplySort(this._database.Query<T>().Where((Expression<Func<T, bool>>) whereExpression), sortExpression).Limit(1).ProjectToAsync<TOut>((Expression<Func<T, TOut>>) projectionExpression)).FirstOrDefault<TOut>();
  }

  public async Task<IEnumerable<T>> ListAllAsync(Sort<T> sortExpression = null, string[] includes = null)
  {
    return (IEnumerable<T>) await (Task<List<T>>) NPocoDataAccess<T>.ApplySort((IQueryProvider<T>) this._database.Query<T>(), sortExpression).ToListAsync();
  }

  public async Task<IEnumerable<T>> ListAsync(
    Expression<Func<T, bool>> whereExpression,
    Sort<T> sortExpression = null,
    string[] includes = null)
  {
    return (IEnumerable<T>) await (Task<List<T>>) NPocoDataAccess<T>.ApplySort(this._database.Query<T>().Where((Expression<Func<T, bool>>) whereExpression), sortExpression).ToListAsync();
  }

  public async Task<IEnumerable<TOut>> ListAsync<TOut>(
    Expression<Func<T, bool>> whereExpression,
    Expression<Func<T, TOut>> projectionExpression,
    Sort<T> sortExpression = null,
    string[] includes = null)
  {
    return (IEnumerable<TOut>) await (Task<List<TOut>>) NPocoDataAccess<T>.ApplySort(this._database.Query<T>().Where((Expression<Func<T, bool>>) whereExpression), sortExpression).ProjectToAsync<TOut>((Expression<Func<T, TOut>>) projectionExpression);
  }

  public async Task<IPagedResult<T>> ListPagedAsync(
    Expression<Func<T, bool>> whereExpression,
    int page,
    int pageSize,
    Sort<T> sortExpression = null,
    string[] includes = null)
  {
    Page<T> pageAsync = await (Task<Page<T>>) NPocoDataAccess<T>.ApplySort(this._database.Query<T>().Where((Expression<Func<T, bool>>) whereExpression), sortExpression).ToPageAsync(page, pageSize);
    return (IPagedResult<T>) new PagedList<T>()
    {
      Page = page,
      PageSize = pageSize,
      Total = (int) pageAsync.TotalItems,
      Items = (IList<T>) pageAsync.Items
    };
  }

  public Task<T> FirstOrDefaultAsync(
    Expression<Func<T, bool>> whereExpression,
    Sort<T> sortExpression = null,
    string[] includes = null)
  {
    return (Task<T>) NPocoDataAccess<T>.ApplySort(this._database.Query<T>().Where((Expression<Func<T, bool>>) whereExpression), sortExpression).FirstOrDefaultAsync();
  }

  private static IQueryProvider<T> ApplySort(IQueryProvider<T> query, Sort<T> sortExpression)
  {
    if (sortExpression == null || !((IEnumerable<SortExpressionInner<T>>) sortExpression.Expressions).Any<SortExpressionInner<T>>())
      return query;
    SortExpressionInner<T> sortExpressionInner1 = ((IEnumerable<SortExpressionInner<T>>) sortExpression.Expressions).First<SortExpressionInner<T>>();
    query = sortExpressionInner1.Type == SortDirection.Asc ? query.OrderBy(sortExpressionInner1.Expression) : query.OrderByDescending(sortExpressionInner1.Expression);
    foreach (SortExpressionInner<T> sortExpressionInner2 in ((IEnumerable<SortExpressionInner<T>>) sortExpression.Expressions).Skip<SortExpressionInner<T>>(1))
      query = sortExpressionInner2.Type == SortDirection.Asc ? query.ThenBy(sortExpressionInner2.Expression) : query.ThenByDescending(sortExpressionInner2.Expression);
    return query;
  }

  public async Task<IPagedResult<TOut>> ListPagedAsync<TOut>(
    string sqlSentence,
    Dictionary<string, object[]> whereStatements = null,
    string orderByStatement = null,
    int? page = null,
    int? pageSize = null)
    where TOut : class
  {
    Dictionary<string, object[]> dictionary = whereStatements;
    string npocoSqlSentece = (dictionary != null && dictionary.Count > 0) ? $"{sqlSentence} where /**where**/" : sqlSentence;
    npocoSqlSentece = !string.IsNullOrWhiteSpace(orderByStatement) ? $"{npocoSqlSentece} /**orderby**/" : npocoSqlSentece;
    SqlBuilder sqlBuilder = new SqlBuilder();
    SqlBuilder.Template template = sqlBuilder.AddTemplate(npocoSqlSentece);
    List<object> parameters = new List<object>();
    if (whereStatements != null)
    {
      foreach (KeyValuePair<string, object[]> whereStatement in whereStatements)
      {
        sqlBuilder.Where(whereStatement.Key, whereStatement.Value);
        parameters.AddRange((IEnumerable<object>) whereStatement.Value);
      }
    }
    if (!string.IsNullOrWhiteSpace(orderByStatement))
      sqlBuilder.OrderBy(orderByStatement.Trim());
    npocoSqlSentece = template.RawSql;
    if (page.HasValue)
    {
      int? nullable = page;
      int num1 = 0;
      if ((nullable.GetValueOrDefault() > num1 ? (nullable.HasValue ? 1 : 0) : 0) != 0 && pageSize.HasValue)
      {
        nullable = pageSize;
        int num2 = 0;
        if ((nullable.GetValueOrDefault() == num2 ? (!nullable.HasValue ? 1 : 0) : 1) != 0)
        {
          Page<TOut> page1 = await (Task<Page<TOut>>) this._database.PageAsync<TOut>((long) page.Value, (long) pageSize.Value, npocoSqlSentece, parameters.ToArray());
          return (IPagedResult<TOut>) new PagedList<TOut>()
          {
            Page = (int) page1.CurrentPage,
            PageSize = (int) page1.TotalPages,
            Total = (int) page1.TotalItems,
            Items = (IList<TOut>) page1.Items
          };
        }
      }
    }
    List<TOut> list = (await (Task<IEnumerable<TOut>>) this._database.QueryAsync<TOut>(npocoSqlSentece, parameters.ToArray())).ToList<TOut>();
    return (IPagedResult<TOut>) new PagedList<TOut>()
    {
      Page = 0,
      PageSize = 0,
      Total = list.Count,
      Items = (IList<TOut>) list
    };
  }
}
