using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace CollegeScraper
{
    public class ConnectionResult
    {
        public byte[] Data { get; private set; }
        public List<Cookie> Cookies { get; private set; }

        public ConnectionResult(byte[] Data, List<Cookie> Cookies)
        {
            this.Data = Data;
            this.Cookies = Cookies;
        }
    }
}
