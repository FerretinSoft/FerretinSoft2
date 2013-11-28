using pe.edu.pucp.ferretin.controller;
using pe.edu.pucp.ferretin.controller.MAlmacen;
using pe.edu.pucp.ferretin.controller.MSeguridad;
using pe.edu.pucp.ferretin.controller.MVentas;
using pe.edu.pucp.ferretin.model;
using pe.edu.pucp.ferretin.viewmodel.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace pe.edu.pucp.ferretin.viewmodel.MVentas
{
    public class MV_ServiciosViewModel :ViewModelBase
    {



        private bool _soloSeleccionarServicio = false;
        public bool soloSeleccionarServicio
        {
            get
            {
                return _soloSeleccionarServicio;
            }
            set
            {
                _soloSeleccionarServicio = value;
                if (value == true)
                {
                    estadoSearch = 1;
                }
                NotifyPropertyChanged("soloSeleccionarServicio");
                NotifyPropertyChanged("nombreBotonGuardar");
                NotifyPropertyChanged("noSoloSeleccionarServicio");
                NotifyPropertyChanged("esAgregar");
                NotifyPropertyChanged("listaServicios");

                detallesTabHeader = value ? "Detalles" : "Agregar";
            }
        }

        public string nombreBotonGuardar
        {
            get
            {
                return soloSeleccionarServicio ? "SELECCIONAR" : "GUARDAR";
            }
        }

        public bool noSoloSeleccionarServicio
        {
            get
            {
                return !soloSeleccionarServicio;
            }
        }

        public bool esAgregar
        {
            get
            {
                return statusTab == Tab.AGREGAR;
            }
        }

        #region Atributos del buscador

        private String _codServSearch = "";
        public String codServSearch
        {
            get
            {
                return _codServSearch;
            }
            set
            {
                _codServSearch = value;
                NotifyPropertyChanged("codServSearch");
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
                if (value == Tab.DETALLES && servicio == null)
                {

                }
                _statusTab = value;
                //Si cambió el estado de las pestañas también cambio los Header
                //Si la pestaña es para agregar nuevo, limpio los input
                switch (_statusTab)
                {
                    case Tab.BUSQUEDA:
                        {
                            if(!soloSeleccionarServicio) ComunService.Clean();
                            detallesTabHeader = soloSeleccionarServicio ? "Detalles" : "Agregar"; break;//Si es agregar, creo un nuevo objeto Servicio
                        }
                    case Tab.AGREGAR:
                        {
                            detallesTabHeader = "Agregar";
                            servicio = new Servicio()
                            {
                                Empleado = ComunService.usuarioL.Empleado,
                                montoAdicional = 0
                            };
                            nroDocSeleccionado = null;
                            servicio.ServicioLinea.ListChanged += ServicioLinea_ListChanged;
                            break;
                        }
                    case Tab.MODIFICAR: detallesTabHeader = "Modificar"; break;
                    case Tab.DETALLES: detallesTabHeader = "Detalles"; break;
                }
                NotifyPropertyChanged("statusTab");
                //Cuando se cambia el status, tambien se tiene que actualizar el currentIndex del tab
                NotifyPropertyChanged("currentIndexTab"); //Hace que cambie el tab automaticamente
                NotifyPropertyChanged("esAgregar");
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
                if (soloSeleccionarServicio)
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


        #region Lista de Servicioes y Servicio a editar
        private Servicio _servicio;
        public Servicio servicio
        {
            get
            {
                return _servicio;
            }
            set
            {
                _servicio = value;
                NotifyPropertyChanged("servicio");
            }
        }

        private IEnumerable<Servicio> _listaServicios;
        public IEnumerable<Servicio> listaServicios
        {
            get
            {
                _listaServicios = MV_ServicioService.buscarServicios(codServSearch, fechaDesdeSearch, fechaHastaSearch, estadoSearch).ToList().OrderBy(s => s.fechaRegistro);
                return _listaServicios;
            }
            set
            {
                _listaServicios = value;
                NotifyPropertyChanged("listaServicios");
            }
        }

        #endregion


        public string codServTipoAgregar { get; set; }




        #region RelayCommand
        RelayCommand _agregarServicioTipoCommand;
        public ICommand agregarServicioTipoCommand
        {
            get
            {
                if (_agregarServicioTipoCommand == null)
                {
                    _agregarServicioTipoCommand = new RelayCommand(agregarServicioTipo);
                }
                return _agregarServicioTipoCommand;
            }
        }

        RelayCommand _actualizarListaCommand;
        public ICommand actualizarListaCommand
        {
            get
            {
                if (_actualizarListaCommand == null)
                {
                    _actualizarListaCommand = new RelayCommand(actualiarLista);
                }
                return _actualizarListaCommand;
            }
        }


        RelayCommand _viewEditServicioCommand;
        public ICommand viewEditServicioCommand
        {
            get
            {
                if (_viewEditServicioCommand == null)
                {
                    _viewEditServicioCommand = new RelayCommand(viewEditServicio);
                }
                return _viewEditServicioCommand;
            }
        }

        RelayCommand _saveServicioCommand;
        public ICommand saveServicioCommand
        {
            get
            {
                if (_saveServicioCommand == null)
                {
                    _saveServicioCommand = new RelayCommand(saveServicio,canSaveOrSelectService);
                }
                return _saveServicioCommand;
            }
        }


        RelayCommand _cancelServicioCommand;
        public ICommand cancelServicioCommand
        {
            get
            {
                if (_cancelServicioCommand == null)
                {
                    _cancelServicioCommand = new RelayCommand(cancelServicio);
                }
                return _cancelServicioCommand;
            }
        }

        RelayCommand _nuevoServicioCommand;
        public ICommand nuevoServicioCommand
        {
            get
            {
                if (_nuevoServicioCommand == null)
                {
                    _nuevoServicioCommand = new RelayCommand(p=>statusTab=Tab.AGREGAR);
                }
                return _nuevoServicioCommand;
            }
        }

        #endregion

        #region Comandos

        public void viewEditServicio(Object id)
        {
            try
            {
                this.servicio = MV_ServicioService.db.Servicio.Single(servicio => servicio.id == (int)id);
                this.statusTab = Tab.DETALLES;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        public void saveServicio(Object obj)
        {
            if (soloSeleccionarServicio)
            {

            }
            else
            {
                String result = String.Empty;
                if (servicio != null)
                {

                    if (servicio.fechaInstalacion != null )
                    {
                        if (DateTime.Compare(DateTime.Today, servicio.fechaInstalacion.Value) >= 0)
                        {
                            result = "La fecha de instalación debe ser mayor a la fecha actual.";
                        }
                        else
                        {
                            if (servicio.Cliente == null)
                            {
                                result = "Debe seleccionar un Cliente";
                            }
                            else
                            {
                                if (servicio.ServicioLinea.Count <= 0)
                                {
                                    result = "Debe agregar al menos una Actividad para el Servicio";
                                }
                                else
                                {
                                    if (String.IsNullOrEmpty(servicio.detalles))
                                    {
                                        var cant = servicio.ServicioLinea.Sum(sl => sl.montoAdicional) + servicio.montoAdicional;
                                        if (cant > 0)
                                        {
                                            result = "Si agregó un monto adicional debe especificarlo en los detalles";
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        result = "Debe seleccionar una fecha para la Instalación";
                    }
                    
                }
                if (result.Length > 0)
                {
                    MessageBox.Show(result, "Mensaje", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return;
                }

                if (servicio.id > 0)//Si existe
                {
                    ComunService.idVentana(59);
                    if (!MV_ServicioService.enviarCambios())
                    {
                        MessageBox.Show("No se pudo actualizar el servicio");
                    }
                    else
                    {
                        MessageBox.Show("El servicio fue guardado con éxito");
                        listaServicios = null;
                        statusTab = Tab.BUSQUEDA;
                    }
                }
                else
                {
                    ComunService.idVentana(59);
                    if (!MV_ServicioService.insertarServicio(servicio))
                    {
                        MessageBox.Show("No se pudo agregar el nuevo servicio");
                    }
                    else
                    {
                        listaServicios = null;
                        statusTab = Tab.BUSQUEDA;
                        MessageBox.Show("El servicio fue agregado con éxito");
                    }
                }
            }
        }

        private void actualiarLista(object obj)
        {
            NotifyPropertyChanged("listaServicios");
        }
        public void cancelServicio(Object obj)
        {
            if (!soloSeleccionarServicio && esAgregar)
            {
                MessageBoxResult result = MessageBox.Show("Al salir, perderá todos los datos ingresados. ¿Desea continuar?",
                                            "ATENCIÓN", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                if (result == MessageBoxResult.OK)
                {
                    ComunService.Clean();
                    listaServicios = null;
                    this.statusTab = Tab.BUSQUEDA;
                }
            }
            else
            {
                this.statusTab = Tab.BUSQUEDA;
            }
        }

        private bool canSaveOrSelectService(object obj)
        {
            return (soloSeleccionarServicio || esAgregar);
        }


        public void agregarServicioTipo(Object id)
        {
            if (codServTipoAgregar != null && codServTipoAgregar.Length > 0)
            {
                ServicioTipo servicioTipo = null;
                try
                {
                    servicioTipo = MV_ServicioService.obtenerServicioTipoxCodigo(codServTipoAgregar);
                }
                catch { }

                if (servicioTipo != null)
                {
                    ServicioLinea servicioLinea = null;
                    if (servicio.ServicioLinea.Count(vp => vp.ServicioTipo.id == servicioTipo.id) == 1)
                    {
                        servicio.ServicioLinea.Single(vp => vp.ServicioTipo.id == servicioTipo.id).cantidad++;
                    }
                    else
                    {
                        servicioLinea = new ServicioLinea()
                        {
                            Servicio = servicio,
                            ServicioTipo = servicioTipo,
                            cantidad = 1,
                            montoParcial = servicioTipo.montoBase,
                            montoAdicional = 0,
                            montoTotal = servicioTipo.montoBase,
                            descripcion = servicioTipo.descripcion
                        };
                        servicioLinea.PropertyChanged += servicio_PropertyChanged;
                        servicio.ServicioLinea.Add(servicioLinea);
                        calcularMontoTotal();
                    }
                    NotifyPropertyChanged("servicio");
                }
                else
                {
                    MessageBox.Show("No se encontró ningún tipo de Servicio con el código proporcionado");
                }
            }
        }


        /// <summary>
        /// Cuando cambia la lista, se agrega o elimina algo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ServicioLinea_ListChanged(object sender, System.ComponentModel.ListChangedEventArgs e)
        {
            calcularMontoTotal();
        }

        /// <summary>
        /// Cuando cambia una propiedad de ServicioProducto
        /// </summary>
        /// <param name="sender">ServicioProducto</param>
        /// <param name="e"></param>
        void servicio_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            var sl = sender as ServicioLinea;
            sl.montoTotal = (sl.montoParcial * sl.cantidad) + sl.montoAdicional;
            calcularMontoTotal();
        }

        public void calcularMontoTotal()
        {
            servicio.montoTotal = servicio.ServicioLinea.Sum(sl => sl.montoTotal) + (servicio.montoAdicional??0);
            NotifyPropertyChanged("servicio");
        }
        #endregion

        private long? _nroDocSeleccionado = null;
        public long? nroDocSeleccionado
        {
            get
            {
                return _nroDocSeleccionado;
            }
            set
            {
                _nroDocSeleccionado = value;
                NotifyPropertyChanged("nroDocSeleccionado");
            }
        }
    }
}
