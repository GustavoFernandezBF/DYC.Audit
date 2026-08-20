// Decompiled with JetBrains decompiler
// Type: TSS.Audit.Common.Helpers.IdsHasher.IdsHasher
// Assembly: TSS.Audit.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8BF748E8-21B6-4DAD-80F1-C9122581C7B1
// Assembly location: D:\Baufest\GIT\TSS.Audit.WebApi\TSS.Audit.Common.dll

using HashidsNet;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace TSS.Audit.Common.Helpers.IdsHasher;

public class IdsHasher
{
  private readonly Hashids _hashids;

  public IdsHasher(IdsHasherSettings idsHasherSettings = null)
  {
    this._hashids = new Hashids(idsHasherSettings?.Salt ?? string.Empty, idsHasherSettings != null ? idsHasherSettings.MinHashLength : 8);
  }

  public string EncodeLongId(long id)
  {
    return this._hashids.EncodeLong(new long[1]{ id });
  }

  public long? DecodeLongId(string hash)
  {
    long[] source = this._hashids.DecodeLong(hash);
    return ((IEnumerable<long>) source).Any<long>() ? new long?(((IEnumerable<long>) source).First<long>()) : new long?();
  }

  public string EncodeIntId(int id)
  {
    return this._hashids.Encode(new int[1]{ id });
  }

  public int? DecodeIntId(string hash)
  {
    int[] source = this._hashids.Decode(hash);
    return ((IEnumerable<int>) source).Any<int>() ? new int?(((IEnumerable<int>) source).First<int>()) : new int?();
  }
}
