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
    public class nGestionProductos
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

            new Datos.bdGestionProductos().guardarDatosXML(doc, ruta);

        }

        public void LeerXML(string ruta)
        {
            rutaXml = ruta;
            doc = new XmlDocument();
            doc.Load(rutaXml);
        }

        public void RegistrarProducto(ObjProductos productos)
        {

            XmlNode Registro = this.CrearProductos(productos);

            XmlNode nodo = doc.DocumentElement;

            nodo.InsertAfter(Registro, nodo.LastChild);

            new bdLogin().guardarDatosXML(doc, rutaXml);

        }


        public XmlNode CrearProductos(ObjProductos productos)
        {
            XmlNode Registro = doc.CreateElement("Producto");

            XmlElement xid = doc.CreateElement("id");
            xid.InnerText = productos.id.ToString();
            Registro.AppendChild(xid);

            XmlElement xnombre = doc.CreateElement("nombre");
            xnombre.InnerText = productos.nombre.ToString();
            Registro.AppendChild(xnombre);

            XmlElement xprecio = doc.CreateElement("precio");
            xprecio.InnerText = productos.precio.ToString();
            Registro.AppendChild(xprecio);

            XmlElement xdescripcion = doc.CreateElement("descripcion");
            xdescripcion.InnerText = productos.descripcion.ToString();
            Registro.AppendChild(xdescripcion);

            XmlElement xcalorias = doc.CreateElement("calorias");
            xcalorias.InnerText = productos.calorias.ToString();
            Registro.AppendChild(xcalorias);

            XmlElement xcantidad = doc.CreateElement("cantidad");
            xcantidad.InnerText = productos.cantidad.ToString();
            Registro.AppendChild(xcantidad);

            return Registro;
        }

        
        public List<ObjProductos> llenarLista()
        {
            List<ObjProductos> Lista = new List<ObjProductos>();
            XmlNodeList listaUsuarios = doc.SelectNodes("Productos/Producto");

            XmlNode unUsuario;

            for (int x = 0; x < listaUsuarios.Count; x++)
            {
                unUsuario = listaUsuarios.Item(x);
                ObjProductos objetos = new ObjProductos
                {
                    id = Convert.ToInt32(unUsuario.SelectSingleNode("id").InnerText),
                    nombre = unUsuario.SelectSingleNode("nombre").InnerText,
                    precio = Convert.ToInt32(unUsuario.SelectSingleNode("precio").InnerText),
                    descripcion = unUsuario.SelectSingleNode("descripcion").InnerText,
                    calorias = Convert.ToInt32(unUsuario.SelectSingleNode("calorias").InnerText),
                    cantidad = Convert.ToInt32(unUsuario.SelectSingleNode("cantidad").InnerText)
                };

                Lista.Add(objetos);
            }
            return Lista;
        }
        
        public void Modificar(ObjProductos productos)
        {
            XmlNodeList listaProducto = doc.SelectNodes("Productos/Producto");

            foreach (XmlNode item in listaProducto)
            {
                if (Convert.ToInt32(item.SelectSingleNode("id").InnerText) == productos.id)
                {
                    item.SelectSingleNode("nombre").InnerText = Convert.ToString(productos.nombre);
                    item.SelectSingleNode("precio").InnerText = Convert.ToString(productos.precio);
                    item.SelectSingleNode("calorias").InnerText = Convert.ToString(productos.calorias);
                    item.SelectSingleNode("cantidad").InnerText = Convert.ToString(productos.cantidad);
                    item.SelectSingleNode("descripcion").InnerText = productos.descripcion;

                }
            }

            new bdGestionProductos().guardarDatosXML(doc, rutaXml);
        }

        public void Eliminar(ObjProductos productos)
        {
            XmlNodeList listaProducto = doc.SelectNodes("Productos/Producto");
            XmlNode estudiante = doc.DocumentElement;

            foreach (XmlNode item in listaProducto)
            {
                if (Convert.ToInt32(item.SelectSingleNode("id").InnerText) == productos.id)
                {
                    XmlNode borrarNodo = item;
                    estudiante.RemoveChild(borrarNodo);
                }
            }

            new bdGestionProductos().guardarDatosXML(doc, rutaXml);
        }

        public List<ObjProductos> llenarComboBox()
        {
            List<ObjProductos> Lista = new List<ObjProductos>();
            XmlNodeList listaUsuarios = doc.SelectNodes("Productos/Producto");

            XmlNode unUsuario;

            for (int x = 0; x < listaUsuarios.Count; x++)
            {
                unUsuario = listaUsuarios.Item(x);

                if (Convert.ToInt32(unUsuario.SelectSingleNode("cantidad").InnerText) > 0)
                {
                    ObjProductos objetos = new ObjProductos
                    {
                        id = Convert.ToInt32(unUsuario.SelectSingleNode("id").InnerText),
                        nombre = unUsuario.SelectSingleNode("nombre").InnerText,
                        precio = Convert.ToInt32(unUsuario.SelectSingleNode("precio").InnerText),
                        descripcion = unUsuario.SelectSingleNode("descripcion").InnerText,
                        calorias = Convert.ToInt32(unUsuario.SelectSingleNode("calorias").InnerText),
                        cantidad = Convert.ToInt32(unUsuario.SelectSingleNode("cantidad").InnerText)
                    };
                    Lista.Add(objetos);
                }

            }
            return Lista;
        }

        public bool consultarCantidad(int id ,int cantidad)
        {
            bool validar = false;

            XmlNodeList listaUsuarios = doc.SelectNodes("Productos/Producto");

            XmlNode unUsuario;

            for (int x = 0; x < listaUsuarios.Count; x++)
            {
                unUsuario = listaUsuarios.Item(x);
                int id_producto = Convert.ToInt32(unUsuario.SelectSingleNode("id").InnerText);
                int producto_cant = Convert.ToInt32(unUsuario.SelectSingleNode("cantidad").InnerText);
               
                if (id_producto.Equals(id))
                {
                    if (producto_cant >= cantidad)
                    {
                        validar = true;
                    }
                    else
                    {
                        validar = false;
                    }
                }
                
                    
            }

            return validar;
        }

        public void restarproductos(int id, int cantidad_restar)
        {
            XmlNodeList listaProducto = doc.SelectNodes("Productos/Producto");

            foreach (XmlNode item in listaProducto)
            {
                if (Convert.ToInt32(item.SelectSingleNode("id").InnerText) == id)
                {

                    int cantidad = Convert.ToInt32(item.SelectSingleNode("cantidad").InnerText);

                    int resultado = (cantidad - cantidad_restar);

                    item.SelectSingleNode("cantidad").InnerText = Convert.ToString(resultado);
                }
            }

            new bdGestionProductos().guardarDatosXML(doc, rutaXml);
        }

    }
}
