using System;
using System.Collections.Generic;
using System.Net;
using System.IO;
using Xamarin.Forms;

namespace CollegeScraper
{
    public abstract class HttpRequest
    {
        public abstract string Method { get; set; }
        public abstract string Accept { get; set; }
        public abstract string ContentType { get; set; }
        public abstract bool AllowAutoRedirect { get; set; }
        public abstract CookieContainer Cookies { get; set; }

        protected abstract void Initialise(Uri Url, List<Parameter> Parameters);
        
        public abstract HttpResponse GetResponse();
        

        public static HttpRequest Create(Uri Url, List<Parameter> Parameters)
        {
            HttpRequest New = DependencyService.Get<HttpRequest>(DependencyFetchTarget.NewInstance);
            New.Initialise(Url, Parameters);

            return New;
        }
    }
}
