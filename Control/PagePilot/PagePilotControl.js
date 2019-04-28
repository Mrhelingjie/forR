function PagePilot_Redirect(des,count,symbol)
{
    var txtDes = document.getElementById(des).value;
    var txtCount = document.getElementById(count).value;
    if(txtDes=="" || parseInt(txtDes) > parseInt(txtCount))
    {
        alert("页码只能在 1-"+txtCount+" 之间");
    }
    else
    {
        var url = window.location.toString();
        var patt = new RegExp(symbol + "=\\d+");
        var currPage = patt.exec(url);
        if(!currPage)
        {
            currPage = symbol + "=1";
            if(url.search("\\?")!=-1)
            {
                url += "&";
            }
            else
            {
                url += "?";
            }
            url += currPage;
        }
        if(currPage.toString() == symbol + "=" + txtDes)
        {
            alert("当前正在页码"+txtDes);
        }
        else
        {
            url = url.replace(currPage.toString(),symbol + "=" + txtDes);
            window.location.href = url;
        }
    }
}
