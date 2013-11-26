using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pe.edu.pucp.ferretin.viewmodel.MAlmacen
{
    public class MA_MainWindowViewModel : ViewModelBase
    {
        public bool isTiendaVenta
        {
            get
            {
                return usuarioLogueado.Empleado.tiendaActual.tipo == 0;
            }
        }

        public bool isTiendaCompraVenta
        {
            get
            {
                return usuarioLogueado.Empleado.tiendaActual.tipo == 1;
            }
        }
    }
}
