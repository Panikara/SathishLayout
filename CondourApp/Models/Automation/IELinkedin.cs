using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Windows.Forms;
using System.Threading;


namespace CondourApp.Models.Automation
{
    public class IELinkedin: System.Windows.Forms.ApplicationContext
    {
        AutoResetEvent resultEvent;
        WebBrowser ieBrowser;
        Thread thrd;

        public string Details { set; get; }

        /// <param name="resultEvent">functionality to keep the main thread waiting</param>
        public IELinkedin(AutoResetEvent resultEvent)
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
            // create a WebBrowser control
            ieBrowser = new WebBrowser();

            ieBrowser.ScriptErrorsSuppressed = true;

            // set WebBrowser event handls
            ieBrowser.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(IEBrowser_DocumentCompleted);           
            //ieBrowser.Navigate("https://www.linkedin.com/");
            ieBrowser.Navigate("https://www.linkedin.com/uas/login");

            
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

        void IEBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

            HtmlDocument doc = ((WebBrowser)sender).Document;

            ieBrowser.DocumentCompleted -= IEBrowser_DocumentCompleted;

            //System.Windows.Forms.HtmlElement cntrlEnail = ieBrowser.Document.GetElementById("login-email");
            //System.Windows.Forms.HtmlElement cntrlPassword = ieBrowser.Document.GetElementById("login-password");
            //System.Windows.Forms.HtmlElement cntrlSubmit = ieBrowser.Document.GetElementById("login-submit");

            System.Windows.Forms.HtmlElement cntrlEnail = ieBrowser.Document.GetElementById("session_key-login");
            System.Windows.Forms.HtmlElement cntrlPassword = ieBrowser.Document.GetElementById("session_password-login");
            System.Windows.Forms.HtmlElement cntrlSubmit = ieBrowser.Document.GetElementById("btn-primary");


            if (cntrlEnail != null && cntrlPassword !=null && cntrlSubmit !=null)
            {
                cntrlEnail.SetAttribute("value", "sukareti@gmail.com");
                cntrlPassword.SetAttribute("value", "kareti1024");
                cntrlSubmit.InvokeMember("click");
                
                ieBrowser.DocumentCompleted += IeBrowser_DocumentCompleted;                
            }

          
        }

        private void IeBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            ieBrowser.DocumentCompleted -= IeBrowser_DocumentCompleted;

            string value = string.Empty;
            System.Windows.Forms.HtmlElement loginCallOut = ieBrowser.Document.GetElementById("login-callout");
            if(loginCallOut!=null && loginCallOut.InnerText.Contains("Trying to sign in?"))
            {
                System.Windows.Forms.HtmlElement cntrlEnail = ieBrowser.Document.GetElementById("login-email");
                System.Windows.Forms.HtmlElement cntrlPassword = ieBrowser.Document.GetElementById("login-password");
                System.Windows.Forms.HtmlElement cntrlSubmit = ieBrowser.Document.GetElementById("login-submit");


                if (cntrlEnail != null && cntrlPassword != null && cntrlSubmit != null)
                {
                    cntrlEnail.SetAttribute("value", "sukareti@gmail.com");
                    cntrlPassword.SetAttribute("value", "kareti1024");
                    cntrlSubmit.InvokeMember("click");

                    ieBrowser.DocumentCompleted += IeBrowser_DocumentCompleted1;
                }
            }


            
        }

        private void IeBrowser_DocumentCompleted1(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            string value = string.Empty;

            System.Windows.Forms.HtmlElement resultTable1 = ieBrowser.Document.GetElementById("nav-typeahead-wormhole");
        }

        void SecondNavigation(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            string value = string.Empty;

            System.Windows.Forms.HtmlElement resultTable1 = ieBrowser.Document.GetElementById("nav-typeahead-wormhole");

            System.Windows.Forms.HtmlElement resultTable = ieBrowser.Document.GetElementById("block-system-main");

            if (resultTable != null)
            {
                HtmlElementCollection input1 = resultTable.GetElementsByTagName("DIV");



                foreach (System.Windows.Forms.HtmlElement input2 in input1)
                {
                    if (input2.GetAttribute("class") == "information")
                    {
                        System.Windows.Forms.HtmlElement mainelement = input2;
                    }

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