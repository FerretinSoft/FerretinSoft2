using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pe.edu.pucp.ferretin.viewmodel.Helper;
using System.Windows.Input;
using pe.edu.pucp.ferretin.model;
using System.Windows;
using pe.edu.pucp.ferretin.controller.MAlmacen;

namespace pe.edu.pucp.ferretin.viewmodel.MAlmacen
{
    public class MA_InventarioProductosViewModel : ViewModelBase
    {
        #region Valores para el cuadro de Búsqueda de inventario de productos
        public Tienda _searchAlmacen = null;
        public Tienda searchAlmacen { get { return _searchAlmacen; } set { _searchAlmacen = value; NotifyPropertyChanged("searchAlmacen"); } }

        public Categoria _searchCategoria = null;
        public Categoria searchCategoria { get { return _searchCategoria; } set { _searchCategoria = value; NotifyPropertyChanged("searchCategoria"); } }

        public String _searchNombre = "";
        public String searchNombre { get { return _searchNombre; } set { _searchNombre = value; NotifyPropertyChanged("_searchNombre"); } }

        #endregion


        #region Manejo de los Tabs
        public enum Tab
        {
            //Pestañas virtuales:
            //0       1        
            BUSQUEDA, DETALLES
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
                _statusTab = value;
                //Si cambió el estado de las pestañas también cambio los Header
                //Si la pestaña es para agregar nuevo, limpio los input
                switch (_statusTab)
                {
                    case Tab.BUSQUEDA: detallesTabHeader = "Busqueda"; break;
                    case Tab.DETALLES: detallesTabHeader = "Detalles"; break;
                    default: detallesTabHeader = "Busqueda"; break;
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
            set { statusTab = value == 0 ? Tab.BUSQUEDA : Tab.DETALLES; }//OBSERVACION
        }
        private String _detallesTabHeader = "Detalles"; //Default
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

        #region lista para combobox
        public IEnumerable<Categoria> Categorias
        {
            get
            {
                //Creo una nueva secuencia
                var sequence = Enumerable.Empty<Categoria>();
                //Primero agrego un item de Todos para que salga al inicio
                //Pongo el ID en 0 para que al buscar, no filtre nada cuando se selecciona todos
                IEnumerable<Categoria> items = new Categoria[] { new Categoria{ id = 0, nombre = "Todos" } };
                //Luego concateno el itemcon los elementos del combobox
                return items.Concat(MA_InventarioService.listaCategoria);
            }
        }


        public IEnumerable<Tienda> Tiendas
        {
            get
            {
                //Creo una nueva secuencia
                var sequence = Enumerable.Empty<Tienda>();
                //Primero agrego un item de Todos para que salga al inicio
                //Pongo el ID en 0 para que al buscar, no filtre nada cuando se selecciona todos
                IEnumerable<Tienda> items = new Tienda[] { new Tienda { id = 0, nombre = "Todos" } };
                //Luego concateno el itemcon los elementos del combobox
                return items.Concat(MA_InventarioService.listaAlmacen);
            }
        }

        #endregion

        #region Lista de inventario de productos y detalle producto

        private Producto _producto;
        private ProductoAlmacen _productoAlmacen;
        private Tienda _tienda;


        //para mostrar los detalles.  
        #region
        public Producto producto
        {
            get
            {
                return _producto;
            }
            set
            {
                _producto = value;

                NotifyPropertyChanged("producto");
            }
        }

        public ProductoAlmacen productoAlmacen
        {
            get
            {
                return _productoAlmacen;
            }
            set
            {
                _productoAlmacen = value;

                NotifyPropertyChanged("productoAlmacen");
            }
        }


        public Tienda tienda
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

        #endregion

        private IEnumerable<Producto> _listaProducto;
        public IEnumerable<Producto> listaProducto
        {
            get
            {

                _listaProducto = MA_InventarioService.obtenerProductosPorAlmacenCategoriaNombre(searchNombre, searchAlmacen, searchCategoria);
                return _listaProducto;
            }
            set
            {
                _listaProducto = value;
                NotifyPropertyChanged("listaProducto");
            }
        }
        #endregion


        #region RelayCommand

        //Permite  actualizar la lista de inventario de productos
        RelayCommand _actualizarListaInventarioCommand;
        public ICommand actualizarListaInventarioCommand
        {
            get
            {
                if (_actualizarListaInventarioCommand == null)
                {
                    _actualizarListaInventarioCommand = new RelayCommand(param => NotifyPropertyChanged("listaProducto"));
                }
                return _actualizarListaInventarioCommand;
            }
        }

        //Enlace para ver el detalle de inventario de productos
        RelayCommand _viewDetalleInventarioCommand;
        public ICommand viewDetalleInventarioCommand
        {
            get
            {
                if (_viewDetalleInventarioCommand == null)
                {
                    _viewDetalleInventarioCommand = new RelayCommand(viewDetalleInventario);
                }
                return _viewDetalleInventarioCommand;
            }
        }
        #endregion;

        #region comandos

        //detalle de los productos de inventario
        private void viewDetalleInventario(object id)
        {
            try
            {
                this.producto = listaProducto.Single(producto => producto.id == (int)id);

                this.statusTab = Tab.DETALLES;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        #endregion;
    }
}
