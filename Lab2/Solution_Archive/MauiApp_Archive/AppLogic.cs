using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Xsl;

namespace MauiApp_Archive
{
    public class BookItem
    {
        public string Title { get; set; } = "";
        public string MaterialType { get; set; } = "";
        public string Faculty { get; set; } = "";
        public string Department { get; set; } = "";
        public string Publisher { get; set; } = "";
        public int Pages { get; set; }
        public int Year { get; set; }
        public List<string> Authors { get; set; } = new List<string>();

        public string DisplayInfo
        {
            get
            {
                string authorsStr = Authors.Count > 0 ? string.Join(", ", Authors) : "—";
                string pubStr = string.IsNullOrEmpty(Publisher) ? "—" : Publisher;
                string yearStr = Year == 0 ? "—" : Year.ToString();

                return $"[{MaterialType}] {Title}\n" +
                       $"Автори: {authorsStr}\n" +
                       $"Факультет: {Faculty}\n" +
                       $"Кафедра: {Department}\n" +
                       $"Видавництво: {pubStr} | Стор.: {Pages} | Рік: {Year}";
            }
        }
    }

    public class SearchFilter
    {
        public string? Author { get; set; }
        public string? Department { get; set; }
        public List<string> MaterialTypes { get; set; } = new List<string>();
        public string? Keyword { get; set; }

        public bool IsMatch(BookItem item)
        {
            if (!string.IsNullOrEmpty(Author) && !item.Authors.Contains(Author))
                return false;

            if (!string.IsNullOrEmpty(Department) && item.Department != Department)
                return false;

            if (MaterialTypes.Count > 0 && !MaterialTypes.Contains(item.MaterialType))
                return false;

            if (!string.IsNullOrWhiteSpace(Keyword))
            {
                bool titleMatch = item.Title != null && item.Title.Contains(Keyword, StringComparison.OrdinalIgnoreCase);
                bool authorMatch = item.Authors != null && item.Authors.Any(author => author.Contains(Keyword, StringComparison.OrdinalIgnoreCase));
                if (!titleMatch && !authorMatch)
                    return false;
            }

            return true;
        }
    }

    public class SelectableItem
    {
        public string Name { get; set; } = "";
        public bool IsSelected { get; set; }
    }

    public interface IXMLSearchStrategy
    {
        List<BookItem> Search(string xmlContent, SearchFilter filter);
    }

    public class LINQ_XML : IXMLSearchStrategy
    {
        public List<BookItem> Search(string xmlContent, SearchFilter filter)
        {
            var doc = XDocument.Parse(xmlContent);

            var query = from book in doc.Descendants("book")
                        let authorsList = (from a in book.Descendants("author")
                                           select (string?)a.Attribute("AU_NAME")).ToList()
                        let item = new BookItem
                        {
                            Title = (string?)book.Attribute("BK_NAME") ?? "",
                            MaterialType = (string?)book.Attribute("BK_TYPE") ?? "",
                            Faculty = (string?)book.Attribute("BK_FACULTY") ?? "",
                            Department = (string?)book.Attribute("BK_DEPARTMENT") ?? "",
                            Publisher = (string?)book.Attribute("BK_PUBLISHER") ?? "",
                            Pages = (int?)book.Attribute("BK_PAGES") ?? 0,
                            Year = (int?)book.Attribute("BK_YEAR") ?? 0,
                            Authors = authorsList
                        }
                        where filter.IsMatch(item)
                        select item;

            return query.ToList();
        }
    }

    public class DOM_API : IXMLSearchStrategy
    {
        public List<BookItem> Search(string xmlContent, SearchFilter filter)
        {
            var list = new List<BookItem>();
            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlContent);
            XmlNodeList? bookNodes = xmlDoc.SelectNodes("//FacultyArchive/book");
            if (bookNodes == null) return list;

