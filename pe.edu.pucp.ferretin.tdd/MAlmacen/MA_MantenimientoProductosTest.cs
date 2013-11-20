using NUnit.Framework;
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

namespace pe.edu.pucp.ferretin.tdd.MAlmacen
{
    class MA_MantenimientoProductosTest
    {

        /***********Test nombre unico de productos en una tienda************************/

        [TestCase]
        public void Nombre_De_Producto_Unico() 
        { 
            //var creo el entorno

            
            IEnumerable<Producto> pro= MA_ProductoService.db.Producto.Where(p=> p.nombre=="Sanitario Celima");

            //Assert - verificar condicion o criterio de aceptacion

            int cantidadRegistros=pro.Count();

            Assert.AreEqual(cantidadRegistros, 1);
            
        }



        [TestCase]
        public void stockActual_Superior_StockMinimo()
        { //verificamoe el Stock Actual sea igual o mayor de un producto de un almacen
            //var  creo el entorno

            /*cumple producto1, producto2, producto3*/
            Producto prod = MA_ProductoService.db.Producto.Single(p => p.nombre.ToLower().Contains("sanitario celima"));
            Tienda tnd= MS_TiendaService.db.Tienda.Single(t => t.nombre.ToLower().Contains("tienda lince 1"));
            ProductoAlmacen prodAlm = MA_ProductoAlmacenService.db.ProductoAlmacen.Single(pa => (pa.id_almacen == tnd.id && pa.id_producto == prod.id));

            int stockActual=(int)prodAlm.stock;
            int stockMinimo = (int)prodAlm.stockMin;
            int descuento= (int)prodAlm.descuento;
                int puntos=(int) prodAlm.puntos;
            
            //Assert - verificar condicion o criterio de aceptacion
            Assert.GreaterOrEqual(stockActual, stockMinimo);
            Assert.GreaterOrEqual(descuento, 0);
            Assert.GreaterOrEqual(puntos, 0);
           
        }
    }
}
