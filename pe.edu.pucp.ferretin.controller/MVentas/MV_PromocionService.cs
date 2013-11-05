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
            return db.Promocion.Where(p => p.codigo.ToUpper().Contains(codPromSearch.ToUpper())).
                Where(p => DateTime.Compare(p.fechaDesde.Value,fechaDesdeSearch)>=0 || DateTime.Compare(p.fechaHasta.Value,fechaHastaSearch)<=0 ).
                Where(p => estadoSearch <= 0 || p.estado == estadoSearch);
        }
    }
}
