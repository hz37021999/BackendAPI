using System;
using System.Web;
using Newtonsoft.Json;
using System.IO;
using System.Text;
using System.Web.SessionState;

namespace ProAPI
{

    /// <summary>
    /// InsertData 的摘要说明
    /// </summary>
    public class InsertData : IHttpHandler, IRequiresSessionState
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

/*            FileStream fs = new FileStream(HttpContext.Current.Server.MapPath(context.Request.ApplicationPath) + @"test\\receive.txt",FileMode.Create);
            byte[] data = System.Text.Encoding.UTF8.GetBytes(strJson);
            fs.Write(data,0,data.Length);
            fs.Flush();
            fs.Close();*/

            DTOBasicInfo model = JsonConvert.DeserializeObject<DTOBasicInfo>(strJson);

            int code = 0; 
            string msg = "";

            string strError = "";
            StringBuilder sbResult = new StringBuilder();


            try
            {
                int ID = model.ID;
                string Temperature = model.Temperature;
                string HeartRate = model.HeartRate;
                string SpO2 = model.SpO2;
                string Pitch = model.Pitch;
                string Roll = model.Roll;
                string Yaw = model.Yaw;
                string AacX = model.AacX;
                string AacY = model.AacY;
                string AacZ = model.AacZ;
                string Longtitude = model.Longtitude;
                string Latitude = model.Latitude;
                string Altitude = model.Altitude;
                string State = model.State;
                string GetInfotime = model.GetInfotime;
                //string GetInfotime = DateTime.Now.ToString("yyyyMMdd HH:mm:ss");
                //string DeviceID = model.DeviceID;



                /*                if (string.IsNullOrEmpty(ID.ToString()))
                                {
                                    strError += "ID不可为空。";
                                }*/

                if (strError == "")
                {
                    string strSql = "insert into aBasicInfo(Temperature,HeartRate,SpO2,GetInfotime) values('" + Temperature + "','" + HeartRate + "','" + SpO2 + "','" + GetInfotime + "');"
                        + "insert into aIsFall(Pitch,Roll,Yaw,AacX,AacY,AacZ,Longtitude,Latitude,State,GetInfotime) values('"+Pitch+"','"+Roll+"','"+Yaw+"','"+AacX+"','"+AacY+"','"+AacZ+"','"+Longtitude+"','"+Latitude+"','"+State+"','"+GetInfotime+"')";
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