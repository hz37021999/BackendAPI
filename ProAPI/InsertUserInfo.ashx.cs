using System;
using System.Web;
using Newtonsoft.Json;
using System.IO;
using System.Text;
using System.Web.SessionState;

namespace ProAPI
{

    /// <summary>
    /// InsertUserInfo的摘要说明
    /// </summary>
    public class InsertUserInfo: IHttpHandler, IRequiresSessionState
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
            DTOUserInfo model = JsonConvert.DeserializeObject<DTOUserInfo>(strJson);

            int code = 0;
            string msg = "";

            string strError = "";
            StringBuilder sbResult = new StringBuilder();


            try
            {
                string UserID = model.UserID;
                string UserName = model.UserName;
                string Password = model.Password;


                /*                if (string.IsNullOrEmpty(ID.ToString()))
                                {
                                    strError += "ID不可为空。";
                                }*/

                if (strError == "")
                {
                    string strSql = "insert into aUserInfo(UserID,UserName,Password) values('" + UserID + "','" + UserName + "','" + Password + "');";
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