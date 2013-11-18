using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pe.edu.pucp.ferretin.controller.MAlmacen;

namespace pe.edu.pucp.ferretin.viewmodel.MAlmacen
{
    public class MA_AtencionSolAuxViewModel: ViewModelBase
    {
        public MA_AtencionSolAuxViewModel()
        {
            _listadoProductos = new List<MA_SolicitudAbastecimientoService.AtencionSolicitudProducto>();
        }

         private List<MA_SolicitudAbastecimientoService.AtencionSolicitudProducto> _listadoProductos;
         public List<MA_SolicitudAbastecimientoService.AtencionSolicitudProducto> listadoProductos
         {
             get
             {
                 return _listadoProductos;
             }
             set
             {
                 _listadoProductos = value;
                 NotifyPropertyChanged("listadoProductos");
             }
         }
    }
}
