using pe.edu.pucp.ferretin.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pe.edu.pucp.ferretin.controller.MVentas
{
    public class MV_ProformasService : MV_ComunService
    {
        public static bool insertarProforma(Proforma proforma){
            if (!listaProformas.Contains(proforma))
            {
                proforma.codigo = newCodProforma;
                db.Proforma.InsertOnSubmit(proforma);
                return enviarCambios();
            }
            else
            {
                return false;
            }
        }

        private static IEnumerable<Proforma> _listaProformas;
        private static IEnumerable<Proforma> listaProformas
        {
            get
            {
                if (_listaProformas == null)
                {
                    _listaProformas = from p in db.Proforma where !p.finalizado.Value select p;
                }
                
                return _listaProformas;
            }
            set
            {
                _listaProformas = value;
            }
        }

        public static IEnumerable<Proforma> buscarProformas(string codProformaSearch, Usuario usuarioSearch, Cliente clienteSearch, DateTime fechaDesdeSearch, DateTime fechaHastaSearch)
        {
            
            codProformaSearch = codProformaSearch==null?String.Empty:codProformaSearch.ToLower();

            return from p in listaProformas where
                   p.codigo.ToUpper().Contains(codProformaSearch.ToUpper())
                   && (usuarioSearch==null || p.Usuario.id.Equals(usuarioSearch.id))
                   && (clienteSearch==null || p.Cliente==null || p.Cliente.id.Equals(clienteSearch.id))
                   && (fechaDesdeSearch.AddDays(-1).CompareTo(p.fecEmision)<=0 && fechaHastaSearch.AddDays(1).CompareTo(p.fecEmision)>=0)
                   select p;
        }


        public static string newCodProforma
        {
            get
            {
                Int64 numCodProf = listaProformas.Count() + 1;
                string codDev = Convert.ToString(numCodProf);
                while (true)
                {
                    if (codDev.Length == 8)
                        break;
                    else
                        codDev = "0" + codDev;
                }
                return codDev+"-"+DateTime.Now.Year.ToString();
            }
        }
    }
}
