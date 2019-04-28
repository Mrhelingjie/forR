/*
*	Copyright VeryIDE,2007-2008
*	http://www.veryide.com/
*	
*	$Id: veryide.color.js,v1.1 18:48 2008-11-19 leilei $
*/


/*
	onfocus="VeryIDE.Color(this)"
*/

VeryIDE.Color=function(o,i,s,f){
	

    VeryIDE.tmpColorA=o;
    VeryIDE.tmpColorB=arguments[1];
    VeryIDE.tmpColorF=f;
    
    //闅忔満ID
    if(!VeryIDE.tmpID){
		VeryIDE.tmpID=getRnd(10);
    }
    
    var id=VeryIDE.tmpID;
    
    var color='';
    if(o.tagName=="IMG"){
		color=VeryIDE.tmpColorB.value;
    }else{
		color=o.value;
    }
    
    color?color:'#eeeeee';

	var active=['#fff','','#0A66EE','#EEEEEE'];
    o.onkeyup=this;
    
    var pos=new getPosition(o);
    
    var obj=$(id);
    if(obj){
		 delElement(id);
    }
    
	var obj=document.createElement("DIV");
		obj.id=id;
		
		with(obj.style){
			backgroundColor = "#f9f8f7";
			border = "solid #999999 1px";
			fontSize = "12px";
			position="absolute";
			top=pos.top+pos.height+"px";
			left=pos.left+"px";
		}

    var colorlist=["#000000","#000033","#000066","#000099","#0000cc","#0000ff","#003300","#003333","#003366","#003399","#0033cc","#0033ff","#006600","#006633","#006666","#006699","#0066cc","#0066ff","#009900","#009933","#009966","#009999","#0099cc","#0099ff","#00cc00","#00cc33","#00cc66","#00cc99","#00cccc","#00ccff","#00ff00","#00ff33","#00ff66","#00ff99","#00ffcc","#00ffff","#330000","#330033","#330066","#330099","#3300cc","#3300ff","#333300","#333333","#333366","#333399","#3333cc","#3333ff","#336600","#336633","#336666","#336699","#3366cc","#3366ff","#339900","#339933","#339966","#339999","#3399cc","#3399ff","#33cc00","#33cc33","#33cc66","#33cc99","#33cccc","#33ccff","#33ff00","#33ff33","#33ff66","#33ff99","#33ffcc","#33ffff","#660000","#660033","#660066","#660099","#6600cc","#6600ff","#663300","#663333","#663366","#663399","#6633cc","#6633ff","#666600","#666633","#666666","#666699","#6666cc","#6666ff","#669900","#669933","#669966","#669999","#6699cc","#6699ff","#66cc00","#66cc33","#66cc66","#66cc99","#66cccc","#66ccff","#66ff00","#66ff33","#66ff66","#66ff99","#66ffcc","#66ffff","#990000","#990033","#990066","#990099","#9900cc","#9900ff","#993300","#993333","#993366","#993399","#9933cc","#9933ff","#996600","#996633","#996666","#996699","#9966cc","#9966ff","#999900","#999933","#999966","#999999","#9999cc","#9999ff","#99cc00","#99cc33","#99cc66","#99cc99","#99cccc","#99ccff","#99ff00","#99ff33","#99ff66","#99ff99","#99ffcc","#99ffff","#cc0000","#cc0033","#cc0066","#cc0099","#cc00cc","#cc00ff","#cc3300","#cc3333","#cc3366","#cc3399","#cc33cc","#cc33ff","#cc6600","#cc6633","#cc6666","#cc6699","#cc66cc","#cc66ff","#cc9900","#cc9933","#cc9966","#cc9999","#cc99cc","#cc99ff","#cccc00","#cccc33","#cccc66","#cccc99","#cccccc","#ccccff","#ccff00","#ccff33","#ccff66","#ccff99","#ccffcc","#ccffff","#ff0000","#ff0033","#ff0066","#ff0099","#ff00cc","#ff00ff","#ff3300","#ff3333","#ff3366","#ff3399","#ff33cc","#ff33ff","#ff6600","#ff6633","#ff6666","#ff6699","#ff66cc","#ff66ff","#ff9900","#ff9933","#ff9966","#ff9999","#ff99cc","#ff99ff","#ffcc00","#ffcc33","#ffcc66","#ffcc99","#ffcccc","#ffccff","#ffff00","#ffff33","#ffff66","#ffff99","#ffffcc","#ffffff"];
    
	var ocbody;
	ocbody = '<table border="0" cellspacing="3" cellpadding="0" style="font-size:12px;cursor:default;line-height:20px;">';
    
    ocbody += "<tr height=\"20\"><td colspan=\"4\" bgcolor=\""+color+"\"><td><td colspan=\"3\" style=\"font-size:12px;\" align=\"center\">褰撳墠棰滆壊</td>";
    
    ocbody += "<td colspan=\"2\"><td><td align=\"center\" height=\"20\" colspan=\"8\" onMouseOut=\"this.style.borderColor='"+active[0]+"';this.bgColor='"+active[1]+"';\" style=\"border:"+active[0]+" 1px solid;\"></td>";
    
    ocbody += "<td colspan=\"2\"><td><td align=\"center\" colspan=\"2\" style='background:#666;color:#fff;' onclick='VeryIDE.Color.close(\"color\");'>鍏抽棴</td></tr>";
    
		for(var i=0;i<colorlist.length;i++){
			if(i%24==0){
				ocbody += "<tr>";
			}
				ocbody += "<td width=\"16\" height=\"18\" style=\"border:"+active[0]+" 1px solid;\" onMouseOut=\"this.style.borderColor='"+active[0]+"';this.bgColor='"+active[1]+"';\" onMouseOver=\"this.style.borderColor='"+active[2]+"';this.bgColor='"+active[3]+"';\" onMouseDown=\"VeryIDE.Color.select('"+colorlist[i]+"','"+s+"')\" align=\"center\" valign=\"middle\"><table style=\"border:1px solid #808080;\" width=\"14\" height=\"14\" bgcolor=\""+colorlist[i]+"\"><tr><td></td></tr></table></td>";

			if(i%24==23){
				ocbody += "</tr>";
			}
		}
    
    ocbody += "</table>";

	obj.innerHTML=ocbody;
    document.body.appendChild(obj);
    
}

