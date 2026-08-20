// Decompiled with JetBrains decompiler
// Type: TSS.Audit.WriteServices.Validation.Validators.AuditEntityDeleteValidator
// Assembly: TSS.Audit.WriteServices, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA5016C0-9FEC-4A45-98E8-F75EBF083899
// Assembly location: D:\Baufest\GIT\TSS.Audit.WebApi\TSS.Audit.WriteServices.dll

using FluentValidation;
using System;
using System.Linq.Expressions;
using TSS.Audit.DTOs.Request;
using TSS.Audit.Resources;

#nullable disable
namespace TSS.Audit.WriteServices.Validation.Validators;

public class AuditEntityDeleteValidator : AbstractValidator<AuditEntityDelete>
{
  public AuditEntityDeleteValidator()
  {
    this.RuleFor<string>((Expression<Func<AuditEntityDelete, string>>) (x => x.Name)).NotEmpty<AuditEntityDelete, string>().WithLocalizedMessage<AuditEntityDelete, string>(typeof (Messages), "EntityNameInvalid");
    this.RuleFor<string>((Expression<Func<AuditEntityDelete, string>>) (x => x.Module)).NotEmpty<AuditEntityDelete, string>().WithLocalizedMessage<AuditEntityDelete, string>(typeof (Messages), "ModuleInvalid");
  }
}
