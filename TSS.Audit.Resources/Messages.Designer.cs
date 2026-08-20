// Decompiled with JetBrains decompiler
// Type: TSS.Audit.Resources.Messages
// Assembly: TSS.Audit.Resources, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: ECF6E8F3-3688-42C4-BE63-288B03E7F00C
// Assembly location: D:\Baufest\GIT\TSS.Audit.WebApi\TSS.Audit.Resources.dll

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

#nullable disable
namespace TSS.Audit.Resources;

[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
[DebuggerNonUserCode]
[CompilerGenerated]
public class Messages
{
  private static ResourceManager resourceMan;
  private static CultureInfo resourceCulture;

  internal Messages()
  {
  }

  [EditorBrowsable(EditorBrowsableState.Advanced)]
  public static ResourceManager ResourceManager
  {
    get
    {
      if (Messages.resourceMan == null)
        Messages.resourceMan = new ResourceManager("TSS.Audit.Resources.Messages", typeof (Messages).Assembly);
      return Messages.resourceMan;
    }
  }

  [EditorBrowsable(EditorBrowsableState.Advanced)]
  public static CultureInfo Culture
  {
    get => Messages.resourceCulture;
    set => Messages.resourceCulture = value;
  }

  public static string AppAlreadyExists
  {
    get => Messages.ResourceManager.GetString(nameof (AppAlreadyExists), Messages.resourceCulture);
  }

  public static string AppInvalid
  {
    get => Messages.ResourceManager.GetString(nameof (AppInvalid), Messages.resourceCulture);
  }

  public static string AppNotExists
  {
    get => Messages.ResourceManager.GetString(nameof (AppNotExists), Messages.resourceCulture);
  }

  public static string BadParameter
  {
    get => Messages.ResourceManager.GetString(nameof (BadParameter), Messages.resourceCulture);
  }

  public static string ColumnNameInvalid
  {
    get => Messages.ResourceManager.GetString(nameof (ColumnNameInvalid), Messages.resourceCulture);
  }

  public static string ColumnNamesDuplicated
  {
    get
    {
      return Messages.ResourceManager.GetString(nameof (ColumnNamesDuplicated), Messages.resourceCulture);
    }
  }

  public static string ColumnNotExistsFormat
  {
    get
    {
      return Messages.ResourceManager.GetString(nameof (ColumnNotExistsFormat), Messages.resourceCulture);
    }
  }

  public static string EntityAlreadyExists
  {
    get
    {
      return Messages.ResourceManager.GetString(nameof (EntityAlreadyExists), Messages.resourceCulture);
    }
  }

  public static string EntityNameInvalid
  {
    get => Messages.ResourceManager.GetString(nameof (EntityNameInvalid), Messages.resourceCulture);
  }

  public static string EntityNotExists
  {
    get => Messages.ResourceManager.GetString(nameof (EntityNotExists), Messages.resourceCulture);
  }

  public static string EntityNotExistsFormat
  {
    get
    {
      return Messages.ResourceManager.GetString(nameof (EntityNotExistsFormat), Messages.resourceCulture);
    }
  }

  public static string EntityTableAlreadyExists
  {
    get
    {
      return Messages.ResourceManager.GetString(nameof (EntityTableAlreadyExists), Messages.resourceCulture);
    }
  }

  public static string EntityTableColumnAlreadyExists
  {
    get
    {
      return Messages.ResourceManager.GetString(nameof (EntityTableColumnAlreadyExists), Messages.resourceCulture);
    }
  }

  public static string EntityTableColumnNotExists
  {
    get
    {
      return Messages.ResourceManager.GetString(nameof (EntityTableColumnNotExists), Messages.resourceCulture);
    }
  }

  public static string EntityTableNameInvalid
  {
    get
    {
      return Messages.ResourceManager.GetString(nameof (EntityTableNameInvalid), Messages.resourceCulture);
    }
  }

  public static string FromDateInvalid
  {
    get => Messages.ResourceManager.GetString(nameof (FromDateInvalid), Messages.resourceCulture);
  }

  public static string MissingExternalSecurityServiceSettings
  {
    get
    {
      return Messages.ResourceManager.GetString(nameof (MissingExternalSecurityServiceSettings), Messages.resourceCulture);
    }
  }

  public static string ModuleInvalid
  {
    get => Messages.ResourceManager.GetString(nameof (ModuleInvalid), Messages.resourceCulture);
  }

  public static string NoExternalServiceResponseReceived
  {
    get
    {
      return Messages.ResourceManager.GetString(nameof (NoExternalServiceResponseReceived), Messages.resourceCulture);
    }
  }

  public static string ProcessAlreadyExists
  {
    get
    {
      return Messages.ResourceManager.GetString(nameof (ProcessAlreadyExists), Messages.resourceCulture);
    }
  }

  public static string ProcessNameInvalid
  {
    get
    {
      return Messages.ResourceManager.GetString(nameof (ProcessNameInvalid), Messages.resourceCulture);
    }
  }

  public static string ProcessNotExists
  {
    get => Messages.ResourceManager.GetString(nameof (ProcessNotExists), Messages.resourceCulture);
  }

  public static string TableNameInvalid
  {
    get => Messages.ResourceManager.GetString(nameof (TableNameInvalid), Messages.resourceCulture);
  }

  public static string TableNamesDuplicated
  {
    get
    {
      return Messages.ResourceManager.GetString(nameof (TableNamesDuplicated), Messages.resourceCulture);
    }
  }

  public static string TableNotExists
  {
    get => Messages.ResourceManager.GetString(nameof (TableNotExists), Messages.resourceCulture);
  }

  public static string TableNotExistsFormat
  {
    get
    {
      return Messages.ResourceManager.GetString(nameof (TableNotExistsFormat), Messages.resourceCulture);
    }
  }

  public static string TenantInvalid
  {
    get => Messages.ResourceManager.GetString(nameof (TenantInvalid), Messages.resourceCulture);
  }

  public static string ToDateInvalid
  {
    get => Messages.ResourceManager.GetString(nameof (ToDateInvalid), Messages.resourceCulture);
  }

  public static string UnexpectedErrorMessage
  {
    get
    {
      return Messages.ResourceManager.GetString(nameof (UnexpectedErrorMessage), Messages.resourceCulture);
    }
  }

  public static string UsernameInvalid
  {
    get => Messages.ResourceManager.GetString(nameof (UsernameInvalid), Messages.resourceCulture);
  }

  public static string UserNotHavePermissions
  {
    get
    {
      return Messages.ResourceManager.GetString(nameof (UserNotHavePermissions), Messages.resourceCulture);
    }
  }
}
