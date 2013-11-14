using pe.edu.pucp.ferretin.controller.MAlmacen;
using pe.edu.pucp.ferretin.controller.MSeguridad;
using pe.edu.pucp.ferretin.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pe.edu.pucp.ferretin.viewmodel.MAlmacen
{
    public class MA_ReportesViewModel:ViewModelBase
    {
        public IEnumerable<Tienda> _listaTiendas;
        public IEnumerable<Tienda> listaTiendas
        {
            get
            {
                _listaTiendas = MS_TiendaService.listaTiendas;
                return _listaTiendas;
            }

            set
            {
                _listaTiendas = value;
                NotifyPropertyChanged("listaTiendas");
            }
        }

        public Tienda _selectedTienda;
        public Tienda selectedTienda
        {
            get
            {
                return (_selectedTienda != null) ? _selectedTienda : usuarioLogueado.Empleado.tiendaActual;
            }

            set
            {
                _selectedTienda = value;
                NotifyPropertyChanged("selectedTienda");
            }

        }

        public DateTime _fechaDesde;
        public DateTime fechaDesde
        {
            get
            {
                return _fechaDesde;
            }
            set
            {
                _fechaDesde = value;
                NotifyPropertyChanged("fechaDesde");
                NotifyPropertyChanged("entradas");
                NotifyPropertyChanged("salidas");
            }
        }

        public DateTime _fechaHasta;
        public DateTime fechaHasta
        {
            get
            {
                return _fechaHasta;
            }
            set
            {
                _fechaHasta = value;
                NotifyPropertyChanged("fechaHasta");
                NotifyPropertyChanged("entradas");
                NotifyPropertyChanged("salidas");
            }
        }









    }
}
