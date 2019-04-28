/*
*	Copyright VeryIDE,2007-2008
*	http://www.veryide.com/
*	
*	$Id: veryide.library.js,v2.3 17:29 2008-11-16 leilei $
*/


function $N(obj){return document.getElementsByName(obj);}
function $T(obj){return document.getElementsByTagName(obj);}

function $V(str){document.write(str);}

//body load
function addLoadEvent(func) {
	var oldonload = window.onload;
	if (typeof window.onload != 'function') {
		window.onload = func;
	} else {
		window.onload = function() {
			oldonload();
			func();
		}
	}
}

//object event
function addObjectEvent(ele,evt,func){
	var oldonevent = ele['on'+evt];
	if (typeof ele['on'+evt] != 'function') {
		ele['on'+evt] = func;
	} else {
		ele['on'+evt] = function(event) {
			oldonevent(event);
			func(event);
		}
	}
}

//key event
function addKeyEvent(key,func){
	if(!VeryIDE.tmpKey){
		VeryIDE.tmpKey=[];
	}
	
	VeryIDE.tmpKey["k"+key]=func;
}

addKeyEvent.Listener=function(e,test){
	var event=e||window.event;
	if(VeryIDE.tmpKey["k"+event.keyCode]){
		VeryIDE.tmpKey["k"+event.keyCode](event);
	}
	if(test){
		alert(event.keyCode);
	}
}
//onkeydown判断charCode/keyCode

//获取对象
function getObject(o){
	if(typeof(o)!="object"){
		var o=$(o);
	}
	return o;
}

/*
String.prototype.trim = function(){
   return this.replace(/(^\s+)|\s+$/g,"");
}
*/

String.prototype.Trim = function(){
    return this.replace(/(^\s*)|(\s*$)/g, "");
}
String.prototype.LTrim = function(){
    return this.replace(/(^\s*)/g, "");
}
String.prototype.Rtrim = function(){
    return this.replace(/(\s*$)/g, "");
}

//返回字符串字节数
String.prototype.long = function(){
	var i;
	var l = this.length;
	var len;
	len = 0;
	for (i=0;i<l;i++){
		if (this.charCodeAt(i)>255) 
			len+=2; 
		else 
			len++;
	}
	return len;
}

//检查在数组内是否存在某�?
function inArray(a,v) {
	var l = a.length;
	for(var i=0; i<=l; i++) {
		if(a[i]==v) return true;
	}
	return false;
}

//class�?的所有元�?
document.getElementsByClassName = function() {
  var children = document.getElementsByTagName('*') || document.all;
  var elements = new Array();
  var len = children.length;

  for (var i = 0; i < len; i++) {
    var child = children[i];
    var classNames = child.className.split(' ');
    for (var j = 0; j < classNames.length; j++) {
    	for (var k = 0; k < arguments.length; k++) {	
			if (classNames[j] == arguments[k]) {
        		elements.push(child);
       			break;
      		}
      }
    }
  }
  
  return elements;
}

//ID�?开头的所有元�?
document.getElementsByPrefix = function(prefix) {
  var children = document.getElementsByTagName('*') || document.all;
  var elements = new Array();
  var len = children.length;
  
  for (var i = 0; i < len; i++) {
    var child = children[i];
    var eid = child.id;
    if(eid&&eid.indexOf(prefix)>-1)
        elements.push(child);
  }
  
  return elements;
}

/*查找页面Meta*/     
function getMeta(name,att){
  metatags = document.getElementsByTagName("meta"); 
  for (cnt = 0; cnt < metatags.length; cnt++){                   
    if(metatags[cnt].getAttribute("name")==name){
      if(!att){
        return metatags[cnt];
      }else{
        return metatags[cnt].getAttribute(att);
      }
    }else{
      return null;
    }
  }
}

//加载新脚�?
function loadScript(src,target){
	if(!target){ 
		var root=$T("HEAD")[0];
	}else{
		var root=getObject(target);
	}
	
	var script=document.createElement("script");
	script.type="text/javascript";
	script.src=src;
	
	var code=arguments[2];
	if(code){
		script.charset=code;
	}
	
	root.appendChild(script);
}

