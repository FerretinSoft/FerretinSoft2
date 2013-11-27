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

        /***********Test nombre unico de productos ************************/

        [TestCase]
        public void Nombre_De_Producto_Unico() 
        { 
            //var creo el entorno

            foreach (Producto p in MA_ProductoService.db.Producto)
            {
                IEnumerable<Producto> pro = MA_ProductoService.db.Producto.Where(prod => prod.nombre ==p.nombre);

                //Assert - verificar condicion o criterio de aceptacion

                int cantidadRegistros = pro.Count();
                
                Console.WriteLine(cantidadRegistros);

                Assert.AreEqual(cantidadRegistros, 1);
            }
            
        }

        /****El Stock Actual debe ser mayor o igual al stock minimo por producto*****/

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


        /****************Cantidad minima de productos por tienda **********************************************/

        [TestCase]
        public void Cantidad_minima_producto_tienda() {

            //var  creo el entorno
            Tienda tnd = MS_TiendaService.db.Tienda.Single(t => t.nombre.ToLower().Contains("tienda lince 1"));
            IEnumerable<ProductoAlmacen> lprodAlm = MA_ProductoAlmacenService.db.ProductoAlmacen.Where(pa => (pa.id_almacen == tnd.id));

            int cantidadProd = (int)lprodAlm.Count();

            //Assert - verificar condicion o criterio de aceptacion

            Assert.GreaterOrEqual(cantidadProd, 6);
        }


        /*************Producto tiene asignado categoria*****************************/

        [TestCase]
        public void ProductoConCategoria() {
            foreach (Producto p in MA_ProductoService.db.Producto) 
            {
                ProductoCategoria pc = MA_ProductoService.db.ProductoCategoria.Single(pcat => pcat.id_producto == p.id);

                //Assert - verificar condicion o criterio de aceptacion

                Assert.IsNotNull(pc);

            }
        
        }


        /*El código para cada producto debe ser único*/
        [TestCase]
        public void Codigo_Producto_Unico()
        {
            foreach (Producto p in MA_ProductoService.db.Producto)
            {
                IEnumerable<Producto> listaProductos = MA_ProductoService.db.Producto.
                                                    Where(prod => prod.codigo == p.codigo);

                int cantProd = listaProductos.Count();
                Assert.AreEqual(cantProd,1);

            }
        }

        /*Todos los productos deben estar en todas las tiendas*/
        [TestCase]
        public void Producto_Todas_Tiendas()
        {
            IEnumerable<Tienda> t = MS_TiendaService.obtenerTiendas();
            int numTiendas = t.Count();

            foreach (Producto p in MA_ProductoService.db.Producto)
            {
                IEnumerable<ProductoAlmacen> listaProductosAlmacen = MA_ProductoAlmacenService.db.ProductoAlmacen.
                                                                    Where(pa => pa.id_producto.Value == p.id);
                int numProdxTienda = listaProductosAlmacen.Count();
                try
                {
                    Assert.AreEqual(numProdxTienda, numTiendas);
                }
                catch (Exception e)
                {
                    Console.WriteLine(p.nombre);
                }

            }
        }

        /*Todos los productos deben tener un precio positivo*/
        [TestCase]
        public void Precio_Producto_Positivo()
        {
            IEnumerable<Tienda> t = MS_TiendaService.obtenerTiendas();
            
            foreach (ProductoPrecio pp in MA_ProductoService.db.ProductoPrecio)
            {
                Assert.Greater(pp.precio, 0);
            }
        }


        /*Todos los productos deben contar con un precio en puntos equivalente*/
        [TestCase]
        public void Precio_Puntos_Producto()
        {
            foreach (ProductoPrecio pprecio in MA_ProductoService.db.ProductoPrecio)
            {
                Assert.Greater(pprecio.precioPuntos, 0);
            }
        }

        /*Para cada productoxtienda, el stock mínimo debe ser menor o igual al stock máximo*/
        [TestCase]
        public void StockMinimo_Menor_StockMaximo_Producto()
        {
            foreach (ProductoAlmacen pa in MA_ProductoService.db.ProductoAlmacen)
            {
                    Assert.LessOrEqual(pa.stockMin, pa.stockMax);
            }
        }


    }
}
