// Decompiled with JetBrains decompiler
// Type: TSS.Audit.Persistence.EFCore.AuditUnitOfWork
// Assembly: TSS.Audit.Persistence.EFCore, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F067C905-29AB-47FD-BDFA-B984FDF57185
// Assembly location: D:\Baufest\GIT\TSS.Audit.WebApi\TSS.Audit.Persistence.EFCore.dll

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TSS.Core.Persistence.Writing;

#nullable disable
namespace TSS.Audit.Persistence.EFCore;

public class AuditUnitOfWork : IAuditUnitOfWork, IUnitOfWork, IDisposable
{
  private readonly DbContext _context;

  public AuditUnitOfWork(DbContext context) => this._context = context;

  public void Commit()
  {
    try
    {
      this._context.SaveChanges();
    }
    catch (Exception ex)
    {
      throw;
    }
  }

  public async Task CommitAsync()
  {
    try
    {
      int num = await (Task<int>) this._context.SaveChangesAsync((CancellationToken) new CancellationToken());
    }
    catch (Exception ex)
    {
      throw new Exception(ex.Message, ex.InnerException);
    }
  }

  public void Rollback()
  {
    this._context.ChangeTracker.DetectChanges();
    foreach (EntityEntry entityEntry in ((IEnumerable<EntityEntry>) this._context.ChangeTracker.Entries()).Where<EntityEntry>((Func<EntityEntry, bool>) (e => e.State != EntityState.Unchanged)).ToList<EntityEntry>())
    {
      object entity = entityEntry.Entity;
      if (entity != null)
      {
        if (entityEntry.State == EntityState.Added)
          this._context.Remove(entity);
        else if (entityEntry.State == EntityState.Modified)
          entityEntry.Reload();
        else if (entityEntry.State == EntityState.Deleted)
          entityEntry.State = EntityState.Unchanged;
      }
    }
  }

  public void Dispose() => this._context?.Dispose();
}
