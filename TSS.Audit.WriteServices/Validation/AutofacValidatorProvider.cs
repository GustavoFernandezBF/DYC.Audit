// Decompiled with JetBrains decompiler
// Type: TSS.Audit.WriteServices.Validation.AutofacValidatorProvider
// Assembly: TSS.Audit.WriteServices, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA5016C0-9FEC-4A45-98E8-F75EBF083899
// Assembly location: D:\Baufest\GIT\TSS.Audit.WebApi\TSS.Audit.WriteServices.dll

using Autofac;
using FluentValidation;

#nullable disable
namespace TSS.Audit.WriteServices.Validation;

public class AutofacValidatorProvider : IValidatorProvider
{
  private readonly IComponentContext _componentContext;

  public AutofacValidatorProvider(IComponentContext componentContext)
  {
    this._componentContext = componentContext;
  }

  public AbstractValidator<T> GetValidatorFor<T>()
  {
    return this._componentContext.Resolve<AbstractValidator<T>>();
  }
}
