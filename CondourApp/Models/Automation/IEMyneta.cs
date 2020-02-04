using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CondourApp.Models.Automation
{
    public class IEMyneta
    {
        public List<string> GetData(string searchString)
        {

            List<string> listAllData = new List<string>();

            try
            {
                HtmlWeb htmlWeb = new HtmlWeb();
                HtmlDocument htmlDoc = htmlWeb.Load("http://www.myneta.info/search_myneta.php?q=" + searchString);


                HtmlNode resultElement = htmlDoc.DocumentNode.SelectSingleNode("//table[@id='table1']");

                List<HtmlNode> allTRElements = resultElement.Elements("tr").ToList();

                foreach (HtmlNode node in allTRElements)
                {
                    listAllData.Add(node.OuterHtml);
                }

            }
            catch (Exception ex)
            {
                Models.Logger.Library.WriteLog("Error at crawling IEM ", ex);
            }

            return listAllData;
        }
    }
}