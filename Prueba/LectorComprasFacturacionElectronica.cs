using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Prueba
{
    public class LectorComprasFacturacionElectronica
    {
        public string Datos { get; set; }
        public string Texto { get; set; }

        //Metodo Encargado de capturar la informacion referente al emisor.
        public string XmlEmisor(string Ruta)
        {
            //Variable para determinar, cuando capturamos toda la informacion. 
            int Saltos= 0;

            //Se lee el xml
            XmlDocument ReadXML = new XmlDocument();
            ReadXML.Load(Ruta);

            //Ciclos para recorrer nodos XML
            foreach (XmlNode N1 in ReadXML.DocumentElement.ChildNodes)
            {
                //Se valida valor del nodo, para determinar si se procede a ingresar a los nodos hijos.
                if (N1.Name == "cac:SenderParty")
                {
                    if (N1.HasChildNodes)
                    {
                        foreach (XmlNode N2 in N1.ChildNodes)
                        {
                            foreach (XmlNode N3 in N2.ChildNodes)
                            {
                                switch (N3.Name)
                                {
                                    case "cbc:RegistrationName":
                                        Datos = Datos + N3.InnerText + ",";
                                        Saltos++;
                                        break;
                                    case "cbc:CompanyID":
                                        Datos = Datos + N3.InnerText +"-"+ N3.Attributes["schemeID"].Value + ",";
                                        Saltos++;
                                        break;
                                    default:
                                        break;
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (N1.Name == "cbc:ParentDocumentID" || N1.Name == "cbc:IssueDate" || N1.Name == "cbc:IssueTime")
                    {
                        //se captura el valor del nodo.
                        Datos = Datos + N1.InnerText + ",";
                        Saltos++;
                    }
                }
                if (Saltos == 5)
                {
                    break;
                }
            }
           return Datos;
        }

        //Metodo Encargado de capturar la informacion referente al Adquiriente.
        public string XmlAdquiriente(string Ruta)
        {
            Datos = string.Empty;
            //Variable para determinar, cuando capturamos toda la informacion. 
            int Saltos = 0;

            //Se lee el xml
            XmlDocument ReadXML = new XmlDocument();
            ReadXML.Load(Ruta);

            //Ciclos para recorrer nodos XML
            foreach (XmlNode N1 in ReadXML.DocumentElement.ChildNodes)
            {
                switch (N1.Name)
                {
                    case "cac:ReceiverParty":
                                if (N1.HasChildNodes)
                                {
                                    foreach (XmlNode N2 in N1.ChildNodes)
                                    {
                                        foreach (XmlNode N3 in N2.ChildNodes)
                                        {
                                            switch (N3.Name)
                                            {
                                                case "cbc:RegistrationName":
                                                    Datos = Datos + N3.InnerText + ",";
                                                    Saltos++;
                                                    break;
                                                case "cbc:CompanyID":
                                                    Datos = Datos + N3.InnerText + "-" + N3.Attributes["schemeID"].Value + ",";
                                                    Saltos++;
                                                    break;
                                                default:
                                                    break;
                                            }
                                        }
                                    }
                                }
                        break;
                    case "cac:Attachment":
                        foreach (XmlNode N2 in N1.ChildNodes)
                        {
                            foreach (XmlNode N3 in N2.ChildNodes)
                            {
                                    if (N3.Name == "cbc:Description")
                                    {
                                        string Nodo = N3.InnerXml;

                                        Datos =Datos + XmlAuxiliar(replace(Nodo));
                                        Saltos++;
                                    break;
                                    }

                            }
                        }
                        break;
                    default:
                        break;
                }

                if (Saltos == 3)
                {
                    break;
                }
            }
            return Datos;
        }


        public string replace(string Nodo)
        {
            string Nodo1 = Nodo.Replace("<![CDATA[", "");
            string Retorno = Nodo1.Replace("]]>", "");
            return Retorno;
        }


        public string XmlAuxiliar(string Conten)
        {
            string Datos1 = string.Empty;
            string ruta = @"C:\Users\Hogar\Documents\Diego\ofima\Desarrollo\Lector Compra\XML\Nuevos";
            Directory.CreateDirectory(ruta);

            string Temporal = System.IO.Path.Combine(ruta, "temp.xml");

            using (System.IO.StreamWriter escribir = new System.IO.StreamWriter(Temporal, true))
            {
                escribir.WriteLine(Conten);
            }

            Datos1 = Emisor2(Temporal);

            //File.Delete(Temporal);

            return Datos1;
        }

        public string Emisor2(string ruta)
        {
            XmlDocument EmisorXML2 = new XmlDocument();
            EmisorXML2.Load(ruta);

            string Datos2 = string.Empty;
            int Salir=0;
          

            foreach (XmlNode N1 in EmisorXML2.DocumentElement.ChildNodes)
            {
                switch (N1.Name)
                {
                    case "cbc:UUID":
                        Datos2 = Datos2 + N1.InnerText + ",";
                            Salir++;
                        break;
                    case "cac:AccountingCustomerParty":
                        foreach (XmlNode N2 in N1.ChildNodes)
                        {
                            foreach (XmlNode N3 in N2.ChildNodes)
                            {
                                switch (N3.Name)
                                {
                                    case "cac:PhysicalLocation":
                                        foreach (XmlNode N4 in N3.ChildNodes)
                                        {
                                            foreach (XmlNode N5 in N4.ChildNodes)
                                            {
                                                switch (N5.Name)
                                                {
                                                    case "cbc:CityName":
                                                        Datos2 = Datos2 + N5.InnerText + ",";
                                                        Salir++;
                                                        break;
                                                    case "cac:AddressLine":
                                                        foreach (XmlNode N6 in N5.ChildNodes)
                                                        {
                                                            if (N6.Name == "cbc:Line")
                                                            {
                                                                Datos2 = Datos2 + N5.InnerText + ",";
                                                                Salir++;
                                                                break;
                                                            }
                                                        }
                                                        break;
                                                    default:
                                                        break;
                                                }
                                            }
                                        }
                                        break;
                                    case "cac:Contact":
                                        foreach (XmlNode N4 in N3.ChildNodes)
                                        {
                                            if (N4.Name == "cbc:Telephone" || N4.Name == "cbc:ElectronicMail")
                                            {
                                                Datos2 = Datos2 + N4.InnerText + ",";
                                                Salir++;
                                                break;
                                            }
                                        }
                                        break;
                                    default:
                                        break;
                                }
                            }
                        }
                        break;
                    default:
                        break ;
                }
                if (Salir == 4)
                {
                    break;
                }
            }


            return Datos2;

        }
    }



}
