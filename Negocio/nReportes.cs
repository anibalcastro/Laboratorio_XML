using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Objeto;

namespace Negocio
{
    public class nReportes
    {
        XmlDocument docProducto;
        string rutaXmlProducto;

        XmlDocument docCompras;
        string rutaXmlCompras;


        public void LeerXMLProducto(string ruta)
        {
            rutaXmlProducto = ruta;
            docProducto = new XmlDocument();
            docProducto.Load(rutaXmlProducto);
        }


        public void LeerXMLCompras(string ruta)
        {
            rutaXmlCompras = ruta;
            docCompras = new XmlDocument();
            docCompras.Load(rutaXmlCompras);
        }

        public List<ObjRep1> buscarProductosCalorias()
        {
            
            List<ObjRep1> lista_rep1 = new List<ObjRep1>();

            XmlNodeList listaProducto = docProducto.SelectNodes("Productos/Producto");

            XmlNode unCompra;

            for (int x = 0; x < listaProducto.Count; x++)
            {
                unCompra = listaProducto.Item(x);
                int calorias = Convert.ToInt32(unCompra.SelectSingleNode("calorias").InnerText);

                if (calorias >= 450 && calorias <= 700)
                {
                    ObjRep1 rep1 = new ObjRep1()
                    {
                        id = Convert.ToInt32(unCompra.SelectSingleNode("id").InnerText),
                        nombre_producto = unCompra.SelectSingleNode("nombre").InnerText,
                        calorias = calorias
                    };
                    lista_rep1.Add(rep1);
                }     
            }
                
                return lista_rep1;
        }


        public List<ObjRep1> buscarCantidadComprada()
        {
            List<ObjRep1> productos = this.buscarProductosCalorias();

            List<ObjRep1> resultado = new List<ObjRep1>();

            XmlNodeList listaCompras = docCompras.SelectNodes("Compras_Restaurante/Datos_usuario/Compra");

            XmlNode unCompra;

            for (int x = 0; x<productos.Count; x++)
            {
                int cant = 0;
                for(int i = 0; i < listaCompras.Count; i++)
                {
                    unCompra = listaCompras.Item(i);

                    int id_productos = Convert.ToInt32(unCompra.SelectSingleNode("id_productos").InnerText);
                    
                    if (productos[x].id.Equals(id_productos))
                    {
                        int comprado = Convert.ToInt32(unCompra.SelectSingleNode("cant_productos").InnerText);
                        cant = cant + comprado;
                        
                    }
                }
                ObjRep1 reporte = new ObjRep1()
                {
                    id = productos[x].id,
                    nombre_producto = productos[x].nombre_producto,
                    calorias = productos[x].calorias,
                    cantidad_total_comprado = cant
                };
                resultado.Add(reporte);
            }
            return resultado;
        }
    }
}
