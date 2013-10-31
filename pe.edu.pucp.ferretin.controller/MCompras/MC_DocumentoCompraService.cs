﻿using pe.edu.pucp.ferretin.model;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pe.edu.pucp.ferretin.controller.MCompras
{
    public class MC_DocumentoCompraService : MC_ComunService
    {

        #region Private Zone
        #endregion

        #region Public Zone

        ///<summary>
        ///Variable privada que almacena la lista de documentos de compra en memoria, para su posterior uso
        ///</summary>
        ///<remarks>
        ///Todas las operaciones se realizan en base a esta lista
        ///</remarks>
        private static IEnumerable<DocumentoCompra> _listaDocumentosCompra;
        public static IEnumerable<DocumentoCompra> listaDocumentosCompra
        {
            get
            {
                if (_listaDocumentosCompra == null)
                {
                    _listaDocumentosCompra = db.DocumentoCompra;
                }
                //Usando concurrencia pesimista:
                ///La lista de documentos de compra se actualizara para ver los cambios
                ///Si quisiera usar concurrencia optimista quito la siguiente linea
                db.Refresh(RefreshMode.OverwriteCurrentValues, _listaDocumentosCompra);
                return _listaDocumentosCompra;
            }
            set
            {
                _listaDocumentosCompra = value;
            }
        }

        ///<summary>
        ///Metodo que busca documentos de compra de acuerdo a los criterios seleccionados en los filtros
        ///</summary>
        public static IEnumerable<DocumentoCompra> buscarDocumentosCompra(string codigo, string proveedor, int tipoDocumento, DateTime? fechaDesde, DateTime? fechaHasta, int estado)
        {

            if (tipoDocumento == 0)
            {
                if (estado != 0)
                {
                    return from d in listaDocumentosCompra
                           where
                           (d.codigo != null && d.codigo.Contains(codigo)
                           && d.Proveedor.razonSoc.Contains(proveedor)
                           && d.id_estado == estado
                           && (fechaDesde == null || (d.fechaEmision != null && d.fechaEmision >= fechaDesde))
                           && (fechaHasta == null || (d.fechaEmision != null && d.fechaEmision <= fechaHasta))
                           )
                           orderby d.codigo
                           select d;
                }
                else
                {
                    return from d in listaDocumentosCompra
                           where
                           (d.codigo != null && d.codigo.Contains(codigo)
                           && d.Proveedor.razonSoc.Contains(proveedor)
                           && (fechaDesde == null || (d.fechaEmision != null && d.fechaEmision >= fechaDesde))
                           && (fechaHasta == null || (d.fechaEmision != null && d.fechaEmision <= fechaHasta))
                            )
                           orderby d.codigo
                           select d;
                }
            }
            else
            {
                if (estado != 0)
                {
                    return from d in listaDocumentosCompra
                           where
                           (d.codigo != null && d.codigo.Contains(codigo)
                           && d.Proveedor.razonSoc.Contains(proveedor)
                           && d.tipo == tipoDocumento
                           && d.id_estado == estado
                           && (fechaDesde == null || (d.fechaEmision != null && d.fechaEmision >= fechaDesde))
                           && (fechaHasta == null || (d.fechaEmision != null && d.fechaEmision <= fechaHasta))
                            )
                           orderby d.codigo
                           select d;
                }
                else
                {
                    return from d in listaDocumentosCompra
                           where
                           (d.codigo != null && d.codigo.Contains(codigo)
                           && d.Proveedor.razonSoc.Contains(proveedor)
                           && d.tipo == tipoDocumento
                           && (fechaDesde == null || (d.fechaEmision != null && d.fechaEmision >= fechaDesde))
                           && (fechaHasta == null || (d.fechaEmision != null && d.fechaEmision <= fechaHasta))
                            )
                           orderby d.codigo
                           select d;
                }
            }
            
        }

        public static IEnumerable<DocumentoCompra> buscarTodosDocumentosCompra()
        {
            return from d in listaDocumentosCompra
                   where
                   (d.codigo != null)
                   orderby d.codigo
                   select d;
        }

        public static IEnumerable<DocumentoCompraEstado> obtenerEstadosPorTipoDC(int tipoDocumento)
        {
            if (tipoDocumento == 1) // Es Cotizacion
            {
                return (from dce in db.DocumentoCompraEstado
                        where dce.tipo.Equals(0)
                        orderby dce.nombre
                        select dce);
            }
            else
            {
                return (from dce in db.DocumentoCompraEstado
                        where dce.tipo.Equals(1)
                        orderby dce.nombre
                        select dce);
            }
            
        }

        #endregion

    }
}
