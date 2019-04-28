<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="Mejoy.WebSite.Admin.Menu.List" %>
<!--#include virtual="/include/admin/header.htm"-->

<script language="javascript">
function listEvent(what, id){
	switch(what){
		case "edit":
			window.location.href = "new.aspx?do=edit&id="+ id +"&temp="+Math.random();
			break;
			
		case "del":
			if (confirm("确认要删除此记录吗？\n\n友情提醒：记录一旦被删除后，将无法恢复！")){
				window.location.href = "?do=del&id="+ id +"&temp="+Math.random();
			}
			break;
			
		case "up":
			window.location.href = "?do=up&id="+ id +"&temp="+Math.random();
			break;
			
		case "down":
			window.location.href = "?do=down&id="+ id +"&temp="+Math.random();
			break;
	}
}
</script>

<form id="frmAdmin" method="post" runat="server">
	<table width="100%" border="0" cellspacing="1" cellpadding="3" class="table_input">
		<tr>
			<td class="table_input_bar">菜单管理</td>
		</tr>
		<tr>
			<td class="table_input_note">操作说明：(无)</td>
		</tr>
	</table>
	
	<table width="100%" border="0" cellpadding="3" cellspacing="1" class="table_list">
		<tr>
			<td colspan="5" align="right">
				<input type="button" name="btn" class="btn" value="添加菜单" onclick="window.location.href='new.aspx?'+Math.random()">
			</td>
		</tr>
	</table>
	<table width="100%" border="0" cellpadding="3" cellspacing="1" class="table_list">
		<tr class="table_list_title">
			<td>菜单标题/链接名称</td>
			<td colspan="2">上/下移动</td>
			<td width="55">编辑</td>
			<td width="55">删除</td>
		</tr>
		<asp:Repeater id="rptPList" OnItemCreated="RePList" runat="server">
			<ItemTemplate>
				<tr class="table_list_item">
					<td id="tdPName" runat="server" align="left">名称</td>
					<td id="tdPUp" runat="server" width="55">上</td>
					<td id="tdPDown" runat="server" width="55">下</td>
					<td id="tdPEdit" runat="server">编辑</td>
					<td id="tdPDel" runat="server">删除</td>
				</tr>
				<asp:Repeater id="rptCList" OnItemCreated="ReCList" runat="server">
					<ItemTemplate>
						<tr class="table_list_item">
							<td id="tdCName" runat="server" align="left">名称</td>
							<td id="tdCUp" runat="server" width="55">上</td>
							<td id="tdCDown" runat="server" width="55">下</td>
							<td id="tdCEdit" runat="server">编辑</td>
							<td id="tdCDel" runat="server">删除</td>
						</tr>
					</ItemTemplate>
				</asp:Repeater>
			</ItemTemplate>
		</asp:Repeater>
	</table>
</form>

<!--#include virtual="/include/admin/footer.htm"-->