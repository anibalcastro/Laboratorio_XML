using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Objeto;
using Negocio;
using System.Xml;
using System.IO;

namespace Presentacion
{
    public partial class FrmRegistro : Form
    {
        Negocio.nLogin nLogin;
        XmlDocument document;
        

        public FrmRegistro()
        {
            InitializeComponent();
            nLogin = new nLogin();
            this.CrearXML();
            
        }

        public void CrearXML()
        {
            if (!File.Exists(@"C:\Users\admin\source\repos\Laboratorio_XML\Datos\ArchivosXML\Login.XML"))
            {
                nLogin.CrearXML(@"C:\Users\admin\source\repos\Laboratorio_XML\Datos\ArchivosXML\Login.XMLL", "Login");
            }
            else
            {
                nLogin.LeerXML(@"C:\Users\admin\source\repos\Laboratorio_XML\Datos\ArchivosXML\Login.XML");
            }
        }


        public void limpiarCampos()
        {
            this.txtCedula.Clear();
            this.txtNombre.Clear();
            this.txtContraseña.Clear();
            this.txtUsuario.Clear();
        }

      
        
        private void button2_Click(object sender, EventArgs e)
        {
            
            ObjUsuarios usuarios = new ObjUsuarios()
            {
                cedula = Convert.ToInt32(this.txtCedula.Text),
                nombre = this.txtNombre.Text,
                genero = cbGenero.SelectedItem.ToString(),
                usuario = this.txtUsuario.Text,
                contrasenna = this.txtContraseña.Text,
                rol = 1
            };
            nLogin.RegistrarUsuario(usuarios);
            this.limpiarCampos();
            MessageBox.Show("Usuario agregado", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Information);
            

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            FrmLogin login = new FrmLogin();
            login.Show();
        }
    }
}
