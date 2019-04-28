<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="New.aspx.cs" Inherits="Mejoy.WebSite.Admin.Admin.New" %>
<!--#include virtual="/include/admin/header.htm"-->
<script language="javascript" type="text/javascript">
function checkInput(form){
	if (trim(form.tbLoginName.value)==""){alert("抱歉，请填写登录帐号！"); form.tbLoginName.focus(); return;}
	if (form.tbAdminId.value=="0"){
		if (trim(form.tbPassword.value)=="") {alert("抱歉，请填写登录密码！"); form.tbPassword.focus(); return;}
	}
	
	if (confirm("保存数据吗？")) form.submit();
}

function setRights(form, level, sel){
	var cb = form.cbRights;
	
	//取消所有选择
	for (var i=0; i<cb.length; i++){
		cb[i].checked = false;
	}
	
	//重新选择
	switch(parseInt(level, 10)){
		case 99:  //超级管理员
			for (var i=0; i<cb.length; i++){
				cb[i].checked = true;
			}
			break;
		default:
			if (sel != ""){
				var Item = sel.split(",");
				for (var i=0; i<cb.length; i++){
					for (var j=0; j<Item.length; j++){
						if (cb[i].value==Item[j]){
							cb[i].checked = true;
							break;
						}
					}
				}
			}
			break;
	}
}

function setArea(form, level, sel){
	var cb = document.getElementsByName("cbArea");

	//取消所有选择
	for (var i=0; i<cb.length; i++){
		cb[i].checked = false;
	}
	
	//重新选择
	switch(parseInt(level, 10)){
		case 99:  //超级管理员
			for (var i=0; i<cb.length; i++){
				cb[i].checked = true;
			}
			break;
		default:
			if (sel != ""){
				var Item = sel.split(",");
				for (var i=0; i<cb.length; i++){
					for (var j=0; j<Item.length; j++){
						if (cb[i].value==Item[j]){
							cb[i].checked = true;
							break;
						}
					}
				}
			}
			break;
	}
}
</script>

<form id="frmAdmin" method="post" runat="server">
	<input type="hidden" id="tbAdminId" name="tbAdminId" runat="server" value="0">
	<table width="100%" border="0" cellspacing="1" cellpadding="3" class="table_input">
		<tr>
			<td class="table_input_bar">管理员管理</td>
		</tr>
		<tr>
			<td class="table_input_note">操作说明：(无)</td>
		</tr>
	</table>
	<table class="table_input" cellSpacing="1" cellPadding="3" width="100%" border="0">
		<tr>
			<td class="table_input_item" width="100">登录名：</td>
			<td class="table_input_content">
				<asp:TextBox id="tbLoginName" runat="server" Width="150px" MaxLength="30"></asp:TextBox></td>
		</tr>
		<tr>
			<td class="table_input_item">登录密码：</td>
			<td class="table_input_content">
				<asp:TextBox id="tbPassword" runat="server" Width="150px" MaxLength="30" TextMode="Password"></asp:TextBox>
				<span class="memo">登录密码区分大小写。</span>
			</td>
		</tr>
		<tr>
			<td class="table_input_item">帐号说明：</td>
			<td class="table_input_content">
				<asp:TextBox id="tbNote" runat="server" Width="200px" MaxLength="100"></asp:TextBox></td>
		</tr>
		<tr>
			<td class="table_input_item">级别：</td>
			<td class="table_input_content">
				<asp:DropDownList id="ddlLevel" runat="server">
					<asp:ListItem Value="0">普通管理员</asp:ListItem>
					<asp:ListItem Value="99">超级管理员</asp:ListItem>
				</asp:DropDownList></td>
		</tr>
		<tr>
			<td class="table_input_item">锁定：</td>
			<td class="table_input_content">
				<asp:RadioButtonList id="rblLock" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" CssClass="nostyle">
					<asp:ListItem Value="0">开启</asp:ListItem>
					<asp:ListItem Value="1">锁定</asp:ListItem>
				</asp:RadioButtonList></td>
		</tr>
		<tr>
			<td class="table_input_item">小区：</td>
			<td class="table_input_content perList" id="tdArea" runat="server"></td>
		</tr>
		<tr>
			<td class="table_input_item">权限：</td>
			<td class="table_input_content perList" id="tdRights" runat="server"></td>
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