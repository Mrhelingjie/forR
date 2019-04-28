<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="Mejoy.WebSite.Admin.Admin.List" %>
<!--#include virtual="/include/admin/header.htm"-->

<form id="frmAdmin" method="post" runat="server">
	<table width="100%" border="0" cellspacing="1" cellpadding="3" class="table_input">
		<tr>
			<td class="table_input_bar">管理员管理</td>
		</tr>
		<tr>
			<td class="table_input_note">操作说明：(无)</td>
		</tr>
	</table>
	
	<table width="100%" border="0" cellpadding="3" cellspacing="1" class="table_list">
		<tr>
			<td colspan="6" align="right">
				<input type="button" name="btn" class="btn" value="添加管理员" onclick="addNew()">
			</td>
		</tr>
	</table>
	
	<table width="100%" border="0" cellpadding="3" cellspacing="1" class="table_list">
		<tr class="table_list_title">
			<td width="55">序号</td>
			<td width="150">登录名</td>
			<td>帐号说明</td>
			<td width="70">状态</td>
			<td width="55">编辑</td>
			<td width="55">删除</td>
		</tr>
		<asp:Repeater id="rptList" OnItemCreated="ReList" runat="server">
			<ItemTemplate>
			<tr class="table_list_item">
				<td runat="server" id="tdNo">序号</td>
				<td runat="server" id="tdLoginName">登录名</td>
				<td runat="server" id="tdNote">帐号说明</td>
				<td runat="server" id="tdLock">状态</td>
				<td runat="server" id="tdEdit">编辑</td>
				<td runat="server" id="tdDel">删除</td>
			</tr>
			</ItemTemplate>
		</asp:Repeater>
	</table>
	
	<table width="100%" border="0" cellpadding="3" cellspacing="1" class="table_list">
		<tr>
			<td align="right" id="tdPage" runat="server">分页</td>
		</tr>
	</table>
</form>
<!--#include virtual="/include/admin/footer.htm"-->