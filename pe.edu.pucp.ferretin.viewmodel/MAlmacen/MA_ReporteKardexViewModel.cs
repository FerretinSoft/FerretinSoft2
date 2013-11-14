using pe.edu.pucp.ferretin.controller.MAlmacen;
using pe.edu.pucp.ferretin.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pe.edu.pucp.ferretin.viewmodel.MAlmacen
{
    public class MA_ReporteKardexViewModel : ViewModelBase
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
                NotifyPropertyChanged("tiendaSeleccionada");
                NotifyPropertyChanged("entradas");
                NotifyPropertyChanged("salidas");
            }
        }

        public List<MovimientoProducto> _entradas;
        public List<MovimientoProducto> entradas
        {
            get
            {
                if (_tiendaSeleccionada != null && _fechaDesde != null && _fechaHasta != null)
                    _entradas = MA_MovimientosService.buscarEntradasPorTiendaPeriodo(tiendaSeleccionada, fechaDesde, fechaHasta);
                return _entradas;

            }

            set
            {
                _entradas = value;
                NotifyPropertyChanged("entradas");
            }


        }

        public List<MovimientoProducto> _salidas;
        public List<MovimientoProducto> salidas
        {
            get
            {
                if (_tiendaSeleccionada != null && _fechaDesde != null && _fechaHasta != null)
                    _salidas= MA_MovimientosService.buscarSalidasPorTiendaPeriodo(tiendaSeleccionada, fechaDesde, fechaHasta);
                return _salidas;

            }

            set
            {
                _salidas = value;
                NotifyPropertyChanged("salidas");
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
