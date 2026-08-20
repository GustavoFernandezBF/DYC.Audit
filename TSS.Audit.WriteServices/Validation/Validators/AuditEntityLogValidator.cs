// Decompiled with JetBrains decompiler
// Type: TSS.Audit.WriteServices.Validation.Validators.AuditEntityLogValidator
// Assembly: TSS.Audit.WriteServices, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA5016C0-9FEC-4A45-98E8-F75EBF083899
// Assembly location: D:\Baufest\GIT\TSS.Audit.WebApi\TSS.Audit.WriteServices.dll

using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using TSS.Audit.DTOs.Core;
using TSS.Audit.Resources;

#nullable disable
namespace TSS.Audit.WriteServices.Validation.Validators;

public class AuditEntityLogValidator : AbstractValidator<AuditEntityLog>
{
  public AuditEntityLogValidator()
  {
    this.RuleFor<string>((Expression<Func<AuditEntityLog, string>>) (x => x.Entity)).NotEmpty<AuditEntityLog, string>().WithLocalizedMessage<AuditEntityLog, string>(typeof (Messages), "EntityNameInvalid");
    this.RuleFor<string>((Expression<Func<AuditEntityLog, string>>) (x => x.Module)).NotEmpty<AuditEntityLog, string>().WithLocalizedMessage<AuditEntityLog, string>(typeof (Messages), "ModuleInvalid");
    this.RuleFor<string>((Expression<Func<AuditEntityLog, string>>) (x => x.AuditBy)).NotEmpty<AuditEntityLog, string>().MinimumLength<AuditEntityLog>(3).MaximumLength<AuditEntityLog>(64 /*0x40*/);
    this.RuleFor<DateTime>((Expression<Func<AuditEntityLog, DateTime>>) (x => x.AuditDate)).NotNull<AuditEntityLog, DateTime>().NotEqual<AuditEntityLog, DateTime>(DateTime.MinValue).NotEqual<AuditEntityLog, DateTime>(DateTime.MaxValue);
    this.RuleFor<List<AuditTableLog>>((Expression<Func<AuditEntityLog, List<AuditTableLog>>>) (x => x.Tables)).NotEmpty<AuditEntityLog, List<AuditTableLog>>();
    ((IRuleBuilder<AuditEntityLog, IEnumerable<AuditTableLog>>) this.RuleFor<List<AuditTableLog>>((Expression<Func<AuditEntityLog, List<AuditTableLog>>>) (x => x.Tables))).SetCollectionValidator<AuditEntityLog, AuditTableLog>((IValidator<AuditTableLog>) new AuditTableLogValidator());
  }
}