            foreach (XmlNode bookNode in bookNodes)
            {
                var item = new BookItem();

                if (bookNode.Attributes != null)
                {
                    if (bookNode.Attributes["BK_NAME"] != null)
                        item.Title = bookNode.Attributes["BK_NAME"]!.Value;

                    if (bookNode.Attributes["BK_TYPE"] != null)
                        item.MaterialType = bookNode.Attributes["BK_TYPE"]!.Value;

                    if (bookNode.Attributes["BK_FACULTY"] != null)
                        item.Faculty = bookNode.Attributes["BK_FACULTY"]!.Value;

                    if (bookNode.Attributes["BK_DEPARTMENT"] != null)
                        item.Department = bookNode.Attributes["BK_DEPARTMENT"]!.Value;

                    if (bookNode.Attributes["BK_PUBLISHER"] != null)
                        item.Publisher = bookNode.Attributes["BK_PUBLISHER"]!.Value;

                    if (bookNode.Attributes["BK_YEAR"] != null &&
                        int.TryParse(bookNode.Attributes["BK_YEAR"]!.Value, out int year))
                    {
                        item.Year = year;
                    }

                    if (bookNode.Attributes["BK_PAGES"] != null &&
                        int.TryParse(bookNode.Attributes["BK_PAGES"]!.Value, out int pages))
                    {
                        item.Pages = pages;
                    }
                }
                XmlNodeList? authorNodes = bookNode.SelectNodes("author");
                if (authorNodes != null)
                {
                    foreach (XmlNode authorNode in authorNodes)
                    {
                        if (authorNode.Attributes?["AU_NAME"] != null)
                        {
                            item.Authors.Add(authorNode.Attributes["AU_NAME"]!.Value);
                        }
                    }
                }

                if (filter.IsMatch(item))
                {
                    list.Add(item);
                }
            }

            return list;
        }
    }

    public class SAX_API : IXMLSearchStrategy
    {
        public List<BookItem> Search(string xmlContent, SearchFilter filter)
        {
            var list = new List<BookItem>();
            using var stringReader = new StringReader(xmlContent);
            var xmlReader = new XmlTextReader(stringReader);
            BookItem? currentBook = null;

            while (xmlReader.Read())
            {
                switch (xmlReader.NodeType)
                {
                    case XmlNodeType.Element:
                        if (xmlReader.Name == "book")
                        {
                            currentBook = new BookItem();
                            if (xmlReader.HasAttributes)
                            {
                                while (xmlReader.MoveToNextAttribute())
                                {
                                    switch (xmlReader.Name)
                                    {
                                        case "BK_NAME":
                                            currentBook.Title = xmlReader.Value;
                                            break;
                                        case "BK_TYPE":
                                            currentBook.MaterialType = xmlReader.Value;
                                            break;
                                        case "BK_FACULTY":
                                            currentBook.Faculty = xmlReader.Value;
                                            break;
                                        case "BK_DEPARTMENT":
                                            currentBook.Department = xmlReader.Value;
                                            break;
                                        case "BK_PUBLISHER":
                                            currentBook.Publisher = xmlReader.Value;
                                            break;
                                        case "BK_YEAR":
                                            if (int.TryParse(xmlReader.Value, out int y))
                                                currentBook.Year = y;
                                            break;
                                        case "BK_PAGES":
                                            if (int.TryParse(xmlReader.Value, out int p))
                                                currentBook.Pages = p;
                                            break;
                                    }
                                }
                                xmlReader.MoveToElement();
                            }
                        }
                        else if (xmlReader.Name == "author" && currentBook != null)
                        {
                            string? auName = xmlReader.GetAttribute("AU_NAME");
                            if (!string.IsNullOrEmpty(auName))
                            {
                                currentBook.Authors.Add(auName);
                            }
                        }
                        break;

                    case XmlNodeType.EndElement:
                        if (xmlReader.Name == "book" && currentBook != null)
                        {
                            if (filter.IsMatch(currentBook))
                            {
                                list.Add(currentBook);
                            }
                            currentBook = null;
                        }
                        break;
                }
            }

            xmlReader.Close();
            return list;
        }
    }

    public class XMLSearchContext
    {
        private IXMLSearchStrategy _strategy = new LINQ_XML();

        public void SetStrategy(IXMLSearchStrategy strategy)
        {
            _strategy = strategy;
        }

        public List<BookItem> ExecuteSearch(string xml, SearchFilter filter)
        {
            return _strategy.Search(xml, filter);
        }
    }

    public static class XsltHelper
    {
        public static void Transform(string xmlPath, string xslPath, string outputPath)
        {
            XslCompiledTransform xslt = new XslCompiledTransform();
            xslt.Load(xslPath);
            xslt.Transform(xmlPath, outputPath);
        }
    }
}
