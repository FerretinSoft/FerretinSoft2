using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pe.edu.pucp.ferretin.model;

namespace pe.edu.pucp.ferretin.controller.MVentas
{
    public partial class MV_ValeService : MV_ComunService
    {
        #region Private Zone
        #endregion

        #region Public Zone
        private static IEnumerable<LoteVale> _listaLoteVale;
        public static IEnumerable<LoteVale> listaLoteVale
        {            
            get
            {
                if (_listaLoteVale == null)
                {
                    _listaLoteVale = db.LoteVale;
                }
                db.Refresh(RefreshMode.OverwriteCurrentValues, _listaLoteVale);
                return _listaLoteVale;
            }
            set
            {
                _listaLoteVale = value;
            }
        }

        public static IEnumerable<LoteVale> buscarLotesVale(string nroCodLote, string nroDocCliente, DateTime fechaInicio, DateTime fechaFin)
        {
            return from c in listaLoteVale
                   where
                   (c.codigo != null && c.codigo.Contains(nroCodLote)
                   && c.Cliente.nroDoc != null && c.Cliente.nroDoc.Contains(nroDocCliente)
                   && c.fechaEmision != null && c.fechaEmision >= fechaInicio           
                    )
                   orderby c.codigo
                   select c;
        }
        
        #endregion





    }
}
