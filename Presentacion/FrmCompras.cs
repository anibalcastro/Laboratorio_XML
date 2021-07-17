using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Negocio;
using Objeto;

namespace Presentacion
{
    public partial class FrmCompras : Form
    {
        string nombre_comprador;
        int id_compra;
        string genero;
        int cedula;
        int calorias_totales;
        int precio_totol;
        int cantida_total;
        List<ObjDTG> lista_data = new List<ObjDTG>();


        int cantidad;
        int id_producto;

        Negocio.nComprador comprador;
        Negocio.nGestionProductos producto;
        Objeto.ObjComprador objeto;
        
        ObjDTG dtg;

        public FrmCompras()
        {
            InitializeComponent();
            
            comprador = new nComprador();
            producto = new nGestionProductos();
            this.CrearXMLCompras();
            this.CrearXMLProducto();
            this.dtgAdquirir.AllowUserToAddRows = false;


        }

        public void CrearXMLCompras()
        {
            if (!File.Exists(@"C:\Users\admin\source\repos\Laboratorio_XML\Datos\ArchivosXML\Compras.XML"))
            {
                comprador.CrearXML(@"C:\Users\admin\source\repos\Laboratorio_XML\Datos\ArchivosXML\Compras.XML", "Compras_Restaurante");
            }
            else
            {
                comprador.LeerXML(@"C:\Users\admin\source\repos\Laboratorio_XML\Datos\ArchivosXML\Compras.XML");
            }
        }

        public void CrearXMLProducto()
        {
            if (!File.Exists(@"C:\Users\admin\source\repos\Laboratorio_XML\Datos\ArchivosXML\Productos.XML"))
            {
                producto.CrearXML(@"C:\Users\admin\source\repos\Laboratorio_XML\Datos\ArchivosXML\Productos.XML", "Productos");
            }
            else
            {
                producto.LeerXML(@"C:\Users\admin\source\repos\Laboratorio_XML\Datos\ArchivosXML\Productos.XML");
            }
        }

        public void mostrarDatos()
        {
            txtNombre.Text = nombre_comprador;
            txtCedula.Text = Convert.ToString(cedula);
            txtCalorias_Totales.Text = calorias_totales.ToString();
            txtMonto_total.Text = precio_totol.ToString();     
        }

        public void infoComprador(ObjUsuarios usu)
        {
            nombre_comprador = usu.nombre;
            genero = usu.genero;
            cedula = usu.cedula;
        }


        public void llenarCombobox()
        {
            List<ObjProductos> lista_productos = producto.llenarComboBox();
            cbProductos.ValueMember = "id";
            cbProductos.DisplayMember = "nombre";
            cbProductos.DataSource = lista_productos;
        }

        private void FrmCompras_Load(object sender, EventArgs e)
        { 
           
            this.llenarCombobox();
            this.mostrarDatos();
            this.generarID();
        }

        public void capturarDatos()
        {
            objeto = new ObjComprador
            {
                id_compra = id_compra,
                nombre_comprador = this.txtNombre.Text,
                cedula_comprador = Convert.ToInt32(this.txtCedula.Text),
                genero_comprador = genero,
                id_productos = Convert.ToString(this.id_producto),
                cantidad_productos = this.cantida_total,
                calorias_totales = this.calorias_totales,
                monto_total = this.precio_totol
            };
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (this.validarCantidad())
            {
                this.cantida_total = cantida_total + cantidad;
                producto.restarproductos(id_producto, cantidad);
                this.txtCantidad.Text = cantida_total.ToString();
                //this.validarPrecio_Calorias();
                this.llenarTabla();
                MessageBox.Show("Compra realizada con éxito \nCalorias totales " + calorias_totales + "\nMonto a pagar ¢" + precio_totol + "", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.mostrarDatos();
                this.capturarDatos();
                comprador.RegistrarCompra(objeto);
                this.limpiar();
                this.mostrarDatos();
               
            }
         
        }


        public bool validarCantidad()
        {
            this.obtenerID();
            cantidad = Convert.ToInt32(txtCantidad.Text);
            bool valido = producto.consultarCantidad(id_producto,cantidad);
            if (valido)
            {
                valido = true;
            }
            else
            {

                MessageBox.Show("Error la cantidad que ingresó", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return valido;
        }

        

        public void validarPrecio_Calorias()
        {
            cantidad = Convert.ToInt32(this.txtCantidad.Text);
            this.obtenerID();
            List<ObjProductos> lista_produ = producto.llenarLista();
            int calorias_produ = 0;
            int precio_produ = 0;
            string nombre_producto = "";

            for (int x = 0; x < lista_produ.Count; x++)
            {
                if (lista_produ[x].id.Equals(id_producto))
                {
                    nombre_producto = lista_produ[x].nombre;
                    calorias_produ = lista_produ[x].calorias;
                    precio_produ = lista_produ[x].precio;
                }
            }
            int calorias = calorias_produ * cantidad;
            int precio = precio_produ * cantidad;

            dtg = new ObjDTG
            {
                id = id_producto,
                nombre = nombre_producto,
                cantidad = cantidad,
                calorias = calorias,
                precio = precio
            };
            lista_data.Add(dtg);


            precio_totol = precio_totol + precio;
            calorias_totales = calorias_totales + calorias;
      }

        public void llenarTabla()
        {
            DataTable comprado = new DataTable("Productos");
            DataColumn columna0 = new DataColumn("Id");
            DataColumn columna1 = new DataColumn("Nombre");
            DataColumn columna4 = new DataColumn("Calorias");
            DataColumn columna5 = new DataColumn("Cantidad");
            DataColumn columna2 = new DataColumn("Precio");

            comprado.Columns.Add(columna0);
            comprado.Columns.Add(columna1);
            comprado.Columns.Add(columna4);
            comprado.Columns.Add(columna5);
            comprado.Columns.Add(columna2);


            for (int x = 0; x < lista_data.Count; x++)
            {
                comprado.Rows.Add(lista_data[x].id,
                    lista_data[x].nombre,
                    lista_data[x].calorias,
                    lista_data[x].cantidad,
                    lista_data[x].precio);
            }
            dtgAdquirir.DataSource = comprado;
        }


        public void obtenerID()
        {
           
            id_producto = Convert.ToInt32(cbProductos.SelectedValue.ToString());
        }

        public void limpiar()
        {
            this.txtCantidad.Text = "";
            this.cantida_total = 0;
            this.calorias_totales = 0;
            this.cantidad  = 0;
            this.precio_totol = 0;
            this.lista_data.Clear();
            this.dtgAdquirir.DataSource = "";
        }

        public void generarID()
        {
            List<int> id = comprador.consultarID();

            int ultimo = id.Last();
            ultimo = ultimo + 1;

            id_compra = ultimo;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            FrmLogin login = new FrmLogin();
            login.Show();
                
        }

        private void txtCantidad_Validating(object sender, CancelEventArgs e)
        {
            this.validarPrecio_Calorias();
            this.mostrarDatos();
        }
    }
}
