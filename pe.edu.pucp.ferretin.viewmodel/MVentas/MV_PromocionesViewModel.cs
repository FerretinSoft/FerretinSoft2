using pe.edu.pucp.ferretin.controller.MVentas;
using pe.edu.pucp.ferretin.model;
using pe.edu.pucp.ferretin.viewmodel.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace pe.edu.pucp.ferretin.viewmodel.MVentas
{
    public class MV_PromocionesViewModel : ViewModelBase
    {

        private bool _soloSeleccionarPromocion = false;
        public bool soloSeleccionarPromocion
        {
            get
            {
                return _soloSeleccionarPromocion;
            }
            set
            {
                _soloSeleccionarPromocion = value;
                NotifyPropertyChanged("soloSeleccionarPromocion");
                NotifyPropertyChanged("nombreBotonGuardar");
                NotifyPropertyChanged("noSoloSeleccionarPromocion");
                detallesTabHeader = value ? "Detalles" : "Agregar";
            }
        }

        #region Atributos del buscador

        private String _codPromSearch="";
        public String codPromSearch
        {
            get
            {
                return _codPromSearch;
            }
            set
            {
                _codPromSearch = value;
                NotifyPropertyChanged("codPromSearch");
            }
        }

        private DateTime _fechaDesdeSearch = DateTime.Today.AddDays(-30);
        public DateTime fechaDesdeSearch
        {
            get
            {
                return _fechaDesdeSearch;
            }
            set
            {
                _fechaDesdeSearch = value;
                NotifyPropertyChanged("fechaDesdeSearch");
            }
        }

        private DateTime _fechaHastaSearch = DateTime.Today;
        public DateTime fechaHastaSearch
        {
            get
            {
                return _fechaHastaSearch;
            }
            set
            {
                _fechaHastaSearch = value;
                NotifyPropertyChanged("fechaHastaSearch");
            }
        }

        private int _estadoSearch = 0;
        public int estadoSearch
        {
            get
            {
                return _estadoSearch;
            }
            set
            {
                _estadoSearch = value;
                NotifyPropertyChanged("estadoSearch");
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
                if (value == Tab.DETALLES && promocion == null)
                {

                }
                _statusTab = value;
                //Si cambió el estado de las pestañas también cambio los Header
                //Si la pestaña es para agregar nuevo, limpio los input
                switch (_statusTab)
                {
                    case Tab.BUSQUEDA: detallesTabHeader = soloSeleccionarPromocion ? "Detalles" : "Agregar"; break;//Si es agregar, creo un nuevo objeto Promocion
                    case Tab.AGREGAR: detallesTabHeader = "Agregar"; promocion = new Promocion(); break;//Si es agregar, creo un nuevo objeto Promocion
                    case Tab.MODIFICAR: detallesTabHeader = "Modificar"; break;
                    case Tab.DETALLES: detallesTabHeader = "Detalles"; break;
                }
                NotifyPropertyChanged("statusTab");
                //Cuando se cambia el status, tambien se tiene que actualizar el currentIndex del tab
                NotifyPropertyChanged("currentIndexTab"); //Hace que cambie el tab automaticamente
            }
        }
        //Usado para mover los tabs de acuerdo a las acciones realizadas
        public int currentIndexTab
        {
            get
            {
                return _statusTab == Tab.BUSQUEDA ? 0 : 1;
            }
            set
            {
                if (soloSeleccionarPromocion)
                {
                    statusTab = value == 0 ? Tab.BUSQUEDA : Tab.DETALLES;
                }
                else
                {
                    statusTab = value == 0 ? Tab.BUSQUEDA : Tab.AGREGAR;
                }
            }
        }
        private String _detallesTabHeader = ""; //Default
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


        #region Lista de Promociones y Promocion a editar
        private Promocion _promocion;
        public Promocion promocion
        {
            get
            {
                return _promocion;
            }
            set
            {
                _promocion = value;
                NotifyPropertyChanged("promocion");
            }
        }

        private IEnumerable<Promocion> _listaPromociones;
        public IEnumerable<Promocion> listaPromociones
        {
            get
            {
                _listaPromociones = MV_PromocionService.buscarPromociones(codPromSearch, fechaDesdeSearch, fechaHastaSearch, estadoSearch);
                return _listaPromociones;
            }
            set
            {
                _listaPromociones = value;
                NotifyPropertyChanged("listaPromociones");
            }
        }
        #endregion

        #region RelayCommand
        RelayCommand _actualizarListaCommand;
        public ICommand actualizarListaCommand
        {
            get
            {
                if (_actualizarListaCommand == null)
                {
                    _actualizarListaCommand = new RelayCommand(param => NotifyPropertyChanged("listaPromociones"));
                }
                return _actualizarListaCommand;
            }
        }
        #endregion

    }
}
