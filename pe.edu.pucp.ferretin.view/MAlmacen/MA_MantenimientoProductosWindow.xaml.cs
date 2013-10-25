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
using pe.edu.pucp.ferretin.controller.MSeguridad;
using pe.edu.pucp.ferretin.controller.MAlmacen;


namespace pe.edu.pucp.ferretin.view.MAlmacen
{
    /// <summary>
    /// Lógica de interacción para MA_MantenimientoProductosWindow.xaml
    /// </summary>
    /// 
    public partial class MA_MantinimientoProductosViewModel : INotifyPropertyChanged
    {
        public String searchNombre{get;set;}
        public String searchIdCategoria { get; set; }
        public IEnumerable<Categoria> listaCategorias { get; set; }
        public IEnumerable<Unidad_Medida> listaUMed { get; set; }
        public IEnumerable<Material> listaMatBase { get; set; }
        public IEnumerable<Material> listaMatSec { get; set; }
        public IEnumerable<Almacen> listaTiendas { get; set; }
        
        
        private Producto _prod;
        public Producto producto
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

        private ProductoAlmacen _prodAlm;
        public ProductoAlmacen prodAlm
        {
            get
            {
                return _prodAlm;
            }
            set
            {
                _prodAlm = value;
                NotifyPropertyChanged("prodAlm");
            }
        }

        private String _detallesTabHeader = "Agregar Producto"; //Default
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
                    case (int)tabs.BUSQUEDA: detallesTabHeader = "Agregar Producto"; producto = new Producto(); break;//Si es agregar, creo un nuevo objeto Cliente
                    case (int)tabs.AGREGAR: detallesTabHeader = "Agregar Producto"; producto = new Producto(); prodAlm = new ProductoAlmacen(); break;//Si es agregar, creo un nuevo objeto Cliente
                    case (int)tabs.MODIFICAR:detallesTabHeader = "Edición de Producto"; break;
                            

                        
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
            pvm.listaUMed = MA_UnidadMedidaServiceMateriales.obtenerUnidadesMedida();
            pvm.listaCategorias = MA_CategoriaService.obtenerTodasCategorias();
            pvm.listaMatBase = MA_UnidadMedidaServiceMateriales.obtenerMaterialesPrimario();
            pvm.listaMatSec = MA_UnidadMedidaServiceMateriales.obtenerMaterialesPrimario();
            pvm.listaTiendas = MS_TiendaService.obtenerListaTiendas();
            
        }

        private void nuevoProductoBtn_Click(object sender, RoutedEventArgs e)
        {
            productoTabControl.SelectedIndex = 1;
            txtCodigo.IsEnabled = true;
            
        }

        private void editarProductoBtn_Click(object sender, RoutedEventArgs e)
        {
            irTabEditarProducto();
        }

        private void buscarClienteBtn_Click(object sender, RoutedEventArgs e)
        {
            Producto producto = new Producto();
            producto.nombre=(pvm.searchNombre==null)?"":pvm.searchNombre;
            int flagAll = 0;
            
            if (chkActivo.IsChecked.Value && chkInactivo.IsChecked.Value)
            {
                flagAll = 1;
            }
            else
            {
                producto.estado=(chkActivo.IsChecked==true)?true:false;
            }

            gridProductos.ItemsSource = MA_ProductoService.obtenerProductosPorNombre(producto,flagAll);

        }

        private void btnGuardar(object sender, RoutedEventArgs e)
        {
            //Console.WriteLine(this.detallesTab.Header);
            pvm.producto.estado=(this.rbtnActivo.IsChecked==true)?true:false;
            //pvm.prodAlm.id_producto = pvm.producto.codigo;
            MA_ProductoService.agregarProducto(pvm.producto,pvm.prodAlm);
        }

        private void irTabEditarProducto()
        {
            if (gridProductos.SelectedItem != null)
            {
                enable_disable_campos(false);
                pvm.producto = (Producto)gridProductos.SelectedItem;
                pvm.statusTab = (int)MA_MantinimientoProductosViewModel.tabs.MODIFICAR;//Modificar
            }
        }

        private void codProducto_click(object sender, RoutedEventArgs e)
        {
            irTabEditarProducto();
        }

        private void enable_disable_campos(Boolean valor)
        {
            this.txtCodigo.IsEnabled = valor;
            this.txtDescuento.IsEnabled = valor;
            this.txtPuntos.IsEnabled = valor;
            this.txtStockAct.IsEnabled = valor;
            this.txtStockMin.IsEnabled = valor;
            this.cmbMatBase.IsEnabled = valor;
            this.cmbMatSec.IsEnabled = valor;
            this.cmbUnidadMed.IsEnabled = valor;
            this.cmbTienda.IsEnabled = valor;
        }

        private void tabBúsqueda_Click(object sender, MouseButtonEventArgs e)
        {
            enable_disable_campos(true);
        }

        private void btnEliminar(object sender, RoutedEventArgs e)
        {

        }
    }
}
