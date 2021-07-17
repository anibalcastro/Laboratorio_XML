using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Objeto;
using Datos;

namespace Negocio
{
    public class nComprador
    {
        XmlDocument doc;
        string rutaXml;

        public void CrearXML(string ruta, string nodoRaiz)
        {
            rutaXml = ruta;
            doc = new XmlDocument();

            XmlDeclaration xmlDeclaration = doc.CreateXmlDeclaration("1.0", "UTF-8", "no");

            XmlNode nodo = doc.DocumentElement;
            doc.InsertBefore(xmlDeclaration, nodo);

            XmlNode elemento = doc.CreateElement(nodoRaiz);
            doc.AppendChild(elemento);

            new Datos.bdComprador().guardarDatosXML(doc, ruta);

        }

        public void LeerXML(string ruta)
        {
            rutaXml = ruta;
            doc = new XmlDocument();
            doc.Load(rutaXml);
        }

        public void RegistrarCompra(ObjComprador comprador)
        {

            XmlNode Registro = this.CrearCompra(comprador);

            XmlNode nodo = doc.DocumentElement;

            nodo.InsertAfter(Registro, nodo.LastChild);

            new bdComprador().guardarDatosXML(doc, rutaXml);

        }


        public XmlNode CrearCompra(ObjComprador compras)
        {
            XmlNode Registro = doc.CreateElement("Datos_usuario");

            XmlElement xid_compra = doc.CreateElement("id_compra");
            xid_compra.InnerText = compras.id_compra.ToString();
            Registro.AppendChild(xid_compra);

            XmlElement xnombre_comprador = doc.CreateElement("nombre_comprador");
            xnombre_comprador.InnerText = compras.nombre_comprador.ToString();
            Registro.AppendChild(xnombre_comprador);

            XmlElement xcedula_comprador = doc.CreateElement("cedula_comprador");
            xcedula_comprador.InnerText = compras.cedula_comprador.ToString();
            Registro.AppendChild(xcedula_comprador);

            XmlElement xgener_comprador = doc.CreateElement("genero_comprador");
            xgener_comprador.InnerText = compras.genero_comprador.ToString();
            Registro.AppendChild(xgener_comprador);

            
            XmlNode compra = doc.CreateElement("Compra");


            XmlElement xid_productos = doc.CreateElement("id_productos");
            xid_productos.InnerText = compras.id_productos.ToString();
            compra.AppendChild(xid_productos);

            XmlElement xcantidad_productos = doc.CreateElement("cant_productos");
            xcantidad_productos.InnerText = compras.cantidad_productos.ToString();
            compra.AppendChild(xcantidad_productos);

            XmlElement xcalorias_totales = doc.CreateElement("calorias_totales");
            xcalorias_totales.InnerText = compras.calorias_totales.ToString();
            compra.AppendChild(xcalorias_totales);

            XmlElement xmonto_pagar = doc.CreateElement("monto_pagar");
            xmonto_pagar.InnerText = compras.monto_total.ToString();
            compra.AppendChild(xmonto_pagar);


            Registro.AppendChild(compra);

             

            return Registro;
        }

        public List<int> consultarID()
        {
            List<int> lista_id = new List<int>();

            XmlNodeList listaCompras = doc.SelectNodes("Compras_Restaurante/Datos_usuario");

            XmlNode unCompra;

            for (int x = 0; x < listaCompras.Count; x++)
            {
                unCompra = listaCompras.Item(x);


                lista_id.Add(Convert.ToInt32(unCompra.SelectSingleNode("id_compra").InnerText));
                
            }
            return lista_id;
        }
    }
}
