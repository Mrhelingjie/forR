<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index_Top.aspx.cs" Inherits="Mejoy.WebSite.Admin.Index_Top" %>
<HTML>
	<HEAD>
		<title>top</title>
		<style type="text/css">
		body,td,th { font-size: 12px; color: #FFFFFF; font-family: "宋体", "Arial Black" }
		body { margin: 0px; background-color: #336699 }
		A:link { COLOR: #ffffff }
		A:active { COLOR: #ff0000 }
		A:visited { COLOR: #ffffff }
		.topBg { BACKGROUND-POSITION: right 50%; BACKGROUND-IMAGE: url(/images/admin/top_bg.gif); BACKGROUND-REPEAT: no-repeat }
		</style>
		<base target="mainFrame">
	</HEAD>
	<body>
		<script language="javascript">
		function showLeftMenu(){
			var obj = top.footFrame;
			if (obj.cols=="180,*"){
				obj.cols = "5,*";
				document.all.imgShow.src = "/images/admin/menu_show.gif";
			}else{
				obj.cols = "180,*";
				document.all.imgShow.src = "/images/admin/menu_hide.gif";
			}
		}
		
		function fnGo(obj){
			if (obj.value=="") return;
			window.open(obj.value);
			obj.options[0].selected = true;
		}
		</script>
		<table width="100%" border="0" cellpadding="0" cellspacing="0" bgcolor="#336699" class="topBg">
			<tr>
				<td width="190" height="60" align="right"><%--<img src="/images/admin/top_logo.gif" border="0" width="165" height="38">--%></td>
				<td>&nbsp;</td>
				<td width="400" align="right">&nbsp;</td>
			</tr>
		</table>
		<table width="100%" border="0" cellpadding="0" cellspacing="0" bgcolor="#4791c5">
			<tr>
				<td colspan="4" height="1" bgcolor="#87b3d0"></td>
			</tr>
			<tr>
				<td width="163" height="22" align="center" id="tdDate" runat="server">&nbsp;</td>
				<td width="20" onclick="showLeftMenu()" style="cursor:hand"><img src="/images/admin/menu_hide.gif" id="imgShow" border="0" width="20" width="16" alt="显示/隐藏菜单"></td>
				<td align="left">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;当前管理员：<asp:Label id="labAdmin" Runat="server"></asp:Label></td>
				<td width="300" align="right" class="nav">
					<a href="index_main.aspx">管理首页</a>│
					<a href="/" target="_blank">网站首页</a>│
					<a href="password.aspx?at=edit">密码管理</a>│<a href="login.aspx?do=logout">退出系统</a>&nbsp;&nbsp;&nbsp;</td>
			</tr>
			<tr>
				<td colspan="4" height="1" bgcolor="#336699"></td>
			</tr>
			<tr>
				<td colspan="4" height="1" bgcolor="#87b3d0"></td>
			</tr>
		</table>
		<iframe id="iTemp" name="iTemp" src="" style="height:0; width:0"></iframe>
	</body>
</HTML>