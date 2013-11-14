using pe.edu.pucp.ferretin.controller.MAlmacen;
using pe.edu.pucp.ferretin.controller.MCompras;
using pe.edu.pucp.ferretin.model;
using pe.edu.pucp.ferretin.viewmodel.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace pe.edu.pucp.ferretin.viewmodel.MCompras
{
    public class MC_ProveedoresViewModel:ViewModelBase
    {


        public string _labelCodigo = null;
        public string labelCodigo
        {
            get
            {
                return _labelCodigo;
            }
            set
            {
                _labelCodigo = value;
                NotifyPropertyChanged("labelCodigo");
            }
        }
        public string _labelNombre = null;
        public string labelNombre
        {
            get
            {
                return _labelNombre;
            }
            set
            {
                _labelNombre = value;
                NotifyPropertyChanged("labelNombre");
            }
        }


        private bool _soloSeleccionarProveedor = false;
        public bool soloSeleccionarProveedor
        {
            get
            {
                return _soloSeleccionarProveedor;
            }
            set
            {
                _soloSeleccionarProveedor = value;
                NotifyPropertyChanged("soloSeleccionarProveedor");
                NotifyPropertyChanged("nombreBotonGuardar");
                NotifyPropertyChanged("noSoloSeleccionarProveedor");
                detallesTabHeader = value ? "Detalles" : "Agregar";
            }
        }
        public bool noSoloSeleccionarProveedor
        {
            get
            {
                return !soloSeleccionarProveedor;
            }
        }
        public String nombreBotonGuardar
        {
            get
            {
                return soloSeleccionarProveedor ? "SELECCIONAR" : "GUARDAR";
            }
        }
        
        #region Constructor
        public MC_ProveedoresViewModel()
        {
            _proveedor = new Proveedor();
        }
        #endregion

        public bool isCreating
        {
            get
            {
                return true;
            }
        }

        private IEnumerable<Rubro> _rubros;
        public IEnumerable<Rubro> rubros
        {
            get
            {
                return _rubros;
            }
            set
            {
                _rubros = value;
                NotifyPropertyChanged("rubros");
            }
        }

        private ProveedorProducto _provProd;
        public ProveedorProducto provProd
        {
            get
            {
                return _provProd;
            }
            set
            {
                _provProd = value;
                NotifyPropertyChanged("provProd");
            }
        }

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
                NotifyPropertyChanged("prod");
            }
        }

      public string codProdAgregar { get; set; }

        public IEnumerable<Rubro> listaRubros
        {
            get
            {
                //Creo una nueva secuencia
                var sequence = Enumerable.Empty<Rubro>();
                //Primero agrego un item de Todos para que salga al inicio
                //Pongo el ID en 0 para que al buscar, no filtre nada cuando se selecciona todos
              
                    IEnumerable<Rubro> items = new Rubro[] { new Rubro { id = 0, nombre = "Todos" } };
               
                
                //Luego concateno el itemcon los elementos del combobox
                return items.Concat(MC_RubroService.rubro);
            }

        }

        private Rubro _selectedRubro;
        public Rubro selectedRubro
        {
            get
            {
                return _selectedRubro;
            }
            set
            {
                _selectedRubro = value;
                NotifyPropertyChanged("selectedRubro");
             

            }
        }


        #region lista de Proveedores
        private Proveedor _proveedor;
        public Proveedor proveedor
        {
            get
            {
                return _proveedor;
            }

            set
            {
                _proveedor = value;
               
                NotifyPropertyChanged("proveedor");
            }
        }
        private IEnumerable<Proveedor> _listaProveedores;
        public IEnumerable<Proveedor> listaProveedores
        {
            get
            {
                String searchTipoDocumento = this.searchTipoDocumento == 1 ? "EMPRESA" : (this.searchTipoDocumento == 2 ? "PERSONA NATURAL" : "");
                _listaProveedores = MC_ProveedorService.buscarProveedores(searchRuc, searchRazonSoc, searchRubro,searchTipoDocumento);
                return _listaProveedores;
            }
            set
            {
                _listaProveedores = value;
                NotifyPropertyChanged("listaProveedores");
                NotifyPropertyChanged("proveedor.ProveedorProducto");
                
            }

        }

        private IEnumerable<ProveedorProducto> _listaProductos;
        public IEnumerable<ProveedorProducto> listaProductos
        {
            get
            {
                return _listaProductos;
            }
            set
            {
                _listaProductos = value;
                NotifyPropertyChanged("proveedor.ProveedorProducto");
            }
        }

    

        #endregion

        #region Valores para el cuadro de Búsqueda
        public String _searchRuc = "";
        public String searchRuc { get { return _searchRuc; } set { _searchRuc = value; NotifyPropertyChanged("searchRuc"); } }

        public String _searchRazonSoc = "";
        public String searchRazonSoc { get { return _searchRazonSoc; } set { _searchRazonSoc = value; NotifyPropertyChanged("searchRazonSoc"); } }

        public Rubro _searchRubro = null;
        public Rubro searchRubro { get { return _searchRubro; } set { _searchRubro = value; NotifyPropertyChanged("searchRubro"); } }
        public int _searchTipoDocumento = 0;
        public int searchTipoDocumento { get { return _searchTipoDocumento; } set { _searchTipoDocumento = value; NotifyPropertyChanged("searchTipoDocumento"); } }
       
        #endregion

        #region RalayCommand

        RelayCommand _actualizarListaProductosCommand;
        public ICommand actualizarListaProductosCommand
        {
            get
            {
                if (_actualizarListaProductosCommand == null)
                {
                    _actualizarListaProductosCommand = new RelayCommand(param => NotifyPropertyChanged("proveedor.ProveedorProducto"));
                }
                return _actualizarListaProductosCommand;
            }
        }

        RelayCommand _actualizarListaProveedoresCommand;
        public ICommand actualizarListaProveedoresCommand
        {
            get
            {
                if (_actualizarListaProveedoresCommand == null)
                {
                    _actualizarListaProveedoresCommand = new RelayCommand(param => NotifyPropertyChanged("listaProveedores"));
                }
                return _actualizarListaProveedoresCommand;
            }
        }

        RelayCommand _viewEditProveedoresCommand;



        public ICommand viewEditProveedoresCommand
        {
            get
            {
                if (_viewEditProveedoresCommand == null)
                {
                    _viewEditProveedoresCommand = new RelayCommand(viewEditProveedor);
                }
                return _viewEditProveedoresCommand;
            }
        }
        RelayCommand _saveProveedoresCommand;
        public ICommand saveProveedoresCommand
        {
            get
            {
                if (_saveProveedoresCommand == null)
                {
                    _saveProveedoresCommand = new RelayCommand(saveProveedor, canSaveExecute);
                }
                return _saveProveedoresCommand;
            }
        }
        RelayCommand _cancelProveedorCommand;
        public ICommand cancelProveedorCommand
        {
            get
            {
                if (_cancelProveedorCommand == null)
                {
                    _cancelProveedorCommand = new RelayCommand(cancelProveedor);
                }
                return _cancelProveedorCommand;
            }
        }

        RelayCommand _agregarProveedorCommand;
        public ICommand agregarProveedorCommand
        {
            get
            {
                if (_agregarProveedorCommand == null)
                {
                    _agregarProveedorCommand = new RelayCommand(p => statusTab = Tab.AGREGAR);
                }
                return _agregarProveedorCommand;
            }
        }

        RelayCommand _agregarNuevoProductoCommand;
        public ICommand agregarNuevoProductoCommand
        {
            get
            {
                if (_agregarNuevoProductoCommand == null)
                {
                    _agregarNuevoProductoCommand = new RelayCommand(agregarProducto);
                    NotifyPropertyChanged("proveedor.ProveedorProducto");
                }
                return _agregarNuevoProductoCommand;
            }
        }
        #endregion

        #region Comandos

        public void viewEditProveedor(Object id)
        {
            try
            {
                this.proveedor= listaProveedores.Single(proveedor => proveedor.id == (int)id);
                this.listaProductos = MC_ProveedorService.obtenerProductosbyIdProveedor((int)id);
                if (this.proveedor.id_ubigeo != null)

                {
                    
                    selectedProvincia = this.proveedor.UbigeoDistrito.UbigeoProvincia;
                    selectedDepartamento = selectedProvincia.UbigeoDepartamento;
                }

                if (this.proveedor.id_rubro != null)
                {
                    selectedRubro = this.proveedor.Rubro;
                }
                if (soloSeleccionarProveedor)
                    this.statusTab = Tab.DETALLES;
                else
                    this.statusTab = Tab.MODIFICAR;
                
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        public void saveProveedor(Object obj)
        {
            if (soloSeleccionarProveedor)
            {

            }
            else
            {
                if (proveedor.id > 0)//Si existe
                {
                    MC_ComunService.idVentana(31);
                    if (!MC_ProveedorService.enviarCambios())
                    {
                        MessageBox.Show("No se pudo actualizar al Proveedor");
                    }
                    else
                    {
                        MessageBox.Show("El Proveedor fue guardado con éxito");
                    }
                }
                else
                {
                    proveedor.estado = 1;

                    MC_ComunService.idVentana(30);
                    if (!MC_ProveedorService.insertarProveedor(proveedor))
                    {
                        MessageBox.Show("No se pudo agregar el nuevo proveedor");
                    }
                    else
                    {
                        MessageBox.Show("El proveedor fue agregado con éxito");
                        this.statusTab = Tab.BUSQUEDA;
                        listaProveedores = MC_ProveedorService.listaProveedores;
                    }
                }
            }
        }

        public void saveProducto(Object obj)
        {
            provProd = (ProveedorProducto)obj;
            if (provProd.id_producto > 0)//Si existe
            {
                MC_ComunService.idVentana(31);
                if (!MC_ProveedorService.enviarCambios())
                {
                    MessageBox.Show("No se pudo actualizar al Producto");
                }
                else
                {
                    MessageBox.Show("El Producto fue guardado con éxito");
                    NotifyPropertyChanged("proveedor.ProveedorProducto");
                }
            }
            else
            {

                if (!MC_ProveedorService.InsertarProducto(provProd))
                {
                    MessageBox.Show("No se pudo agregar el nuevo proveedor");
                }
                else
                {
                    MessageBox.Show("El producto fue agregado con éxito");
                    this.statusTab = Tab.BUSQUEDA;
                    listaProductos = MC_ProveedorService.listaProductos;
                }
            }
        }

        public void cancelProveedor(Object obj)
        {
            this.statusTab = Tab.BUSQUEDA;
            listaProveedores = MC_ProveedorService.listaProveedores;
        }

        public void agregarProducto(Object obj)
        {
            if (codProdAgregar != null && codProdAgregar.Length > 0)
            {
                Producto producto = null;
                try
                {
                    producto = MA_SharedService.obtenerProductoxCodigo(codProdAgregar);
                    string nombre = producto.nombre;
                }
                catch { }

                if (producto != null)
                {
                    ProveedorProducto pP = null;
                    if (proveedor.ProveedorProducto.Count(vp => vp.Producto.id == producto.id) == 1)
                    {
                        //saveProducto(pP);
                        NotifyPropertyChanged("proveedor");
                    }
                    else
                    {
                        pP = new ProveedorProducto()
                        {
                            Producto = producto,
                            Proveedor=proveedor,
                            precio = 4,
                            tiempoEntrega="",
                            estado=1
                        };
                        proveedor.ProveedorProducto.Add(pP);
                        NotifyPropertyChanged("proveedor");
                         //saveProducto(pP);
                    }
                    NotifyPropertyChanged("proveedor.PoveedorProducto");
                }
                
            }
            else
            {
                MessageBox.Show("No se encontro ningun producto con el código proporcionado");
            }
            
        }
        #endregion

        #region Manejo de los Tabs
        public enum Tab
        {
            //Pestañas virtuales:
            //0       1        2          3
            BUSQUEDA, AGREGAR, MODIFICAR, DETALLES
        }
        private Tab _statusTab = Tab.BUSQUEDA; //pestaña default 
        public Tab statusTab
        {
            get
            {
                return _statusTab;
            }
            set
            {
                if (value == Tab.DETALLES && proveedor == null)
                {

                }
                _statusTab = value;
                //Si cambió el estado de las pestañas también cambio los Header
                //Si la pestaña es para agregar nuevo, limpio los input
                switch (_statusTab)
                {
                    case Tab.BUSQUEDA: detallesTabHeader = soloSeleccionarProveedor ? "Detalles" : "Agregar"; break;
                    case Tab.AGREGAR: detallesTabHeader = "Agregar"; proveedor = new Proveedor(); labelCodigo = "Código:"; labelNombre = "Nombre Proveedor:"; NotifyPropertyChanged("labelCodigo");
                        NotifyPropertyChanged("labelNombre"); break;
                    case Tab.MODIFICAR: detallesTabHeader = "Modificar"; if (this.proveedor.tipo == "EMPRESA")
                        {
                            labelCodigo = "RUC :";
                            labelNombre = "Razón Social :";
                            NotifyPropertyChanged("labelCodigo");
                            NotifyPropertyChanged("labelNombre");

                        }
                        if (this.proveedor.tipo == "PERSONA NATURAL")
                        {
                            labelCodigo = "DNI :";
                            labelNombre = "Nombre :";
                            NotifyPropertyChanged("labelNombre");
                            NotifyPropertyChanged("labelCodigo");
                        } break;
                    case Tab.DETALLES: detallesTabHeader = "Detalles"; break;
                   // default: detallesTabHeader = "Agregar"; proveedor = new Proveedor(); break;
                }
                NotifyPropertyChanged("statusTab");
                //Cuando se cambia el status, tambien se tiene que actualizar el currentIndex del tab
                NotifyPropertyChanged("currentIndexTab"); //Hace que cambie el tab automaticamente
             
            }
        }
        //Usado para mover los tabs de acuerdo a las acciones realizadas
        public int currentIndexTab
        {
            get { return _statusTab == Tab.BUSQUEDA ? 0 : 1; }

            set {
                if (soloSeleccionarProveedor)
                {
                    statusTab = value == 0 ? Tab.BUSQUEDA : Tab.DETALLES;
                }
                else
                {
                    statusTab = value == 0 ? Tab.BUSQUEDA : Tab.AGREGAR;
                }
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


       
       
        #endregion

        private bool canSaveExecute(object obj)
        {
            if (soloSeleccionarProveedor)
            {
                return proveedor != null;
            }
            return base.UIValidationErrorCount == 0 && this.proveedor.Errors.Count == 0;
        }
    }
}
