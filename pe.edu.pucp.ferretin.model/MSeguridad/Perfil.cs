using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pe.edu.pucp.ferretin.model
{
    public partial class Perfil
    {        
        public string modulo { get; set;}

        private IEnumerable<PerfilMenu> _PerfilMenuPadre = null;
        public IEnumerable<PerfilMenu> PerfilMenuPadre
        {
            get
            {
                if(_PerfilMenuPadre==null || _PerfilMenuPadre.Count() <= 0){
                    if(PerfilMenu.Count>=0)
                        try
                        {
                            _PerfilMenuPadre = PerfilMenu.First().PerfilMenu2;
                        }
                        catch { }
                }
                return _PerfilMenuPadre;
            }
        }
    }
}
