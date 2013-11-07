using pe.edu.pucp.ferretin.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pe.edu.pucp.ferretin.viewmodel.MVentas
{
    public class MV_AdministrarProformasViewModel : ViewModelBase
    {
        #region Atributos del Buscador
        public string codProforma { get; set; }
        public string clienteSearch { get; set; }

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

        public Empleado vendedor { get; set; }

        #endregion

        private bool _soloSeleccionarProforma = false;
        public bool soloSeleccionarProforma
        {
            get
            {
                return _soloSeleccionarProforma;
            }
            set
            {
                _soloSeleccionarProforma = value;
                NotifyPropertyChanged("soloSeleccionarProforma");
                NotifyPropertyChanged("nombreBotonGuardar");
                NotifyPropertyChanged("noSoloSeleccionarProforma");
                detallesTabHeader = value ? "Detalles" : "Agregar";
            }
        }
        public bool noSoloSeleccionarProforma
        {
            get
            {
                return !soloSeleccionarProforma;
            }
        }

        #region Lista Proformas y Edicion de Proforma
        private Proforma _proforma;
        public Proforma proforma
        {
            get
            {
                return _proforma;
            }
            set
            {
                _proforma = value;
                NotifyPropertyChanged("proforma");
                NotifyPropertyChanged("proformaImagen");
            }
        }

        private IEnumerable<Proforma> _listaProformas;
        public IEnumerable<Proforma> listaProformas
        {
            get
            {
                
                //_listaProformas = MV_ProformaService.buscarProformas(searchNroDoc, searchNombre, searchApPaterno, searchApMaterno, searchTipoDocumento);

                return _listaProformas;
            }
            set
            {
                _listaProformas = value;
                NotifyPropertyChanged("listaProformas");
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
                if (value == Tab.DETALLES && proforma == null)
                {

                }
                _statusTab = value;
                //Si cambió el estado de las pestañas también cambio los Header
                //Si la pestaña es para agregar nuevo, limpio los input
                switch (_statusTab)
                {
                    case Tab.BUSQUEDA: detallesTabHeader = soloSeleccionarProforma ? "Detalles" : "Agregar"; break;//Si es agregar, creo un nuevo objeto Proforma
                    case Tab.AGREGAR: detallesTabHeader = "Agregar"; proforma = new Proforma(); break;//Si es agregar, creo un nuevo objeto Proforma
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
                if (soloSeleccionarProforma)
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

    }
}
