using System;
using System.Web;
using System.Web.Configuration;
using System.Configuration;

namespace Mejoy.Common
{
    public class Config
    {
        private static System.Configuration.Configuration webConfig = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration(HttpContext.Current.Request.ApplicationPath);
        private static System.Configuration.KeyValueConfigurationCollection appSetting = webConfig.AppSettings.Settings;

        #region 数据库链接信息
        /// <summary>
        /// 数据库位置
        /// </summary>
        public static string DB_DATA_SOURCE
        {
            get
            {
                if (appSetting["dbDataSource"] == null)
                {
                    return "";
                }
                else
                {
                    return Convert.ToString(appSetting["dbDataSource"].Value).Trim();
                }
            }
        }
        /// <summary>
        /// 数据库的名称
        /// </summary>
        public static string DB_INITIAL_CATALOG
        {
            get
            {
                if (appSetting["dbInitialCatalog"] == null)
                {
                    return "";
                }
                else
                {
                    return Convert.ToString(appSetting["dbInitialCatalog"].Value).Trim();
                }
            }
        }
        /// <summary>
        /// 数据库用户帐号
        /// </summary>
        public static string DB_USER_ID
        {
            get
            {
                if (appSetting["dbUserId"] == null)
                {
                    return "";
                }
                else
                {
                    return Convert.ToString(appSetting["dbUserId"].Value).Trim();
                }
            }
        }
        /// <summary>
        /// 数据库用户登录密码
        /// </summary>
        public static string DB_USER_PASSWORD
        {
            get
            {
                if (appSetting["dbUserPwd"] == null)
                {
                    return "";
                }
                else
                {
                    return Convert.ToString(appSetting["dbUserPwd"].Value).Trim();
                }
            }
        }
        #endregion



        #region 网站信息
        /// <summary>
        /// 程序版本
        /// </summary>
        public static string WEB_VERSION
        {
            get
            {
                return "1.0.0";
            }
        }
        /// <summary>
        /// 网站名称
        /// </summary>
        public static string WEB_NAME
        {
            get
            {
                try
                {
                    return Convert.ToString(appSetting["webName"].Value).Trim();
                }
                catch
                {
                    return "交友网";
                }
            }
        }
        /// <summary>
        /// 网站描述
        /// </summary>
        public static string WEB_DESCRIPTION
        {
            get
            {
                try
                {
                    return Convert.ToString(appSetting["webDescription"].Value).Trim();
                }
                catch
                {
                    return "";
                }
            }
        }
        /// <summary>
        /// 网站关键词
        /// </summary>
        public static string WEB_KEYWORDS
        {
            get
            {
                try
                {
                    return Convert.ToString(appSetting["webKeywords"].Value).Trim().Replace("，", ",").Trim();
                }
                catch
                {
                    return "";
                }
            }
        }
        #endregion



        #region 邮件发送信息
        /// <summary>
        /// 邮箱server
        /// </summary>
        public static string Main_Server
        {
            get
            {
                try
                {
                    return Convert.ToString(appSetting["mailServer"].Value).Trim();
                }
                catch
                {
                    return "smtp.sina.com";
                }
            }
        }



        /// <summary>
        /// 邮箱帐号
        /// </summary>
        public static string Mail_Account
        {
            get
            {
                try
                {
                    return Convert.ToString(appSetting["mailAccount"].Value).Trim();
                }
                catch
                {
                    return "in_try@sina.com";
                }
            }
        }



        /// <summary>
        /// 邮箱密码
        /// </summary>
        public static string Mail_Pwd
        {
            get
            {
                try
                {
                    return Convert.ToString(appSetting["mailPwd"].Value).Trim();
                }
                catch
                {
                    return "in_try";
                }
            }
        }



        /// <summary>
        /// 邮件主题
        /// </summary>
        public static string Mail_Subject
        {
            get
            {
                try
                {
                    return Convert.ToString(appSetting["mailSubject"].Value).Trim();
                }
                catch
                {
                    return "内江12580 找回密码";
                }
            }
        }



        /// <summary>
        /// 邮箱smtp端口号
        /// </summary>
        public static string Mail_Port
        {
            get
            {
                try
                {
                    return Convert.ToString(appSetting["mailPort"].Value).Trim();
                }
                catch
                {
                    return "25";
                }
            }
        }
        #endregion


