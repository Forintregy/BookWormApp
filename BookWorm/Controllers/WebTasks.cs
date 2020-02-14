using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;


namespace BookWorm
{
    public class WebTasks
    {
        public static readonly HttpClient client = new HttpClient();

        static HtmlDocument GetContents(string html)
        {
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);
            return htmlDocument;
        }

        public static async Task<List<Book>> FindBooks(string query)
        {
            var books = new List<Book>();
            var url = "http://www.gutenberg.org/ebooks/search/?query=" + query;
            var html = await client.GetStringAsync(url);
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);
            var contents =
                htmlDocument.DocumentNode.Descendants("li")
                .Where(node => node.GetAttributeValue("class", "").Equals("booklink")).ToList();
            foreach (var item in contents)
            {
                var title = item.Descendants("span").Where(node => node.GetAttributeValue("class", "").Equals("title")).FirstOrDefault().InnerText.Trim();
                var author = "";
                try
                {
                    author = item.Descendants("span").Where(node => node.GetAttributeValue("class", "").Equals("subtitle")).FirstOrDefault().InnerText;
                }
                catch { author = "Author not specified"; }
                var link = item.ChildNodes.Where(node => node.Name == "a").FirstOrDefault().Attributes.Where(a => a.Name == "href").FirstOrDefault().Value;
                books.Add(new Book(author, title, link));
            }
            return books;
        }

        public static async Task<Book> DownloadText(Book book)
        {
            Console.WriteLine("Attempting download...");
            var url = "http://www.gutenberg.org" + book.Link;
            var html = await client.GetStringAsync(url);
            var contents = GetContents(html).DocumentNode.Descendants("tr").Where(node => node.GetAttributeValue("typeof", "").Equals("pgterms:file"));
            var links = contents.Select(item => item.Attributes.Where(a => a.Name == "about").FirstOrDefault().Value).ToList();
            var linkToTxtFile = links.Where(item => item.Contains(".txt")).FirstOrDefault();
            if (linkToTxtFile != null)
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                var webClient = new WebClient();
                Stream data = webClient.OpenRead(linkToTxtFile);
                book.Text = new StringBuilder(new StreamReader(data).ReadToEnd()).ToString();
                 return book;
            }
            else throw new HttpRequestException("Book download failed");
        }
    }
}
