using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Text;

using System.Threading.Tasks;

using NUnit.Framework;
using pe.edu.pucp.ferretin.model;
using pe.edu.pucp.ferretin.viewmodel.MAlmacen;
using pe.edu.pucp.ferretin.controller;
using pe.edu.pucp.ferretin.controller.MAlmacen;
using pe.edu.pucp.ferretin.controller.MSeguridad;

namespace pe.edu.pucp.ferretin.tdd.MAlmacen
{    
    [TestFixture]
    public class MA_MovimientosTest
    {
        /******************* Test Nombre de Tienda *************************/
        [TestCase]
        public void stock_LamparaGrande_TiendaLince1_entre_200_y_2000()
        {
            //Arrange = Creo el entorno            
            //var ventana = new MS_AdministrarTiendasViewModel();
            
            //Act - No hay nada que hacer, porque la accion fue la creacion misma            
            Tienda tnd = MS_TiendaService.db.Tienda.Single(t => t.nombre.ToLower().Contains("tienda lince 1"));
            Producto prod = MA_ProductoService.db.Producto.Single(p => p.nombre.ToLower().Contains("lampara grande"));
            ProductoAlmacen prodAlm = MA_ProductoAlmacenService.db.ProductoAlmacen.Single(pa => (pa.id_almacen == tnd.id && pa.id_producto == prod.id));

            int stock = (int)(prodAlm.stock);            

            //Assert - Verificar la condicion o criterio de aceptacion
            Assert.GreaterOrEqual(2000, stock);
            Assert.LessOrEqual(200, stock);
        }

        /************ tipo de movimiento unico*******************************************/
        [TestCase]
        public void Nombre_De_TipoMovimiento_Unico()
        {
            //var creo el entorno

            foreach (MovimientoTipo mt in MA_MovimientoProductoService.db.MovimientoTipo)
            {
                IEnumerable<MovimientoTipo> movTipo = MA_CategoriaService.db.MovimientoTipo.Where(p => p.nombre == mt.nombre);

                //Assert - verificar condicion o criterio de aceptacion

                int cantidadRegistros = movTipo.Count();

                Assert.AreEqual(cantidadRegistros, 1);
            }
        }


        [TestCase]
        public void Movimiento_Con_Campos_Obligatorios()
        {
            //var creo el entorno

            foreach (MovimientoTipo moviTipo in MA_MovimientosService.db.MovimientoTipo)
            {
                MovimientoTipo mt = MA_MovimientosService.db.MovimientoTipo.Single(t => t.nombre.ToLower() ==moviTipo.nombre.ToLower()); ;
                IEnumerable<Movimiento> movi = MA_MovimientosService.db.Movimiento.Where(mov => mov.id_tipo == mt.id);

                int i = 0;
                //Assert - verificar condicion o criterio de aceptacion
                foreach (Movimiento m in movi)
                {
                    Assert.IsNotNull(m.id_estado);
                    Assert.IsNotNull(m.codigo);
                    Assert.IsNotNull(m.fecha);


                }
            }
        }







    }

}
