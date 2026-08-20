// Decompiled with JetBrains decompiler
// Type: TSS.Audit.DTOs.Core.AuditTableLog
// Assembly: TSS.Audit.DTOs, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8CA03111-1FE1-44F2-8494-9D295448C35A
// Assembly location: D:\Baufest\GIT\TSS.Audit.WebApi\TSS.Audit.DTOs.dll

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using TSS.Audit.Common;

#nullable disable
namespace TSS.Audit.DTOs.Core;

public class AuditTableLog
{
  private string _tableName;
  private string _updateMask;
  private string _idColumnValue;
  private string _keyColumnValue;
  private string _rowVersion;

  public string TableName
  {
    get => this._tableName?.Trim();
    set => this._tableName = value;
  }

  public bool IsMainTable { get; set; }

  [JsonConverter(typeof (StringEnumConverter))]
  public Constants.TableOperation Operation { get; set; }

  public string UpdateMask
  {
    get => this._updateMask?.Trim();
    set => this._updateMask = value;
  }

  public string IdColumnValue
  {
    get => this._idColumnValue?.Trim();
    set => this._idColumnValue = value;
  }

  public string KeyColumnValue
  {
    get => this._keyColumnValue?.Trim();
    set => this._keyColumnValue = value;
  }

  public string RowVersion
  {
    get => this._rowVersion?.Trim();
    set => this._rowVersion = value;
  }

  public DateTime Timestamp { get; set; }

  public List<AuditColumnLog> Columns { get; set; } = new List<AuditColumnLog>();
}
