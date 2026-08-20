// Decompiled with JetBrains decompiler
// Type: TSS.Audit.Common.Constants
// Assembly: TSS.Audit.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8BF748E8-21B6-4DAD-80F1-C9122581C7B1
// Assembly location: D:\Baufest\GIT\TSS.Audit.WebApi\TSS.Audit.Common.dll

using System.Runtime.InteropServices;

#nullable disable
namespace TSS.Audit.Common;

public static class Constants
{
  public enum TableOperation : byte
  {
    Insert = 1,
    Update = 2,
    Delete = 3,
  }

  [StructLayout(LayoutKind.Sequential, Size = 1)]
  public struct AuditResultExportConfiguration
  {
    public const int Pages = 1;
    public const int PageSize = 3000;
  }

  [StructLayout(LayoutKind.Sequential, Size = 1)]
  public struct YesNo
  {
    public const string YesLabel = "Yes";
    public const string NoLabel = "No";
  }

  [StructLayout(LayoutKind.Sequential, Size = 1)]
  public struct ClaimTypes
  {
    public const string TenantId = "tenant_id";
    public const string UserId = "id";
  }
}
