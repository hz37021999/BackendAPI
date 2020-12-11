using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using System.IO;
using System.Text;
using System.Web.SessionState;

namespace ProAPI
{
    /// <summary>
    /// Delete 的摘要说明
    /// </summary>
    public class Delete : IHttpHandler, IRequiresSessionState
    {
          

            public void ProcessRequest(HttpContext context)
            {
            context.Response.AppendHeader("Access-Control-Allow-Origin", "*");
            context.Response.ContentType = "application/json";
                context.Response.ContentEncoding = Encoding.UTF8;
 

                int code = 0;
                string msg = "";

                string strError = "";
                StringBuilder sbResult = new StringBuilder();


                try
                {
                

                    if (context.Request["id"]==null)
                    {
                        strError += "id不可为空。";
                    }

                    if (strError == "")
                    {
                        string strSql = "delete from temperature  where id=" + context.Request["id"].ToString();
                        int iRows = SqlHelper.ExecInsert_Update_Delete(strSql);
                        if (iRows > 0)
                        {
                            code = 1;
                            msg = "操作成功！";
                        }
                    }
                    else
                    {
                        code = 0;
                        msg = strError;
                    }
                }
                catch (Exception ex)
                {
                    code = 0;
                    msg = "操作失败！" + ex.Message;
                }

                string strResult = "{\"code\":" + code + ",\"msg\":\"" + msg + "\"}";
                context.Response.Write(strResult);
            }

            public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}