using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Xml;
using HtmlAgilityPack;
using JobSpecCollector.Data;
using JobSpecCollector.Interfaces;

namespace JobSpecCollector.Implementations
{
    public class WebGetter : IJobSpecWebGetter
    {
        private const string INDEEDURL = "http://www.indeed.co.uk/rss?q=c%23+.net+%C2%A350%2C000%2B&l=London&radius=0&jt=fulltime&sort=date&limit=10000";

        public IEnumerable<JobSpec> GetJobSpecs()
        {
            var rss = DownloadString(INDEEDURL);

            var xml = new XmlDocument();
            xml.LoadXml(rss);
            var items = xml.SelectNodes("/rss/channel/item");

            foreach (XmlNode item in items)
            {
                yield return NodeToJobSpec(item);
            }
        }


        private Data.JobSpec NodeToJobSpec(XmlNode node)
        {
            var link = ToUtf8(node.SelectSingleNode("link").InnerText);
            return new Data.JobSpec
            {
                Title = ToUtf8(node.SelectSingleNode("title").InnerText),
                Link = link,
                Source = node.SelectSingleNode("source").InnerText,
                Guid = Guid.Parse(node.SelectSingleNode("guid").InnerText),
                PubDate = DateTime.Parse(node.SelectSingleNode("pubDate").InnerText),
                Description = ToUtf8(node.SelectSingleNode("description").InnerText),
                SpecText = ToUtf8(GetIndeedSpec(link))
            };
        }

        private string GetIndeedSpec(string link)
        {
            if (string.IsNullOrEmpty(link)) return string.Empty;
            var page = DownloadString(link);
            var doc = new HtmlDocument();
            doc.LoadHtml(page);
            var jsNode = doc.GetElementbyId("job_summary");
            var text = jsNode.InnerText;
            return text;
        }

        private string ToUtf8(string s)
        {
            if (string.IsNullOrEmpty(s)) return string.Empty;
            return Encoding.UTF8.GetString(Encoding.Default.GetBytes(s));
        }

        private string DownloadString(string url)
        {
            using (var wc = new WebClient())
            {
                return wc.DownloadString(url);
            }
        }
    }
}
