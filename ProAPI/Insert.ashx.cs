using System;
using System.Web;
using Newtonsoft.Json;
using System.IO;
using System.Text;
using System.Web.SessionState;

namespace ProAPI
{
   
    /// <summary>
    /// Insert 的摘要说明
    /// </summary>
    public class Insert : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.AppendHeader("Access-Control-Allow-Origin", "*");
            context.Response.ContentType = "application/json";
            context.Response.ContentEncoding = Encoding.UTF8;

            Stream inputStream = context.Request.InputStream;  //传入的HTTP
            Encoding encoding = context.Request.ContentEncoding;
            StreamReader streamReader = new StreamReader(inputStream, encoding);

            string strJson = streamReader.ReadToEnd();
            DTOTemperature model = JsonConvert.DeserializeObject<DTOTemperature>(strJson);

            int code = 0;
            string msg = "";

            string strError = "";
            StringBuilder sbResult = new StringBuilder();


            try
            {
                string uid = model.uid;
                string location = model.location;
                float temperature = model.temperature;
                string time = model.time;



                if (string.IsNullOrEmpty(uid))
                {
                    strError += "uid不可为空。";
                }

                if (strError == "")
                {
                    string strSql = "insert into temperature(uid,location,temperature,time) values('"+uid+"','"+location+"',"+temperature+",'"+time+"')";
                    int iRows=SqlHelper.ExecInsert_Update_Delete(strSql);
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