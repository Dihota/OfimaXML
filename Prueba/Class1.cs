using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;


namespace Prueba
{
    public class Class1
    {
        public string xmlread( string ruta)
        {
            string datos = string.Empty;
            int Saltos = 0;
            string texto = string.Empty;

            XmlDocument readXML = new XmlDocument();
            readXML.Load(ruta);

            foreach (XmlNode N1 in readXML.DocumentElement.ChildNodes)
            {

                if (N1.Name == "cac:SenderParty")
                {
                    if (N1.HasChildNodes)
                    {
                        foreach (XmlNode N2 in N1.ChildNodes)
                        {
                            foreach (XmlNode N3 in N2.ChildNodes)
                            {
                                if (N3.Name == "cbc:RegistrationName" || N3.Name == "cbc:CompanyID")
                                {
                                    datos =datos +  N3.InnerText + ",";
                                    Saltos++;
                                }
                            }
                        }

                    }
                }
                else
                {
                    if (N1.Name == "cbc:ParentDocumentID")
                    {
                        datos = datos + N1.InnerText + ",";
                        Saltos++;
                    }
                }
                
                if (Saltos == 3)
                {
                    break;
                }
               
            }

            return datos;
        }

    }
}
