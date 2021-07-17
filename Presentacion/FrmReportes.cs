using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Negocio;
using Objeto;

namespace Presentacion
{
    public partial class FrmReportes : Form
    {
        Negocio.nReportes reportes;
        public FrmReportes()
        {
            InitializeComponent();
            reportes = new nReportes();
            this.crearXML();
        }

        public void crearXML()
        {
            reportes.LeerXMLCompras(@"C:\Users\admin\source\repos\Laboratorio_XML\Datos\ArchivosXML\Compras.XML");
            reportes.LeerXMLProducto(@"C:\Users\admin\source\repos\Laboratorio_XML\Datos\ArchivosXML\Productos.XML");
        }

        public void llenartablaRep1()
        {
            List<ObjRep1> lista = reportes.buscarCantidadComprada();

            DataTable tabla = new DataTable("Reporte 1");

            DataColumn columna0 = new DataColumn("Id Producto");
            DataColumn columna1 = new DataColumn("Nombre Producto");
            DataColumn columna2 = new DataColumn("Calorias");
            DataColumn columna3 = new DataColumn("Cantidad comprado");

            tabla.Columns.Add(columna0);
            tabla.Columns.Add(columna1);
            tabla.Columns.Add(columna2);
            tabla.Columns.Add(columna3);

            for (int x = 0; x < lista.Count; x++)
            {
                tabla.Rows.Add(lista[x].id,
                    lista[x].nombre_producto, 
                    lista[x].calorias, 
                    lista[x].cantidad_total_comprado);
            }

            dtgReportes.DataSource = tabla;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            this.llenartablaRep1();
        }

        private void FrmReportes_Load(object sender, EventArgs e)
        {
            radioButton1.Checked = true;
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            this.Hide();
            FrmDashboardAdmin admin = new FrmDashboardAdmin();
            admin.Show();
        }
    }
}
