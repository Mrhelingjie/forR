<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="list.aspx.cs" Inherits="ThoughtWeb.Admin.US_USER.list" %>

<%@ Register Assembly="Control" Namespace="Havsh.Application.Control" TagPrefix="UD" %>
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
			
			case "view":
			window.location.href = "view.aspx?id="+ id +"&temp="+Math.random();
			break;
	}
}
</script>

<form id="frmAdmin" method="post" runat="server">
	<table width="100%" border="0" cellspacing="1" cellpadding="3" class="table_input">
		<tr>
			<td class="table_input_bar">用户管理</td>
		</tr>
		<tr>
			<td class="table_input_note">操作说明：(无)</td>
		</tr>
	</table>
	
	<table width="100%" border="0" cellpadding="3" cellspacing="1" class="table_list">
		<tr>
			<td colspan="5" align="right">
				<input type="button" name="btn" class="btn" value="添加用户" onclick="window.location.href='new.aspx?'+Math.random()">
			</td>
		</tr>
	</table>
	
	<table width="100%" border="0" cellpadding="3" cellspacing="1" class="table_list">
		<tr class="table_list_title">
			<td  align="center">姓名</td>
				<td  align="center">联系电话</td>
					<%--<td  align="center">账号类型</td>--%>
						<td  align="center">身份证</td>
							<%--<td  align="center">账号类别</td>--%>
			<td  width="10%" align="center">编辑</td>
			<td  width="10%" align="center">删除</td>
		</tr>
		<asp:Repeater id="rptList"  runat="server">
			<ItemTemplate>
			<tr class="table_list_item">
				 <td>
                                    <%#Eval("RealName")%>
                                </td>

                                		 <td>
                                    <%#Eval("Telephone")%>
                                </td>	
                                <%-- <td>
                                    <%#Eval("Dept")%>
                                </td>	--%>
                                <td>
                                    <%#Eval("IDCard")%>
                                </td>	
                                <%-- <td>
                                    <%#Eval("UserType")%>
                                </td>	--%>
                                 <td>
                                  <input type="button" name="btn" class="btn" value="编辑" onclick="listEvent('edit', '<%#Eval("Admin_ID")%>')"/>
                                </td>
				 <td>
                             <input type="button" name="btn" class="btn" value="删除" onclick="listEvent('del', '<%#Eval("Admin_ID")%>')"/>
                                </td>
			</tr>
			</ItemTemplate>
		</asp:Repeater>
	</table>
		<table width="100%" border="0" cellpadding="3" cellspacing="1" class="table_list">
		<tr><td align="right">
			    <div id="div_fengye_left" class="float_left">

                    <asp:Literal ID="ltrRecordCount" runat="server"></asp:Literal>
                </div>
                <div id="div_fengye_right" class="float_right pager">
                    <UD:PagePilot runat="server" ID="pp1" />
                </div></td>
		</tr>
	</table>
</form>

<!--#include virtual="/include/admin/footer.htm"-->