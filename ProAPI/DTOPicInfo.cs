using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProAPI
{
    public class DTOPicInfo
    {
        //public int ID { get; set; }
        //public string[] faces { get; set; }
        //public string GetInfoTime { get; set; }
        public string error_message { get; set; }
        public string attributes { get; set; }
        public int face_num { get; set; }  // 0号设备实验 置0
        public float anger { get; set; }
        public float disgust { get; set; }
        public float fear { get; set; }
        public float happiness { get; set; }
        public float neutral { get; set; }
        public float sadness { get; set; }
        public float surprise { get; set; }
    }
}