// Decompiled with JetBrains decompiler
// Type: TSS.Audit.WebApi.Startup
// Assembly: TSS.Audit.WebApi, Version=0.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9CA87E92-3DDD-413F-AD9B-DAF8395B6F1D
// Assembly location: D:\Baufest\GIT\TSS.Audit.WebApi\TSS.Audit.WebApi.dll
// XML documentation location: D:\Baufest\GIT\TSS.Audit.WebApi\TSS.Audit.WebApi.xml

using Autofac;
using Autofac.Core;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;
using NSwag;
using NSwag.AspNetCore;
using StackifyMiddleware;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using TSS.Audit.WebApi.Configuration;
using TSS.Audit.WebApi.DependencyInjection;
using TSS.Audit.WebApi.Filters;

#nullable disable
namespace TSS.Audit.WebApi;

public class Startup
{
  public Startup(IConfiguration configuration) => this.Configuration = configuration;

  public IConfiguration Configuration { get; }

  public IServiceCollection Services { get; set; }

  public IApplicationBuilder App { get; private set; }

  public IServiceProvider ConfigureServices(IServiceCollection services)
  {
    this.Services = services;
    services.AddMvc(options => {
        options.Filters.Add(new AuditExceptionFilterAttribute());
        options.EnableEndpointRouting = false;
    });
    this.AddSecurity();
    ContainerBuilder builder = new ContainerBuilder();
    builder.Populate((IEnumerable<ServiceDescriptor>) services);
    builder.RegisterModule((IModule) new ApplicationModule(this.Configuration));
    builder.RegisterModule((IModule) new InfrastructureModule(this.Configuration));
    this.ApplicationContainer = builder.Build();
    return (IServiceProvider) new AutofacServiceProvider((IComponentContext) this.ApplicationContainer);
  }

  public IContainer ApplicationContainer { get; private set; }

  public void Configure(
    IApplicationBuilder app,
    IHostingEnvironment env,
    IApplicationLifetime appLifetime)
  {
    this.App = app;
    if (env.IsDevelopment())
      app.UseDeveloperExceptionPage();
    else if (this.Configuration.GetValue<bool>("Stackify:Enabled"))
      app.UseMiddleware<RequestTracerMiddleware>();
    app.UseCors((Action<CorsPolicyBuilder>) (policy =>
    {
      policy.AllowAnyOrigin();
      policy.AllowAnyHeader();
      policy.AllowAnyMethod();
    }));
    app.UseSwagger(typeof (Startup).Assembly, new SwaggerSettings()
    {
      PostProcess = (Action<SwaggerDocument>) (document =>
      {
        ((List<SwaggerSchema>) document.Schemes).Add(SwaggerSchema.Http);
        ((List<SwaggerSchema>) document.Schemes).Add(SwaggerSchema.Https);
      })
    });
    this.UseSecurity();
    app.UseSwaggerUi3(new SwaggerUi3Settings());
    app.UseMvc();
    ((CancellationToken) appLifetime.ApplicationStopped).Register((Action) (() => ((IDisposable) this.ApplicationContainer).Dispose()));
  }
}
