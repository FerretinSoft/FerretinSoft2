using System;
using System.Collections;
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
using pe.edu.pucp.ferretin.model;
using pe.edu.pucp.ferretin.controller;
using System.Threading;
using System.Windows.Threading;
using System.ComponentModel;

namespace pe.edu.pucp.ferretin.view.MSeguridad
{
    
    public class MS_TiendasViewModel : INotifyPropertyChanged
    {
        #region Valores para el cuadro de Búsqueda
        public String searchCodTienda { get; set; }
        public String searchNombre { get; set; }
        private int _searchEstado;
        public int searchEstado { get { return _searchEstado; } set { _searchEstado = value; NotifyPropertyChanged("searchEstado"); } }
        private int  _searchProvincia { get; set; }
        public int searchProvincia
        {
            get
            {
                return _searchProvincia > 0 ? _searchProvincia : 0;
            }
            set
            {
                _searchProvincia = value;
                NotifyPropertyChanged("searchProvincia");
                NotifyPropertyChanged("provincias");
                NotifyPropertyChanged("distritos");
            }
        }
        private int _searchTipo;
        public int searchTipo { get { return _searchTipo; } set { _searchTipo = value; NotifyPropertyChanged("searchTipo"); } }
        public int _searchDistrito;
        public int searchDistrito
        {
            get
            {
                return _searchDistrito;
            }
            set
            {
                _searchDistrito = value;
                NotifyPropertyChanged("searchDistrito");
                NotifyPropertyChanged("distritos");
            }
        }
        #endregion

        public enum tabs
        {
            //Pestañas virtuales:
            //0       1        2          3
            BUSQUEDA, AGREGAR, MODIFICAR, DETALLES
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
                    case (int)tabs.BUSQUEDA: detallesTabHeader = "Agregar"; tienda = new Almacen(); break;//Si es agregar, creo un nuevo objeto Tienda
                    case (int)tabs.AGREGAR: detallesTabHeader = "Agregar"; tienda = new Almacen(); break;//Si es agregar, creo un nuevo objeto Tienda
                    case (int)tabs.MODIFICAR: detallesTabHeader = "Modificar"; break;
                    case (int)tabs.DETALLES: detallesTabHeader = "Detalles"; break;
                    default: detallesTabHeader = "Agregar"; tienda = new Almacen(); break;//Si es agregar, creo un nuevo objeto Tienda
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

        private Almacen _tienda = new Almacen();
        
        public Almacen tienda
        {
            get
            {
                return _tienda;
            }
            set
            {
                _tienda = value;
                NotifyPropertyChanged("tienda");
            }
        }

        int selectedDepartamento = 14; //index Lima default
        public IEnumerable<UbigeoProvincia> provincias
        {
            get
            {
                
                return MR_UbigeoService.departamentos.ElementAt(selectedDepartamento).UbigeoProvincia;
            }
        }

        public IEnumerable<UbigeoDistrito> distritos
        {
            get
            {
                return MR_UbigeoService.departamentos.ElementAt(selectedDepartamento).UbigeoProvincia.ElementAt(searchProvincia).UbigeoDistrito;
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
    /// Interaction logic for MS_AdministrarTiendas.xaml
    /// </summary>
    public partial class MS_AdministrarTiendas : Window
    {
        MS_TiendasViewModel MS_TiendasViewModel = new MS_TiendasViewModel();

        public MS_AdministrarTiendas()
        {
            InitializeComponent();
            
            tiendasGrid.ItemsSource = MS_TiendaService.obtenerListaTiendas();
            DataContext = MS_TiendasViewModel;
        }

        private IEnumerable<Almacen> ListaTiendas()
        {
            return MS_TiendaService.obtenerListaTiendas();
        }

        public void codTienda_Click(object sender, RoutedEventArgs e)
        {
            if (tiendasGrid.SelectedItem != null)
            {
                MS_TiendasViewModel.tienda = (Almacen)tiendasGrid.SelectedItem;
                MS_TiendasViewModel.statusTab = (int)MS_TiendasViewModel.tabs.MODIFICAR;//Modificar                 
            }
        }

        private void nuevaTiendaBtn_Click(object sender, RoutedEventArgs e)
        {
            MS_TiendasViewModel.statusTab = (int)MS_TiendasViewModel.tabs.AGREGAR;

        }

        private void cancelarListaTiendaBtn_Click(object sender, RoutedEventArgs e)
        {
            MS_TiendasViewModel.statusTab = (int)MS_TiendasViewModel.tabs.BUSQUEDA;
        }

        private void buscarTiendaBtn_Click(object sender, RoutedEventArgs e)
        {
            Almacen tienda = new Almacen();
            tienda.codigo = (MS_TiendasViewModel.searchCodTienda == null) ? "" : MS_TiendasViewModel.searchCodTienda;
            tienda.nombre = (MS_TiendasViewModel.searchNombre == null) ? "" : MS_TiendasViewModel.searchNombre;

            if (MS_TiendasViewModel.searchEstado > 0)
            {
                tienda.estado = (MS_TiendasViewModel.searchEstado == 1 ? true : false);
            }
            if( MS_TiendasViewModel.searchTipo > 0){
                tienda.tipo = MS_TiendasViewModel.searchTipo==1 ? true : false;
            }

            if (MS_TiendasViewModel.searchDistrito >= 0)
            {
                tienda.id_ubigeo = MS_TiendasViewModel.distritos.ElementAt(MS_TiendasViewModel.searchDistrito).id;
            }

            tiendasGrid.ItemsSource = MS_TiendaService.obtenerListaTiendasBy(tienda);
        }


        private void guardarDetalleTiendaBtn_Click(object sender, RoutedEventArgs e)
        {
            //Puede ser nuevo o modificar
            if (MS_TiendasViewModel.tienda.codigo != " ")
            {
                //TiendaService.actualizarTienda(MS_TiendasViewModel.tienda);

            }
            else
            {
                //TiendaService.insertarTienda(MS_TiendasViewModel.tienda);
            }
            MS_TiendasViewModel.statusTab = (int)MS_TiendasViewModel.tabs.BUSQUEDA;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void edTiendaBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

    }
}
