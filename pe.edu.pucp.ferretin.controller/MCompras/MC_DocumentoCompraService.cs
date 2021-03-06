﻿using pe.edu.pucp.ferretin.model;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;

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
                //db.Refresh(RefreshMode.OverwriteCurrentValues, _listaDocumentosCompra);
                return _listaDocumentosCompra;
            }
            set
            {
                _listaDocumentosCompra = value;
            }
        }

        private static IEnumerable<DocumentoCompraProducto> _listaProductosDC;
        public static IEnumerable<DocumentoCompraProducto> listaProductosDC
        {
            get
            {
                if (_listaProductosDC == null)
                {
                    _listaProductosDC = db.DocumentoCompraProducto;
                }
                //Usando concurrencia pesimista:
                ///La lista de documentos de compra se actualizara para ver los cambios
                ///Si quisiera usar concurrencia optimista quito la siguiente linea
                //db.Refresh(RefreshMode.OverwriteCurrentValues, _listaProductosDC);
                return _listaProductosDC;
            }
            set
            {
                _listaProductosDC = value;
            }
        }

        private static IEnumerable<DocumentoCompraEstado> _listaEstadosDC;
        public static IEnumerable<DocumentoCompraEstado> listaEstadosDC
        {
            get
            {
                if (_listaEstadosDC == null)
                {
                    _listaEstadosDC = db.DocumentoCompraEstado;
                }
                //Usando concurrencia pesimista:
                ///La lista de documentos de compra se actualizara para ver los cambios
                ///Si quisiera usar concurrencia optimista quito la siguiente linea
                //db.Refresh(RefreshMode.OverwriteCurrentValues, _listaEstadosDC);
                return _listaEstadosDC;
            }
            set
            {
                _listaEstadosDC = value;
            }
        }

        ///<summary>
        ///Metodo que busca documentos de compra de acuerdo a los criterios seleccionados en los filtros
        ///</summary>
        public static IEnumerable<DocumentoCompra> buscarDocumentosCompra(string codigo, string proveedor, int tipoDocumento, DateTime? fechaDesde, DateTime? fechaHasta, int estado, int tienda)
        {

            if (tipoDocumento == 0)
            {
                if (estado != 0)
                {
                    return from d in listaDocumentosCompra
                           where
                           (d.codigo != null && d.codigo.ToLower().Trim().Contains(codigo.ToLower().Trim())
                           && d.Proveedor.razonSoc.ToLower().Trim().Contains(proveedor.ToLower().Trim())
                           && d.id_estado == estado
                           && (fechaDesde == null || (d.fechaEmision != null && d.fechaEmision >= fechaDesde))
                           && (fechaHasta == null || (d.fechaEmision != null && d.fechaEmision <= fechaHasta))
                           && (d.id_tienda != null && d.Tienda.id == tienda))
                           orderby d.codigo
                           select d;
                }
                else
                {
                    return from d in listaDocumentosCompra
                           where
                           (d.codigo != null && d.codigo.ToLower().Trim().Contains(codigo.ToLower().Trim())
                           && d.Proveedor.razonSoc.ToLower().Trim().Contains(proveedor.ToLower().Trim())
                           && (fechaDesde == null || (d.fechaEmision != null && d.fechaEmision >= fechaDesde))
                           && (fechaHasta == null || (d.fechaEmision != null && d.fechaEmision <= fechaHasta))
                           && (d.id_tienda != null && d.Tienda.id == tienda))
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
                           (d.codigo != null && d.codigo.ToLower().Trim().Contains(codigo.ToLower().Trim())
                           && d.Proveedor.razonSoc.ToLower().Trim().Contains(proveedor.ToLower().Trim())
                           && d.tipo == tipoDocumento
                           && d.id_estado == estado
                           && (fechaDesde == null || (d.fechaEmision != null && d.fechaEmision >= fechaDesde))
                           && (fechaHasta == null || (d.fechaEmision != null && d.fechaEmision <= fechaHasta))
                           && (d.id_tienda != null && d.Tienda.id == tienda))
                           orderby d.codigo
                           select d;
                }
                else
                {
                    return from d in listaDocumentosCompra
                           where
                           (d.codigo != null && d.codigo.ToLower().Trim().Contains(codigo.ToLower().Trim())
                           && d.Proveedor.razonSoc.ToLower().Trim().Contains(proveedor.ToLower().Trim())
                           && d.tipo == tipoDocumento
                           && (fechaDesde == null || (d.fechaEmision != null && d.fechaEmision >= fechaDesde))
                           && (fechaHasta == null || (d.fechaEmision != null && d.fechaEmision <= fechaHasta))
                           && (d.id_tienda != null && d.Tienda.id == tienda))
                           orderby d.codigo
                           select d;
                }
            }
            
        }

        //public static IEnumerable<DocumentoCompraProducto> buscarProductosDC(DocumentoCompra doc)
        //{           
        //    return from g in listaProductosDC
        //           where (
        //               //Cada fila es un filtro
        //                  (g.id_documento_compra == doc.id))
        //           orderby g.id
        //           select g;
        //}

        //public static IEnumerable<DocumentoCompra> buscarTodosDocumentosCompra()
        //{
        //    return from d in listaDocumentosCompra
        //           where
        //           (d.codigo != null)
        //           orderby d.codigo
        //           select d;
        //}

        //obtenerEstado
        //public static DocumentoCompraEstado obtenerEstado(int id)
        //{

        //    IEnumerable<DocumentoCompraEstado> estados = (from e in listaEstadosDC
        //                                               where e.id == id
        //                                               select e);
        //    if (estados.Count() > 0)
        //        return estados.First();
        //    else
        //        return null;          
        //}

        public static IEnumerable<DocumentoCompraEstado> obtenerEstadosPorTipoDC(int tipoDocumento)
        {
            if (tipoDocumento == 1) // Es Cotizacion
            {
                return (from dce in db.DocumentoCompraEstado
                        where dce.tipo.Equals(0)
                        orderby dce.id
                        select dce);
            }
            else
            {
                return (from dce in db.DocumentoCompraEstado
                        where dce.tipo.Equals(1)
                        orderby dce.id
                        select dce);
            }           
        }

        public static DocumentoCompra obtenerDCByCodigo(string codigo)
        {
            IEnumerable<DocumentoCompra> documentos = (from e in listaDocumentosCompra
                                                       where e.codigo != null && e.codigo.Equals(codigo)
                                                        select e);
            if (documentos.Count() > 0)
                return documentos.First();
            else
                return null;
        }

        public static string generarCodigoDC_V2(byte? tipoDC)
        {
            IEnumerable<DocumentoCompra> documentos = listaDocumentosCompra.Where(d => d.tipo == tipoDC).OrderByDescending(d => d.id);
            string ultimoCod = documentos.Count() <= 0 ? "" : documentos.First().codigo;

            if (ultimoCod.Length == 0)
            {
                if (tipoDC == 1)
                    return "COT1";
                else
                    return "ORD1";
            }
            else
            {
                int id = Int32.Parse(ultimoCod.Substring(3)) + 1;
                if (tipoDC == 1)
                    return "COT" + id.ToString();
                else
                    return "ORD" + id.ToString();
            }

        }

        public static string generarCodigoDC(byte? tipoDC)
        {
            IEnumerable<DocumentoCompra> documentos = (from e in listaDocumentosCompra
                                                       where e.codigo != null && e.tipo.Equals(tipoDC)
                                                       select e);
            if (tipoDC == 1)
                return "COT" + (documentos.Count()+1).ToString();
            else
                return "ORD" + (documentos.Count()+1).ToString();
        }

        public static int devuelvecantidadDC(byte? tipoDC)
        {
            IEnumerable<DocumentoCompra> documentos = (from e in listaDocumentosCompra
                                                       where e.codigo != null && e.tipo.Equals(tipoDC)
                                                       select e);
            return documentos.Count()+1;

        }

        public static int devuelvecantidadDC_V2(byte? tipoDC)
        {
            IEnumerable<DocumentoCompra> documentos = listaDocumentosCompra.Where(d => d.tipo == tipoDC).OrderByDescending(d => d.id);
            string ultimoCod = documentos.Count() <= 0 ? "" : documentos.First().codigo;

            if (ultimoCod.Length == 0)
            {
                return 1;
            }
            else
            {
                int id = Int32.Parse(ultimoCod.Substring(3)) + 1;
                return id;
            }   
        }

        public static bool insertarDocumentoCompra(DocumentoCompra documentoCompra)
        {
            DocumentoCompra doc;
            try
            {
                try
                {
                    doc = listaDocumentosCompra.Single(t => t.id == documentoCompra.id);
                    return false;
                }
                catch (Exception e)
                {
                    db.DocumentoCompra.InsertOnSubmit(documentoCompra);
                    enviarCambios();
                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }
        #endregion

    }
}
