using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProAPI
{
    public class DTOIsFall
    {
        public int ID { get; set; }
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
        public string GetInfoTime { get; set; }
        public string DeviceID { get; set; }  // 0号设备实验 置0
    }
}