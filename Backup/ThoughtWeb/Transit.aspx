<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Transit.aspx.cs" Inherits="ThoughtWeb.Transit" %>

<%@ Register Assembly="CKFinder" Namespace="CKFinder" TagPrefix="CKFinder" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <CKFinder:FileBrowser ID="FileBrowser1" runat="server">
        </CKFinder:FileBrowser>
    </div>
    </form>
</body>
</html>
