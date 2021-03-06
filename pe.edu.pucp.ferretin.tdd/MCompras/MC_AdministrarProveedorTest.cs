﻿using NUnit.Framework;
using pe.edu.pucp.ferretin.controller.MCompras;
using pe.edu.pucp.ferretin.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pe.edu.pucp.ferretin.tdd.MCompras
{
     [TestFixture]
    class MC_AdministrarProveedorTest
    {
        /******************** Test Cantidad de Proveedores *************************/
        [TestCase]
        public void cantidad_de_proveedor_igual_a_6()
        {
          

            //Act - No hay nada que hacer, porque la accion fue la creacion misma
            int cantProveedores = MC_ProveedorService.db.Proveedor.Count();

            //Assert - Verificar la condicion o criterio de aceptacion
            Assert.AreEqual(6, cantProveedores);
        }

        /******************** Test Proveedor unico con codido *************************/
        [TestCase]
        public void codigo_de_proveedor_unico()
        {
            //var creo el entorno

            foreach (Proveedor p in MC_ProveedorService.db.Proveedor)
            {
                IEnumerable<Proveedor> pro = MC_ProveedorService.db.Proveedor.Where(prop => prop.ruc == p.ruc);

                //Act - No hay nada que hacer, porque la accion fue la creacion misma
                int cantidadRegistros = pro.Count();
                //Assert - verificar condicion o criterio de aceptacion
                Assert.AreEqual(cantidadRegistros, 1);
            }
        }

        [TestCase]
        public void producto_unico_proveedor()
        {
            IEnumerable <ProveedorProducto> listaProveedores=MC_ProveedorService.db.ProveedorProducto;
            foreach (Proveedor p in MC_ProveedorService.db.Proveedor)
                {
                    foreach (Producto prod in MC_ProveedorService.db.Producto)
                    {
                        int cant=MC_ProveedorService.db.ProveedorProducto.Count(pp => pp.id_producto == prod.id && pp.id_proveedor == p.id);
                        Console.Write(cant+" "+ prod.nombre+" "+ p.razonSoc + '\n');
                        if (cant != 0)
                        Assert.AreEqual(1, cant);

                    }
                    
                    
                }

        }

        /******************** Test Proveedor unico con nombre *************************/
        [TestCase]
        public void nombre_de_proveedor_unico()
        {
            //var creo el entorno

            foreach (Proveedor p in MC_ProveedorService.db.Proveedor)
            {
                IEnumerable<Proveedor> pro = MC_ProveedorService.db.Proveedor.Where(prop => prop.razonSoc == p.razonSoc);

                //Act - No hay nada que hacer, porque la accion fue la creacion misma
                int cantidadRegistros = pro.Count();
                //Assert - verificar condicion o criterio de aceptacion
                Assert.AreEqual(cantidadRegistros, 1);
            }
        }
    }
}
