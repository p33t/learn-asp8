using System.Xml;
using System.Xml.XPath;

namespace webapi;

public static class ProgramHelper
{
    /// Ensures all '/doc/members/member' tags contain a 'summary' tag by wrapping contents when no 'summary' tag is found 
    public static XPathDocument InsertSummaryTags(string inputFile)
    {
        if (!File.Exists(inputFile))
        {
            throw new FileNotFoundException($"The input file '{inputFile}' does not exist.");
        }

        // Load the XML using XPathDocument
        var doc = new XPathDocument(inputFile);
        var navigator = doc!.CreateNavigator();

        // Create output document
        var workingDoc = new XmlDocument();
        workingDoc.LoadXml(navigator.OuterXml);

        // Select all member nodes
        XmlNodeList? memberNodes = workingDoc.SelectNodes("//doc/members/member");

        if (memberNodes != null)
        {
            foreach (XmlNode memberNode in memberNodes)
            {
                // Check if summary element already exists
                XmlNode? existingSummary = memberNode.SelectSingleNode("summary");

                if (existingSummary == null && memberNode.HasChildNodes)
                {
                    // Create a new summary element
                    XmlElement summaryElement = workingDoc.CreateElement("summary");

                    // Create a list to store all child nodes
                    List<XmlNode> childNodes = new List<XmlNode>();
                    foreach (XmlNode child in memberNode.ChildNodes)
                    {
                        childNodes.Add(child);
                    }

                    // Move all child nodes to the summary element
                    foreach (XmlNode child in childNodes)
                    {
                        memberNode.RemoveChild(child);
                        summaryElement.AppendChild(child);
                    }

                    // Add the summary element to the member
                    memberNode.AppendChild(summaryElement);
                }
            }
        }

        // Convert modified XmlDocument back to XPathDocument
        using MemoryStream ms = new MemoryStream();
        workingDoc.Save(ms);
        ms.Position = 0;
        
        return new XPathDocument(ms);
    }
}