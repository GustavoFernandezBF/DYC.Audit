// Decompiled with JetBrains decompiler
// Type: TSS.Audit.Persistence.NPoco.Abstract.NPocoDatabaseFactory
// Assembly: TSS.Audit.Persistence.NPoco, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F1D411E7-D536-4883-86F6-699D5668BAB4
// Assembly location: D:\Baufest\GIT\TSS.Audit.WebApi\TSS.Audit.Persistence.NPoco.dll

using NPoco;
using NPoco.FluentMappings;
using System;
using System.Data.Common;
using System.Data.SqlClient;
using TSS.Audit.Persistence.NPoco.Mapping;

#nullable disable
namespace TSS.Audit.Persistence.NPoco.Abstract;

public static class NPocoDatabaseFactory
{
  public static DatabaseFactory DbFactory { get; set; }

  public static void Setup(string connectionSring, string providerName)
  {
    FluentConfig fluentConfig = FluentMappingConfiguration.Configure((IMap) new AuditProcessDescriptionMapping(), (IMap) new AuditEntityMapping(), (IMap) new AuditEntityTableMapping(), (IMap) new AuditEntityTableColumnMapping(), (IMap) new AuditLogDataMapping());
    NPocoDatabaseFactory.DbFactory = DatabaseFactory.Config((Action<DatabaseFactoryConfig>) (x =>
    {
      x.UsingDatabase((Func<Database>) (() => new Database(connectionSring, DatabaseType.SqlServer2012, (DbProviderFactory) SqlClientFactory.Instance)));
      x.WithFluentConfig(fluentConfig);
    }));
  }
}
