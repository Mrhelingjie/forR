<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="New.aspx.cs" Inherits="Mejoy.WebSite.Admin.Menu.New" %>
<!--#include virtual="/include/admin/header.htm"-->

<script language="javascript" type="text/javascript">
function checkInput(form){
	if (form.ddlParentId.value!="0" && form.ddlParentId.value==form.tbMenuId.value){alert("菜单级别为相同级别！"); return;}
	if (trim(form.tbName.value)==""){alert("请填写菜单标题或链接名称！"); form.tbName.focus(); return;}
	
	if (confirm("保存数据吗？")) form.submit();
}
</script>

<form id="frmAdmin" method="post" runat="server">
	<input type="hidden" id="tbMenuId" name="tbMenuId" runat="server" value="0">
	<table width="100%" border="0" cellspacing="1" cellpadding="3" class="table_input">
		<tr>
			<td class="table_input_bar">菜单管理</td>
		</tr>
		<tr>
			<td class="table_input_note">操作说明：(无)</td>
		</tr>
	</table>
	<table class="table_input" cellSpacing="1" cellPadding="3" width="100%" border="0">
		<tr>
			<td class="table_input_item" width="120">菜单级别：</td>
			<td class="table_input_content">
				<asp:DropDownList id="ddlParentId" runat="server"></asp:DropDownList></td>
		</tr>
		<tr>
			<td class="table_input_item">菜单名称：</td>
			<td class="table_input_content">
				<asp:TextBox id="tbName" runat="server" MaxLength="50" Width="200px"></asp:TextBox></td>
		</tr>
		<tr>
			<td class="table_input_item">链接地址：</td>
			<td class="table_input_content">
				<asp:TextBox id="tbLink" runat="server" MaxLength="200" Width="300px"></asp:TextBox></td>
		</tr>
		<tr>
			<td class="table_input_item">菜单树状态：</td>
			<td class="table_input_content">
				<asp:RadioButtonList id="rblTreeClose" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow"
					CssClass="nostyle">
					<asp:ListItem Value="0">展开</asp:ListItem>
					<asp:ListItem Value="1">收缩</asp:ListItem>
				</asp:RadioButtonList></td>
		</tr>
		<tr>
			<td class="table_input_item">菜单类型：</td>
			<td class="table_input_content">
				<asp:DropDownList id="ddlType" runat="server">
					<asp:ListItem Value="0">普通菜单</asp:ListItem>
					<asp:ListItem Value="1">高级菜单</asp:ListItem>
				</asp:DropDownList>
			</td>
		</tr>
		<tr>
			<td class="table_input_item">排序：</td>
			<td class="table_input_content">
				<asp:TextBox id="tbIndex" runat="server" MaxLength="5" Width="50px" Text="0"></asp:TextBox>
				<span class="memo">下数提升，负数下降。</span>
			</td>
		</tr>
		<tr>
			<td class="table_input_item">打开窗口：</td>
			<td class="table_input_content">
			    <asp:DropDownList id="ddlTarget" runat="server">
					<asp:ListItem Value="0">默认窗口</asp:ListItem>
					<asp:ListItem Value="1">新窗口</asp:ListItem>
				</asp:DropDownList>
			</td>
		</tr>
	</table>
	<table class="table_btn">
		<tr>
			<td>
				<input type="button" name="btn" class="btn" value="保存数据" onclick="checkInput(this.form)">
				<input type="button" name="btn" class="btn" value="返回列表" onclick="window.location.href='list.aspx?'+Math.random()">
			</td>
		</tr>
	</table>
</form>
<!--#include virtual="/include/admin/footer.htm"-->