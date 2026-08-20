// Decompiled with JetBrains decompiler
// Type: TSS.Audit.WebApi.Helpers.ExcelExportHelper
// Assembly: TSS.Audit.WebApi, Version=0.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9CA87E92-3DDD-413F-AD9B-DAF8395B6F1D
// Assembly location: D:\Baufest\GIT\TSS.Audit.WebApi\TSS.Audit.WebApi.dll
// XML documentation location: D:\Baufest\GIT\TSS.Audit.WebApi\TSS.Audit.WebApi.xml

using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Collections.Generic;
using System.IO;
using TSS.Audit.DTOs.Response;

#nullable disable
namespace TSS.Audit.WebApi.Helpers;

public class ExcelExportHelper
{
  public static MemoryStream CreateAuditLogDataExcelFile(
    List<AuditLogDataExport> exportAuditLogDataList)
  {
    MemoryStream logDataExcelFile = new MemoryStream();
    using (SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Create((Stream) logDataExcelFile, SpreadsheetDocumentType.Workbook))
    {
      WorkbookPart workbookPart = spreadsheetDocument.AddWorkbookPart();
      workbookPart.Workbook = new Workbook();
      WorksheetPart part = workbookPart.AddNewPart<WorksheetPart>();
      part.Worksheet = new Worksheet();
      workbookPart.Workbook.AppendChild<Sheets>(new Sheets()).Append((OpenXmlElement) new Sheet()
      {
        Id = (StringValue) workbookPart.GetIdOfPart((OpenXmlPart) part),
        SheetId = (UInt32Value) 1U,
        Name = (StringValue) "Audit Data"
      });
      workbookPart.Workbook.Save();
      SheetData sheetData = part.Worksheet.AppendChild<SheetData>(new SheetData());
      Row newChild1 = new Row();
      newChild1.Append((OpenXmlElement) ExcelExportHelper.ConstructCell("Transaction Code", CellValues.String), (OpenXmlElement) ExcelExportHelper.ConstructCell("Appplication Code", CellValues.String), (OpenXmlElement) ExcelExportHelper.ConstructCell("Process Name", CellValues.String), (OpenXmlElement) ExcelExportHelper.ConstructCell("Process Description", CellValues.String), (OpenXmlElement) ExcelExportHelper.ConstructCell("Process Module", CellValues.String), (OpenXmlElement) ExcelExportHelper.ConstructCell("Begin Process Timestamp", CellValues.String), (OpenXmlElement) ExcelExportHelper.ConstructCell("End Process Timestamp", CellValues.String), (OpenXmlElement) ExcelExportHelper.ConstructCell("Audit User Description", CellValues.String), (OpenXmlElement) ExcelExportHelper.ConstructCell("Audit User Identifier", CellValues.String), (OpenXmlElement) ExcelExportHelper.ConstructCell("Entity Name", CellValues.String), (OpenXmlElement) ExcelExportHelper.ConstructCell("Entity Module", CellValues.String), (OpenXmlElement) ExcelExportHelper.ConstructCell("Is Main Entity", CellValues.String), (OpenXmlElement) ExcelExportHelper.ConstructCell("Audit By", CellValues.String), (OpenXmlElement) ExcelExportHelper.ConstructCell("Audit Date", CellValues.String), (OpenXmlElement) ExcelExportHelper.ConstructCell("Table Name", CellValues.String), (OpenXmlElement) ExcelExportHelper.ConstructCell("Is Main Table", CellValues.String), (OpenXmlElement) ExcelExportHelper.ConstructCell("Operation", CellValues.String), (OpenXmlElement) ExcelExportHelper.ConstructCell("Update Mask", CellValues.String), (OpenXmlElement) ExcelExportHelper.ConstructCell("Id Column Value", CellValues.String), (OpenXmlElement) ExcelExportHelper.ConstructCell("Key Field Value", CellValues.String), (OpenXmlElement) ExcelExportHelper.ConstructCell("Timestamp", CellValues.String), (OpenXmlElement) ExcelExportHelper.ConstructCell("Row Version", CellValues.String), (OpenXmlElement) ExcelExportHelper.ConstructCell("Column Name", CellValues.String), (OpenXmlElement) ExcelExportHelper.ConstructCell("Previous Value", CellValues.String), (OpenXmlElement) ExcelExportHelper.ConstructCell("Current Value", CellValues.String));
      sheetData.AppendChild<Row>(newChild1);
      Row row = new Row();
      foreach (AuditLogDataExport exportAuditLogData in exportAuditLogDataList)
      {
        Row newChild2 = new Row();
        newChild2.Append((OpenXmlElement) ExcelExportHelper.ConstructCell(exportAuditLogData.TransactionCode, CellValues.String), (OpenXmlElement) ExcelExportHelper.ConstructCell(exportAuditLogData.ApplicationCode, CellValues.String), (OpenXmlElement) ExcelExportHelper.ConstructCell(exportAuditLogData.ProcessName, CellValues.String), (OpenXmlElement) ExcelExportHelper.ConstructCell(exportAuditLogData.ProcessDescription, CellValues.String), (OpenXmlElement) ExcelExportHelper.ConstructCell(exportAuditLogData.ProcessModule, CellValues.String), (OpenXmlElement) ExcelExportHelper.ConstructCell(exportAuditLogData.BeginProcessTimestamp, CellValues.String), (OpenXmlElement) ExcelExportHelper.ConstructCell(exportAuditLogData.EndProcessTimestamp, CellValues.String), (OpenXmlElement) ExcelExportHelper.ConstructCell(exportAuditLogData.AuditUserDescription, CellValues.String), (OpenXmlElement) ExcelExportHelper.ConstructCell(exportAuditLogData.AuditUserIdentifier, CellValues.String), (OpenXmlElement) ExcelExportHelper.ConstructCell(exportAuditLogData.EntityName, CellValues.String), (OpenXmlElement) ExcelExportHelper.ConstructCell(exportAuditLogData.EntityModule, CellValues.String), (OpenXmlElement) ExcelExportHelper.ConstructCell(exportAuditLogData.IsMainEntity, CellValues.String), (OpenXmlElement) ExcelExportHelper.ConstructCell(exportAuditLogData.AuditByFieldValue, CellValues.String), (OpenXmlElement) ExcelExportHelper.ConstructCell(exportAuditLogData.AuditDateFieldValue, CellValues.String), (OpenXmlElement) ExcelExportHelper.ConstructCell(exportAuditLogData.TableName, CellValues.String), (OpenXmlElement) ExcelExportHelper.ConstructCell(exportAuditLogData.IsMainTable, CellValues.String), (OpenXmlElement) ExcelExportHelper.ConstructCell(exportAuditLogData.Operation, CellValues.String), (OpenXmlElement) ExcelExportHelper.ConstructCell(exportAuditLogData.UpdateMask, CellValues.String), (OpenXmlElement) ExcelExportHelper.ConstructCell(exportAuditLogData.IdColumnValue, CellValues.String), (OpenXmlElement) ExcelExportHelper.ConstructCell(exportAuditLogData.KeyFieldValue, CellValues.String), (OpenXmlElement) ExcelExportHelper.ConstructCell(exportAuditLogData.Timestamp, CellValues.String), (OpenXmlElement) ExcelExportHelper.ConstructCell(exportAuditLogData.RowVersion, CellValues.String), (OpenXmlElement) ExcelExportHelper.ConstructCell(exportAuditLogData.ColumnName, CellValues.String), (OpenXmlElement) ExcelExportHelper.ConstructCell(exportAuditLogData.PreviousValue, CellValues.String), (OpenXmlElement) ExcelExportHelper.ConstructCell(exportAuditLogData.CurrentValue, CellValues.String));
        sheetData.AppendChild<Row>(newChild2);
      }
      part.Worksheet.Save();
    }
    logDataExcelFile.Seek(0L, SeekOrigin.Begin);
    return logDataExcelFile;
  }

  private static Cell ConstructCell(string value, CellValues dataType)
  {
    Cell cell = new Cell();
    cell.CellValue = new CellValue(value);
    cell.DataType = new EnumValue<CellValues>(dataType);
    return cell;
  }
}
