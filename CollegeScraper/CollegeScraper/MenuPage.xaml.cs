using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace CollegeScraper
{
    public partial class MenuPage
        : ContentPage
    {
        private string labelText = "Initial text";
        public string LabelText { get { return labelText; } set { labelText = value; OnPropertyChanged("LabelText"); } }

        public MenuPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            // https://raven.cam.ac.uk/auth/authenticate2.html?ver=3&userid=kc506&pwd=9j*E1D~y1235810&url=https%3A%2F%2Fmeal.robinson.cam.ac.uk

            object locker = new object();
            // Need to run networking stuff on a different thread to obey Android policies
            Task connectionTask = Task.Factory.StartNew(() =>
            {
                if (!Monitor.TryEnter(locker))
                    return;

                try
                {
                    Uri Url = new Uri(URLs.NetworkUsage);
                    //WebConnector auth = WebConnector.Create(@"https://raven.cam.ac.uk/auth/authenticate2.html");
                    //auth.AddParameter("userid", "kc506");
                    //auth.AddParameter("pwd", "9j*E1D~y1235810");
                    ////auth.AddParameter("submit", "Submit");
                    //ConnectionResult authResult = auth.Connect(true, false);
                    //string authResponse = ToString(authResult.Data);

                    //WebConnector page = WebConnector.Create(Url);
                    //page.AddCookies(authResult.Cookies);
                    //ConnectionResult pageResult = page.Connect(false, true);

                    //LabelText = ToString(pageResult.Data);
                    WebConnector conn = new WebConnector(Url, "kc506", "9j*E1D~y1235810");
                    ConnectionResult Result = conn.Connect();

                    LabelText = ToString(Result.Data);
                }
                catch (Exception e)
                {
                    LabelText = e.Message + "\n" + e.StackTrace;
                }
                Monitor.Exit(locker);
            });
            connectionTask.Wait();

            base.OnAppearing();
        }

        public string ToString(byte[] Data)
        {
            StringBuilder builder = new StringBuilder();
            foreach (byte b in Data)
                builder.Append((char)b);

            return builder.ToString();
        }
    }
}
