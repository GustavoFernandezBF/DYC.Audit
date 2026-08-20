// Decompiled with JetBrains decompiler
// Type: TSS.Audit.WebApi.Configuration.SecurityConfig
// Assembly: TSS.Audit.WebApi, Version=0.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9CA87E92-3DDD-413F-AD9B-DAF8395B6F1D
// Assembly location: D:\Baufest\GIT\TSS.Audit.WebApi\TSS.Audit.WebApi.dll
// XML documentation location: D:\Baufest\GIT\TSS.Audit.WebApi\TSS.Audit.WebApi.xml

using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

#nullable disable
namespace TSS.Audit.WebApi.Configuration;

public static class SecurityConfig
{
  public static Startup AddSecurity(this Startup startup)
  {
    IConfigurationSection section = startup.Configuration.GetSection("Security");
    if (Convert.ToBoolean(section["Enabled"]))
      startup.Services.AddAuthentication("Bearer").AddIdentityServerAuthentication((Action<IdentityServerAuthenticationOptions>) (o =>
      {
        o.Authority = section["Authority"];
        o.ApiName = section["ApiName"];
        o.ApiSecret = section["ApiSecret"];
        o.NameClaimType = "id";
        o.RoleClaimType = "role";
        o.RequireHttpsMetadata = Convert.ToBoolean(section["RequireHttpsMetadata"]);
      }));
    return startup;
  }

  public static Startup UseSecurity(this Startup startup)
  {
    if (Convert.ToBoolean(startup.Configuration.GetSection("Security")["Enabled"]))
      startup.App.UseAuthentication();
    return startup;
  }
}
