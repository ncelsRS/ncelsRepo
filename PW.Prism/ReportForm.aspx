<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportForm.aspx.cs" Inherits="PW.Prism.ReportForm" %>
<%@ Register Assembly="Stimulsoft.Report.Web, Version=2012.2.1400.0, Culture=neutral, PublicKeyToken=ebe6666cba19647a" Namespace="Stimulsoft.Report.Web" TagPrefix="cc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <cc1:StiWebViewer ID="StiWebViewer1"  runat="server" ShowToolBar="true" />
    </div>
    </form>
</body>
</html>
