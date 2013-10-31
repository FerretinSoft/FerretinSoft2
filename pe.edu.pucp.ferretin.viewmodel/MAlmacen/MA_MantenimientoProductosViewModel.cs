using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pe.edu.pucp.ferretin.controller.MSeguridad;
using pe.edu.pucp.ferretin.controller.MAlmacen;
using pe.edu.pucp.ferretin.controller;
using pe.edu.pucp.ferretin.model;
using System.Windows.Input;
using pe.edu.pucp.ferretin.viewmodel.Helper;
using System.Windows;

namespace pe.edu.pucp.ferretin.viewmodel.MAlmacen
{
    public class MA_MantenimientoProductosViewModel : ViewModelBase
    {

        public String searchNombre { get; set; }
        public Int16 searchIdCategoria { get; set; }

        private IEnumerable<Categoria> _treeParents;
        public IEnumerable<Categoria> treeParents
        {
            get
            {
                _treeParents = MA_CategoriaService.obtenerCategoriasPadres();
                foreach (Categoria c in _treeParents)
                {
                    c.listaHijos = MA_CategoriaService.obtenerCategoriasHijos();
                }
                return _treeParents;
            }
            set { _treeParents = value; OnPropertyChanged("treeParents"); }
        }



        public Boolean _radioYes;
        public Boolean radioYes
        {
            get
            {
                return this._radioYes;

            }

            set
            {
                this._radioYes=value;
                this.OnPropertyChanged("radioYes");

            }
        }

        public Boolean _radioNo;
        public Boolean radioNo
        {
            get
            {
                return this._radioNo;

            }

            set
            {
                this._radioNo=value;
                this.OnPropertyChanged("radioNo");

            }
        }



        public bool chkActivo { get; set; }
        public bool chkInactivo { get; set; }


        public Tienda _selectedTienda;
        public Tienda selectedTienda
        {
            set
            {   
                _selectedTienda = value;
                if (_selectedTienda != null)
                {
                    prodAlm = MA_ProductoService.obtenerProdxTienda(producto.id, _selectedTienda.id);
                    if (prodAlm != null)
                    {
                        radioYes = false;
                        radioNo = false;
                        if (prodAlm.estado == 1)
                            radioYes = true;
                        else
                            radioNo = true;
                    }
                }
            }

            get
            {
                return _selectedTienda;
            }



        }


        public IEnumerable<Tienda> _listaTiendas;
        public IEnumerable<Tienda> listaTiendas
        {
            get
            {
                _listaTiendas = MS_TiendaService.listaAlmacenes;
                return _listaTiendas;
            }
            set
            {
                _listaTiendas = value;
                NotifyPropertyChanged("listaTiendas");
            }
        }

        public IEnumerable<Material> _listaMatSec;
        public IEnumerable<Material> listaMatSec
        {
            get
            {
                _listaMatSec = MA_UnidadMedidaServiceMateriales.obtenerMaterialesPrimario();
                return _listaMatSec;
            }
            set
            {
                _listaMatSec = value;
                NotifyPropertyChanged("listaMatSec");
            }
        }


        public IEnumerable<Material> _listaMatBase;
        public IEnumerable<Material> listaMatBase
        {
            get
            {
                _listaMatBase = MA_UnidadMedidaServiceMateriales.obtenerMaterialesPrimario();
                return _listaMatBase;
            }
            set
            {
                _listaMatBase = value;
                NotifyPropertyChanged("listaMatBase");
            }
        }

        public IEnumerable<UnidadMedida> _listaUMed;
        public IEnumerable<UnidadMedida> listaUMed
        {
            get
            {
                _listaUMed = MA_UnidadMedidaServiceMateriales.obtenerUnidadesMedida();
                return _listaUMed;            }
            set
            {
                _listaUMed = value;
                NotifyPropertyChanged("listaUMed");
            }
        }



        public IEnumerable<Categoria> _listaCategorias;
        public IEnumerable<Categoria> listaCategorias
        {
            get
            {
                _listaCategorias = MA_CategoriaService.categorias;
                return _listaCategorias;
            }
            set
            {
                _listaCategorias=value;
                NotifyPropertyChanged("listaCategorias");
            }
        }



        public IEnumerable<Producto> _listaProductos;
        public IEnumerable<Producto> listaProductos {
            
            get
            {
                searchNombre = searchNombre == null ? "" : searchNombre;
                _listaProductos=MA_ProductoService.obtenerProductosPorNombre(searchNombre,chkActivo,chkInactivo,searchIdCategoria);
                
                return _listaProductos;
            }
            
            set
            {
                _listaProductos = value;
                NotifyPropertyChanged("listaProductos");
            }
        }


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
                    case (int)tabs.MODIFICAR: detallesTabHeader = "Edición de Producto"; break;



                    //case (int)tabs.DETALLES: detallesTabHeader = "Detalles"; break;
                    //default: detallesTabHeader = "Agregar"; cliente = new Cliente(); break;//Si es agregar, creo un nuevo objeto Cliente
                }
                //Cuando se cambia el status, tambien se tiene que cambiar el currentIndex del tab
                //currentIndexTab = _statusTab == 0 ? 0 : 1;
                NotifyPropertyChanged("statusTab");
            }
        }

        #region RelayCommand
        RelayCommand _nuevoProductoCommand;
        public ICommand nuevoProductoCommand
        {
            get
            {
                if (_nuevoProductoCommand == null)
                {
                    _nuevoProductoCommand = new RelayCommand(nuevoProductoBtn);
                }
                return _nuevoProductoCommand;
            }
        }

        RelayCommand _buscarClienteCommand;
        public ICommand buscarClienteCommand
        {
            get
            {
                if (_buscarClienteCommand == null)
                {
                    _buscarClienteCommand = new RelayCommand(param => NotifyPropertyChanged("listaProductos"));
                }
                return _buscarClienteCommand;
            }
        }

        RelayCommand _guardarCommand;
        public ICommand guardarCommand
        {
            get
            {
                if (_guardarCommand == null)
                {
                    _guardarCommand = new RelayCommand(guardarBtn);
                }
                return _guardarCommand;
            }
        }

        RelayCommand _editarCommand;
        public ICommand editarCommand
        {
            get
            {
                if (_editarCommand == null)
                {
                    _editarCommand = new RelayCommand(editarProductoBtn);
                }
                return _editarCommand;
            }
        }

        #endregion


        private void nuevoProductoBtn(Object obj)
        {
            //productoTabControl.SelectedIndex = 1;
            //txtCodigo.IsEnabled = true;

        }

        private void editarProductoBtn(Object codigo)
        {
            try
            {
                this.producto = listaProductos.Single(producto => producto.codigo == (String)codigo);
                this.prodAlm = new ProductoAlmacen();
                this.statusTab = (int)tabs.MODIFICAR;
            }
            catch (Exception e)
            {
                //MessageBox.Show(e.Message);
            }
        }


        private void guardarBtn(object obj)
        {
            String header=(String)obj;
            if (header.Contains("Agregar"))
            {

                //Validaciones

                if (MA_ProductoService.agregarNuevoProducto(producto))
                {
                    MessageBox.Show("El producto fue agregado con éxito");
                    producto = new Producto();
                }
                else
                {
                    MessageBox.Show("No se pudo agregar el producto");
                }

                //*************
                
                
            }
            else //Editar
            {
                //Validaciones




                //*************

                MA_ProductoService.actualizarProducto();
            }
            
            //prodAlm = new ProductoAlmacen();
        }

        private void tabBúsqueda_Click(object sender, MouseButtonEventArgs e)
        {
            //enable_disable_campos(true);
        }

    }
}
