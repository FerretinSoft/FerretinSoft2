using pe.edu.pucp.ferretin.controller.MAlmacen;
using pe.edu.pucp.ferretin.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pe.edu.pucp.ferretin.viewmodel.MAlmacen
{
    public class MA_ReportesStockPorTiendaViewModel:ViewModelBase
    {
        public String _fechaRep;
        public String fechaRep
        {
            get
            {
                _fechaRep = System.DateTime.Now.ToString("dd/MM/yyyy h:mm tt");
                return _fechaRep;
            }
            set
            {
                _fechaRep = value;
                NotifyPropertyChanged("fechaRep");
            }
        }

        public Tienda _tiendaSeleccionada;
        public Tienda tiendaSeleccionada
        {
            get
            {
                return _tiendaSeleccionada;
            }
            set
            {
                _tiendaSeleccionada = value;
                IEnumerable<ProductoAlmacen> l = listaProductos;
                ProductoAlmacen pa;
                
                NotifyPropertyChanged("tiendaSeleccionada");
                NotifyPropertyChanged("listaProductos");
            }
        }

        public IEnumerable<ProductoAlmacen> _listaProductos;
        public IEnumerable<ProductoAlmacen> listaProductos
        {
            get
            {
                if (_tiendaSeleccionada!=null)
                    _listaProductos=MA_ProductoService.obtenerProductosPorTienda(tiendaSeleccionada.id);
                return _listaProductos;

            }

            set
            {
                _listaProductos = value;
                NotifyPropertyChanged("listaProductos");
            }


        }


    }
}
