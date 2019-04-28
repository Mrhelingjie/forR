/*
 * 功能：去除字符首尾空格
 * 参数：inputString - 执行去除空格的字符串
 * 返回：string(返回首尾空格的字符)
 */
function trim(inputString) {
	if (typeof inputString != "string") { return inputString; }
	var retValue = inputString;
	var ch = retValue.substring(0, 1);
	while (ch == " ") {
		//检查字符串开始部分的空格
		retValue = retValue.substring(1, retValue.length);
		ch = retValue.substring(0, 1);
	}
	
	ch = retValue.substring(retValue.length-1, retValue.length);
	while (ch == " ") {
		//检查字符串结束部分的空格
		retValue = retValue.substring(0, retValue.length-1);
		ch = retValue.substring(retValue.length-1, retValue.length);
	}
	
	while (retValue.indexOf("  ") != -1) {
		//将文字中间多个相连的空格变为一个空格
		retValue = retValue.substring(0, retValue.indexOf("  ")) + retValue.substring(retValue.indexOf("  ")+1, retValue.length);
	}
	
	return retValue;
}

/*
 * 功能：检查是邮箱格式是否正确
 * 参数：email - 检查的邮箱字符串
 * 返回：bool(格式正确返回true，否则返回false)
 */
function isEmail(email){
	var re = /^[_\.0-9a-z-]+@([0-9a-z][0-9a-z-]+\.){1,4}[a-z]{2,3}$/i;
	return re.test(email);
}

/*
 * 功能：检查字符串是否为日期格式
 * 参数：date - 日期字符串
 * 返回：bool(日期格式正确返回true，否则返回false)
 */
function isDate(date){
	if (/^\d{4}[-,\.,\/]\d{1,2}[-,\.,\/]\d{1,2}$/.test(date)==false) return(false);
	date = date.replace(".", "-").replace("/", "-");
	var r=date.split("-");
	var dayArray=new Array(31,28,31,30,31,30,31,31,30,31,30,31);
	if (((r[0]%4==0)&&(r[0]%100!=0))||(r[0]%100==0&&r[0]%400==0)) dayArray[1]=29;
	if (r[1]==0||r[1]>12) return(false);
	if ((r[0]<0||r[0]>9999)||(r[2]==0||r[2]>dayArray[r[1]-1])) return(false);
	return(true);
}

/*
 * 功能：检查是否是图片格式文件
 * 参数：file - 检查的图片文件
 */
function isImage(file){
    var aExt = file.split(".");
    var sExt = aExt[aExt.length-1];
    var aImg = new Array("gif", "jpg", "bmp", "jpeg", "png");
    for (var i=0; i<aImg.length; i++){
        if (sExt.toLowerCase() == aImg[i]){
            return true;
        }
    }
    return false;
}

/*
 * 功能：复制文本内容到剪贴板
 * 参数：str - 被复制的字符
 */
function copy(str){
    window.clipboardData.setData("Text", str);
    alert("成功复制到剪贴板中！");
}

/*
 * 功能：XmlHttp提交
 */
function xmlHttpPost(url){
    var xmlHttp;
    if (window.XMLHttpRequest)
    {
        xmlHttp = new XMLHttpRequest();
    }
    else if (window.ActiveXObject)
    {
        var msxmls = new Array('Msxml2.XMLHTTP.5.0', 'Msxml2.XMLHTTP.4.0', 'Msxml2.XMLHTTP.3.0', 'Msxml2.XMLHTTP', 'Microsoft.XMLHTTP');
        for (var i = 0; i < msxmls.length; i++)
        {
            try{
                xmlHttp = new ActiveXObject(msxmls[i]);
            }catch(e){}
        }
    }
    if (xmlHttp == null) return "";
    
    xmlHttp.open("GET", url, false);
    xmlHttp.send();
    
//    xmlHttp.callback = function(){
//        alert();
//    }
    
    xmlHttp.onreadyStateChange = function(){
        return xmlHttp.responseText;
    }
    
    if (xmlHttp.readyState == 4){
        //if (xmlHttp.status == 200){
            return xmlHttp.responseText;
        //}
    }
    return "";
}

/*
 * 功能：保存cookie
 */
function setCookie(name, value, day){
    var sCookie = name + "=" + escape(value) +"; path=/; ";
    if (day>0){
        var ms = day * 24 * 60 * 60 * 1000;
        var oDate = new Date();
        oDate.setTime(oDate.getTime() + ms);
        sCookie += "expires="+ oDate.toGMTString() + "; ";
    }
    document.cookie = sCookie;
}

/*
 * 功能：获以cookie
 */
