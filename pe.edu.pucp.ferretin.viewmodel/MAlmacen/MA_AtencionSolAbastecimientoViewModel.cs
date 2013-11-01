using pe.edu.pucp.ferretin.controller.MAlmacen;
using pe.edu.pucp.ferretin.model;
using pe.edu.pucp.ferretin.viewmodel.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace pe.edu.pucp.ferretin.viewmodel.MAlmacen
{
    public class MA_AtencionSolAbastecimientoViewModel : ViewModelBase
    {
        #region Valores para el cuadro de Búsqueda

        public Tienda _searchTienda;
        public Tienda searchTienda
        {
            get
            {
                return _searchTienda;
            }
            set
            {
                _searchTienda = value;
                NotifyPropertyChanged("searchTienda");
            }
        }

        public SolicitudAbastecimientoEstado _searchEstado;
        public SolicitudAbastecimientoEstado searchEstado
        {
            get
            {
                return _searchEstado;
            }
            set
            {
                _searchEstado = value;
                NotifyPropertyChanged("searchEstado");
            }
        }

        public DateTime _searchFechaDesde = new DateTime(2000, 1, 1);
        public DateTime searchFechaDesde
        {
            get
            {
                return _searchFechaDesde;
            }
            set
            {
                _searchFechaDesde = value;
                NotifyPropertyChanged("searchFechaDesde");
            }
        }

        public DateTime _searchFechaHasta = DateTime.Today;
        public DateTime searchFechaHasta
        {
            get
            {
                return _searchFechaHasta;
            }
            set
            {
                _searchFechaHasta = value;
                NotifyPropertyChanged("searchFechaHasta");
            }
        }

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
                    case Tab.BUSQUEDA: detallesTabHeader = "Detalles"; break;
                    case Tab.DETALLES: detallesTabHeader = "Detalles"; break;
                    default: detallesTabHeader = "Detalles"; break;//Si es agregar, creo un nuevo objeto Almacen
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
            set { statusTab = value == 0 ? Tab.BUSQUEDA : Tab.DETALLES; }
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

        #region Lista de Solicitudes y Edición de Solicitudes

        public Tienda currentAlmacen
        {
            get
            {
                return almacenes.ElementAt(0);
            }
        }

        private SolicitudAbastecimiento _solicitud;
        public SolicitudAbastecimiento solicitud
        {
            get
            {
                return _solicitud;
            }
            set
            {
                _solicitud = value;
                NotifyPropertyChanged("solicitud");
            }
        }

        private IEnumerable<SolicitudAbastecimiento> _listaSolicitudes;
        public IEnumerable<SolicitudAbastecimiento> listaSolicitudes
        {
            get
            {
                _listaSolicitudes = MA_SolicitudAbastecimientoService
                    .buscar(currentAlmacen, ((searchTienda != null && searchTienda.nombre == "Todas")?null:searchTienda), searchEstado, searchFechaDesde, searchFechaHasta);
                return _listaSolicitudes;
            }
            set
            {
                _listaSolicitudes = value;
                NotifyPropertyChanged("listaSolicitudes");
            }
        }

        #endregion

        #region RelayCommand

        RelayCommand _actualizarListaSolicitudesCommand;
        public ICommand actualizarListaSolicitudesCommand
        {
            get
            {
                if (_actualizarListaSolicitudesCommand == null)
                {
                    _actualizarListaSolicitudesCommand = new RelayCommand(param => NotifyPropertyChanged("listaSolicitudes"));
                }
                return _actualizarListaSolicitudesCommand;
            }
        }

        RelayCommand _viewEditSolicitudCommand;
        public ICommand viewEditSolicitudCommand
        {
            get
            {
                if (_viewEditSolicitudCommand == null)
                {
                    _viewEditSolicitudCommand = new RelayCommand(viewEditSolicitud);
                }
                return _viewEditSolicitudCommand;
            }
        }

        RelayCommand _atenderSolicitudCommand;
        public ICommand atenderSolicitudCommand
        {
            get
            {
                if (_atenderSolicitudCommand == null)
                {
                    _atenderSolicitudCommand = new RelayCommand(atenderSolicitud);
                }
                return _atenderSolicitudCommand;
            }
        }

        RelayCommand _anularSolicitudCommand;
        public ICommand anularSolicitudCommand
        {
            get
            {
                if (_anularSolicitudCommand == null)
                {
                    _anularSolicitudCommand = new RelayCommand(anularSolicitud);
                }
                return _anularSolicitudCommand;
            }
        }

        RelayCommand _cancelSolicitudCommand;
        public ICommand cancelSolicitudCommand
        {
            get
            {
                if (_cancelSolicitudCommand == null)
                {
                    _cancelSolicitudCommand = new RelayCommand(cancelSolicitud);
                }
                return _cancelSolicitudCommand;
            }
        }

        #endregion

        #region Comandos

        public void viewEditSolicitud(Object id)
        {
            try
            {
                int idSolicitud = Int32.Parse(id.ToString());
                this.solicitud = listaSolicitudes.Single(sol => sol.id == idSolicitud);
                this.statusTab = Tab.DETALLES;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public void atenderSolicitud(Object obj)
        {

            if (solicitud.id > 0)//Si existe
            {
                if (!MA_SolicitudAbastecimientoService.atenderSolicitud(currentAlmacen, solicitud))
                {
                    MessageBox.Show("No se pudo atender la solicitud de abastecimiento");
                }
                else
                {
                    solicitud.SolicitudAbastecimientoEstado = estadoSolicitud.FirstOrDefault(e => e.nombre == "Atendida");
                    //falta descontar el stock del almacen central, para cada producto de la solicitud
                    if (!MA_SolicitudAbastecimientoService.enviarCambios())
                    {
                        MessageBox.Show("No se pudo atender la solicitud de abastecimiento");
                    }
                    else
                    {
                        MessageBox.Show("La solicitud de abastecimiento fue atendida con éxito");
                    }
                }
                NotifyPropertyChanged("solicitud");
                NotifyPropertyChanged("listaSolicitudes");
                
            }
        }

        public void anularSolicitud(Object obj)
        {

            if (solicitud.id > 0)//Si existe
            {
                solicitud.SolicitudAbastecimientoEstado = estadoSolicitud.FirstOrDefault(e => e.nombre == "Anulada");
                if (!MA_SolicitudAbastecimientoService.enviarCambios())
                {
                    MessageBox.Show("No se pudo anular la solicitud de abastecimiento");
                }
                else
                {
                    MessageBox.Show("La solicitud de abastecimiento fue anulada con éxito");
                }
                NotifyPropertyChanged("solicitud");
                NotifyPropertyChanged("listaSolicitudes");

            }
        }

        public void cancelSolicitud(Object obj)
        {
            this.statusTab = Tab.BUSQUEDA;
            listaSolicitudes = MA_SolicitudAbastecimientoService.listaSolicitudes;
        }

        
        #endregion


    }
}
