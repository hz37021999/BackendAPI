using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProAPI
{
    public class DTOTemperature
    {
        public int id { get; set; }
        public string uid { get; set; }
        public string location { get; set; }
        public float temperature { get; set; }
        public string time { get; set; }

        public string ts { get; set; }
    }
}