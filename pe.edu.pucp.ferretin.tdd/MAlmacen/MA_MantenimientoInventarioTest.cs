using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pe.edu.pucp.ferretin.model;
using pe.edu.pucp.ferretin.viewmodel.MAlmacen;
using pe.edu.pucp.ferretin.controller;
using pe.edu.pucp.ferretin.controller.MAlmacen;
using pe.edu.pucp.ferretin.controller.MSeguridad;
using NUnit.Framework;

namespace pe.edu.pucp.ferretin.tdd.MAlmacen
{
    class MA_MantenimientoInventarioTest
    {
        /**************Cantidade minima de productos en inventario en tienda************************/
        [TestCase]
        public void cantidadProductos_Inventario_tienda() {
            Tienda ti = MS_TiendaService.db.Tienda.Single(t=> t.nombre.ToLower()=="tienda lince 1");
            IEnumerable<ProductoAlmacen> pro = MA_ProductoAlmacenService.db.ProductoAlmacen.Where(p => (p.id_almacen == ti.id));

            int cantidadProductos = pro.Count();

            //Assert - verificar condicion o criterio de aceptacion

            Assert.GreaterOrEqual(cantidadProductos, 5);
        }


    }
}