        #region 网站Config.web配置
        /// <summary>
        /// 公文收发后台目录
        /// </summary>
        public static string DIR_ADMIN
        {
            get
            {
                if (appSetting["dirAdmin"] == null)
                {
                    return "";
                }
                else
                {
                    return Convert.ToString(appSetting["dirAdmin"].Value).Trim();
                }
            }
        }
        /// <summary>
        /// 附件上传总目录
        /// </summary>
        public static string DIR_UPLOAD
        {
            get
            {
                try
                {
                    if (appSetting["dirUpload"] == null)
                    {
                        return "/upload/";
                    }
                    else
                    {
                        return Convert.ToString(appSetting["dirUpload"].Value).Trim();
                    }
                }
                catch
                {
                    return "/upload/";
                }
            }
        }
        /// <summary>
        /// 文章上传目录
        /// </summary>
        public static string DIR_UPLOAD_ARTICLE
        {
            get
            {
                try
                {
                    if (appSetting["dirArticleUpload"] == null)
                    {
                        return DIR_UPLOAD + "article/";
                    }
                    else
                    {
                        return DIR_UPLOAD + Convert.ToString(appSetting["dirArticleUpload"].Value).Trim();
                    }
                }
                catch
                {
                    return DIR_UPLOAD + "article/";
                }
            }
        }
        /// <summary>
        /// 文章扩展内容上传目录
        /// </summary>
        public static string DIR_UPLOAD_ARTICLE_MORE
        {
            get
            {
                try
                {
                    if (appSetting["dirArticleMoreUpload"] == null)
                    {
                        return DIR_UPLOAD + "articlemore/";
                    }
                    else
                    {
                        return DIR_UPLOAD + Convert.ToString(appSetting["dirArticleMoreUpload"].Value).Trim();
                    }
                }
                catch
                {
                    return DIR_UPLOAD + "articlemore/";
                }
            }
        }
        /// <summary>
        /// 友情链接上传目录
        /// </summary>
        public static string DIR_UPLOAD_FRIENDLINK
        {
            get
            {
                try
                {
                    if (appSetting["dirFriendLinkUpload"] == null)
                    {
                        return DIR_UPLOAD + "friendlink/";
                    }
                    else
                    {
                        return DIR_UPLOAD + Convert.ToString(appSetting["dirFriendLinkUpload"].Value).Trim();
                    }
                }
                catch
                {
                    return DIR_UPLOAD + "friendlink/";
                }
            }
        }
        /// <summary>
        /// 商户LOGO目录
        /// </summary>
        public static string DIR_UPLOAD_MANUFACTURER_LOGO
        {
            get
            {
                try
                {
                    if (appSetting["dirManufacturerLogo"] == null)
                    {
                        return DIR_UPLOAD + "Manufacturer/Logo/";
                    }
                    else
                    {
                        return DIR_UPLOAD + Convert.ToString(appSetting["dirManufacturerLogo"].Value).Trim();
                    }
                }
                catch
                {
                    return DIR_UPLOAD + "Manufacturer/Logo/";
                }
            }
        }
        /// <summary>
        /// 会员头像目录
        /// </summary>
        public static string DIR_UPLOAD_USER_LOGO
        {
            get
            {
                try
                {
                    if (appSetting["dirUserLogo"] == null)
                    {
                        return DIR_UPLOAD + "User/Logo/";
                    }
                    else
                    {
                        return DIR_UPLOAD + Convert.ToString(appSetting["dirUserLogo"].Value).Trim();
                    }
                }
                catch
                {
                    return DIR_UPLOAD + "User/Logo/";
                }
            }
        }
        /// <summary>
        /// 商品封面图片目录
        /// </summary>
        public static string DIR_UPLOAD_PRODUCT_COVER
        {
            get
            {
                try
                {
                    if (appSetting["dirProductCover"] == null)
                    {
                        return DIR_UPLOAD + "Product/Cover/";
                    }
                    else
                    {
                        return DIR_UPLOAD + Convert.ToString(appSetting["dirProductCover"].Value).Trim();
                    }
                }
                catch
                {
                    return DIR_UPLOAD + "Product/Cover/";
                }
            }
        }
        /// <summary>
        /// Xml静态文件存放总目录
        /// </summary>
        public static string DIR_XMLDATA
        {
            get
            {
                try
                {
                    if (appSetting["dirXmlData"] == null)
                    {
                        return "/xmldata/";
                    }
                    else
                    {
                        return Convert.ToString(appSetting["dirXmlData"].Value).Trim();
                    }
                }
                catch
                {
                    return "/xmldata/";
                }
            }
        }
        /// <summary>
        /// 内容管理xml静态文件存放目录
        /// </summary>
        public static string DIR_XMLDATA_ARTICLE
        {
            get
            {
                try
                {
                    if (appSetting["dirArticleXmlData"] == null)
                    {
                        return DIR_XMLDATA + "Article/";
                    }
                    else
                    {
                        return DIR_XMLDATA + Convert.ToString(appSetting["dirArticleXmlData"].Value).Trim();
                    }
                }
                catch
                {
                    return DIR_XMLDATA + "Article/";
                }
            }
        }
        /// <summary>
        /// 链接（图文）广告附件上传目录
        /// </summary>
        public static string DIR_ADS_LINK_UPLOAD
        {
            get
            {
                try
                {
                    if (appSetting["dirLinkAdsUpload"] == null)
                    {
                        return DIR_UPLOAD + "ads/link/";
                    }
                    else
                    {
                        return DIR_UPLOAD + Convert.ToString(appSetting["dirLinkAdsUpload"].Value).ToLower().Trim();
                    }
                }
                catch
                {
                    return DIR_UPLOAD + "ads/link/";
                }
            }
        }
        /// <summary>
        /// 轮播广告上传目录
        /// </summary>
        public static string DIR_ADS_CYCLE_UPLOAD
        {
            get
            {
                try
                {
                    if (appSetting["dirCycleAdsUpload"] == null)
                    {
                        return DIR_UPLOAD + "ads/cycle/";
                    }
                    else
                    {
                        return DIR_UPLOAD + Convert.ToString(appSetting["dirCycleAdsUpload"].Value).ToLower().Trim();
                    }
                }
                catch
                {
                    return DIR_UPLOAD + "ads/cycle/";
                }
            }
        }
        /// <summary>
        /// 静态生成的HTML目录
        /// </summary>
        public static string DIR_STATIC_HTML
        {
            get
            {
                return Convert.ToString(appSetting["dirHtml"].Value).ToLower().Trim();
            }
        }
        /// <summary>
        /// 群组Logo上传目录
        /// </summary>
        public static string DIR_STATIC_GROUP
        {
            get
            {
                try
                {
                    if (appSetting["dirGroupLogo"] == null)
                    {
                        return DIR_UPLOAD + "Group/Logo/";
                    }
                    else
                    {
                        return DIR_UPLOAD + Convert.ToString(appSetting["dirGroupLogo"].Value).ToLower().Trim();
                    }
                }
                catch
                {
                    return DIR_UPLOAD + "Group/Logo/";
                }
            }
        }
        /// <summary>
        /// 优惠券图片上传目录
        /// </summary>
        public static string DIR_STATIC_ETICKET
        {
            get
            {
                try
                {
                    if (appSetting["dirETicketLogo"] == null)
                    {
                        return DIR_UPLOAD + "ETicket/Logo/";
                    }
                    else
                    {
                        return DIR_UPLOAD + Convert.ToString(appSetting["dirETicketLogo"].Value).ToLower().Trim();
                    }
                }
                catch
                {
                    return DIR_UPLOAD + "ETicket/Logo/";
                }
            }
        }
        /// <summary>
        /// 店铺LOGO图片
        /// </summary>
        public static string DIR_STATIC_ETICKET_TEMP
        {
            get
            {
                try
                {
                    if (appSetting["shopLogo"] == null)
                    {
                        return DIR_UPLOAD + "ShopLogo/";
                    }
                    else
                    {
                        return DIR_UPLOAD + Convert.ToString(appSetting["shopLogo"].Value).ToLower().Trim();
                    }
                }
                catch
                {
                    return DIR_UPLOAD + "ShopLogo/";
                }
            }
        }


