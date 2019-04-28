function selMenu(id){
    var oMenu = document.getElementById(id);
    if (oMenu){
        oMenu.className = "selMenu";
    }
}

function listEvent(what, id){
    switch(what){
        case "edit":
            window.location.href = "new.aspx?do=edit&id="+ id +"&temp="+ Math.random();
            break;
            
        case "del":
            if (confirm("确认删除？\n\n系统提示：一旦删除成功后，将无法恢复！")){
                window.location.href = "?do=del&id="+ id +"&temp="+ Math.random();
            }
            break;
    }
}