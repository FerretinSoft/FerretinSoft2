using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using pe.edu.pucp.ferretin.controller;
using pe.edu.pucp.ferretin.controller.MCompras;
using pe.edu.pucp.ferretin.model;
using pe.edu.pucp.ferretin.viewmodel.Helper;

namespace pe.edu.pucp.ferretin.viewmodel.MCompras
{
    public class MC_BuscadorCotizacionesViewModel : ViewModelBase
    {
        public string _searchRazonSoc = "";
        public string searchRazonSoc
        {
            get
            {
                return _searchRazonSoc;
            }
            set
            {
                _searchRazonSoc = value;
                NotifyPropertyChanged("searchRazonSoc");
            }
        }

        public string _searchRuc = "";
        public string searchRuc
        {
            get
            {
                return _searchRuc;
            }
            set
            {
                _searchRuc = value;
                NotifyPropertyChanged("searchRuc");
            }
        }

        public DateTime? _searchFechaDesde = null;
        public DateTime? searchFechaDesde { get { return _searchFechaDesde; } set { _searchFechaDesde = value; NotifyPropertyChanged("searchFechaDesde"); } }

        public DateTime? _searchFechaHasta = null;
        public DateTime? searchFechaHasta { get { return _searchFechaHasta; } set { _searchFechaHasta = value; NotifyPropertyChanged("searchFechaHasta"); } }

        private IEnumerable<DocumentoCompra> _listaCotizaciones;
        public IEnumerable<DocumentoCompra> listaCotizaciones
        {
            get
            {
                _listaCotizaciones = MC_DocumentoCompraService.listaDocumentosCompra.Where(dc => dc.Proveedor.razonSoc.ToLower().Trim().Contains(searchRazonSoc.ToLower().Trim()) && dc.Proveedor.ruc.ToLower().Trim().Contains(searchRuc.ToLower().Trim())
                    && (searchFechaDesde == null || (dc.fechaEmision != null && dc.fechaEmision >= searchFechaDesde))
                    && (searchFechaHasta == null || (dc.fechaEmision != null && dc.fechaEmision <= searchFechaHasta))
                    && (dc.Usuario1.Empleado.tiendaActual.id == ComunService.usuarioL.Empleado.tiendaActual.id)
                    && dc.tipo == 2);

                return _listaCotizaciones;
            }
        }

        RelayCommand _actualizarListaCotizacionesCommand;
        public ICommand actualizarListaCotizacionesCommand
        {
            get
            {
                if (_actualizarListaCotizacionesCommand == null)
                {
                    _actualizarListaCotizacionesCommand = new RelayCommand(buscarCotizaciones);
                }
                return _actualizarListaCotizacionesCommand;
            }
        }

        public void buscarCotizaciones(object obj)
        {
            NotifyPropertyChanged("listaCotizaciones");
        }
    }
}
