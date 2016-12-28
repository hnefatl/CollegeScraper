using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Xamarin.Forms;

namespace CollegeScraper
{
    public class HttpResponse
    {
        public CookieCollection Cookies { get; protected set; }

        public byte[] Data { get; protected set; }
        public string Content { get { return new string(Encoding.UTF8.GetChars(Data)); } }

        public HttpResponse(byte[] Data, CookieCollection Cookies)
        {
            this.Data = Data;
            if (Cookies == null)
                this.Cookies = new CookieCollection();
            else
                this.Cookies = Cookies;
        }
    }
}
