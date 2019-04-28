
function TouchLi(liid)
{
   var allLi=$("#ul_list li");
   allLi.each(function(){
   if(liid==$(this).attr("id")){$(this).show();}
   else{$(this).hide();}
});
     
   var allPage=$("#div_Page a");
   allPage.each(function(){
      $(this).attr("class","");
     if($(this).attr("controlDiv")==liid)
     {
        $(this).attr("class","active");
     }
     
   });
}

function NextPage(param)
{
   var allPage=$("#div_Page a");
   allPage.each(function(){
         if($(this).attr("class")=="active")
         {
               if(typeof($(this).next("a").attr("controlDiv"))=="undefined")
               {
                  window.location.href=param;
                  return false;
               }

               $(this).attr("class","");
               $(this).next("a").attr("class","active");
               $(this).next("a").click();
               return false;
         }
  });
}
function PrevPage(param)
{
    var allPage=$("#div_Page a");
    allPage.each(function(){
         if($(this).attr("class")=="active")
         {   
              if(typeof($(this).prev("a").attr("controlDiv"))=="undefined")
               {
                  window.location.href=param;
                  return false;
               }
               $(this).attr("class","");
               $(this).prev("a").attr("class","active");
               $(this).prev("a").click();
               return false;
         }
  });
}

$(document).ready(function(){
$("#div_Page a:first").click();
})

