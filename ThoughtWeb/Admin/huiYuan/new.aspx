<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="new.aspx.cs" Inherits="ThoughtWeb.Admin.huiYuan._new" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<!--#include virtual="/include/admin/header.htm"-->
<script type="text/javascript" src="../../Script/DatePicker/config.js" language="javascript"></script>
<script language="javascript" src="../../Script/jquery.js"></script>
    <script type="text/javascript" src="../../Script/DatePicker/WdatePicker.js" language="javascript"></script>
<script language="javascript" type="text/javascript">
    function checkInput(form) {
      
        //        if (trim(form.tbYeZhuName.value) == "") { alert("抱歉，请填写业主姓名！"); form.tbYeZhuName.focus(); return; }
        if (confirm("保存数据吗？")) form.submit();
    }

</script>
<form id="frmAdmin" method="post" runat="server">
<input type="hidden" id="tbAdminId" name="tbAdminId" runat="server" value="0">
<table width="100%" border="0" cellspacing="1" cellpadding="3" class="table_input">
    <tr>
        <td class="table_input_bar">
          会员车管理
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
            车牌号：
        </td>
        <td class="table_input_content">
         <asp:textbox id="tbChePai" runat="server" width="250px" maxlength="100"></asp:textbox>
        </td>
         <td class="table_input_item">
            车主姓名：
        </td>
        <td class="table_input_content">
            <asp:textbox id="tbName" runat="server" width="250px" maxlength="100"></asp:textbox>
        </td>
       
    </tr>
    <tr>
    
         <td class="table_input_item">
            联系电话：
        </td>
        <td class="table_input_content" colspan="3">
          <asp:textbox id="tbPhone" runat="server" width="250px" maxlength="100"></asp:textbox>
        </td>
       
    </tr>
   
</table>
<table class="table_btn">
    <tr>
        <td>
            <input type="button" name="btn" class="btn" value="保存数据" onclick="checkInput(this.form)">
            <input type="button" name="btn" class="btn" value="返回列表" id="btnBack" runat="server" />
        </td>
    </tr>
</table>
</form>
<asp:label id="labJsInit" runat="server"></asp:label>
<!--#include virtual="/include/admin/footer.htm"-->
