using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MiPrimerProyectoASP.Models
{
    public class CompraCliente
    {

        public Nullable<System.DateTime> fechaCompra { get; set; }

        public Nullable<int> totalCompra { get; set; }

        public string nombreCliente { get; set; }

        public string documentoCliente { get; set; }

    }
}