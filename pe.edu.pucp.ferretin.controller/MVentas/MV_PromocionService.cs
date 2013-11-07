using pe.edu.pucp.ferretin.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pe.edu.pucp.ferretin.controller.MVentas
{
    public class MV_PromocionService : MV_ComunService
    {
        public static IEnumerable<Promocion> buscarPromociones(string codPromSearch, DateTime fechaDesdeSearch, DateTime fechaHastaSearch, int estadoSearch)
        {
            return from p in db.Promocion where
                (p.codigo.ToUpper().Contains(codPromSearch.ToUpper()))
                && (DateTime.Compare(p.fechaDesde.Value,fechaDesdeSearch)>=0 || DateTime.Compare(p.fechaHasta.Value,fechaHastaSearch)<=0 )
                && (estadoSearch <= 0 || p.estado == estadoSearch) select p;
        }

        /// <summary>
        /// Guarda un nuevo Promocion en la Base de Datos
        /// </summary>
        /// <param name="promocion">La Promocion a guardar</param>
        public static bool insertarPromocion(Promocion promocion)
        {
            if (!db.Promocion.Contains(promocion))
            {
                db.Promocion.InsertOnSubmit(promocion);
                return enviarCambios();
            }
            else
            {
                return false;
            }
        }

        public static PromocionProducto ultimaPromocionPorProducto(Producto producto)
        {
            try
            {
                //Todas las promociones con stock del producto
                foreach (var ultimoProdProm in db.PromocionProducto.Where(pp => pp.Producto.id == producto.id && pp.stockActual > 0))
                {
                    if (ultimoProdProm != null && ultimoProdProm.Promocion.fechaDesde <= DateTime.Today && ultimoProdProm.Promocion.fechaHasta >= DateTime.Today && ultimoProdProm.Promocion.estado == 1)
                    {
                        return ultimoProdProm;
                    }
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

    }
}
