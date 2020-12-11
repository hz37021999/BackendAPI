using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Drawing;
using System.Drawing.Imaging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ProAPI
{
    /// <summary>
    /// InsertPicture 的摘要说明
    /// </summary>
    public class InsertPicture : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {

            context.Response.AppendHeader("Access-Control-Allow-Origin", "*");
            context.Response.ContentType = "application/x-www-form-urlencoded";
            //context.Response.ContentType = "application/json";
            context.Response.ContentEncoding = Encoding.UTF8;
            int code = 0;
            string msg = "";
            string strError = "";
            string strResult = "";


            //图片二进制转文件
            //读入已有的二进制文件
            //设置打开文件的格式
            
            /*string ConfigPth = HttpContext.Current.Server.MapPath(context.Request.ApplicationPath) + @"\test\002.dat";//获取当前路
            string binary = "";//二进制流字符串
            byte[] bytes = null;
            //使用“打开”对话框中选择的文件名实例化FileStream对象
            FileStream myStream = new FileStream(ConfigPth, FileMode.Open, FileAccess.Read);
            //使用FileStream对象实例化BinaryReader二进制写入流对象
            BinaryReader myReader = new BinaryReader(myStream);
            if (myReader.PeekChar() != -1)
            {
                //以二进制方式读取文件中的内容
                try
                {
                    myReader.BaseStream.Seek(0, SeekOrigin.Begin);
                    bytes = myReader.ReadBytes(Convert.ToInt32(myStream.Length.ToString()));
                    binary = System.Text.Encoding.Default.GetString(bytes);
                }
                catch(EndOfStreamException ex)
                {
                    msg = ex.Message;
                }
            }
            //关闭当前二进制读取流
            myReader.Close();
            //关闭当前文件流
            myStream.Close();
            strResult = "{\"binary\":\"" + binary + "\",\"msg\":\"" + msg + "\"}";
            //context.Response.Write(strResult);*/

            //读取context中的二进制流,与读入已有的二进制文件二选一
            
            byte[] bytes = new byte[context.Request.InputStream.Length];



            context.Request.InputStream.Read(bytes, 0, bytes.Length);
            string binary = System.Text.Encoding.Default.GetString(bytes);
            //msg = "";
            strResult = "{\"binary\":\"" + binary + "\",\"msg\":\"" + msg + "\"}";
            //context.Response.Write(strResult); 


            //转化图片

            int filelength = bytes.Length;//获得数组的长度
            string time = DateTime.Now.ToString("yyyyMMdd-HH-mm-ss");
            System.Random a = new Random(System.DateTime.Now.Millisecond); // use System.DateTime.Now.Millisecond as seed
            int RandKey = a.Next(10000);
            string picName = time + RandKey.ToString() + ".jpeg";
            //string myUrl = "E:/STUDY/ProAPI/ProAPI/ProAPI/"+picName;//图片地址
            string myUrl = HttpContext.Current.Server.MapPath(context.Request.ApplicationPath) + @"test\" + picName;//图片地址

            // 创建文件流
            FileStream fs = new FileStream(myUrl, FileMode.OpenOrCreate);
            BinaryWriter w = new BinaryWriter(fs);//以二进制的形式将基元内写入流
            w.BaseStream.Write(bytes, 0, filelength);//把数据库中的图片二进制添加到BinaryWriter
            w.Flush();
            w.Close();

            //执行情绪识别
            //参数字典
            Dictionary<string, object> verifyPostParameters = new Dictionary<string, object>();
            verifyPostParameters.Add("api_key", "JOjwHIfsDGiJddKzdq2Fgbc4xmrcQrFD");
            verifyPostParameters.Add("api_secret", "PMJ-qFt4anrI5AZgv7waPjaBhaN1ejJw");
            verifyPostParameters.Add("return_landmark", "1");
            verifyPostParameters.Add("return_attributes", "emotion");
            Bitmap bmp = new Bitmap(myUrl); // 图片地址
            byte[] fileImage;
            using (Stream stream1 = new MemoryStream())
            {
                bmp.Save(stream1, ImageFormat.Jpeg);
                byte[] arr = new byte[stream1.Length];
                stream1.Position = 0;
                stream1.Read(arr, 0, (int)stream1.Length);
                stream1.Close();
                fileImage = arr;
            }
            //添加图片参数
            verifyPostParameters.Add("image_file", new HttpHelper4MultipartForm.FileParameter(fileImage, picName, "application/octet-stream"));
            HttpWebResponse verifyResponse = HttpHelper4MultipartForm.MultipartFormDataPost("https://api-cn.faceplusplus.com/facepp/v3/detect", "", verifyPostParameters);

            //context.Response.Write(verifyResponse);
            //HttpWebResponse myResponse = (HttpWebResponse)verifyResponse.GetResponse();
            Stream myResponseStream = verifyResponse.GetResponseStream();
            StreamReader reader = new StreamReader(myResponseStream, Encoding.UTF8);
            string returnjson = reader.ReadToEnd();
            myResponseStream.Close();
            reader.Close();
            verifyResponse.Close();
            //context.Response.Write(returnjson);
            //return returnjson;

            //上传数据库
            try
            {
                JObject result = JObject.Parse(returnjson);//假设result为数据结构
                DTOPicInfo data = new DTOPicInfo();
                //data.error_message = result["error_message"].Value<string>("error_message");
                data.face_num = result.Value<int>("face_num");
                JArray faces = result.Value<JArray>("faces");

                JObject facesjb = JObject.Parse(faces[0].ToString());
                data.attributes = facesjb["attributes"]["emotion"].ToString();
                DTOPicInfo model = JsonConvert.DeserializeObject<DTOPicInfo>(data.attributes);
                data.anger = model.anger;
                data.disgust = model.disgust;
                data.happiness = model.happiness;
                data.neutral = model.neutral;
                data.sadness = model.sadness;
                data.surprise = model.surprise;
                data.fear = model.fear;
                time = DateTime.Now.ToString("yyyyMMdd HH:mm:ss");
                strResult = "{\"anger\":\"" + data.anger.ToString() + ",\"disgust\":\"" + data.disgust.ToString() + ",\"happiness\":\"" + data.happiness.ToString() + "\"}";
                context.Response.Write(strResult);

  
                if (strError == "")
                {
                    //string strSql = "insert into aPicture(anger,disgust,happiness,neutral,sadness,surprise) values('" + data.anger + "','" + data.disgust + "','" + data.happiness + "','" + data.neutral + "','" + data.sadness + "','" + data.surprise + "');";
                    string strSql = "insert into aPicture(anger,disgust,happiness,neutral,sadness,GetInfoTime,facenum,Path,surprise,fear) values('" + data.anger + "','" + data.disgust + "','" + data.happiness + "','" + data.neutral + "','" + data.sadness + "','" + time + "','" + data.face_num + "','" + myUrl + "','" + data.surprise + "','" + data.fear + "');";
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
            strResult = "{\"code\":" + code + ",\"msg\":\"" + msg + "\"}";
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