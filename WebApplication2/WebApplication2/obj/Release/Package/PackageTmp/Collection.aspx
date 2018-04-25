<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Collection.aspx.cs" Inherits="WebApplication2.Collection" %>

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
         <div>

             Start Date:<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
             End Date:<asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>

             <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="View" />

          </div>
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Height="653px" Width="850px">
        </rsweb:ReportViewer>
    
    </div>
    </form>
</body>
</html>

<link href="Content/jquery-ui.min.css" rel="stylesheet" />
<script src="Scripts/jquery-1.10.2.min.js"></script>
<script src="Scripts/jquery-ui.min.js"></script>
<script>
   
$(function() {
    $("#<%= TextBox1.ClientID %>").datepicker({ dateFormat: 'dd-mm-yy' });
    $("#<%= TextBox2.ClientID %>").datepicker({ dateFormat: 'dd-mm-yy' });
});
</script>
