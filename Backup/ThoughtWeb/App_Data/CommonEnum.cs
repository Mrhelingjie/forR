using System;
using System.Data;
using System.Configuration;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;


public class CommonEnum
{
   
    /// <summary>
    /// 返回某个枚举类型的指定名称
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="obj"></param>
    /// <param name="ID"></param>
    /// <returns></returns>
    public static string GetName(Type EnumType, int ID)
    {
        return Enum.GetName(EnumType, ID);
    }

    /// <summary>     
    /// 从 枚举中输出字符串  ,并绑定到dropdown    
    public static void StringFromEnum(Type EnumType,DropDownList ddl,bool isSelect)
    {
        ddl.Items.Clear();
        if(isSelect)
        {
            ddl.Items.Add(new ListItem("请选择","请选择"));
        }
        foreach (string str in Enum.GetNames(EnumType))
        {
            ddl.Items.Add(new ListItem(str, ((int)Enum.Parse(EnumType, str)).ToString()));
        }
    }
}
