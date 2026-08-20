// Decompiled with JetBrains decompiler
// Type: TSS.Audit.Persistence.IAuditUnitOfWork
// Assembly: TSS.Audit.Persistence, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F095A8AF-792F-4104-B44E-0AE2DABC28F9
// Assembly location: D:\Baufest\GIT\TSS.Audit.WebApi\TSS.Audit.Persistence.dll

using System;
using TSS.Core.Persistence.Writing;

#nullable disable
namespace TSS.Audit.Persistence;

public interface IAuditUnitOfWork : IUnitOfWork, IDisposable
{
}
