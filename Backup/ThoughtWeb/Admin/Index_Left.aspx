<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index_Left.aspx.cs" Inherits="Mejoy.WebSite.Admin.Index_Left" %>
<HTML>
	<HEAD>
		<title>left</title>
		<style type="text/css">
		body,td,th {
			color: #FFFFFF;
			font-family: 宋体, Arial;
			font-size: 12px;
		}
		body {
			background-color: #4791C5;
			margin: 0px;
			scrollbar-3d-light-color:#CCCCCC;
			scrollbar-base-color:#A6D2FF;
			scrollbar-arrow-color: #003366;
		}
		a:link {
			color: #000000;
			text-decoration: none;
		}
		a:active {
			color: #FF0000;
			text-decoration: none;
		}
		a:visited {
			color: #000000;
			text-decoration: none;
		}
		a:hover {
			color: #FF6600;
			text-decoration: underline;
		}
		.menu_title{
			padding-top: 3px;
			font-size: 12px;
			font-weight: bold;
			color: #FFFFFF;
			cursor: hand;
			background: #4f8ed1 url(/images/admin/menu_bg.gif) repeat-x;
		}
		.menu_border{
			border: 1px solid #666666;
		}
		</style>
	</HEAD>
	<base target="mainFrame">
	
	<body bgcolor="#4791C5">
		<script language="javascript">
		function showMenuTree(obj){
			(obj.parentNode.parentNode.rows[1].style.display=='none') ? obj.parentNode.parentNode.rows[1].style.display='' : obj.parentNode.parentNode.rows[1].style.display='none';
		}
		</script>
		<table width="100%" border="0" cellspacing="5" cellpadding="0">
			<asp:Repeater OnItemCreated="ReParentMenuCreate" ID="rptParentMenu" Runat="server">
				<ItemTemplate>
					<tr id="trPMenu" runat="server">
						<td>
							<table width="100%" border="0" cellpadding="0" cellspacing="0" class="menu_border">
								<tr bgcolor="#4f8ed1">
									<td width="16" height="24" bgcolor="#4f8ed1" onclick="showMenuTree(this)"><img src="/images/admin/menu_left.gif" width="16" height="24" /></td>
									<td align="left" class="menu_title" id="tdMenuTitle" runat="server" onclick="showMenuTree(this)">菜单标题</td>
									<td width="49" bgcolor="#4f8ed1" onClick="showMenuTree(this)"><img src="/images/admin/menu_right.gif" width="49" height="24" style="CURSOR:hand" /></td>
								</tr>
								<tr align="left" valign="top" bgcolor="#f6f6f6" id="trLink" runat="server">
									<td colspan="3">
										<table width="100%" border="0" cellspacing="0" cellpadding="0">
											<asp:Repeater ID="rptChildMenu" OnItemCreated="ReChildMenuCreate" Runat="server">
												<ItemTemplate>
													<tr>
														<td width="24" height="22" align="center"><img src="/images/admin/menu_dot.gif" width="4" height="7"></td>
														<td id="tdLink" runat="server">链接</td>
													</tr>
												</ItemTemplate>
											</asp:Repeater>
										</table>
									</td>
								</tr>
							</table>
						</td>
					</tr>
				</ItemTemplate>
			</asp:Repeater>
		</table>
	</body>
</HTML>