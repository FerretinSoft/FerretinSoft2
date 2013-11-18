using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using pe.edu.pucp.ferretin.controller;
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

        public long? _searchNroDocCliente = null;
        public long? searchNroDocCliente { get { return _searchNroDocCliente; } set { _searchNroDocCliente = value; NotifyPropertyChanged("searchNroDocCliente"); } }

        public string _detallesTabHeader = "";
        public String detallesTabHeader { get { return _detallesTabHeader; } set { _detallesTabHeader = value; NotifyPropertyChanged("detallesTabHeader"); } }

        public bool _noSoloDetallarLoteVale = true;
        public bool noSoloDetallarLoteVale { get { return _noSoloDetallarLoteVale; } set { _noSoloDetallarLoteVale = value; NotifyPropertyChanged("noSoloDetallarLoteVale"); } }

        private System.Windows.Visibility _detallarVale = System.Windows.Visibility.Visible;
        public System.Windows.Visibility detallarVale
        {
            get
            {
                return _detallarVale;
            }
            set
            {
                _detallarVale = value;
                NotifyPropertyChanged("detallarVale");
               

            }
        }
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


        public bool _valesGenerados = false;
        public bool valesGenerados { get { return _valesGenerados; } set { _valesGenerados = value; NotifyPropertyChanged("valesGenerados"); } }

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

        private IEnumerable<Vale> _listaVales;
        public IEnumerable<Vale> listaVales
        {
            get
            {

                return _listaVales;
            }
            set
            {
                _listaVales = value;
                NotifyPropertyChanged("listaVales");
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

        RelayCommand _nuevoLoteValesCommand;
        public ICommand nuevoLoteValesCommand
        {
            get
            {
                if (_nuevoLoteValesCommand == null)
                {
                    _nuevoLoteValesCommand = new RelayCommand(nuevoLotesVale);
                }
                return _nuevoLoteValesCommand;
            }
        }

        RelayCommand _viewDetailLoteValeCommand;
        public ICommand viewDetailLoteValeCommand
        {
            get
            {
                if (_viewDetailLoteValeCommand == null)
                {
                    _viewDetailLoteValeCommand = new RelayCommand(viewLoteVale);
                }
                return _viewDetailLoteValeCommand;
            }
        }


        RelayCommand _generarValesCommand;
        public ICommand generarValesCommand
        {
            get
            {
                if (_generarValesCommand == null)
                {
                    _generarValesCommand = new RelayCommand(generarVales);
                }
                return _generarValesCommand;
            }
        }

        RelayCommand _saveLoteValeCommand;
        public ICommand saveLoteValeCommand
        {
            get
            {
                if (_saveLoteValeCommand == null)
                {
                    _saveLoteValeCommand = new RelayCommand(saveLoteVale);
                }
                return _saveLoteValeCommand;
            }
        }

        RelayCommand _cancelarLoteValeCommand;
        public ICommand cancelarLoteValeCommand
        {
            get
            {
                if (_cancelarLoteValeCommand == null)
                {
                    _cancelarLoteValeCommand = new RelayCommand(cancelarLoteVale);
                }
                return _cancelarLoteValeCommand;
            }
        }
        #endregion

        #region commands

        public void cancelarLoteVale(object id)
        {
            string messageBoxText = "Al salir, perderá todos los datos ingresados. ¿Desea continuar?";
            string caption = "ATENCIÓN";
            MessageBoxButton button = MessageBoxButton.OKCancel;
            MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, MessageBoxImage.Warning);
            switch (result)
            {
                case MessageBoxResult.OK:
                    loteVale.Vale = new System.Data.Linq.EntitySet<Vale>();
                    this.loteVale = new LoteVale();
                    this.selectedTab = 0;
                    break;

                case MessageBoxResult.Cancel:
                    break;
            }
        }

        public void saveLoteVale(object id)
        {
            string messageBoxText = "¿Desea confirmar la transacción? Se procederá a almacenar la información ingresada";
            string caption = "Mensaje de confirmación";
            MessageBoxButton button = MessageBoxButton.OKCancel;
            MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, MessageBoxImage.Warning);
            switch (result)
            {
                case MessageBoxResult.OK:
            ComunService.idVentana(50);
            if (!MV_ValeService.insertarLoteVale(loteVale))
            {
                MessageBox.Show("No se pudo agregar el nuevo lote de vales", "Error");
            }
            else
            {
                MessageBox.Show("El lote de vales fue agregado con éxito", "Mensaje de confirmación");
            }
            this.selectedTab = 0;
                break;
                case MessageBoxResult.Cancel:
                break;
            }
        }

        public void generarVales(object id)
        {
            if (loteVale.cantidad != null && loteVale.cantidad > 0 && loteVale.monto != null && loteVale.monto > 0)
            {
                string messageBoxText = "¿Desea generar " + loteVale.cantidad + " vales?";
                string caption = "Mensaje de confirmación";
                MessageBoxButton button = MessageBoxButton.OKCancel;
                MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button);
                switch (result)
                {
                    case MessageBoxResult.OK:
                        loteVale.Vale = new System.Data.Linq.EntitySet<Vale>();
                        for (int i = 1; i <= (int)loteVale.cantidad; i++)
                        {
                            Vale vale = MV_ValeService.generarVale((int)loteVale.cantidad, i);

                            loteVale.Vale.Add(vale);
                            NotifyPropertyChanged("loteVale");
                        }
                        valesGenerados = true;
                        break;
                    case MessageBoxResult.Cancel:
                        break;
                }
            }
            else
            {
                string messageBoxText = "Debe ingresar una cantidad válida de vales";
                string caption = "Error";
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBox.Show(messageBoxText, caption, button);
            }

        }

        public void viewLoteVale(Object id)
        {
            this.loteVale = MV_ValeService.obtenerLoteValebyId((int)id);
            this.selectedTab = 1;
            detallarVale = System.Windows.Visibility.Hidden;
            this.detallesTabHeader = "Detalle";
            this.listaVales = MV_ValeService.obtenerValesValebyIdLote((int)id);
            this.noSoloDetallarLoteVale = false;
           
        }

        public void nuevoLotesVale(Object id)
        {
            this.loteVale = new LoteVale();
            this.selectedTab = 1;
            detallarVale = System.Windows.Visibility.Visible;
            this.noSoloDetallarLoteVale = true;
            this.listaVales = null;
            this.loteVale = MV_ValeService.obtenerNuevoLote();
            this.detallesTabHeader = "Generar";

        }
        #endregion






    }
}
