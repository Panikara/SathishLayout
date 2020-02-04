using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CondourApp.Models.Automation
{
    public class IEZauba
    {
        public string GetData(string searchString)
        {
            List<string> listAllData = new List<string>();

            string resultText = string.Empty;
            string companyInfoText = string.Empty;
            string directorDetails = string.Empty;
            string prosecutionDetails = string.Empty;
            string chargesDetails = string.Empty;

            try
            {


                string url = "https://www.zaubacorp.com/companysearchresults/" + searchString;
                string cin = GetCompanyCinNumber(url);

                HtmlWeb htmlWeb = new HtmlWeb();
                HtmlDocument htmlDoc = htmlWeb.Load(cin);



                HtmlNode resultElement = htmlDoc.DocumentNode.SelectSingleNode("//div[@class='container information']");
                foreach(HtmlNode childsOfContainerInfo in resultElement.ChildNodes)
                {
                    if (childsOfContainerInfo.Name == "#text")
                    {
                        companyInfoText += childsOfContainerInfo.InnerText;
                    }
                    else if(childsOfContainerInfo.Name == "br")
                    {
                        companyInfoText += "<br>";
                    }
                    else if(childsOfContainerInfo.Name == "div")
                    {
                        HtmlNode h4Node = childsOfContainerInfo.Element("h4");
                        if (h4Node != null)
                        {
                            if(h4Node.InnerText.Trim() == "Director Details")
                            {
                               directorDetails  = childsOfContainerInfo.Element("table").OuterHtml;
                            }
                            
                        }
                    }


                }

                prosecutionDetails = resultElement.NextSibling.Element("table").OuterHtml;
                chargesDetails = resultElement.NextSibling.NextSibling.Element("table").OuterHtml;


            }
            catch (Exception ex)
            {
                Models.Logger.Library.WriteLog("Error at crawling IEM ", ex);
            }
            
            return resultText;
        }

        public  string GetCompanyCinNumber(string url)
        {
            string cinNumber = string.Empty;

            HtmlWeb htmlWeb = new HtmlWeb();
            HtmlDocument htmlDoc = htmlWeb.Load(url);


           
            HtmlNode resultElement = htmlDoc.DocumentNode.SelectSingleNode("//table[@id='results']");

            List<HtmlNode> allTRElements = resultElement.SelectNodes("//td").ToList();

            //cinNumber = allTRElements[0].InnerText;
            cinNumber = allTRElements[1].Element("a").Attributes["href"].Value;

            return cinNumber;
        }
    }
}