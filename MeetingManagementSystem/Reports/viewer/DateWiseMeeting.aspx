<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DateWiseMeeting.aspx.cs" Inherits="MeetingManagementSystem.Reports.Viewer.DateWiseMeeting" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <rsweb:ReportViewer ID="ReportViewer1" runat="server" Height="1500px" Width="100%"></rsweb:ReportViewer>
            <asp:ScriptManager ID="ScriptManager1" runat="server" ></asp:ScriptManager>
        </div>
    </form>
</body>
</html>




