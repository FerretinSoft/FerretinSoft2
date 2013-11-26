using pe.edu.pucp.ferretin.model;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using pe.edu.pucp.ferretin.controller.MAlmacen;
using System.Windows;

namespace pe.edu.pucp.ferretin.controller.MCompras
{

    public class MC_GuiaRemisionService : MC_ComunService
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
        private static IEnumerable<GuiaRemision> _listaGuiasRemision;
        public static IEnumerable<GuiaRemision> listaGuiasRemision
        {
            get
            {
                if (_listaGuiasRemision == null)
                    _listaGuiasRemision = db.GuiaRemision;
                //Usando concurrencia pesimista:
                ///La lista de documentos de compra se actualizara para ver los cambios
                ///Si quisiera usar concurrencia optimista quito la siguiente linea
                //db.Refresh(RefreshMode.OverwriteCurrentValues, _listaGuiasRemision);
                return _listaGuiasRemision;
            }
            set
            {
                _listaGuiasRemision = value;
            }
        }

        private static IEnumerable<GuiaRemisionProducto> _listaGuiaRemisionProducto;
        public static IEnumerable<GuiaRemisionProducto> listaGuiaRemisionProducto
        {
            get
            {
                if (_listaGuiaRemisionProducto == null)
                    _listaGuiaRemisionProducto = db.GuiaRemisionProducto;
                //Usando concurrencia pesimista:
                ///La lista de documentos de compra se actualizara para ver los cambios
                ///Si quisiera usar concurrencia optimista quito la siguiente linea
                //db.Refresh(RefreshMode.OverwriteCurrentValues, _listaGuiasRemision);
                return _listaGuiaRemisionProducto;
            }
            set
            {
                _listaGuiaRemisionProducto = value;
            }
        }


        public static IEnumerable<GuiaRemision> buscarGuiasRemision(string codigo, string proveedor, DateTime? fechaDesde, DateTime? fechaHasta)
        {
            return from g in db.GuiaRemision
                   where (
                       //Cada fila es un filtro
                          (g.codigo != null && g.codigo.ToLower().Contains(codigo.ToLower().Trim()))
                          && (g.DocumentoCompra.Proveedor.razonSoc.ToLower().Trim().Contains(proveedor.ToLower().Trim()))
                          && (fechaDesde == null || (g.fechaRecepcion != null && g.fechaRecepcion >= fechaDesde))
                          && (fechaHasta == null || (g.fechaRecepcion != null && g.fechaRecepcion <= fechaHasta))
                    )
                   orderby g.codigo
                   select g;
        }

        //public static IEnumerable<GuiaRemisionProducto> buscarProductosGuiaRemision(GuiaRemision guia)
        //{
        //    return from g in db.GuiaRemisionProducto
        //           where (
        //               //Cada fila es un filtro
        //                  (g.GuiaRemision == guia))
        //           orderby g.id
        //           select g;
        //}

        private static bool verificaSiExisteGR(GuiaRemision guia)
        {
            int gAux = (from g in db.GuiaRemision
                        where ((g.codigo == guia.codigo) && (g.DocumentoCompra.codigo == guia.DocumentoCompra.codigo))
                        select g).Count();

            if (gAux > 0)
                return true;
            else
                return false;
        }

        public static bool insertarGuiaRemision(GuiaRemision guiaRemision)
        {
            //GuiaRemision guia = null;
            bool flagF = false;
            int i, cont;
            try
            {
                //flag = verificaSiExisteGR(guiaRemision);
                //db.GuiaRemision.Single(t => t.codigo == guiaRemision.codigo);
                if (verificaSiExisteGR(guiaRemision))
                    return false;
                else
                {
                    cont = guiaRemision.GuiaRemisionProducto.Count();
                    guiaRemision.estado = 1;
                    for (i = 0; i < cont; i++)
                        guiaRemision.DocumentoCompra.DocumentoCompraProducto[i].cantidadRestante = guiaRemision.DocumentoCompra.DocumentoCompraProducto[i].cantidadRestante - guiaRemision.GuiaRemisionProducto[i].cantidadRecibida;
                    db.GuiaRemision.InsertOnSubmit(guiaRemision);
                    MA_SharedService.registrarCompra(guiaRemision.Tienda, guiaRemision.GuiaRemisionProducto);
                    enviarCambios();
                    flagF = true;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            return flagF;
        }

        #endregion
    }
}
