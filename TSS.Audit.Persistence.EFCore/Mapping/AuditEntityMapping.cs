// Decompiled with JetBrains decompiler
// Type: TSS.Audit.Persistence.EFCore.Mapping.AuditEntityMapping
// Assembly: TSS.Audit.Persistence.EFCore, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F067C905-29AB-47FD-BDFA-B984FDF57185
// Assembly location: D:\Baufest\GIT\TSS.Audit.WebApi\TSS.Audit.Persistence.EFCore.dll

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Linq.Expressions;
using TSS.Audit.Domain;

#nullable disable
namespace TSS.Audit.Persistence.EFCore.Mapping;

public class AuditEntityMapping : IEntityTypeConfiguration<AuditEntity>
{
  public void Configure(EntityTypeBuilder<AuditEntity> builder)
  {
    builder.ToTable<AuditEntity>("AuditEntity");
    builder.HasKey((Expression<Func<AuditEntity, object>>) (x => (object) x.AuditEntityId));
    builder.Property<string>((Expression<Func<AuditEntity, string>>) (e => e.ApplicationCode)).IsRequired(true).HasMaxLength(20).IsUnicode(false);
    builder.Property<string>((Expression<Func<AuditEntity, string>>) (e => e.Module)).IsRequired(true).HasMaxLength(50).IsUnicode(false);
    builder.Property<string>((Expression<Func<AuditEntity, string>>) (e => e.Name)).IsRequired(true).HasMaxLength(50).IsUnicode(false);
    builder.Property<bool>((Expression<Func<AuditEntity, bool>>) (e => e.Enabled)).IsRequired(true);
  }
}
