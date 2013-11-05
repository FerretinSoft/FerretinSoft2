using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using pe.edu.pucp.ferretin.controller.MVentas;
using pe.edu.pucp.ferretin.model;
using pe.edu.pucp.ferretin.viewmodel.Helper;

namespace pe.edu.pucp.ferretin.viewmodel.MVentas
{
    public class MV_ValesViewModel : ViewModelBase
    {
        #region Constructor
        public MV_ValesViewModel()
        {
            _loteVale = new LoteVale();
        }
        #endregion

        #region Valores para el cuadro de Búsqueda
        public String _searchCodLote = "";
        public String searchCodLote { get { return _searchCodLote; } set { _searchCodLote = value; NotifyPropertyChanged("searchCodLote"); } }

        public DateTime _searchFechaInicio = DateTime.Parse("10/09/2013");
        public DateTime searchFechaInicio { get { return _searchFechaInicio; } set { _searchFechaInicio = value; NotifyPropertyChanged("searchFechaInicio"); } }

        public DateTime _searchFechaFin = DateTime.Today;
        public DateTime searchFechaFin { get { return _searchFechaFin; } set { _searchFechaFin = value; NotifyPropertyChanged("searchFechaFin"); } }

        public String _nombreCliente = "";
        public String nombreCliente { get { return _nombreCliente; } set { _nombreCliente = value; NotifyPropertyChanged("nombreCliente"); } }

        public String _searchNroDocCliente = "";
        public String searchNroDocCliente { get { return _searchNroDocCliente; } set { _searchNroDocCliente = value; NotifyPropertyChanged("searchNroDocCliente"); } }

        private int _selectedTab = 0;
        public int selectedTab
        {
            get
            {
                return _selectedTab;
            }
            set
            {
                _selectedTab = value;
                NotifyPropertyChanged("selectedTab");
            }
        }

        #endregion

        #region

        private LoteVale _loteVale;
        public LoteVale loteVale
        {
            get
            {
                return _loteVale;
            }
            set
            {

                _loteVale = value;
                NotifyPropertyChanged("loteVale");
            }
        }


        private IEnumerable<LoteVale> _listaLoteVale;
        public IEnumerable<LoteVale> listaLoteVale
        {
            get
            {

                _listaLoteVale = MV_ValeService.buscarLotesVale(searchCodLote, searchNroDocCliente, searchFechaInicio, searchFechaFin);

                return _listaLoteVale;
            }
            set
            {
                _listaLoteVale = value;
                NotifyPropertyChanged("listaLoteVale");
            }
        }
        #endregion


        #region RalayCommand
        RelayCommand _actualizarListaLoteValeCommand;
        public ICommand actualizarListaLoteValeCommand
        {
            get
            {
                if (_actualizarListaLoteValeCommand == null)
                {
                    _actualizarListaLoteValeCommand = new RelayCommand(param => NotifyPropertyChanged("listaLoteVale"));
                }
                return _actualizarListaLoteValeCommand;
            }
        }
        #endregion







    }
}
