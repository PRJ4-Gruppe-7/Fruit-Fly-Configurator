using System;
using System.Collections.Generic;
using System.Text;

namespace FFC.Models
{
    public class Reference
    {
        public int referencepointId { get; set; }
        public int x { get; set; }
        public int y { get; set; }
        public int rssI1 { get; set; }
        public int rssI2 { get; set; }
        public int rssI3 { get; set; }
        public string detail { get { return string.Format("x: {0}, y: {1}, rssi: {2}", x, y, rssI1); } }
    }
}
