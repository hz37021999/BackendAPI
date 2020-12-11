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
    /// SelectBasicInfoTemperature 的摘要说明
    /// </summary>
    public class SelectBasicInfoTemperature : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.AppendHeader("Access-Control-Allow-Origin", "*");
            context.Response.ContentType = "text/plain";
            int code = 0;
            string msg = "";
            string result = "";
            StringBuilder sbResult = new StringBuilder();
            if (context.Request["DeviceID"] == null)
            {
                code = 0;
                msg = "参数0错误！";
            }
            else
            {
                string DeviceID = context.Request["DeviceID"].ToString();   //只有一个设备 暂时不启用DeviceID字段
                try
                {

                    string strSql = "select * from aBasicInfo";
                    DataTable dt = SqlHelper.ExecDataTable(strSql);
                    for (int i = 0; i < dt.Rows.Count; i++)  //row 行
                    {
                        sbResult.Append("{\"ID\":" + dt.Rows[i]["ID"].ToString() + ",\"Temperature\":\"" + dt.Rows[i]["Temperature"].ToString() + "\",\"GetInfoTime\":\"" + dt.Rows[i]["GetInfoTime"].ToString() + "\",\"DeviceID\":\"" + dt.Rows[i]["DeviceID"].ToString() + "\"},");

                        //sbResult.Append("{\"ID\":\"" + dt.Rows[i]["ID"].ToString() + "\",\"Temperature\":\"" + dt.Rows[i]["Temperature"].ToString() + "\",\"GetInfoTime\":" + dt.Rows[i]["GetInfoTime"].ToString() + ",\"DeviceID\":\"" + dt.Rows[i]["DeviceID"].ToString() + "\"},");

                        //                        sbResult.Append("{\"ID\":\"" + dt.Rows[i]["ID"].ToString() + "\",\"Temperature\":\"" + dt.Rows[i]["Temperature"].ToString() + "\",\"GetInfoTime\":" + dt.Rows[i]["GetInfoTime"].ToString() + "\"},");
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