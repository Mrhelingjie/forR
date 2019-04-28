function backList(){
    window.location.href = "list.aspx?"+ Math.random();
}

function addNew(){
    window.location.href = "new.aspx?"+ Math.random();
}

function listEvent(what, id){
    switch(what){
        case "edit":
            showFrame("new.aspx?do=edit&id="+ id +"&temp="+ Math.random());
            break;
            
        case "del":
            if (confirm("确认删除？\n\n系统提示：一旦删除成功后，将无法恢复！")){
                window.location.href = "?do=del&id="+ id +"&temp="+ Math.random();
            }
            break;
    }
}

function delSelected(form, name){
    var oCb = document.getElementsByName(name);
    if (oCb){
        var Found = false;
        for (var i=0; i<oCb.length; i++){
            if (oCb[i].checked){
                Found = true;
                break;
            }
        }
        if (!Found) {
            alert("抱歉，并未选取记录！"); return;
        }else if (confirm("确认删除所选记录？")){
            form.submit();
        }
    }
}

//弹出一个居中窗口
function openWin(url, w, h){
    var iLeft = (parseInt(document.body.clientWidth,10)-w)/2 + 180;
    var iTop = (parseInt(document.body.clientHeight, 10)-h)/2 + 120;
    
    window.open(url, "admin", "width="+ w +", height="+ h +", top="+ iTop +", left="+ iLeft +",");
}

function showFrame(url){
	document.body.scroll = "no";
	var oDiv = document.getElementById("divFrame");
	if (oDiv){
		var w = parseInt(document.body.offsetWidth, 10);
		var h = parseInt(document.body.offsetHeight, 10);
		var t = parseInt(document.body.scrollTop, 10);
		
		//显示
		oDiv.style.width  = w;
		oDiv.style.height = h;
		oDiv.style.top    = t;
		oDiv.style.display = "";
		
		var oFrame = document.getElementById("iMain");
		oFrame.src = url;
		oFrame.style.width  = w;
		oFrame.style.height = h;
	}
}

function hideFrame(){
	document.body.scroll = "yes";
	var oDiv = document.getElementById("divFrame");
	if (oDiv){
		oDiv.style.display = "none";
		oDiv.style.width  = 0;
		oDiv.style.height = 0;
		
		var oFrame = document.getElementById("iMain");
		if (oFrame){
			oFrame.src = "about:blank";
			oFrame.style.width  = 0;
			oFrame.style.height = 0;
		}
	}
}

function checkId(){
    var iCount = 0;
    var oId = document.getElementsByName("cbId");
    for (var i=0; i<oId.length; i++){
        if (oId[i].checked){
            iCount++;
            break;
        }
    }
    
    document.getElementById("cbAll").checked = (oId.length==iCount) ? true : false;
}

function checkAllId(check){
    var oId = document.getElementsByName("cbId");
    for (var i=0; i<oId.length; i++){
        oId[i].checked = !oId[i].checked;
    }
}

function isCheckedId(){
    var iCount = 0;
    var oId = document.getElementsByName("cbId");
    for (var i=0; i<oId.length; i++){
        if (oId[i].checked){
            iCount++;
            break;
        }
    }
    return (iCount>0) ? true : false;
}

function deleteAll(form){
    if (!isCheckedId()){
        alert("抱歉，未选择删除项！");
    }else if (confirm("确定要删除选定项？")){
        form.submit();
    }
}