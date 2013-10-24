using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.ComponentModel;
using pe.edu.pucp.ferretin.model;
using pe.edu.pucp.ferretin.controller;
using System.Threading;
using System.Windows.Threading;

namespace pe.edu.pucp.ferretin.view.MAlmacen
{
    public class MA_MovimientosViewModel : INotifyPropertyChanged
    {
        public int searchAlmacen { get; set; }
        public String searchNombreProducto { get; set; }
        public DateTime searchFechaDesde { get; set; }
        public DateTime searchFechaHasta { get; set; }
        
        public enum tabs
        {
            //Pestañas virtuales:
            //0       1        2          3         4
            BUSQUEDA, AGREGAR, MODIFICAR, DETALLES, TIPOMOV
        }

        private Movimiento _movimiento = new Movimiento();
        public Movimiento movimiento
        {
            get
            {
                return _movimiento;
            }
            set
            {
                _movimiento = value;
                NotifyPropertyChanged("movimiento");
            }
        }

        private int _statusTab = (int)tabs.BUSQUEDA; //pestaña default 
        public int statusTab
        {
            get
            {
                return _statusTab;
            }
            set
            {
                _statusTab = value == 0 ? 0 : 1;
                //Si cambió el estado de las pestañas también cambio los Header
                //Si la pestaña es para agregar nuevo, limpio los input
                switch (value)
                {
                    case (int)tabs.BUSQUEDA: detallesTabHeader = "Agregar"; movimiento = new Movimiento(); break;//Si es agregar, creo un nuevo objeto Movimiento
                    case (int)tabs.AGREGAR: detallesTabHeader = "Agregar"; movimiento = new Movimiento(); break;//Si es agregar, creo un nuevo objeto Movimiento
                    case (int)tabs.MODIFICAR: detallesTabHeader = "Modificar"; break;
                    case (int)tabs.DETALLES: detallesTabHeader = "Detalles"; break;
                    default: detallesTabHeader = "Agregar"; movimiento = new Movimiento(); break;//Si es agregar, creo un nuevo objeto Movimiento
                }
                //Cuando se cambia el status, tambien se tiene que cambiar el currentIndex del tab
                //currentIndexTab = _statusTab == 0 ? 0 : 1;
                NotifyPropertyChanged("statusTab");
            }
        }

        //Usado para mover los tabs de acuerdo a las acciones realizadas
        public int currentIndexTab
        {
            get { return _statusTab == 0 ? 0 : 1; }
            set { statusTab = (int)tabs.AGREGAR; NotifyPropertyChanged("currentIndexTab"); }
        }

        private String _detallesTabHeader = "Agregar"; //Default
        public String detallesTabHeader
        {
            get
            {
                return _detallesTabHeader;
            }
            set
            {
                _detallesTabHeader = value;
                NotifyPropertyChanged("detallesTabHeader");
            }
        }

        

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        #endregion

    }


    /// <summary>
    /// Lógica de interacción para MA_MovimientosWindow.xaml
    /// </summary>
    public partial class MA_MovimientosWindow : Window
    {
        MA_MovimientosViewModel movViewModel = new MA_MovimientosViewModel();
        public MA_MovimientosWindow()
        {
            InitializeComponent();
            Thread thread = new Thread(
              new ThreadStart(
                delegate()
                {
                      busquedaMovGrid.Dispatcher.Invoke(
                      DispatcherPriority.Normal,
                      new Action(
                        delegate()
                        {
                            busquedaMovGrid.ItemsSource = MovimientosService.ObtenerListaMovimientos();
                        }
                    ));
                }
            ));
            thread.Start();
            movimientosTabControl.DataContext = movViewModel;
        }

        

        private IEnumerable<Movimiento> ListaMovimientos()
        {
            return MovimientosService.ObtenerListaMovimientos();
        }

        private void nuevoMvimientoBtn_Click(object sender, RoutedEventArgs e)
        {
            movViewModel.statusTab = (int)MA_MovimientosViewModel.tabs.AGREGAR;
        }

        private void editarMovimientoBtn_Click(object sender, RoutedEventArgs e)
        {
            if (busquedaMovGrid.SelectedItem != null)
            {
                movViewModel.movimiento = (Movimiento)busquedaMovGrid.SelectedItem;
                movViewModel.statusTab = (int)MA_MovimientosViewModel.tabs.MODIFICAR;//Modificar
            }
        }

        private void cancelarMovimientoBtn_Click(object sender, RoutedEventArgs e)
        {
            movViewModel.statusTab = (int)MA_MovimientosViewModel.tabs.BUSQUEDA;
        }

        private void buscarMovimientoBtn_Click(object sender, RoutedEventArgs e)
        {
            Dictionary<String, Object> parametros = new Dictionary<string, object>();

            if (movViewModel.searchAlmacen > 0) parametros.Add("almacen", movViewModel.searchAlmacen);
            if (movViewModel.searchNombreProducto != null) parametros.Add("nombreProducto", movViewModel.searchNombreProducto);
            if (movViewModel.searchFechaDesde != null) parametros.Add("fechaDesde", movViewModel.searchFechaDesde);
            if (movViewModel.searchFechaHasta != null) parametros.Add("fechaHasta", movViewModel.searchFechaHasta);

            busquedaMovGrid.ItemsSource = MovimientosService.ObtenerListaMovimientos(parametros);
        }  

        private void guardarMovimientoBtn_Click(object sender, RoutedEventArgs e)
        {//Puede ser nuevo o modificar
            if (movViewModel.movimiento.id > 0)
            {
                MovimientosService.ActualizarMovimiento(movViewModel.movimiento);
                
            }
            else
            {
                MovimientosService.InsertarMovimiento(movViewModel.movimiento);
            }
            movViewModel.statusTab = (int)MA_MovimientosViewModel.tabs.BUSQUEDA;
        }

        
        

        

        

        

        

        
    }
}