function getCookie(name){
    var arr = document.cookie.match(new RegExp("(^| )"+name+"=([^;]*)(;|$)"));
    if(arr != null) return unescape(arr[2]);
    return null;
}

/*
 * 删除：保存cookie
 */
function delCookie(name){
    setCookie(name, "");
}

/*
 * 功能：设置样式
*/
function setClass(id, cssName){
    var oId = document.getElementById(id);
    if (oId){
        oId.className = cssName;
    }
}

/*
 * 功能：显示页面遮罩层
 */
function showPageBlock(){
    var oBlock = document.getElementById("__PageBlock");
    if (!oBlock){
        oBlock = document.createElement("DIV");
        oBlock.id = "__PageBlock";
        document.getElementsByTagName("body")[0].appendChild(oBlock)
        
        oBlock = document.getElementById("__PageBlock");
    }

    oBlock.style.backgroundColor = "#CCCCCC";
    oBlock.style.position = "absolute";
    oBlock.style.left = "0px";
    oBlock.style.top = "0px";
    oBlock.style.filter = "alpha(opacity=20)";
    oBlock.style.opacity = "0.2";
    oBlock.style.zIndex = 30001;
    
    var hBlock = parseInt(document.documentElement.clientHeight,10);
    var hBody  = parseInt(document.body.clientHeight, 10);
    var wBlock = parseInt(document.documentElement.clientWidth,10);
    var wBody  = parseInt(document.body.clientWidth, 10);
    oBlock.style.height = ((hBody>=hBlock) ? hBody : hBlock) + "px";
    oBlock.style.width = ((wBody>=wBlock) ? wBody : wBlock) + "px";
    
    oBlock.style.display = "";
}

/*
 * 功能：隐藏页面遮罩层
 */
function hidePageBlock(){
    var oBlock = document.getElementById("__PageBlock");
    if (oBlock){
        oBlock.style.display = "none";
    }
}

/*
 * 功能：加入收藏夹
 * 参数：url - 收藏地址
 *       name- 收藏名
 */
function addBookMark(url, name){
    if (document.all){    //IE
        window.external.addFavorite(url,name);
    }else if (window.sidebar){   //ff
        window.sidebar.addPanel(name,url,"");
    }else{
        alert("你的浏览器不支持此事件！");
    }
}

/*
 * 功能：设为首页
 * 参数：url - 首页地址
 */
function setHomepage(url){
    if (document.all){
        document.body.style.behavior='url(#default#homepage)';
        document.body.setHomePage(url);
    }else if (window.sidebar){
        if(window.netscape){
            try{ 
                netscape.security.PrivilegeManager.enablePrivilege("UniversalXPConnect"); 
            }catch (e){
                alert( "该操作被浏览器拒绝，如果想启用该功能，请在地址栏内输入 about:config,然后将项 signed.applets.codebase_principal_support 值该为true" );
            }
        }else{
            alert("你的浏览器不支持此事件！");
        }
        var prefs = Components.classes['@mozilla.org/preferences-service;1'].getService(Components.interfaces.nsIPrefBranch);
        prefs.setCharPref('browser.startup.homepage',url);
    }else{
        alert("你的浏览器不支持此事件！");
    }
}



/*
 * 功能：前台关键字搜索
 */
 function fnSearchInBar()
 {
    if (trim(document.getElementById("tbSKey").value)==""){alert("请输入搜索关键字！");document.getElementById("tbSKey").focus();return;}
    //if (trim(document.getElementById("selStreetInBar").value)==""){alert("请选择街区！");document.getElementById("selStreetInBar").focus();return;}
    var url = "/manufacturer/list.aspx?do=searchbar&streetid=" + trim(document.getElementById("selStreetInBar").value) + "&key=" + escape(trim(document.getElementById("tbSKey").value));
    location.href = url;
 }
 
 
 
 /*
 * 功能：前台热门关键字搜索
 */
 function fnRunSearch(url, key)
 {
    url += "?key=" + escape(key);
    location.href = url;
 }
 
 
 /*
 * 功能：获取URL地址指定参数
 */
 function $G()
{
    var Url=top.window.location.href;
    var u,g,StrBack='';
    if(arguments[arguments.length-1]=="#")
        u=Url.split("#");
    else
        u=Url.split("?");
    if (u.length==1) g='';
    else g=u[1];

    if(g!='')
    {
        gg=g.split("&");
        var MaxI=gg.length;
        str = arguments[0]+"=";
        for(i=0;i<MaxI;i++)
        {
            if(gg[i].indexOf(str)==0)
            {
                StrBack=gg[i].replace(str,"");
                break;
            }
        }
    }
    return StrBack;
}