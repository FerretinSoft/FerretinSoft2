using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pe.edu.pucp.ferretin.controller.MSeguridad;
using pe.edu.pucp.ferretin.model;

namespace pe.edu.pucp.ferretin.viewmodel.MCompras
{
    public class MC_ReportesViewModel:ViewModelBase
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
            }
        }
    }
}
