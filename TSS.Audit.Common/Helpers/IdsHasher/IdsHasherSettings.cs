// Decompiled with JetBrains decompiler
// Type: TSS.Audit.Common.Helpers.IdsHasher.IdsHasherSettings
// Assembly: TSS.Audit.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8BF748E8-21B6-4DAD-80F1-C9122581C7B1
// Assembly location: D:\Baufest\GIT\TSS.Audit.WebApi\TSS.Audit.Common.dll

#nullable disable
namespace TSS.Audit.Common.Helpers.IdsHasher;

public class IdsHasherSettings
{
  public string Salt { get; set; } = string.Empty;

  public int MinHashLength { get; set; } = 8;
}
