<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Password.aspx.cs" Inherits="Mejoy.WebSite.Admin.Admin.Password" %>
<!--#include virtual="/include/admin/header.htm"-->

<script language="javascript" type="text/javascript">
function checkInput(form){
	if (trim(form.tbPwdA.value)=="") {alert("请输入旧密码！"); return;}
	if (trim(form.tbPwdB.value)=="" || trim(form.tbPwdB.value)!=trim(form.tbPwdC.value)) {alert("未输入完新密码或两次输入的新密码不相同！"); return;}
	if (trim(form.tbPwdA.value)==trim(form.tbPwdB.value)) {alert("旧密码与新密码相同，请修改！"); return;}
	
	if (confirm("确认修改密码？\n\n友情提示：密码修改成功后，系统将自动退出，请用新密码登录！")) form.submit();
}
</script>

<form id="frmAdmin" method="post" runat="server">
	<table width="100%" border="0" cellspacing="1" cellpadding="3" class="table_input">
		<tr>
			<td class="table_input_bar">管理员密码修改</td>
		</tr>
		<tr>
			<td class="table_input_note">操作说明：(无)</td>
		</tr>
	</table>
	<table class="table_input" cellSpacing="1" cellPadding="3" width="100%" border="0">
		<tr>
			<td class="table_input_item" width="100">登录名：</td>
			<td class="table_input_content" id="tdLoginName" runat="server"></td>
		</tr>
		<tr>
			<td class="table_input_item">旧密码：</td>
			<td class="table_input_content">
				<asp:TextBox id="tbPwdA" runat="server" TextMode="Password" MaxLength="30" Width="200px"></asp:TextBox></td>
		</tr>
		<tr>
			<td class="table_input_item">新密码：</td>
			<td class="table_input_content">
				<asp:TextBox id="tbPwdB" runat="server" TextMode="Password" MaxLength="30" Width="200px"></asp:TextBox></td>
		</tr>
		<tr>
			<td class="table_input_item">重复新密码：</td>
			<td class="table_input_content">
				<asp:TextBox id="tbPwdC" runat="server" TextMode="Password" MaxLength="30" Width="200px"></asp:TextBox></td>
		</tr>
	</table>
	<table class="table_btn">
		<tr>
			<td>
				<input type="button" name="btn" class="btn" value="修改密码" onclick="checkInput(this.form)">
			</td>
		</tr>
	</table>
</form>

<!--#include virtual="/include/admin/footer.htm"-->