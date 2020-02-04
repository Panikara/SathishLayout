using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Threading;
using System.Windows.Forms;


namespace CondourApp.Models.Automation
{
    public class Kanoon:System.Windows.Forms.ApplicationContext
    {

        AutoResetEvent resultEvent;
        WebBrowser ieBrowser;
        Thread thrd;

        public string Details { set; get; }

        /// <param name="resultEvent">functionality to keep the main thread waiting</param>
        public Kanoon(AutoResetEvent resultEvent)
        {
            this.resultEvent = resultEvent;

            thrd = new Thread(new ThreadStart(
                delegate
                {
                    Init(false);
                    System.Windows.Forms.Application.Run(this);
                }));
            // set thread to STA state before starting
            thrd.SetApartmentState(ApartmentState.STA);
            thrd.Start();
        }

        // initialize the WebBrowser and the form
        private void Init(bool visible)
        {
            //scriptCallback = new ScriptCallback(this);

            // create a WebBrowser control
            ieBrowser = new WebBrowser();

            ieBrowser.ScriptErrorsSuppressed = true;

            // set the location of script callback functions
            //ieBrowser.ObjectForScripting = scriptCallback;
            // set WebBrowser event handls
            ieBrowser.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(IEBrowser_DocumentCompleted);
            //ieBrowser.Navigating += new WebBrowserNavigatingEventHandler(IEBrowser_Navigating);

            ieBrowser.Navigate("http://www.mca.gov.in/mcafoportal/showCheckCompanyName.do");
        }

        // dipose the WebBrowser control and the form and its controls
        protected override void Dispose(bool disposing)
        {
            if (thrd != null)
            {
                thrd.Abort();
                thrd = null;
                return;
            }

            System.Runtime.InteropServices.Marshal.Release(ieBrowser.Handle);
            ieBrowser.Dispose();

            base.Dispose(disposing);
        }


        // DocumentCompleted event handle
        void IEBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            HtmlDocument doc = ((WebBrowser)sender).Document;

            ieBrowser.DocumentCompleted -= IEBrowser_DocumentCompleted;

            System.Windows.Forms.HtmlElement search = ieBrowser.Document.GetElementById("name1");
            if (search != null)
            {
                search.SetAttribute("value", "verinon");
                System.Windows.Forms.HtmlElement searchButton = ieBrowser.Document.GetElementById("checkCompanyName_0");

                searchButton.InvokeMember("click");

                ieBrowser.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(IEBrowser_DocumentCompleted1);

            }
        }

        void IEBrowser_DocumentCompleted1(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            string value = string.Empty;
            System.Windows.Forms.HtmlElement resultTable = ieBrowser.Document.GetElementById("companyList");

            if (resultTable != null)
            {
                HtmlElementCollection input1 = resultTable.GetElementsByTagName("TD");

                foreach (System.Windows.Forms.HtmlElement input2 in input1)
                {
                    value += input2.InnerText + "\n";


                }

            }
            else
            {
                value = "Not Found";
            }

            Details = value;

            this.resultEvent.Set();
        }
    }
}