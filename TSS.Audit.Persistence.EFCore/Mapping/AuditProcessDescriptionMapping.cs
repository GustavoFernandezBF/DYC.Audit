// Decompiled with JetBrains decompiler
// Type: TSS.Audit.Persistence.EFCore.Mapping.AuditProcessDescriptionMapping
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

public class AuditProcessDescriptionMapping : IEntityTypeConfiguration<AuditProcessDescription>
{
  public void Configure(EntityTypeBuilder<AuditProcessDescription> builder)
  {
    builder.ToTable<AuditProcessDescription>("AuditProcessDescription");
    builder.HasKey((Expression<Func<AuditProcessDescription, object>>) (x => (object) x.AuditProcessDescriptionId));
    builder.Property<string>((Expression<Func<AuditProcessDescription, string>>) (e => e.ApplicationCode)).IsRequired(true).HasMaxLength(20).IsUnicode(false);
    builder.Property<string>((Expression<Func<AuditProcessDescription, string>>) (e => e.Description)).HasMaxLength(600).IsUnicode(false);
    builder.Property<string>((Expression<Func<AuditProcessDescription, string>>) (e => e.Module)).IsRequired(true).HasMaxLength(50).IsUnicode(false);
    builder.Property<string>((Expression<Func<AuditProcessDescription, string>>) (e => e.Name)).IsRequired(true).HasMaxLength(250).IsUnicode(false);
    builder.Property<bool>((Expression<Func<AuditProcessDescription, bool>>) (e => e.Enabled)).IsRequired(true);
  }
}
