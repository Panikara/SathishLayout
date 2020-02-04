using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HtmlAgilityPack;

namespace CondourApp.Models.Automation
{
    public class IEKanoon
    {
        public List<string> GetData(string searchString)
        {

            List<string> listAllData = new List<string>();

            try
            {



                HtmlWeb htmlWeb = new HtmlWeb();


                for (int i = 0; i <= 2; i++)
                {
                    HtmlDocument htmlDoc = htmlWeb.Load("https://indiankanoon.org/search/?formInput=" + searchString + "&pagenum=" + i);


                    HtmlNode resultElement = htmlDoc.DocumentNode.SelectSingleNode("//div[@class='results_middle']");

                    if (resultElement.InnerText.Contains("No matching results"))
                    {
                        return listAllData;
                    }

                    HtmlNodeCollection nodeCollection = htmlDoc.DocumentNode.SelectNodes("//div[@class='result']");

                    string resultText = string.Empty;

                    foreach (HtmlNode node in nodeCollection)
                    {

                        node.Attributes["class"].Remove();
                        node.SetAttributeValue("style", "margin-bottom: 1em;");

                        List<HtmlNode> childNodes = node.Elements("div").ToList();
                        foreach (HtmlNode childNode in childNodes)
                        {

                            if (childNode.Attributes["class"].Value == "result_title")
                            {
                                childNode.Attributes["class"].Remove();
                                childNode.SetAttributeValue("style", "display: inline;margin: 0;font-size: 1.1em;");

                                HtmlNode subChild = childNode.Element("a");
                                string urlOfHeading = subChild.Attributes["href"].Value;
                                subChild.Attributes["class"].Remove();
                                subChild.SetAttributeValue("href", "https://indiankanoon.org" + urlOfHeading);
                                subChild.SetAttributeValue("target", "_blank");
                            }
                            else if (childNode.Attributes["class"].Value == "headline")
                            {
                                childNode.Attributes["class"].Remove();
                                childNode.SetAttributeValue("style", "font-size: small;");
                            }
                            else if (childNode.Attributes["class"].Value == "docsource")
                            {
                                childNode.Attributes["class"].Remove();
                                childNode.SetAttributeValue("style", "color: #329300; display: inline;");

                            }
                        }

                        foreach (HtmlNode spanAnchorTag in node.Element("span").Elements("a"))
                        {
                            spanAnchorTag.Attributes["class"].Remove();
                            spanAnchorTag.SetAttributeValue("style", "text-decoration: none;color: #0473B8;");
                            string urlOfHeading = spanAnchorTag.Attributes["href"].Value;
                            spanAnchorTag.SetAttributeValue("href", "https://indiankanoon.org" + urlOfHeading);
                            spanAnchorTag.SetAttributeValue("target", "_blank");
                        }


                        listAllData.Add(node.OuterHtml);

                        //resultText = resultText + "\n" + node.OuterHtml;
                    }
                }

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

            return listAllData;
        }
    }
}