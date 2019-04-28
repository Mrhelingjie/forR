/*
*	Copyright VeryIDE,2007-2008
*	http://www.veryide.com/
*	
*	$Id: veryide.core.js v1.2 19:47 2008-2-18 leilei $
*/

var VeryIDE={

	/*config*/
	site		:"VeryIDE",
	url			:"http://"+location.host+"/",
	domain	:location.host,
	path		:"/veryide/",

	/*version*/
	version		:1.2,
	versionBuild : 20080904,
	
	/*developer*/
	developer 		: "VeryIDE.Com",
	developerWeb : "http://www.veryide.com",

	/*event*/
	mouseX:0,
	mouseY:0,
	
	/*inti*/
	inti:null,
	layer:100,
	temp:"",
	
	/*script*/
	script:[],
	
	/*debug*/
	debug:false,
	
	/*info*/
	title:document.title,
	url:location.href,
	host:location.host,
	
	/*random*/
	rnd:Math.random(),
	getRnd:function(){
		return Math.random();
	},
	
	/*ui*/
	screenW:screen.width,
	screenH:screen.height,
	bodyW:document.documentElement.scrollWidth,
	bodyH:document.documentElement.scrollHeight,
	
	/*analytics*/
	referrer:document.referrer,
	isSearch:(/(baidu|yahoo|google|sogou|zhongsou|soso|youdao|tom|yisou|vnet|china|live|msn)\.(cn|com|net)/.test(document.referrer)),
	isIE			:false,
	isOpera		:false,
	isSafari		:false,
	isFirefox		:false,
	isMaxthon	:false,
	appName:"",
	appVersion:"",
	appLang:"",

	/*load*/
	start:function(){
		/*powered*/
		VeryIDE.powered="Powered by <a href='http://www.veryide.com/?version="+VeryIDE.version+"&versionBuild="+VeryIDE.versionBuild+"&developer="+VeryIDE.developer+"&from="+location.host+"' title='VeryIDE' target='_blank'>VeryIDE</a> &amp; <a href='"+VeryIDE.developerWeb+"' title='"+VeryIDE.developerWeb+"' target='_blank'>"+VeryIDE.developer+"</a>",

	
		/*folder*/
		VeryIDE.folder={
			js 			:	VeryIDE.path+"js/",
			xml 		: 	VeryIDE.path+"xml/",
			skin		:	VeryIDE.path+"skins/",
			icon		:	VeryIDE.path+"images/icon/",
			cache 	: 	VeryIDE.path+"cache/",
			images	:	VeryIDE.path+"images/",
			plugins	:	VeryIDE.path+"plugins/"
		}

		/*browse*/
		switch (navigator.appName){
			case "Microsoft Internet Explorer":{
				VeryIDE.appName = "ie";
				var reg = /^.+MSIE (\d+\.\d+);.+$/;
				VeryIDE.isIE=true;
				
				if(navigator.userAgent.indexOf ("MAXTHON") != -1){
					VeryIDE.isMaxthon=true;
					var regMax = /^.+MAXTHON ([\d\.]+).{0,}$/;
					VeryIDE.maxVersion=navigator.userAgent.replace (regMax, "$1");
				}
				
				break;
			}default:{
				if (navigator.userAgent.indexOf ("Safari") != -1){
					VeryIDE.appName = "safari";
					var reg = /^.+Version\/([\d\.]+?) Safari.+$/;
					VeryIDE.isSafari=true;
				}else if (navigator.userAgent.indexOf ("Opera") != -1){
					VeryIDE.appName = "opera";
					var reg = /^.{0,}Opera\/(.+?) \(.+$/;
					VeryIDE.isOpera=true;
				}else{
					VeryIDE.appName = "firefox";
					var reg = /^.+Firefox\/([\d\.]+).{0,}$/;
					VeryIDE.isFirefox=true;
				}
			}
			break;
		}
    	VeryIDE.appVersion = navigator.userAgent.replace (reg, "$1");
    	
		if(!VeryIDE.isIE){
			var lang=navigator.language;
		}else{
			var lang=navigator.browserLanguage;
		}
		VeryIDE.appLang=lang.toLowerCase();
    	
		/*bg cache*/
		if(VeryIDE.isIE && !VeryIDE.appVersion<7){
			try{
				//document.documentElement.addBehavior("#default#userdata");
				VeryIDE.getDocment().addBehavior("#default#userdata");
				document.execCommand("BackgroundImageCache", false, true);
			}catch(e){}
		}	    	
    	
    },
    
	/*document*/
	getDocment:function(){
		var doc= (document.compatMode=="BackCompat"?document.body:document.documentElement);
		return doc;
	},
	
	/*script*/
	loadScript:function(name){
		if(VeryIDE.script[name]) return true;
	
		var root=document.getElementsByTagName("HEAD")[0];
		
		var script=document.createElement("script");
		script.type="text/javascript";
		script.src=VeryIDE.folder.js +"veryide."+ name+".js";
		root.appendChild(script);
	},
	
	showScript:function(){
		var str="";
		for(var key in VeryIDE.script){
			str+="veryide."+key+".js"+"\n";
		}
		alert(str);
	},
	
	showNavigator:function(){
		var str="";
		for(var key in navigator){
			str+= key+": "+navigator[key]+"\n";
		}
		alert(str);
	},	
	
	/*event*/
	getMouse:function(e){
		if(!document.all){
			VeryIDE.mouseX = e.pageX;
			VeryIDE.mouseY = e.pageY;
		}else{
			VeryIDE.mouseX = e.x + VeryIDE.getDocment().scrollLeft;
			VeryIDE.mouseY = e.y + VeryIDE.getDocment().scrollTop;
		}
	},
	
	/*status*/
	setStatus:function(str){
		window.status=str;
	},
		
	/*message*/
	showMessage:function(str,fun){
		if (typeof fun != 'function') {
			alert(str.replace("<br />","\n\n"));
		}else{
			fun(str);
		}
	},

	/*test*/
	test:function(){
		//	<input type="button" class="input-button" value="测 试" onclick="VeryIDE.test();" />
		VeryIDE.showError("拖动框测试<br />拖动框测试<br />拖动框测试");
	}
	
}

VeryIDE.start();

/*state*/
VeryIDE.script["core"]=true;
