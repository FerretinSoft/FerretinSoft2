﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pe.edu.pucp.ferretin.model;

namespace pe.edu.pucp.ferretin.controller.MSeguridad
{
    public class MS_TiendaService : MS_ComunService
    {


        private static IEnumerable<Almacen> _listaTiendas = null;

        private static IEnumerable<Almacen> listaTiendas
        {
            get
            {
                if (_listaTiendas == null)
                {
                    obtenerListaTiendas();
                }
                return _listaTiendas;
            }
            set
            {
                _listaTiendas = value;
            }
        }

        public static IEnumerable<Almacen> obtenerListaTiendas()
        {
            listaTiendas = from p in db.Almacen
                            orderby p.id
                            select p;
            return listaTiendas;
        }

        public static Almacen obtenerTiendaByCodigo(String codigoTienda)
        {
            IEnumerable<Almacen> tiendas = (from t in listaTiendas
                                             where t.codigo != null && t.codigo.Contains(codigoTienda)
                                             select t);
            if (tiendas.Count() > 0)
                return tiendas.First();
            else
                return null;
        }

        public static IEnumerable<Almacen> obtenerListaTiendasBy(Almacen tienda)
        {
            return from t in listaTiendas
                   where
                          (tienda.codigo==null || (t.codigo != null && t.codigo.ToLower().Contains(tienda.codigo.ToLower().Trim()))
                       && (tienda.nombre == null || (t.nombre != null && t.nombre.ToLower().Contains(tienda.nombre.ToLower().Trim())))
                       && (tienda.tipo == null || (t.tipo != null && t.tipo.Equals(tienda.tipo)))
                       && (tienda.estado == null || ( t.estado != null && t.estado.Equals(tienda.estado)))
                       && (tienda.id_ubigeo == null || (t.id_ubigeo != null && t.id_ubigeo.Equals(tienda.id_ubigeo)))
                    )
                   orderby t.codigo
                   select t;
        }

        public static void insertarTienda(Almacen tienda)
        {
            db.Almacen.InsertOnSubmit(tienda);
            db.SubmitChanges();
        }

        public static void actualizarTienda(Almacen tienda)
        {
            db.SubmitChanges();
        }
    }
}
