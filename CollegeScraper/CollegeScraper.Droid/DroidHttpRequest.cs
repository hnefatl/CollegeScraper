using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.IO;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using CollegeScraper;

[assembly: Xamarin.Forms.Dependency(typeof(HttpRequest))]
namespace CollegeScraper.Droid
{
    public class DroidHttpRequest
        : HttpRequest
    {
        private HttpWebRequest Inner;

        public override string Method { get { return Inner.Method; } set { Inner.Method = value; } }
        public override string Accept { get { return Inner.Accept; } set { Inner.Accept = value; } }
        public override string ContentType { get { return Inner.ContentType; } set { Inner.ContentType = value; } }
        public override bool AllowAutoRedirect { get { return Inner.AllowAutoRedirect; } set { Inner.AllowAutoRedirect = value; } }
        public override CookieContainer Cookies { get { return Inner.CookieContainer; } set { Inner.CookieContainer = value; } }

        protected override void Initialise(Uri Url, List<Parameter> Parameters)
        {
            Inner = new HttpWebRequest(Url);
            
            string PostParams = string.Join("&", Parameters.Select(p => p.Key + "=" + p.Value));
            using (StreamWriter Writer = new StreamWriter(Inner.GetRequestStream()))
            {
                Writer.Write(PostParams);
            }
        }

        public override HttpResponse GetResponse()
        {
            using (HttpWebResponse Response = (HttpWebResponse)Inner.GetResponse())
            {
                byte[] Data;
                using (MemoryStream Result = new MemoryStream())
                {
                    Response.GetResponseStream().CopyTo(Result);
                    Data = Result.ToArray();
                }

                return new HttpResponse(Data, Response.Cookies);
            }
        }
    }
}