        /// <summary>
        /// 店铺LOGO图片
        /// </summary>
        public static string DIR_STATIC_SHOP_LOGO
        {
            get
            {
                try
                {
                    if (appSetting["shopLogo"] == null)
                    {
                        return DIR_UPLOAD + "ShopLogo/";
                    }
                    else
                    {
                        return DIR_UPLOAD + Convert.ToString(appSetting["shopLogo"].Value).ToLower().Trim();
                    }
                }
                catch
                {
                    return DIR_UPLOAD + "ShopLogo/";
                }
            }
        }

        /// <summary>
        /// 店铺LOGO图片
        /// </summary>
        public static string DIR_STATIC_RENT_LOGO
        {
            get
            {
                try
                {
                    if (appSetting["rentLogo"] == null)
                    {
                        return DIR_UPLOAD + "RentLogo/";
                    }
                    else
                    {
                        return DIR_UPLOAD + Convert.ToString(appSetting["rentLogo"].Value).ToLower().Trim();
                    }
                }
                catch
                {
                    return DIR_UPLOAD + "RentLogo/";
                }
            }
        }

        /// <summary>
        /// 活动图片
        /// </summary>
        public static string DIR_STATIC_ACTIVITY_LOGO
        {
            get
            {
                try
                {
                    if (appSetting["activityLogo"] == null)
                    {
                        return DIR_UPLOAD + "ActivityLogo/";
                    }
                    else
                    {
                        return DIR_UPLOAD + Convert.ToString(appSetting["activityLogo"].Value).ToLower().Trim();
                    }
                }
                catch
                {
                    return DIR_UPLOAD + "ActivityLogo/";
                }
            }
        }

