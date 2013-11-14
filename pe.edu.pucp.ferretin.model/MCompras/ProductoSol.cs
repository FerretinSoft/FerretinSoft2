using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pe.edu.pucp.ferretin.model
{
    public partial class ProductoSol
    {
        public ProductoAlmacen producto { get; set; }
        public decimal cantidad {get; set;}
        public IEnumerable<Proveedor> posiProveedor { get; set; }
        public Proveedor selectedProveedor { get; set; }
    }
}
