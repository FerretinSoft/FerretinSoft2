using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pe.edu.pucp.ferretin.controller.MSeguridad;
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


        private static IEnumerable<Vale> _listaVales;
        public static IEnumerable<Vale> listaVales
        {
            get
            {
                if (_listaVales == null)
                {
                    _listaVales = db.Vale;
                }
                db.Refresh(RefreshMode.OverwriteCurrentValues, _listaVales);
                return _listaVales;
            }
            set
            {
                _listaVales = value;
            }
        }

        public static IEnumerable<LoteVale> buscarLotesVale(string nroCodLote, long? nroDocCliente, DateTime fechaInicio, DateTime fechaFin)
        {
            return from c in listaLoteVale
                   where
                   (c.codigo != null && c.codigo.Contains(nroCodLote)
                   && (c.Cliente == null || nroDocCliente == null || c.Cliente.nroDoc.Equals(nroDocCliente))
                   && c.fechaEmision != null && c.fechaEmision >= fechaInicio           
                    )
                   orderby c.codigo
                   select c;
        }

        public static LoteVale obtenerLoteValebyId(int id)
        {
            LoteVale loteVale = (from c in listaLoteVale
                                       where c.id.Equals(id)
                                       select c).Single();

            return loteVale;
        }

        public static IEnumerable<Vale> obtenerValesValebyIdLote(int id)
        {
            return from c in listaVales
                   where
                   (c.LoteVale.id != null && c.LoteVale.id.Equals(id)
                    )
                   orderby c.LoteVale.id
                   select c;
        }

        public static Vale generarVale(int cantidad, int offset)
        {
            string codVale = (from c in listaVales
                               select c.codigo).Max();;
            
                Vale vale = new Vale();
            
                vale.codigo =  Convert.ToString(Convert.ToInt32(codVale) + offset);
                while (true)
                {
                    if (vale.codigo.Length == 6)
                        break;
                    else
                        vale.codigo = "0" + vale.codigo;
                }
                vale.codigo = "V" + vale.codigo + "-" + DateTime.Now.Year.ToString();
                vale.estado = 0;
            return vale;
        }

        public static LoteVale obtenerNuevoLote()
        {
            string codLote = (from c in listaLoteVale
                              select c.codigo).Max(); ;
            codLote = Convert.ToString(Convert.ToInt32(codLote) + 1);
            LoteVale loteVale = new LoteVale();
            loteVale.fechaEmision = DateTime.Today;
            loteVale.fechaVencimiento = DateTime.Today.AddDays(MS_SharedService.obtenerVigenciaVale());
            while (true)
            {
                if (codLote.Length == 8)
                    break;
                else
                    codLote = "0" + codLote;
            }
            loteVale.codigo = codLote;
            return loteVale;

        }

        public static bool insertarLoteVale(LoteVale loteVale){
            if (!db.LoteVale.Contains(loteVale))
            {
                db.LoteVale.InsertOnSubmit(loteVale);
                return enviarCambios();
            }
            else
            {
                return false;
            }
        }
        #endregion





    }
}
