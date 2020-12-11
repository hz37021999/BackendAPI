using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.SessionState;

namespace ProAPI
{
    /// <summary>
    /// SelectUserInfo的摘要说明
    /// </summary>
    public class SelectUserInfo: IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.AppendHeader("Access-Control-Allow-Origin", "*");
            context.Response.ContentType = "text/plain";
            int code = 0;
            string msg = "";
            string result = "";
            StringBuilder sbResult = new StringBuilder();
            if (context.Request["UserID"] == null)
            {
                code = 0;
                msg = "参数错误！";
            }
            else
            {
                string ID = context.Request["UserID"].ToString();   //只有一个设备 暂时不启用DeviceID字段
                try
                {

                    string strSql = "select * from aUserInfo where UserID =\'" + ID + "\'";
                    DataTable dt = SqlHelper.ExecDataTable(strSql);
                    for (int i = 0; i < dt.Rows.Count; i++)  //row 行
                    {
                        sbResult.Append("{\"UserID\":\"" + dt.Rows[i]["UserID"].ToString() + "\",\"UserName\":\"" + dt.Rows[i]["UserName"].ToString() + "\",\"Password\":\"" + dt.Rows[i]["Password"].ToString() + "\"},");
                    }
                    if (!string.IsNullOrEmpty(sbResult.ToString()))
                    {
                        result = sbResult.ToString().Substring(0, sbResult.ToString().Length-1);
                    }
                    if (result == null)
                    {
                        msg = "result为空";
                    }
                    code = 1;
                    //msg = "操作成功！";
                }
                catch (Exception ex)
                {
                    code = 0;
                    msg = "操作失败！" + ex.Message;
                }
            }

            string strResult = "{\"code\":" + code + ",\"msg\":\"" + msg + "\",\"result\":[" + result + "]}";
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