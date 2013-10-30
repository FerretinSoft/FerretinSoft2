using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pe.edu.pucp.ferretin.model.MAlmacen
{
    class ProductoTienda
    {
        Producto prod { get; set; }
        ProductoAlmacen prodAlm { get; set; }
        ProductoCategoria prodCat { get; set; }
        Tienda tiend { get; set; }
        Categoria cat { get; set; }
    }
}