//取得文件�?
function getFileName(url){
	var strUrl=location.href;
	if(url){
		strUrl=url;
	}
	var arrUrl=strUrl.split("/"); 
	var strPage=arrUrl[arrUrl.length-1]; 
	return strPage; 
}

//对象绝对位置
function getPosition(obj){	
	var obj=getObject(obj);

	this.width=obj.offsetWidth; 
	this.height=obj.offsetHeight;  
	this.top=obj.offsetTop;  
	this.left=obj.offsetLeft;  
	while(obj=obj.offsetParent){  
		this.top+=obj.offsetTop;  
		this.left+=obj.offsetLeft;
	}
}

function getSelect(obj){
	var obj=getObject(obj);
	
	this.value="";
	this.text="";
	this.index="";
	
	if(obj.length>0){
		this.value=obj[obj.selectedIndex].value;
		this.text=obj[obj.selectedIndex].text;
		this.index=obj.selectedIndex;
	}
	this.getAtt=function(att){
		return obj[obj.selectedIndex].getAttribute(att);
	}
}

//
function getRadio(obj){
	var obj=$N(obj);
	var len = obj.length;
	
	this.value="";
	for(var i=0;i<len;i++){
		if(obj[i].checked==true){
			this.value=obj[i].value;
			break;
		}
	}
}



//对象显示隐藏
function showHide(obj){
	var obj=getObject(obj);
	if(obj){
		if(obj.offsetHeight==0||obj.style.display== "none") {obj.style.display = "block";}else{obj.style.display = "none";}
	}
}

function setCheckBox(obj,v){
	var obj=$N(obj);

	if(obj[0]){
		var len = obj.length;
		for(var i=0;i<len;i++){
			if(obj[i].value==v){
				obj[i].checked=true;
			}else{
				obj[i].checked=false;
			}
		}
	}else{
		obj=$(obj);
		if(obj.value==v){
			obj.checked=true;
		}else{
			obj[i].checked=false;
		}
	}
}

//选择列表�?
function setSelect(obj,v){
	var obj=$(obj);
	var len = obj.length;
	for(var i=0;i<len;i++){
		if(obj[i].value == v){
			obj.selectedIndex=i;
			break;
		}
	}
}

//选择单选项
function setRadio(o,v){		
	var obj=$N(o);
	var len = obj.length;
	for(var i=0;i<len;i++){
		if(obj[i].value == v){
			obj[i].checked=true;
			break;
		}
	}
}

//禁用对象
function setDisabled(obj,b){	
	var obj=getObject(obj);
	if (obj){obj.disabled=b;}
}

function setClass(obj,Class,Type){
	var obj=getObject(obj);
	if(obj){
		switch(Type){
			case "+":
				obj.className+=" "+Class;
			break;
			
			case "-":
				obj.className=obj.className.replace(Class,"");
			break;
			
			case "":
				obj.className=Class;
			break;
			
		}
	}
}

//新建连续下接列表�?
function newNumOption(obj,s,e,t){
	var obj=getObject(obj);
	if(t=="new"){obj.length=0;}
	for(var i=s;i<(e+1);i++){
		obj.options[obj.length] = new Option(i,i); 
	}
}

//删除对象
function delElement(obj){
	var obj=getObject(obj);
	var p=obj.parentNode;
	p.removeChild(obj);
}

//确认操作
function getConfirm(info){
	if(!confirm(info)){return false}
}

//新窗口打开链接
//rel="_blank"
function _blank(){
	var anchors = document.getElementsByTagName("A");
	var len = anchors.length;
	for (var i=0; i<len; i++) {
		var anchor = anchors[i];
		if (anchor.getAttribute("href") && anchor.getAttribute("rel") == "_blank"){
			anchor.target = "_blank";
		}
	}
}

