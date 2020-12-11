using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.SessionState;
using System.IO;
using Newtonsoft.Json;
using System.Security.Policy;
using System.Net;

namespace ProAPI
{
    /// <summary>
    /// InsertPic 的摘要说明
    /// </summary>
    public class InsertPic : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.AppendHeader("Access-Control-Allow-Origin", "*");
            /*
            context.Response.ContentType = "text/plain";

            ///获取二进制文件

            int code = 0;
            string msg = "";
            string result = "";
            StringBuilder sbResult = new StringBuilder();
            if (context.Request["DeviceID"] == null)
            {
                code = 0;
                msg = "参数1错误！";
            }
            else
            {
                string DeviceID = context.Request["DeviceID"].ToString();   //只有一个设备 暂时不启用DeviceID字段
                try
                {

                    //
                    string strSql = "select PicFile from aPicture";
                    DataTable dt = SqlHelper.ExecDataTable(strSql);
                    for (int i = 0; i < dt.Rows.Count; i++)  //row 行
                    {
                        sbResult.Append("{\"ID\":" + dt.Rows[i]["ID"].ToString() + ",\"PicFile\":\"" + dt.Rows[i]["PicFile"].ToString() + "\"},");
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
            */
            //test 将图片转换成二进制
            int code1 = 0;
            string msg1 = "afs";
            string strResult1 = "{\"code\":" + code1 + ",\"msg\":\"" + msg1 + "\"}";
            context.Response.Write(strResult1);
            string data = "";
            string api_key = "JOjwHIfsDGiJddKzdq2Fgbc4xmrcQrFD";//传过去的参数，格式为 变量名：变量值
            string api_secret = "PMJ-qFt4anrI5AZgv7waPjaBhaN1ejJw";
            string image_url = "https://dss1.bdstatic.com/70cFuXSh_Q1YnxGkpoWK1HF6hhy/it/u=3923659610,3741122401&fm=26&gp=0.jpg";
            string return_attributes = "emotion";
            string url = "https://api-cn.faceplusplus.com/facepp/v3/detect";
            string imgdata = "{\"api_key\":\"" + api_key + "\",\"api_secret\":\"" + api_secret + "\",\"image_url\":\"" + image_url + "\",\"return_attributes\":\"" + return_attributes + "\"}";
            //string requestsentence = "api_key="+api_key+;

            string ResponseJson = Post(imgdata, url);
            
            DTOPicInfo model = JsonConvert.DeserializeObject<DTOPicInfo>(ResponseJson);

            ///回传数据库
            string strError = "";
            string str = "";
            StringBuilder sbResult = new StringBuilder();
            string msg = "";
            string result = "";
            int code = 0;
            try
            {
               
                if (str == "")
                {
                    str = "fasdfa";
                }
                //int ID = 4;
                //string PicFile = "";
                //string Lord = "12345567890.jpg";
                //string EmotionState = model.faces[0];
                //string GetInfotime = model.GetInfotime;
                //string DeviceID = "124567";

                /*if (strError == "")
                    {
                        string strSql = "insert into aPicture(DeviceID,Lord,EmotionState,PicFIle) values('" + DeviceID + "','" + Lord + "','" + EmotionState + "','" + PicFile + "')";
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
                    }*/
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

        public string Post(string data,string url)
        {
            //先根据用户请求的url构造请求地址
            //string serviceUrl = url;
            //创建Web访问对象
            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(url);
            //把数据转成“UTF-8”的字节流
            byte[] buf = System.Text.Encoding.GetEncoding("UTF-8").GetBytes(data);

            myRequest.Method = "POST";
            myRequest.ContentLength = buf.Length;
            myRequest.ContentType = "multipart/form-data";  //multipart/form-data
            myRequest.MaximumAutomaticRedirections = 1;
            myRequest.AllowAutoRedirect = true;
            //发送请求
            Stream stream = myRequest.GetRequestStream();
            stream.Write(buf, 0, buf.Length);
            stream.Close();

            //获取接口返回值
            //通过Web访问对象获取响应内容
            HttpWebResponse myResponse = (HttpWebResponse)myRequest.GetResponse();
            //通过响应内容流创建StreamReader对象，因为StreamReader更高级更快
            StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8);
            //string returnXml = HttpUtility.UrlDecode(reader.ReadToEnd());//如果有编码问题就用这个方法
            string returnjson = reader.ReadToEnd();//利用StreamReader就可以从响应内容从头读到尾
            reader.Close();
            myResponse.Close();
            return returnjson;

        }
    }
}