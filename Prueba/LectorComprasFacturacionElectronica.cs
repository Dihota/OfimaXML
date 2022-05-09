using System;
using System.Collections.Generic;
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
                                        string prueba = N3.InnerXml;
                                        XmlAuxiliar(prueba);
                                    }



                                                                 
                               
                            }
                        }
                        break;
                    default:
                        break;
                }

                //if (Saltos == 2)
                //{
                //    break;
                //}
            }
            return Datos;
        }


        public void XmlAuxiliar(string Conten)
        {

            

        }
    }



}
