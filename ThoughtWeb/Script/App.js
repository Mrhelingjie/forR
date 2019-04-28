function userLogin(form){
    var re = /^[a-z0-9]{3,30}$/
    if (!re.test(form.tbUName.value)) {alert("抱歉，用户名不正确！"); form.tbUName.focus(); return;}
    if (trim(form.tbUPwd.value)=="") {
        alert("请填写登录密码！"); form.tbUPwd.focus(); return;
    }else if (trim(form.tbUPwd.value).length<6 || trim(form.tbUPwd.value).length>30){
        alert("抱歉，登录密码不正确！"); form.tbUPwd.focus(); return;
    }
    form.submit();
}