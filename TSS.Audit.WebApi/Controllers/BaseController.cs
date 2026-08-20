// Decompiled with JetBrains decompiler
// Type: TSS.Audit.WebApi.Controllers.BaseController
// Assembly: TSS.Audit.WebApi, Version=0.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9CA87E92-3DDD-413F-AD9B-DAF8395B6F1D
// Assembly location: D:\Baufest\GIT\TSS.Audit.WebApi\TSS.Audit.WebApi.dll
// XML documentation location: D:\Baufest\GIT\TSS.Audit.WebApi\TSS.Audit.WebApi.xml

using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;

#nullable disable
namespace TSS.Audit.WebApi.Controllers;

public class BaseController : Controller
{
  protected Guid? GetAuthenticatedUserTenantId()
  {
    Claim claim = ((ClaimsPrincipal) this.User).Claims.FirstOrDefault<Claim>((Func<Claim, bool>) (x => x.Type == "tenant_id"));
    if (string.IsNullOrWhiteSpace(claim?.Value))
      return new Guid?();
    Guid result;
    return Guid.TryParse(claim.Value, out result) ? new Guid?(result) : new Guid?();
  }

  protected string GetAuthenticatedUserName()
  {
    return ((ClaimsPrincipal) this.User).Claims.FirstOrDefault<Claim>((Func<Claim, bool>) (x => x.Type == "id"))?.Value;
  }
}
