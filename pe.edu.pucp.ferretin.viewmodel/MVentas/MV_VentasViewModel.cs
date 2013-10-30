using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using pe.edu.pucp.ferretin.controller.MVentas;
using pe.edu.pucp.ferretin.model;
using pe.edu.pucp.ferretin.viewmodel.Helper;

namespace pe.edu.pucp.ferretin.viewmodel.MVentas
{
    public class MV_VentasViewModel : ViewModelBase
    {
        #region Constructor
        public MV_VentasViewModel()
        {
            _venta = new Venta();
        }
        #endregion

        #region Valores para el cuadro de Búsqueda
        public String _searchNroDocumento = "";
        public String searchNroDocumento { get { return _searchNroDocumento; } set { _searchNroDocumento = value; NotifyPropertyChanged("searchNroDocumento"); } }

       #endregion

        #region Lista Ventas y Edicion de Venta
        private Venta _venta;
        public Venta venta
        {
            get
            {
                return _venta;
            }
            set
            {
                _venta = value;
                NotifyPropertyChanged("venta");
            }
        }

        private IEnumerable<Venta> _listaVentas;
        public IEnumerable<Venta> listaVentas
        {
            get
            {

                _listaVentas = MV_VentaService.buscarVentas(searchNroDocumento);

                return _listaVentas;
            }
            set
            {
                _listaVentas = value;
                NotifyPropertyChanged("listaVentas");
            }
        }
        #endregion

        #region RalayCommand
        RelayCommand _actualizarListaVentasCommand;
        public ICommand actualizarListaVentasCommand
        {
            get
            {
                if (_actualizarListaVentasCommand == null)
                {
                    _actualizarListaVentasCommand = new RelayCommand(param => NotifyPropertyChanged("listaVentas"));
                }
                return _actualizarListaVentasCommand;
            }
        }

        RelayCommand _viewDetailVentaCommand;
        public ICommand viewDetailVentaCommand
        {
            get
            {
                if (_viewDetailVentaCommand == null)
                {
                    _viewDetailVentaCommand = new RelayCommand(viewDetailVenta);
                }
                return _viewDetailVentaCommand;
            }
        }
        #endregion

        #region commands
        public void viewDetailVenta(Object id)
        {
            //try
            //{
                this.venta = listaVentas.Single(venta => venta.id == (long)id);
            //}
            /*catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }*/
        }
        #endregion

    }
}
