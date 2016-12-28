using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

using Xamarin.Forms;
using System.Net;

namespace CollegeScraper
{
    public class WebConnector
    {
        private static readonly Uri AuthenticatorUrl = new Uri(URLs.Authenticator);

        private Uri TargetUrl;
        private string UserID;
        private string Password;

        protected CookieCollection Cookies;

        public WebConnector(Uri Url, string UserID, string Password)
        {
            TargetUrl = Url;
            Cookies = new CookieCollection();

            this.UserID = UserID;
            this.Password = Password;
        }

        protected HttpResponse Connect(Uri Url)
        {
            return Connect(Url, new List<Parameter>());
        }
        protected HttpResponse Connect(Uri Url, List<Parameter> Parameters)
        {
            HttpRequest Request = HttpRequest.Create(Url, Parameters);
            Request.Method = "POST";
            Request.Accept = "*/*";
            Request.ContentType = "application/x-www-form-urlencoded";
            Request.AllowAutoRedirect = true;
            if (Request.Cookies == null)
                Request.Cookies = new CookieContainer();
            Request.Cookies.Add(Url, Cookies);

            HttpResponse Response = Request.GetResponse();

            Cookies.Add(Response.Cookies);

            return Response;
        }

        public ConnectionResult Connect()
        {
            List<Parameter> AuthParameters = new List<Parameter>() { new Parameter("userid", UserID), new Parameter("pwd", Password) };
            HttpResponse AuthResponse = Connect(AuthenticatorUrl, AuthParameters); // Automatically accumulate cookies
            HttpResponse DataResponse = Connect(TargetUrl);

            return new ConnectionResult(DataResponse.Data, Cookies.OfType<Cookie>().ToList());
        }
    }
}