VeryIDE.Color.select=function(color,select){
    var o=VeryIDE.tmpColorA;
    
    if(typeof(VeryIDE.tmpColorF)=="function"){
		VeryIDE.tmpColorF(color);
    }
    
    if(select=="rgb"){
		var colorValue=VeryIDE.Color.rgb(color);
    }else{
		var colorValue=color;
    }
    
    if(o.tagName=="IMG"){
		o.style.backgroundColor=color;
		
		VeryIDE.tmpColorB.value=colorValue;
    }else{
		o.style.color=color;
		
		o.value=colorValue;
    }
	delElement(VeryIDE.tmpID);
}

VeryIDE.Color.close=function(){
    delElement(VeryIDE.tmpID);
}

VeryIDE.Color.rgb=function(hexColor){
	var a=hexColor;
	if(a.substr(0,1)=="#"){a=a.substring(1);}
	if(a.length!=6){return alert("请输入正确的颜色编码?");}
	a = a.toLowerCase();

	var b=new Array();
	for(x=0;x<3;x++){
		b[0]=a.substr(x*2,2)
		b[3]="0123456789abcdef";b[1]=b[0].substr(0,1)
		b[2]=b[0].substr(1,1)
		b[20+x]=b[3].indexOf(b[1])*16+b[3].indexOf(b[2])
	}
	return b[20]+","+b[21]+","+b[22];
}

VeryIDE.Color.hex=function(rgbColor){
	var hexcode="#";
	for(x=0;x<3;x++){
		var n=r1e[x].value;
		if(n==""){n="0";}
		
		if(parseInt(n)!=n){
			return alert("璇疯緭鍏ユ暟瀛楋紒");
		}
		if(a<0 && a>255){
			return alert("數字在0-255之間");
		}
		var c="0123456789abcdef";
		var b="";
		var a=n%16;b=c.substr(a,1)
		a=(n-a)/16;hexcode+=c.substr(a,1)+b
	}
	return hexcode;
}

/*state*/
VeryIDE.script["color"]=true;