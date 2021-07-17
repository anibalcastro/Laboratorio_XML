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
    public partial class FrmGestionProductos : Form
    {
        Negocio.nGestionProductos gestion;
        ObjProductos objetos;
        public FrmGestionProductos()
        {
            InitializeComponent();
            gestion = new nGestionProductos();
            this.CrearXML();
            dtgProductos.AllowUserToAddRows = false;
        }

        public void llenarTabla()
        {
            List < ObjProductos > lista = gestion.llenarLista();

            DataTable productos = new DataTable("Productos");
            DataColumn columna0 = new DataColumn("id");
            DataColumn columna1 = new DataColumn("Nombre");
            DataColumn columna2 = new DataColumn("Precio");
            DataColumn columna3 = new DataColumn("Descripcion");
            DataColumn columna4 = new DataColumn("Calorias");
            DataColumn columna5 = new DataColumn("Cantidad");

            productos.Columns.Add(columna0);
            productos.Columns.Add(columna1);
            productos.Columns.Add(columna2);
            productos.Columns.Add(columna3);
            productos.Columns.Add(columna4);
            productos.Columns.Add(columna5);
            

            for (int x = 0; x < lista.Count; x++)
            {
                productos.Rows.Add(lista[x].id,
                    lista[x].nombre, 
                    lista[x].precio, 
                    lista[x].descripcion, 
                    lista[x].calorias, 
                    lista[x].cantidad);
            }
            dtgProductos.DataSource = productos;
        }

        public void CrearXML()
        {
            if (!File.Exists(@"C:\Users\admin\source\repos\Laboratorio_XML\Datos\ArchivosXML\Productos.XML"))
            {
                gestion.CrearXML(@"C:\Users\admin\source\repos\Laboratorio_XML\Datos\ArchivosXML\Productos.XML", "Productos");
            }
            else
            {
                gestion.LeerXML(@"C:\Users\admin\source\repos\Laboratorio_XML\Datos\ArchivosXML\Productos.XML");
            }
        }

        public void capturarDatos()
        {
            objetos = new ObjProductos()
            {
                id = Convert.ToInt32(txtID.Text),
                nombre = txtNombre.Text,
                precio = Convert.ToInt32(txtPrecio.Text),
                descripcion = txtDescripcion.Text,
                calorias = Convert.ToInt32(txtCalorias.Text),
                cantidad = Convert.ToInt32(txtCantidad.Text)
            };
        }

        public void limpiarCampos()
        {
            this.txtID.Enabled = true;

            this.txtID.Clear();
            this.txtNombre.Clear();
            this.txtPrecio.Clear();
            this.txtDescripcion.Clear();
            this.txtCantidad.Clear();
            this.txtCalorias.Clear();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            this.capturarDatos();
            gestion.RegistrarProducto(objetos);
            MessageBox.Show("Producto registrado", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.limpiarCampos();
            this.llenarTabla();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            this.limpiarCampos();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            this.limpiarCampos();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.capturarDatos();
            gestion.RegistrarProducto(objetos);
            MessageBox.Show("Producto registrado", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.limpiarCampos();
            this.llenarTabla();
        }

        private void FrmGestionProductos_Load(object sender, EventArgs e)
        {
            this.llenarTabla();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            this.capturarDatos();
            gestion.Modificar(objetos);
            this.limpiarCampos();
            this.llenarTabla();
        }

        private void dtgProductos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex >=0 )
            {
                txtID.Enabled = false;

                DataGridViewRow row = dtgProductos.Rows[e.RowIndex];

                txtID.Text = row.Cells["Id"].Value.ToString();
                txtNombre.Text = row.Cells["Nombre"].Value.ToString();
                txtPrecio.Text = row.Cells["Precio"].Value.ToString();
                txtDescripcion.Text = row.Cells["Descripcion"].Value.ToString();
                txtCalorias.Text = row.Cells["Calorias"].Value.ToString();
                txtCantidad.Text = row.Cells["Cantidad"].Value.ToString();
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
           DialogResult result =  MessageBox.Show("¿Desea eliminar el producto?", "INFORMACIÓN", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                this.capturarDatos();
                gestion.Eliminar(objetos);
                MessageBox.Show("Producto eliminado", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.limpiarCampos();
                this.llenarTabla();
            }
            else
            {

            }

            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.llenarTabla();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.capturarDatos();
            gestion.Modificar(objetos);
            this.limpiarCampos();
            this.llenarTabla();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("¿Desea eliminar el producto?", "INFORMACIÓN", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                this.capturarDatos();
                gestion.Eliminar(objetos);
                MessageBox.Show("Producto eliminado", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.limpiarCampos();
                this.llenarTabla();
            }
            else
            {

            }
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            this.llenarTabla();
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            this.Hide();
            FrmDashboardAdmin admin = new FrmDashboardAdmin();
            admin.Show();
        }
    }
}
