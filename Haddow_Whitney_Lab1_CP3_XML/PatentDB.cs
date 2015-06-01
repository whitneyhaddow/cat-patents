using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace Haddow_Whitney_Lab1_CP3_XML
{
    class PatentDB
    {
        private const string path = "Patents.xml";

        //read XML file for patent info
        public static List<Patent> GetPatents()
        {
            XmlDocument doc = new XmlDocument();

            if (!File.Exists("Patents.xml"))
                doc.Save("Patents.xml");

            List<Patent> patents = new List<Patent>();//create empty list

            XmlReaderSettings readerSettings = new XmlReaderSettings(); //define reader settings
            readerSettings.IgnoreWhitespace = true;
            readerSettings.IgnoreComments = true;
            XmlReader readXml = null; 
           
            try
            {
                readXml = XmlReader.Create(path, readerSettings); 
                if (readXml.ReadToDescendant("Patent")) //read to first Patent node
                {
                    do //create new patent object
                    {
                        Patent patent = new Patent();
                        patent.Number = Convert.ToInt32(readXml["Number"]);
                        readXml.ReadStartElement("Patent");
                        patent.AppNumber = readXml.ReadElementContentAsString();
                        patent.Description = readXml.ReadElementContentAsString();
                        patent.FilingDate = DateTime.Parse(readXml.ReadElementContentAsString());
                        patent.Inventor = readXml.ReadElementContentAsString();
                        patent.Inventor2 = readXml.ReadElementContentAsString();
                        patents.Add(patent);
                    }
                    while (readXml.ReadToNextSibling("Patent")); 
                }
            }
            catch (XmlException ex)
            {
                MessageBox.Show(ex.Message, "Xml Error");
            }
            catch (IOException ex)
            {
                MessageBox.Show(ex.Message, "IO Exception");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception Occurred");
            }
            finally
            {
                if (readXml != null)
                    readXml.Close();
            }

            return patents;
         
        } //end GetPatents()


        //save any new patents to XML file
        public static void SavePatents(List<Patent> patents)
        {
            XmlWriterSettings writerSettings = new XmlWriterSettings(); //define writer settings
            writerSettings.Indent = true;
            writerSettings.IndentChars = ("    ");
            XmlWriter writeXml = null; 

            try
            {
                writeXml = XmlWriter.Create(path, writerSettings); 

                writeXml.WriteStartDocument(); 
                writeXml.WriteStartElement("Patents");

                foreach (Patent patent in patents) //write each object in list to Xml file
                {
                    writeXml.WriteStartElement("Patent");
                    writeXml.WriteAttributeString("Number", patent.Number.ToString());
                    writeXml.WriteElementString("AppNumber", patent.AppNumber);
                    writeXml.WriteElementString("Description", patent.Description);
                    string datestring = patent.FilingDate.ToString("yyyy-MM-dd"); 
                    writeXml.WriteElementString("FilingDate", datestring);
                    writeXml.WriteElementString("Inventor", patent.Inventor);
                    writeXml.WriteElementString("Inventor2", patent.Inventor2);
                    writeXml.WriteEndElement();
                }

                writeXml.WriteEndElement(); //close tag of root element
            }
            catch (XmlException ex)
            {
                MessageBox.Show(ex.Message, "Xml Error");
            }
            catch (IOException ex)
            {
                MessageBox.Show(ex.Message, "IO Exception");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception Occurred");
            }
            finally
            {
                if (writeXml != null)
                    writeXml.Close();
            }

        } //end SavePatents()

    } //end class
}
