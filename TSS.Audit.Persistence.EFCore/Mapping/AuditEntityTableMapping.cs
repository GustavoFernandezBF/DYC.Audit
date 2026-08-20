// Decompiled with JetBrains decompiler
// Type: TSS.Audit.Persistence.EFCore.Mapping.AuditEntityTableMapping
// Assembly: TSS.Audit.Persistence.EFCore, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F067C905-29AB-47FD-BDFA-B984FDF57185
// Assembly location: D:\Baufest\GIT\TSS.Audit.WebApi\TSS.Audit.Persistence.EFCore.dll

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using TSS.Audit.Domain;

#nullable disable
namespace TSS.Audit.Persistence.EFCore.Mapping;

public class AuditEntityTableMapping : IEntityTypeConfiguration<AuditEntityTable>
{
  public void Configure(EntityTypeBuilder<AuditEntityTable> builder)
  {
    builder.ToTable<AuditEntityTable>("AuditEntityTable");
    builder.HasKey((Expression<Func<AuditEntityTable, object>>) (x => (object) x.AuditEntityTableId));
    builder.Property<string>((Expression<Func<AuditEntityTable, string>>) (e => e.AuditByFieldName)).HasMaxLength(50).IsUnicode(false);
    builder.Property<string>((Expression<Func<AuditEntityTable, string>>) (e => e.AuditDateFieldName)).HasMaxLength(50).IsUnicode(false).IsRequired(false);
    builder.Property<string>((Expression<Func<AuditEntityTable, string>>) (e => e.IdColumnName)).HasMaxLength(50).IsUnicode(false);
    builder.Property<string>((Expression<Func<AuditEntityTable, string>>) (e => e.KeyFieldName)).HasMaxLength(50).IsUnicode(false);
    builder.Property<string>((Expression<Func<AuditEntityTable, string>>) (e => e.TableDescriptionFormat)).HasMaxLength(250).IsUnicode(false);
    builder.Property<string>((Expression<Func<AuditEntityTable, string>>) (e => e.TableName)).HasMaxLength(50).IsUnicode(false);
    builder.Property<bool>((Expression<Func<AuditEntityTable, bool>>) (e => e.Enabled)).IsRequired(true);
    builder.HasOne<AuditEntity>((Expression<Func<AuditEntityTable, AuditEntity>>) (t => t.AuditEntity)).WithMany((Expression<Func<AuditEntity, IEnumerable<AuditEntityTable>>>) (e => e.AuditEntityTables)).HasForeignKey((Expression<Func<AuditEntityTable, object>>) (t => (object) t.AuditEntityId));
  }
}
