<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="new.aspx.cs" Inherits="ThoughtWeb.Admin.US_USER._new" %>
<!--#include virtual="/include/admin/header.htm"-->
  <script type="text/javascript" src="../../Script/DatePicker/config.js" language="javascript"></script>
  
<script language="javascript" src="../../Script/jquery.js"></script>
<script language="javascript" type="text/javascript">
function checkInput(form){
	if (trim(form.tbUserID.value)==""){alert("抱歉，请填写用户名！"); form.tbUserID.focus(); return;}
	if (trim(form.tbRealName.value)==""){alert("抱歉，请填写姓名！"); form.tbRealName.focus(); return;}
	if (confirm("保存数据吗？")) form.submit();
}

</script>

<form id="frmAdmin" method="post" runat="server">
	<input type="hidden" id="tbAdminId" name="tbAdminId" runat="server" value="0">
	<table width="100%" border="0" cellspacing="1" cellpadding="3" class="table_input">
		<tr>
			<td class="table_input_bar">用户管理</td>
		</tr>
		<tr>
			<td class="table_input_note">操作说明：(无)</td>
		</tr>
	</table>
	<table class="table_input" cellSpacing="1" cellPadding="3" width="100%" border="0">
		<tr>
			<td class="table_input_item">用户名(学号)：</td>
			<td class="table_input_content">
					<asp:TextBox id="tbUserID" runat="server" Width="250px" MaxLength="100"></asp:TextBox>
					</td>
					<td class="table_input_item">姓名：</td>
			<td class="table_input_content">
					<asp:TextBox id="tbRealName" runat="server" Width="250px" MaxLength="100"></asp:TextBox>
					</td>
		</tr>
		<tr runat="server" id="trPass">
			<td class="table_input_item">密码：</td>
			<td class="table_input_content">
					<asp:TextBox id="tbPassword" TextMode="Password" runat="server" Width="250px" MaxLength="100"></asp:TextBox>
					</td>
					<td class="table_input_item">确认密码：</td>
			<td class="table_input_content">
		
					<asp:TextBox id="tbPasswordConfirm" TextMode="Password" runat="server" Width="250px" MaxLength="100"></asp:TextBox>
					</td>
		</tr>
	
		<tr>
			<td class="table_input_item">联系电话：</td>
			<td class="table_input_content">
					<asp:TextBox id="tbphone" runat="server" Width="250px" MaxLength="100"></asp:TextBox>
					</td>
					<td class="table_input_item">身份证：</td>
			<td class="table_input_content">
					<asp:TextBox id="tbIdCard" runat="server" Width="250px" MaxLength="100"></asp:TextBox>
					</td>
		</tr>


		<tr>
			<td class="table_input_item">用户类型：</td>
			<td class="table_input_content">
					<asp:DropDownList runat="server" id="ddl_usertype"></asp:DropDownList>
					</td>
			<td class="table_input_item">地址：</td>
			<td class="table_input_content">
					<asp:TextBox id="tbAddress" runat="server" Width="450px" MaxLength="100"></asp:TextBox>
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

<asp:Label id="labJsInit" runat="server"></asp:Label>
<!--#include virtual="/include/admin/footer.htm"-->
