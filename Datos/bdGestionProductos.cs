﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Datos
{
   public class bdGestionProductos
    {
        public void guardarDatosXML(XmlDocument doc, string ruta)
        {
            doc.Save(ruta);
        }
    }
}
