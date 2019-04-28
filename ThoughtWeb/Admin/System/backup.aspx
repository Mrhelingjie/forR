<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="backup.aspx.cs" Inherits="ThoughtWeb.Admin.System.backup" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<!--#include virtual="/include/admin/header.htm"-->
<script type="text/javascript" src="../../Script/DatePicker/config.js" language="javascript"></script>
<script language="javascript" src="../../Script/jquery.js"></script>
    <script type="text/javascript" src="../../Script/DatePicker/WdatePicker.js" language="javascript"></script>
<script language="javascript" type="text/javascript">
    function checkInput(form) {
        if (confirm("数据库备份？")) form.submit();
    }

</script>
<form id="frmAdmin" method="post" runat="server">
<input type="hidden" id="tbAdminId" name="tbAdminId" runat="server" value="0">
<table width="100%" border="0" cellspacing="1" cellpadding="3" class="table_input">
    <tr>
        <td class="table_input_bar">
          数据库备份
        </td>
    </tr>
    <tr>
        <td class="table_input_note">
            操作说明：(无)
        </td>
    </tr>
</table>
<table class="table_input" cellspacing="1" cellpadding="3" width="100%" border="0">
<tr>
     <td class="table_input_item">
            数据库备份：
        </td>
        <td class="table_input_content" colspan="3">
       <input type="button" name="btn" class="btn" value="确定备份" onclick="checkInput(this.form)">
        </td>
  
    </tr>
  
</table>
<table class="table_btn">
   
</table>
</form>
<asp:label id="labJsInit" runat="server"></asp:label>
<!--#include virtual="/include/admin/footer.htm"-->



