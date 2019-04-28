<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Mejoy.WebSite.Admin.Login" %>
<HTML>
	<HEAD>
		<title></title>
		<style type="text/css">
			body,td,th { FONT-SIZE: 12px; COLOR: #000000 }
			body {
				margin: 0px;
				background: #FFF url(/images/admin/login_bg.gif) repeat-x left top;
				font-family:"宋体","Arial Black";
			}
			a {
				color: #CCCCCC;
				text-decoration: none;
			}
			a:hover {
				text-decoration: underline;
			}
			.box {
				border-top: 3px solid #FFFFFF;
				border-left: 3px solid #FFFFFF;
				border-right: 3px solid #999999;
				border-bottom: 3px solid #999999;
			}
			.copyright{
				color: #0000FF;
			}
			.powerby{
				height: 20px;
				padding-right: 20px;
				background-color: #f4faff;
				color: #CCCCCC;
			}
			.powerby a{
				color: #CCCCCC
			}
		</style>
		
		<script language="javascript" type="text/javascript">
		function adminLogin(form){
			var re=/["'"]/gi;
			if (form.tbName.value=="" || re.test(form.tbName.value)) {alert("帐号不正确。"); form.tbName.focus(); return;}
			if (form.tbPassword.value=="" || re.test(form.tbPassword.value)) {alert("密码不正确。"); form.tbPassword.focus(); return;}
			re=/["'"]/gi;
			if (form.tbCode.value=="" || re.test(form.tbCode.value)) {alert("附加码不正确。"); form.tbCode.focus(); return;}
			form.submit();
		}
		</script>
	</HEAD>
	<body>
		<form id="Form_Admin_Login" method="post" runat="server">
			<table width="100%" height="100%" border="0" cellpadding="0" cellspacing="0">
				<tr>
					<td align="center" valign="middle">
						<table width="400" border="0" cellpadding="0" cellspacing="0" bgcolor="#d7ebff" class="box">
							<tr>
								<td height="35" align="center" bgcolor="#d7ebff" id="tdTitle" runat="server" valign="middle"><strong>系统管理平台</strong></td>
							</tr>
							<tr>
								<td bgcolor="#adbfde" height="1"></td>
							</tr>
							<tr>
								<td bgcolor="#ffffff" height="1"></td>
							</tr>
							<tr>
								<td height="150" valign="bottom" align="center" bgcolor="#f4faff">
									<table width="350" border="0" cellspacing="0" cellpadding="5">
										<tr>
											<td width="100" align="right">帐&nbsp;&nbsp;号：</td>
											<td width="250">
											    <input type="text" id="tbName" name="tbName" style="width:120px"  MaxLength="30" onkeyup="if (event.keyCode==13) adminLogin(this.form)" />
											</td>
										</tr>
										<tr>
											<td align="right">密&nbsp;&nbsp;码：</td>
											<td>
												<input type="password" id="tbPassword" name="tbPassword"  MaxLength="50" style="width:120px" onkeyup="if (event.keyCode==13) adminLogin(this.form)" />
												&nbsp;<font color="#666666">请区分大小写</font>
											</td>
										</tr>
											<tr>
											<td align="right">附加码：</td>
											<td>
												<input type="text" id="tbCode" name="tbCode" MaxLength="5"  style="width:45px" onkeyup="this.value=this.value.replace(/[^\d,a-z]/gi,''); this.value=this.value.toUpperCase(); if (event.keyCode==13) adminLogin(this.form);" />
												<img id="imgCheckCode" src="verifycode.aspx?do=adminlogin" width="65" height="22" align="absmiddle" onclick="this.src='verifycode.aspx?do=adminlogin&temp='+Math.random()" style="cursor:hand" alt="验证码" />&nbsp;&nbsp;
												<span onclick="document.getElementById('imgCheckCode').src='verifycode.aspx?do=adminlogin&temp='+Math.random()" style="cursor:hand; color:#666666"><u>看不清,换一张</u></span>
											</td>
										</tr>
										<tr>
											<td colspan="2" height="35" nowrap align="right" style="color:#CCCCCC">&nbsp;PowerBy&nbsp;<a href="http://www.mejoy.cn" target="_blank"></a></td>
										</tr>
									</table>
								</td>
							</tr>
							<tr>
								<td bgcolor="#adbfde" height="1"></td>
							</tr>
							<tr>
								<td bgcolor="#ffffff" height="1"></td>
							</tr>
							<tr>
								<td height="35" align="center"><input name="btnLogin" type="button" id="btnLogin" value="登 录" onclick="adminLogin(this.form)"></td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
		<script language="javascript" defer>document.all.tbName.focus();</script>
	</body>
</HTML>