        /// <summary>
        /// 品质生活图片
        /// </summary>
        public static string DIR_STATIC_PZSH_LOGO
        {
            get
            {
                try
                {
                    if (appSetting["PZSHLogo"] == null)
                    {
                        return DIR_UPLOAD + "PZSHLOGO/";
                    }
                    else
                    {
                        return DIR_UPLOAD + Convert.ToString(appSetting["PZSHLOGO"].Value).ToLower().Trim();
                    }
                }
                catch
                {
                    return DIR_UPLOAD + "PZSHLOGO/";
                }
            }
        }

        /// <summary>
        /// 品质生活缩略图片
        /// </summary>
        public static string DIR_STATIC_PZSHSMALL_LOGO
        {
            get
            {
                try
                {
                    if (appSetting["PZSHSmallLogo"] == null)
                    {
                        return DIR_UPLOAD + "PZSHSMALLLOGO/";
                    }
                    else
                    {
                        return DIR_UPLOAD + Convert.ToString(appSetting["PZSHSmallLogo"].Value).ToLower().Trim();
                    }
                }
                catch
                {
                    return DIR_UPLOAD + "PZSHSMALLLOGO/";
                }
            }
        }



        /// <summary>
        /// 店铺二维码图片
        /// </summary>
        public static string DIR_STATIC_SHOP_DIGITAL
        {
            get
            {
                try
                {
                    if (appSetting["shopDigital"] == null)
                    {
                        return DIR_UPLOAD + "shopDigital/";
                    }
                    else
                    {
                        return DIR_UPLOAD + Convert.ToString(appSetting["shopDigital"].Value).ToLower().Trim();
                    }
                }
                catch
                {
                    return DIR_UPLOAD + "shopDigital/";
                }
            }
        }


        /// <summary>
        /// 便民服务图片
        /// </summary>
        public static string DIR_STATIC_PEOPERTY_DIGITAL
        {
            get
            {
                try
                {
                    if (appSetting["PEOPERTY"] == null)
                    {
                        return DIR_UPLOAD + "PEOPERTY/";
                    }
                    else
                    {
                        return DIR_UPLOAD + Convert.ToString(appSetting["PEOPERTY"].Value).ToLower().Trim();
                    }
                }
                catch
                {
                    return DIR_UPLOAD + "PEOPERTY/";
                }
            }
        }


        /// <summary>
        /// 商户照片
        /// </summary>
        public static string DIR_MANUFACTURER_PHOTO
        {
            get
            {
                try
                {
                    if (appSetting["dirManufacturerPhoto"] == null)
                    {
                        return DIR_UPLOAD + "Manufacturer/Photo/";
                    }
                    else
                    {
                        return DIR_UPLOAD + Convert.ToString(appSetting["dirManufacturerPhoto"].Value).ToLower().Trim();
                    }
                }
                catch
                {
                    return DIR_UPLOAD + "Manufacturer/Photo/";
                }
            }
        }
        #endregion


        #region 常用变量
        /// <summary>
        /// 编号长度
        /// </summary>
        public static byte NO_LENGTH
        {
            get
            {
                return 3;
            }
        }
        #endregion


        #region Cookie键
        /// <summary>
        /// 用户登录信息Cookie键
        /// </summary>
        public static string COOKIE_USER_LOGIN_KEY
        {
            get
            {
                return "C_UserLogin";
            }
        }
        /// <summary>
        /// 管理员登录信息Cookie键
        /// </summary>
        public static string COOKIE_ADMIN_LOGIN_KEY
        {
            get
            {
                return "C_AdminLogin";
            }
        }
        #endregion


        #region 验证码Session键
        /// <summary>
        /// 用户注册验证码Session键
        /// </summary>
        public static string VERIFY_CODE_USER_REG_KEY
        {
            get
            {
                return "VC_UserReg";
            }
        }
        /// <summary>
        /// 用户登录验证码Session键
        /// </summary>
        public static string VERIFY_CODE_USER_LOGIN_KEY
        {
            get
            {
                return "VC_UserLogin";
            }
        }
        /// <summary>
        /// 管理员登录验证码Session键
        /// </summary>
        public static string VERIFY_CODE_ADMIN_LOGIN_KEY
        {
            get
            {
                return "VC_AdminLogin";
            }
        }
        #endregion
    }
}
