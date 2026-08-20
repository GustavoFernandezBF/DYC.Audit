// Decompiled with JetBrains decompiler
// Type: TSS.Audit.Domain.Operation
// Assembly: TSS.Audit.Domain, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9D95FB3D-318C-4872-B305-85847E8E57E6
// Assembly location: D:\Baufest\GIT\TSS.Audit.WebApi\TSS.Audit.Domain.dll

#nullable disable
namespace TSS.Audit.Domain;

public enum Operation : byte
{
  Insert = 1,
  Update = 2,
  Delete = 3,
}
