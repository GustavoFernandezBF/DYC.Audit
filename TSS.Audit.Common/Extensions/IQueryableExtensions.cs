// Decompiled with JetBrains decompiler
// Type: TSS.Audit.Common.Extensions.IQueryableExtensions
// Assembly: TSS.Audit.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8BF748E8-21B6-4DAD-80F1-C9122581C7B1
// Assembly location: D:\Baufest\GIT\TSS.Audit.WebApi\TSS.Audit.Common.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

#nullable disable
namespace TSS.Audit.Common.Extensions;

public static class IQueryableExtensions
{
  public static IOrderedQueryable<T> OrderBy<T>(
    this IQueryable<T> query,
    string propertyName,
    IComparer<object> comparer = null)
  {
    return query.CallOrderedQueryable<T>(nameof (OrderBy), propertyName, comparer);
  }

  public static IOrderedQueryable<T> OrderByDescending<T>(
    this IQueryable<T> query,
    string propertyName,
    IComparer<object> comparer = null)
  {
    return query.CallOrderedQueryable<T>(nameof (OrderByDescending), propertyName, comparer);
  }

  public static IOrderedQueryable<T> ThenBy<T>(
    this IOrderedQueryable<T> query,
    string propertyName,
    IComparer<object> comparer = null)
  {
    return query.CallOrderedQueryable<T>(nameof (ThenBy), propertyName, comparer);
  }

  public static IOrderedQueryable<T> ThenByDescending<T>(
    this IOrderedQueryable<T> query,
    string propertyName,
    IComparer<object> comparer = null)
  {
    return query.CallOrderedQueryable<T>(nameof (ThenByDescending), propertyName, comparer);
  }

  public static IOrderedQueryable<T> CallOrderedQueryable<T>(
    this IQueryable<T> query,
    string methodName,
    string propertyName,
    IComparer<object> comparer = null)
  {
    ParameterExpression seed = Expression.Parameter(typeof (T), "x");
    Expression body = ((IEnumerable<string>) propertyName.Split('.')).Aggregate<string, Expression>((Expression) seed, new Func<Expression, string, Expression>(Expression.PropertyOrField));
    return comparer == null ? (IOrderedQueryable<T>) query.Provider.CreateQuery((Expression) Expression.Call(typeof (Queryable), methodName, new Type[2]
    {
      typeof (T),
      body.Type
    }, query.Expression, (Expression) Expression.Lambda(body, seed))) : (IOrderedQueryable<T>) query.Provider.CreateQuery((Expression) Expression.Call(typeof (Queryable), methodName, new Type[2]
    {
      typeof (T),
      body.Type
    }, query.Expression, (Expression) Expression.Lambda(body, seed), (Expression) Expression.Constant((object) comparer)));
  }
}
