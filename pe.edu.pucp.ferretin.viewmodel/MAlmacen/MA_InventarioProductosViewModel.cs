using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pe.edu.pucp.ferretin.viewmodel.Helper;
using System.Windows.Input;
using pe.edu.pucp.ferretin.model;

namespace pe.edu.pucp.ferretin.viewmodel.MAlmacen
{
    class MA_InventarioProductosViewModel : ViewModelBase
    {
        #region Valores para el cuadro de Búsqueda de inventario de productos
        public string _searchAlmacen = "";
        public string searchAlmacen { get { return _searchAlmacen; } set { _searchAlmacen = value; NotifyPropertyChanged("searchAlmacen"); } }

        public string _searchCategoria = "";
        public string searchCategoria { get { return _searchCategoria; } set { _searchCategoria = value; NotifyPropertyChanged("searchCategoria"); } }

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
                    default: detallesTabHeader = "Agregar"; break;
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
            set { statusTab = value == 0 ? Tab.BUSQUEDA : Tab.BUSQUEDA; }//OBSERVACION
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

        #region Lista de inventario de productos
        


        private IEnumerable<ProductoAlmacen> _listaProdTienda;
        public IEnumerable<ProductoAlmacen> listaProdTienda
        {
            get
            {
                /* String searchAlmacen;
                 if (this.searchAlmacen == 1) searchAlmacen="Almacén Central";
                 if (this.searchAlmacen == 2) searchAlmacen = "Tienda 1";
                 if (this.searchAlmacen == 3) searchAlmacen = "Tienda 2";
                 if (this.searchAlmacen == 4) searchAlmacen = "Tienda 3";
               

                 String searchCategoria;
                 if (this.searchCategoria == 1) searchCategoria = "Todas";
                 if (this.searchCategoria == 2) searchCategoria = "Categoria A";
                 if (this.searchCategoria == 3) searchCategoria = "Categoria B";
                 if (this.searchCategoria == 4) searchCategoria = "Categoria C";*/


                //falta crear el MA_inventarioService;
             //   _listaProdTienda = MA_InventarioService.MostrarProductosInventario(searchAlmacen, searchCategoria, searchNombre);




                return _listaProdTienda;
            }
            set
            {
                _listaProdTienda = value;
                NotifyPropertyChanged("listaProdTienda");
            }
        }
        #endregion

        #region RelayCommand
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
        #endregion;
    }
}
