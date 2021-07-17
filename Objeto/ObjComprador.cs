using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Objeto
{
    public class ObjComprador
    {
        public int id_compra { set; get; }
        public string nombre_comprador { set; get; }
        public int cedula_comprador { set; get; }
        public string genero_comprador { set; get; }

        public string id_productos { set; get; }
        public int cantidad_productos { set; get; }
        public int calorias_totales { set; get; }
        public int monto_total { set; get; }
    }
}
