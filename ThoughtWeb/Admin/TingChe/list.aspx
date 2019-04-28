<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="list.aspx.cs" Inherits="ThoughtWeb.Admin.TingChe.list" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="Control" Namespace="Havsh.Application.Control" TagPrefix="UD" %>
<!--#include virtual="/include/admin/header.htm"-->
<script language="javascript">
    function listEvent(what, id) {
        switch (what) {
            case "edit":
                window.location.href = "new.aspx?do=edit&id=" + id + "&temp=" + Math.random();
                break;

            case "del":
                if (confirm("确认要删除此记录吗？\n\n友情提醒：记录一旦被删除后，将无法恢复！")) {
                    window.location.href = "?do=del&id=" + id + "&temp=" + Math.random();
                }
                break;

            case "up":
                window.location.href = "?do=up&id=" + id + "&temp=" + Math.random();
                break;

            case "view":
                window.location.href = "view.aspx?id=" + id + "&temp=" + Math.random();
                break;
        }
    }
</script>
<form id="frmAdmin" method="post" runat="server">
<table width="100%" border="0" cellspacing="1" cellpadding="3" class="table_input">
    <tr>
        <td class="table_input_bar">
            入场管理
        </td>
    </tr>
    <tr>
        <td class="table_input_note">
            操作说明：(无)
        </td>
    </tr>
</table>
<table width="100%" border="0" cellpadding="3" cellspacing="1" class="table_list">
    
</table>
<table width="100%" border="0" cellpadding="3" cellspacing="1" class="table_list">
    <tr>
        <td colspan="5" align="right" runat="server" id="kkk">
            
        </td>
    </tr>
</table>
<table width="100%" border="0" cellpadding="3" cellspacing="1" class="table_list">
    <tr class="table_list_title">
        <td align="center">
            车牌号
        </td>
        <td align="center">
            入场时间
        </td>

        <td width="10%" align="center">
            删除
        </td>
    </tr>
    <asp:repeater id="rptList" runat="server">
			<ItemTemplate>
			<tr class="table_list_item">
				 <td>
                                    <%#Eval("ChePai")%>
                                </td> 
                                	 <td>
                                    <%#Eval("RuChangShiJian")%>
                                </td>
                                
				 <td>
                             <input type="button" name="btn" class="btn" value="删除" onclick="listEvent('del', '<%#Eval("ID")%>')"/>
                                </td>
			</tr>
			</ItemTemplate>
		</asp:repeater>
</table>
<table width="100%" border="0" cellpadding="3" cellspacing="1" class="table_list">
    <tr>
        <td align="right">
            <div id="div_fengye_left" class="float_left">
                <asp:literal id="ltrRecordCount" runat="server"></asp:literal>
            </div>
            <div id="div_fengye_right" class="float_right pager">
                <UD:PagePilot runat="server" ID="pp1" />
            </div>
        </td>
    </tr>
</table>
</form>
<!--#include virtual="/include/admin/footer.htm"-->
