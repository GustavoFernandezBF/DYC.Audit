// Decompiled with JetBrains decompiler
// Type: TSS.Audit.DTOs.Response.AuditLogDataExport
// Assembly: TSS.Audit.DTOs, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8CA03111-1FE1-44F2-8494-9D295448C35A
// Assembly location: D:\Baufest\GIT\TSS.Audit.WebApi\TSS.Audit.DTOs.dll

#nullable disable
namespace TSS.Audit.DTOs.Response;

public class AuditLogDataExport
{
  public string TransactionCode { get; set; }

  public string ApplicationCode { get; set; }

  public string ProcessName { get; set; }

  public string ProcessDescription { get; set; }

  public string ProcessModule { get; set; }

  public string BeginProcessTimestamp { get; set; }

  public string EndProcessTimestamp { get; set; }

  public string AuditUserDescription { get; set; }

  public string AuditUserIdentifier { get; set; }

  public string EntityName { get; set; }

  public string EntityModule { get; set; }

  public string IsMainEntity { get; set; }

  public string AuditByFieldValue { get; set; }

  public string AuditDateFieldValue { get; set; }

  public string TableName { get; set; }

  public string IsMainTable { get; set; }

  public string Operation { get; set; }

  public string UpdateMask { get; set; }

  public string IdColumnValue { get; set; }

  public string KeyFieldValue { get; set; }

  public string Timestamp { get; set; }

  public string RowVersion { get; set; }

  public string ColumnName { get; set; }

  public string PreviousValue { get; set; }

  public string CurrentValue { get; set; }
}
