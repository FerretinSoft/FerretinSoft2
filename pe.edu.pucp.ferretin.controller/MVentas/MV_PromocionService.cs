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
        public static FerretinDataContext dbPromocion
        {
            get
            {
                return db;
            }
        }

        public static IEnumerable<Promocion> buscarPromociones(string codPromSearch, DateTime fechaDesdeSearch, DateTime fechaHastaSearch, int estadoSearch)
        {
            return from p in dbPromocion.Promocion where
                (p.codigo.ToUpper().Contains(codPromSearch.ToUpper()))
                && (p.fechaDesde == null || p.fechaHasta == null || DateTime.Compare(p.fechaDesde.Value, fechaDesdeSearch) >= 0 || DateTime.Compare(p.fechaHasta.Value, fechaHastaSearch) <= 0)
                && (estadoSearch <= 0 || (p.fechaDesde <= DateTime.Today && p.fechaHasta >= DateTime.Today) == (estadoSearch==1)) select p;
        }

        /// <summary>
        /// Guarda un nuevo Promocion en la Base de Datos
        /// </summary>
        /// <param name="promocion">La Promocion a guardar</param>
        public static bool insertarPromocion(Promocion promocion)
        {
            if (!dbPromocion.Promocion.Contains(promocion))
            {
                foreach (var pp in promocion.PromocionProducto)
                    pp.stockActual = pp.stockTotal;
                promocion.estado = 1;
                promocion.codigo = newCodPromocion;
                dbPromocion.Promocion.InsertOnSubmit(promocion);
                return enviarCambios(dbPromocion);
            }
            else
            {
                return false;
            }
        }

        public static PromocionProducto ultimaPromocionPorProducto(Producto producto, Tienda tienda)
        {
            try
            {
                //Todas las promociones con stock del producto
                var promociones = dbPromocion.Promocion.Where(p => (p.PromocionTienda.Count(pt => pt.Tienda.id.Equals(tienda.id)) > 0) && p.estado == 1 && p.fechaDesde <= DateTime.Today && p.fechaHasta >= DateTime.Today);
                foreach (var promocion in promociones)
                {
                    foreach(var promocionProducto in promocion.PromocionProducto.Where(pp=>pp.Producto.id.Equals(producto.id) && pp.stockActual>0 ) )
                    {
                        return promocionProducto;
                    }
                }
                return null;
            }
            catch
            {
                return null;
            }
        }


        public static string newCodPromocion
        {
            get
            {
                var max = dbPromocion.Promocion.Count();
                Int64 numCodProm = max + 1;
                string codProm = Convert.ToString(numCodProm);
                while (true)
                {
                    if (codProm.Length == 8)
                        break;
                    else
                        codProm = "0" + codProm;
                }
                return codProm + "-" + DateTime.Now.Year.ToString();
            }
        }

    }
}
