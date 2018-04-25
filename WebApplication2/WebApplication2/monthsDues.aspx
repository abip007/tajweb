<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="monthsDues.aspx.cs" Inherits="WebApplication2.monthsDues" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:Label ID="Label1" runat="server" Text="Label">Number of Months</asp:Label>
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        <asp:Button ID="Button1" runat="server" Text="View" OnClick="Button1_Click" />
        
        <br />
    
    </div>
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Height="815px" Width="716px">
        </rsweb:ReportViewer>
    </form>
</body>
</html>
