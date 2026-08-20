// Decompiled with JetBrains decompiler
// Type: TSS.Audit.Persistence.EFCore.AuditDbContext
// Assembly: TSS.Audit.Persistence.EFCore, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F067C905-29AB-47FD-BDFA-B984FDF57185
// Assembly location: D:\Baufest\GIT\TSS.Audit.WebApi\TSS.Audit.Persistence.EFCore.dll

using Microsoft.EntityFrameworkCore;
using TSS.Audit.Domain;
using TSS.Audit.Persistence.EFCore.Mapping;

#nullable disable
namespace TSS.Audit.Persistence.EFCore;

public class AuditDbContext : DbContext
{
  private readonly string _connectionString;

  public AuditDbContext(string connectionString) => this._connectionString = connectionString;

  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
  {
    optionsBuilder.UseSqlServer(this._connectionString);
  }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.ApplyConfiguration<AuditEntity>((IEntityTypeConfiguration<AuditEntity>) new AuditEntityMapping());
    modelBuilder.ApplyConfiguration<AuditEntityTableColumn>((IEntityTypeConfiguration<AuditEntityTableColumn>) new AuditEntityTableColumnMapping());
    modelBuilder.ApplyConfiguration<AuditEntityTable>((IEntityTypeConfiguration<AuditEntityTable>) new AuditEntityTableMapping());
    modelBuilder.ApplyConfiguration<AuditProcessDescription>((IEntityTypeConfiguration<AuditProcessDescription>) new AuditProcessDescriptionMapping());
    modelBuilder.ApplyConfiguration<AuditProcessLog>((IEntityTypeConfiguration<AuditProcessLog>) new AuditProcessLogMapping());
    modelBuilder.ApplyConfiguration<AuditTransactionEntityLog>((IEntityTypeConfiguration<AuditTransactionEntityLog>) new AuditTransactionEntityLogMapping());
    modelBuilder.ApplyConfiguration<AuditTransactionEntityTableColumnLog>((IEntityTypeConfiguration<AuditTransactionEntityTableColumnLog>) new AuditTransactionEntityTableColumnLogMapping());
    modelBuilder.ApplyConfiguration<AuditTransactionEntityTableLog>((IEntityTypeConfiguration<AuditTransactionEntityTableLog>) new AuditTransactionEntityTableLogMapping());
  }
}
