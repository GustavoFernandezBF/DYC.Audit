// Decompiled with JetBrains decompiler
// Type: TSS.Audit.WriteServices.Validation.Validators.AuditProcessValidator
// Assembly: TSS.Audit.WriteServices, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA5016C0-9FEC-4A45-98E8-F75EBF083899
// Assembly location: D:\Baufest\GIT\TSS.Audit.WebApi\TSS.Audit.WriteServices.dll

using FluentValidation;
using System;
using System.Linq.Expressions;
using TSS.Audit.DTOs.Core;
using TSS.Audit.Resources;

#nullable disable
namespace TSS.Audit.WriteServices.Validation.Validators;

public class AuditProcessValidator : AbstractValidator<AuditProcess>
{
  public AuditProcessValidator()
  {
    this.RuleFor<string>((Expression<Func<AuditProcess, string>>) (x => x.Name)).NotEmpty<AuditProcess, string>().MaximumLength<AuditProcess>(250).MinimumLength<AuditProcess>(3).Matches<AuditProcess>("^([A-Za-z0-9_-]+\\.*)+$").WithLocalizedMessage<AuditProcess, string>(typeof (Messages), "ProcessNameInvalid");
    this.RuleFor<string>((Expression<Func<AuditProcess, string>>) (x => x.Description)).MaximumLength<AuditProcess>(600);
    this.RuleFor<string>((Expression<Func<AuditProcess, string>>) (x => x.Module)).NotEmpty<AuditProcess, string>().MaximumLength<AuditProcess>(50).MinimumLength<AuditProcess>(3).Matches<AuditProcess>("^([\\sA-Za-z0-9_-]+\\.*)+$").WithLocalizedMessage<AuditProcess, string>(typeof (Messages), "ModuleInvalid");
  }
}
