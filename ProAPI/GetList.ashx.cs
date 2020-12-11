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
    /// GetList 的摘要说明
    /// </summary>
    public class GetList : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.AppendHeader("Access-Control-Allow-Origin", "*");
            context.Response.ContentType = "text/plain";
            int code = 0;
            string msg = "";
            string result = "";
            StringBuilder sbResult = new StringBuilder();
            if (context.Request["uid"] == null)
            {
                code = 0;
                msg = "参数2222错误！";
            }
            else
            {
                string uid = context.Request["uid"].ToString();
                try
                {

                    string strSql = "select * from temperature where uid='"+uid+"' ";
                    //string strSql = "select * from ";
                    DataTable dt = SqlHelper.ExecDataTable(strSql);
                        for(int i=0;i<dt.Rows.Count;i++)  //row 行
                        { 
                            sbResult.Append("{\"uid\":\"" + dt.Rows[i]["uid"].ToString() + "\",\"location\":\"" + dt.Rows[i]["location"].ToString() + "\",\"temperature\":" + dt.Rows[i]["temperature"].ToString() + ",\"time\":\"" + dt.Rows[i]["time"].ToString() + "\"},");
                        }
                        if (!string.IsNullOrEmpty(sbResult.ToString()))
                        {
                            result = sbResult.ToString().Substring(0, sbResult.ToString().Length - 1);
                        }

             
                    code = 1;
                    msg = "操作成功！";
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