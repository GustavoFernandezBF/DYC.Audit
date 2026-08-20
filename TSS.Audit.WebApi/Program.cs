// Decompiled with JetBrains decompiler
// Type: TSS.Audit.WebApi.Program
// Assembly: TSS.Audit.WebApi, Version=0.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9CA87E92-3DDD-413F-AD9B-DAF8395B6F1D
// Assembly location: D:\Baufest\GIT\TSS.Audit.WebApi\TSS.Audit.WebApi.dll
// XML documentation location: D:\Baufest\GIT\TSS.Audit.WebApi\TSS.Audit.WebApi.xml

using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;

#nullable disable
namespace TSS.Audit.WebApi;

public class Program
{
  public static void Main(string[] args) => Program.BuildWebHost(args).Run();

  public static IWebHost BuildWebHost(string[] args)
  {
    return WebHost.CreateDefaultBuilder(args).UseStartup<Startup>().UseSerilog((Action<WebHostBuilderContext, LoggerConfiguration>) ((hostingContext, loggerConfiguration) => loggerConfiguration.ReadFrom.Configuration(hostingContext.Configuration))).ConfigureServices((Action<IServiceCollection>) (services => services.AddAutofac())).Build();
  }
}