//获取URL参数
function getQuery(n,url){
	if(!url){
		var url=location.href;
	}
	
	var v = '';
	var o = url.indexOf(n+"=");
	if (o != -1){
		o += n.length + 1 ;
		e = url.indexOf("&", o);
		if (e == -1){
			e = url.length;
		}
		v = unescape(url.substring(o, e));
	}
	
	//seo
	if(!v){
		var o = url.indexOf(n+"-");
		if (o != -1){
			o += n.length + 1 ;
			e = url.indexOf("-", o);
			if (e == -1){
				e = url.length;
			}
			v = unescape(url.substring(o, e));
		}
	}
	
	return v;
}

//全角转半�?
function switchChar(str){
	var str1="1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
	var str2="１２３４５６７８９０ａｂｃｄｅｆｇｈｉｊｋｌｍｎｏｐｑｒｓｔｕｖｗｘｙｚＡＢＣＤＥＦＧＨＩＪＫＬＭＮＯＰＱＲＳＴＵＶＷＸＹＺ";
	
	var len=str.length;
	for(var i=0; i<len; i++){
		var n = str2.indexOf(str.charAt(i));
		if(n != -1) str = str.replace(str.charAt(i), str1.charAt(n));
	}
	return str;
}

/*
	获取随机字符
	***********
	len		长度 
	vUpper	是否大写字母 
	vLower	是否小写字母 
	vNum		是否数字
*/
function getRnd(len,vUpper,vLower,vNum){ 
	var seed_array=new Array(); 
	var seedary; 

	seed_array[0]="" 
	seed_array[1]= "a b c d e f g h i j k l m n o p q r s t u v w x y z"; 
	seed_array[2]= "a b c d e f g h i j k l m n o p q r s t u v w x y z"; 
	seed_array[3]= "0 1 2 3 4 5 6 7 8 9"; 

	if (!vUpper&&!vLower&&!vNum){vUpper=true;vLower=true;vNum=true;} 

	if (vUpper){seed_array[0]+=seed_array[1];} 
	if (vLower){seed_array[0]+=" "+seed_array[2];} 
	if (vNum){seed_array[0]+=" "+seed_array[3];} 

	seed_array[0]= seed_array[0].split(" "); 
	seedary="";
	
	for (var i=0;i<len;i++){ 
		seedary+=seed_array[0][Math.round(Math.random()*(seed_array[0].length-1))] 
	} 
	return(seedary); 
}


/*
	获取cookies
	name	cookie名称
	sub		子cookie名称
*/
function getCookie(name,sub){
	var str="";
	var arr = document.cookie.replace(/%25/g,"%").replace(/%5F/g,"_").match(new RegExp("(^| )"+name+"=([^;]*)(;|$)"));
	if(arr !=null){
		try{
			str=decodeURIComponent(arr[2]);
		}catch(e){
			str=arr[2];
		}
	}
	
	if(sub){
		if(str){
			var nSubValueBegin = str.indexOf(sub+"=");
		}else{
			var nSubValueBegin =-1
		}
		
		if(nSubValueBegin != -1){
			var nSubValueEnd = str.indexOf("&", nSubValueBegin);
			if(nSubValueEnd == -1)
			nSubValueEnd = str.length;
			var sSubValue = str.substring(nSubValueBegin + sub.length+1, nSubValueEnd);//获得指定的子键�?
			str=sSubValue;
		}else{
			str="";
		}
	}
	return str;
}

function setCookie(key,value,iExpireDays,domain){
	var cookies=key.replace("_","%5F") + "=" + encodeURIComponent(value)+ "; ";
	
	if (iExpireDays){
		var dExpire = new Date();
		dExpire.setTime(dExpire.getTime()+parseInt(iExpireDays*24*60*60*1000));
		cookies += "expires=" + dExpire.toGMTString()+ "; ";
	}
	
	if(domain){
		cookies += "domain="+domain+"; ";
	}
	cookies += "path=/;";
	document.cookie = cookies;
}

/*state*/
VeryIDE.script["library"]=true;