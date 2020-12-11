using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProAPI
{
    public class DTOBasicInfo
    {
        public int ID { get; set; }
        public string Temperature { get; set; }
        public string HeartRate { get; set; }
        public string SpO2 { get; set; }
        public string Pitch { get; set; }
        public string Roll { get; set; }
        public string Yaw { get; set; }
        public string AacX { get; set; }
        public string AacY { get; set; }
        public string AacZ { get; set; }
        public string Longtitude { get; set; }
        public string Latitude { get; set; }
        public string Altitude { get; set; }
        public string Location { get; set; }
        public string State { get; set; }
        public string GetInfotime { get; set; }
        public string DeviceID { get; set; }  // 0号设备实验 置0
        //public string Lord { get; set; }  
        //public string EmotionState { get; set; }
        //public string PicFile { get; set; }  // 0号设备实验 置0


    }
}