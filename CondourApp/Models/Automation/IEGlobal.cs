using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CondourApp.Models.Automation
{
    public class IEGlobal
    {
        public string GetData(string searchString)
        {
            List<string> listAllData = new List<string>();

            string resultText = string.Empty;

            try
            {
                HtmlWeb htmlWeb = new HtmlWeb();

               
                    HtmlDocument htmlDoc = htmlWeb.Load("https://www.google.com/search?q=" + searchString + "&start=0");


                    HtmlNode resultElement = htmlDoc.DocumentNode.SelectSingleNode("//div[@id='rso']"); 
                    
                    HtmlNodeCollection htmlNodecol = htmlDoc.DocumentNode.SelectNodes("//div[@id='rso']");
                    HtmlNodeCollection htmlNodecol1 = htmlDoc.DocumentNode.SelectNodes("//div[@class='g']");
                    HtmlNodeCollection htmlNodecol2 = htmlDoc.DocumentNode.SelectNodes("//div[@class='bkWMgd']");



                    foreach (HtmlNode node in htmlNodecol1)
                    {
                        resultText = resultText + "\n" + node.OuterHtml;
                    }

                    //resultText = resultText + "\n" + resultElement.OuterHtml;
             

            }
            catch (Exception ex)
            {
                Models.Logger.Library.WriteLog("Error at crawling IEM ", ex);
            }

            //HtmlNode node1 =  resultText;

            //if (link12 != null)
            //{
            //    List<string> link1 = link12.Select(x => x.Attributes["class"].Value).ToList<string>();
            //}

            return resultText;
        }
    }
}