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
using pe.edu.pucp.ferretin.controller;
using pe.edu.pucp.ferretin.model;
using System.ComponentModel;

namespace pe.edu.pucp.ferretin.view.MAlmacen
{
    /// <summary>
    /// Lógica de interacción para MA_MantenimientoProductosWindow.xaml
    /// </summary>
    /// 
    public partial class MA_MantinimientoProductosViewModel : INotifyPropertyChanged
    {
        public String searchNombre{get;set;}
        public IEnumerable<Categoria> listaCategorias { get; set; }
        
        private Producto _prod;
        public Producto prod
        {
            get
            {
                return _prod;
            }
            set
            {
                _prod = value;
                NotifyPropertyChanged("producto");
            }
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
                    case (int)tabs.BUSQUEDA: detallesTabHeader = "Agregar"; prod = new Producto(); break;//Si es agregar, creo un nuevo objeto Cliente
                    case (int)tabs.AGREGAR: detallesTabHeader = "Agregar"; prod = new Producto(); break;//Si es agregar, creo un nuevo objeto Cliente
                    //case (int)tabs.MODIFICAR: detallesTabHeader = "Modificar"; break;
                    //case (int)tabs.DETALLES: detallesTabHeader = "Detalles"; break;
                    //default: detallesTabHeader = "Agregar"; cliente = new Cliente(); break;//Si es agregar, creo un nuevo objeto Cliente
                }
                //Cuando se cambia el status, tambien se tiene que cambiar el currentIndex del tab
                //currentIndexTab = _statusTab == 0 ? 0 : 1;
                NotifyPropertyChanged("statusTab");
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


    public partial class MA_MantenimientoProductosWindow : Window
    {
        MA_MantinimientoProductosViewModel pvm=new MA_MantinimientoProductosViewModel();

        public MA_MantenimientoProductosWindow()
        {
            InitializeComponent();
            productoTabControl.DataContext = pvm;
            pvm.listaCategorias = CategoriaService.obtenerTodasCategorias();
        }

        private void nuevoProductoBtn_Click(object sender, RoutedEventArgs e)
        {
            //mantenimientoTab.SelectedIndex = 1;
        }

        private void editarProductoBtn_Click(object sender, RoutedEventArgs e)
        {
            //mantenimientoTab.SelectedIndex = 1;
        }

        private void buscarClienteBtn_Click(object sender, RoutedEventArgs e)
        {
            pvm.searchNombre= (pvm.searchNombre==null)?"":pvm.searchNombre;
            if (pvm.searchNombre != null) 
            {
                gridProductos.ItemsSource = ProductoService.obtenerProductosPorNombre(pvm.searchNombre);
                
            }
            else
                gridProductos.ItemsSource=ProductoService.obtenerTodosProductos();
        }
    }
}
