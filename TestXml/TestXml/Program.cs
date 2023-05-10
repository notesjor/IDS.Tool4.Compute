using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace TestXml
{
  internal class Program
  {
    static void Main(string[] args)
    {
      var xml = new XmlDocument();
      xml.LoadXml(File.ReadAllText(@"C:/Users/Jan/Desktop/tokens.xml"));

      
      var nodes = xml.SelectNodes("//*[local-name()='span']");
    }
  }
}
