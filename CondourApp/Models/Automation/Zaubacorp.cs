using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Windows.Forms;
using System.Threading;

namespace CondourApp.Models.Automation
{
    public class Zaubacorp:System.Windows.Forms.ApplicationContext
    {
        AutoResetEvent resultEvent;
        WebBrowser ieBrowser;
        Thread thrd;

        public string Details { set; get; }

        /// <param name="resultEvent">functionality to keep the main thread waiting</param>
        public Zaubacorp(AutoResetEvent resultEvent)
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

            // set WebBrowser event handls
            ieBrowser.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(IEBrowser_DocumentCompleted);
            //ieBrowser.Navigating += new WebBrowserNavigatingEventHandler(IEBrowser_Navigating);
            //ieBrowser.Navigate("https://www.zaubacorp.com/");
            ieBrowser.Navigate("https://www.zaubacorp.com/companysearchresults/VERINON TECHNOLOGY SOLUTIONS PRIVATE LIMITED");
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

            System.Windows.Forms.HtmlElement search = ieBrowser.Document.GetElementById("results");
            if (search != null)
            {

                HtmlElementCollection input1 = search.GetElementsByTagName("TD");


                //input1[2].InvokeMember("click");


                //ieBrowser.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(SecondNavigation);
                ieBrowser.Navigate("https://www.zaubacorp.com/company/VERINON%20TECHNOLOGY%20SOLUTIONS%20PRIVATE%20LIMITED/U72200TG2004FTC042623");


                while (ieBrowser.ReadyState != WebBrowserReadyState.Complete)
                {
                    Application.DoEvents();
                }

                //ieBrowser.Navigate("https://www.google.com/");
                ieBrowser.DocumentCompleted += IeBrowser_DocumentCompleted;

                //foreach (System.Windows.Forms.HtmlElement input2 in input1)
                //{
                //}

                //search.SetAttribute("value", "VERINON TECHNOLOGY SOLUTIONS PRIVATE LIMITED");
                //System.Windows.Forms.HtmlElement searchButton = ieBrowser.Document.GetElementById("edit-submit--3");

            }

            //HtmlDocument doc = ((WebBrowser)sender).Document;

            //ieBrowser.DocumentCompleted -= IEBrowser_DocumentCompleted;

            //System.Windows.Forms.HtmlElement search = ieBrowser.Document.GetElementById("searchid");
            //if (search != null)
            //{
            //    search.SetAttribute("value", "VERINON TECHNOLOGY SOLUTIONS PRIVATE LIMITED");
            //    System.Windows.Forms.HtmlElement searchButton = ieBrowser.Document.GetElementById("edit-submit--3");

            //    ieBrowser.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(SecondNavigation);
            //    searchButton.InvokeMember("click");



            //}
        }

        private void IeBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            string value = string.Empty;
            System.Windows.Forms.HtmlElement resultTable = ieBrowser.Document.GetElementById("block-system-main");
        }

        void SecondNavigation(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            string value = string.Empty;
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