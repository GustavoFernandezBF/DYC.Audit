// Decompiled with JetBrains decompiler
// Type: TSS.Audit.WriteServices.Validation.Validators.AuditLogValidator
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

public class AuditLogValidator : AbstractValidator<AuditLog>
{
  public AuditLogValidator()
  {
    this.RuleFor<Guid>((Expression<Func<AuditLog, Guid>>) (x => x.TenantId)).NotEmpty<AuditLog, Guid>().WithLocalizedMessage<AuditLog, Guid>(typeof (Messages), "TenantInvalid");
    this.RuleFor<string>((Expression<Func<AuditLog, string>>) (x => x.ApplicationCode)).NotEmpty<AuditLog, string>().WithLocalizedMessage<AuditLog, string>(typeof (Messages), "AppInvalid");
    this.RuleFor<string>((Expression<Func<AuditLog, string>>) (x => x.ProcessName)).NotEmpty<AuditLog, string>().WithLocalizedMessage<AuditLog, string>(typeof (Messages), "ProcessNameInvalid");
    this.RuleFor<string>((Expression<Func<AuditLog, string>>) (x => x.Module)).NotEmpty<AuditLog, string>().WithLocalizedMessage<AuditLog, string>(typeof (Messages), "ModuleInvalid");
    this.RuleFor<DateTime?>((Expression<Func<AuditLog, DateTime?>>) (x => x.BeginProcess)).NotEqual<AuditLog, DateTime?>(new DateTime?(DateTime.MinValue)).NotEqual<AuditLog, DateTime?>(new DateTime?(DateTime.MaxValue));
    this.RuleFor<DateTime>((Expression<Func<AuditLog, DateTime>>) (x => x.EndProcess)).NotNull<AuditLog, DateTime>().NotEqual<AuditLog, DateTime>(DateTime.MinValue).NotEqual<AuditLog, DateTime>(DateTime.MaxValue);
    this.RuleFor<string>((Expression<Func<AuditLog, string>>) (x => x.AuditUserDescription)).MaximumLength<AuditLog>(250);
    this.RuleFor<string>((Expression<Func<AuditLog, string>>) (x => x.AuditUserIdentifier)).MaximumLength<AuditLog>(250);
    this.RuleFor<List<AuditEntityLog>>((Expression<Func<AuditLog, List<AuditEntityLog>>>) (x => x.Entities)).NotEmpty<AuditLog, List<AuditEntityLog>>();
    ((IRuleBuilder<AuditLog, IEnumerable<AuditEntityLog>>) this.RuleFor<List<AuditEntityLog>>((Expression<Func<AuditLog, List<AuditEntityLog>>>) (x => x.Entities))).SetCollectionValidator<AuditLog, AuditEntityLog>((IValidator<AuditEntityLog>) new AuditEntityLogValidator());
  }
}
