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
    public partial class FrmLogin : Form
    {
        nLogin nLogin;
        XmlDocument doc;
        FrmRegistro registro;
        ObjUsuarios usuario;

        public FrmLogin()
        {
            InitializeComponent();
            nLogin = new nLogin();
            this.CrearXML();
            

        }

        public void CrearXML()
        {
            if (!File.Exists(@"C:\Users\admin\source\repos\Laboratorio_XML\Datos\ArchivosXML\Login.XML"))
            {
                nLogin.CrearXML(@"C:\Users\admin\source\repos\Laboratorio_XML\Datos\ArchivosXML\Login.XML", "Login");
            }
            else
            {
                nLogin.LeerXML(@"C:\Users\admin\source\repos\Laboratorio_XML\Datos\ArchivosXML\Login.XML");
            }
        }
        
        private void btnRegistro_Click(object sender, EventArgs e)
        {
            this.Hide();
            registro = new FrmRegistro();
            registro.Show();
           
            
        }
        /// <summary>
        /// rol 0 = administrados
        /// rol 1 = usuario
        /// </summary>
        /// <param name="rol"></param>
        public void roles(int rol)
        {
            if (rol.Equals(0))
            {
                this.Hide();
                FrmDashboardAdmin admin = new FrmDashboardAdmin();
                admin.Show();

            }
            else if (rol.Equals(1))
            {
                this.Hide();
                FrmCompras compras = new FrmCompras();
                compras.infoComprador(nLogin.retornarInfo());
                compras.Show();

            }
        }

        private void btnIniciar_Click(object sender, EventArgs e)
        {
            string user = this.txtUsuario.Text;
            string password = this.txtContrasena.Text;

            
            bool valido = nLogin.validarUsuario(user, password);
            int rol = nLogin.retornarRol();

            if (valido)
            {
                MessageBox.Show("Bienvenido", "Bienvenida", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.roles(rol);
            }
            else
            {
                MessageBox.Show("Usuario o contraseña son incorrectas", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
