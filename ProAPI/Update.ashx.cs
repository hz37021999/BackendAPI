using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System;
using System.Web;
using Newtonsoft.Json;
using System.IO;
using System.Text;
using System.Web.SessionState;

namespace ProAPI
{

    /// <summary>
    /// Update 的摘要说明
    /// </summary>
    public class Update : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            context.Response.ContentEncoding = Encoding.UTF8;

            Stream inputStream = context.Request.InputStream;
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
                int id = model.id;
                string uid = model.uid;
                string time = model.time;
                string location = model.location;
                float temperature = model.temperature;


                if (string.IsNullOrEmpty(id.ToString()))
                {
                    strError += "id不可为空。";
                }

                if (strError == "")
                {
                    string strSql = "update temperature set location='" + location + "' , temperature="+ temperature + "  where id=" + id;
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