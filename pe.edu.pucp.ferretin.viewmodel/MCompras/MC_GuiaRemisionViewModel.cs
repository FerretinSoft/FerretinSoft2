using System;
using System.Collections.Generic;
using System.Linq;
using pe.edu.pucp.ferretin.model;
using pe.edu.pucp.ferretin.viewmodel.Helper;
using pe.edu.pucp.ferretin.controller.MCompras;
using System.Windows.Input;
using System.Windows;
using pe.edu.pucp.ferretin.controller;
using pe.edu.pucp.ferretin.controller.MSeguridad;

namespace pe.edu.pucp.ferretin.viewmodel.MCompras
{
    public class MC_GuiaRemisionViewModel : ViewModelBase
    {
        #region Constructor
        public MC_GuiaRemisionViewModel()
        {
            _guiaRemision = new GuiaRemision();  
        }
        #endregion

        #region Valores para el cuadro de Búsqueda

        public String _searchCodigo = "";
        public String searchCodigo { get { return _searchCodigo; } set { _searchCodigo = value; NotifyPropertyChanged("searchCodigo"); } }

        public String _searchProveedor = "";
        public String searchProveedor { get { return _searchProveedor; } set { _searchProveedor = value; NotifyPropertyChanged("searchProveedor"); } }
        
        public DateTime? _searchFechaDesde = null;
        public DateTime? searchFechaDesde { get { return _searchFechaDesde; } set { _searchFechaDesde = value; NotifyPropertyChanged("searchFechaDesde"); } }

        public DateTime? _searchFechaHasta = null;
        public DateTime? searchFechaHasta { get { return _searchFechaHasta; } set { _searchFechaHasta = value; NotifyPropertyChanged("searchFechaHasta"); } }
        #endregion

        public void refrescarGuia()
        {
            NotifyPropertyChanged("guiaRemision");
        }

        #region Valores para la segunda pestana

        public String _searchOC = "";
        public String searchOC { get { return _searchOC; } set { _searchOC = value; NotifyPropertyChanged("searchOC"); } }

        private DocumentoCompra _documentoCompra = null;
        public DocumentoCompra documentoCompra
        {
            get
            {
                return _documentoCompra;
            }
            set
            {
                _documentoCompra = value;
                NotifyPropertyChanged("documentoCompra");
            }
        }
        #endregion

        #region Lista Guias de Remision y Edicion de Guias de Remision
        private GuiaRemision _guiaRemision;
        public GuiaRemision guiaRemision
        {
            get
            {
                return _guiaRemision;
            }
            set
            {
                _guiaRemision = value;
                NotifyPropertyChanged("guiaRemision");
            }
        }

        private bool _isDetalle;
        public bool isDetalle
        {
            get
            {
                return _isDetalle;
            }
            set
            {
                _isDetalle = value;
                NotifyPropertyChanged("isDetalle");
            }
        }

        private bool _btnBusqOCEnable;
        public bool btnBusqOCEnable
        {
            get
            {
                return _btnBusqOCEnable;
            }
            set
            {
                _btnBusqOCEnable = value;
                NotifyPropertyChanged("btnBusqOCEnable");
            }
        }
        

        private IEnumerable<GuiaRemision> _listaGuiasRemision;
        public IEnumerable<GuiaRemision> listaGuiasRemision
        {
            get
            {
                if (searchFechaDesde > searchFechaHasta)
                    MessageBox.Show("La 'Fecha Hasta' no puede ser menor que la 'Fecha Desde'", "Guia de Remision", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                else
                {
                    int idTienda = ComunService.usuarioL.Empleado.tiendaActual.id;
                    _listaGuiasRemision = MC_GuiaRemisionService.buscarGuiasRemision(searchCodigo, searchProveedor, searchFechaDesde, searchFechaHasta, idTienda).ToList();
                }                   
                return _listaGuiasRemision.ToList();
            }
            set
            {
                _listaGuiasRemision = value;
                NotifyPropertyChanged("listaGuiasRemision");
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
                if (value == Tab.DETALLES && guiaRemision == null)
                {
                }
                _statusTab = value;
                switch (_statusTab)
                {
                    case Tab.BUSQUEDA: 
                        detallesTabHeader = "Agregar"; 
                        break;

                    case Tab.AGREGAR: 
                        detallesTabHeader = "Agregar";
                        isDetalle = true;
                        ordenCompraCod = "";
                        guiaRemision = new GuiaRemision(); 
                        //listaGuiaRemisionProducto = null;
                        guiaRemision.Tienda = MC_ComunService.usuarioL.Empleado.tiendaActual;
                        btnBusqOCEnable = true;
                        break;

                    case Tab.MODIFICAR: 
                        detallesTabHeader = "Modificar"; 
                        break;

                    case Tab.DETALLES: 
                        detallesTabHeader = "Detalles";  
                        break;

                    default: 
                        detallesTabHeader = "Agregar";
                        isDetalle = true;
                        guiaRemision = new GuiaRemision();
                        ordenCompraCod = "";
                        break;//Si es agregar, creo un nuevo objeto Guia de Remision
                }
                NotifyPropertyChanged("statusTab");
                //Cuando se cambia el status, tambien se tiene que actualizar el currentIndex del tab
                NotifyPropertyChanged("currentIndexTab"); //Hace que cambie el tab automaticamente
                NotifyPropertyChanged("guiaRemision");
                NotifyPropertyChanged("ordenCompraCod");
            }
        }
        //Usado para mover los tabs de acuerdo a las acciones realizadas
        public int currentIndexTab
        {
            get { return _statusTab == Tab.BUSQUEDA ? 0 : 1; }
            set { statusTab = value == 0 ? Tab.BUSQUEDA : Tab.AGREGAR; }
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

        #region RelayCommand

        RelayCommand _actualizarListaGuiasRemisionCommand;
        public ICommand actualizarListaGuiasRemisionCommand
        {
            get
            {
                if (_actualizarListaGuiasRemisionCommand == null)
                    _actualizarListaGuiasRemisionCommand = new RelayCommand(param => NotifyPropertyChanged("listaGuiasRemision"));
                return _actualizarListaGuiasRemisionCommand;
            }
        }

        RelayCommand _agregarGuiaCommand;
        public ICommand agregarGuiaCommand
        {
            get
            {
                if (_agregarGuiaCommand == null)
                    _agregarGuiaCommand = new RelayCommand(agregarGuia);

                return _agregarGuiaCommand;
            }
        }

        RelayCommand _viewEditGuiaRemisionCommand;
        public ICommand viewEditGuiaRemisionCommand
        {
            get
            {
                if (_viewEditGuiaRemisionCommand == null)
                    _viewEditGuiaRemisionCommand = new RelayCommand(viewEditGuiaRemision);

                return _viewEditGuiaRemisionCommand;
            }
        }

        RelayCommand _saveGuiaRemisionCommand;
        public ICommand saveGuiaRemisionCommand
        {
            get
            {
                if (_saveGuiaRemisionCommand == null)
                    _saveGuiaRemisionCommand = new RelayCommand(saveGuiaRemision);
                return _saveGuiaRemisionCommand;
            }
        }

        RelayCommand _cancelGuiaRemisionCommand;
        public ICommand cancelGuiaRemisionCommand
        {
            get
            {
                if (_cancelGuiaRemisionCommand == null)
                    _cancelGuiaRemisionCommand = new RelayCommand(cancelGuiaRemision);
                return _cancelGuiaRemisionCommand;
            }
        }

        RelayCommand _cargarOCCommand;
        public ICommand cargarOCCommand
        {
            get
            {
                if (_cargarOCCommand == null)
                    _cargarOCCommand = new RelayCommand(cargarOC);
                return _cargarOCCommand;
            }
        }
        #endregion

        #region commands

        public void agregarGuia(Object id)
        {
            try
            {
                ordenCompraCod = "";
                isDetalle = true;
                //NotifyPropertyChanged("ordenCompraCod");
                statusTab = Tab.AGREGAR;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        
        public void viewEditGuiaRemision(Object id)
        {
            try
            {
                this.guiaRemision = listaGuiasRemision.Single(guiaRemision => guiaRemision.id == (int)id);
                ordenCompraCod = this.guiaRemision.DocumentoCompra.codigo;
                isDetalle = false;
                btnBusqOCEnable = false;
                this.statusTab = Tab.MODIFICAR;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private bool camposObligatorios()
        {
            //PRIMERO VALIDO LOS CAMPOS GENERALES
            if ((guiaRemision.codigo != null) && (guiaRemision.fechaEmision != null) && (guiaRemision.fechaRecepcion != null) && (guiaRemision.DocumentoCompra != null))
            {
                //SI SE CUMPLE LO DE ARRIBA VERIFICO QUE SE HAYA INGRESADO VALORES EN LAS CANTIDADES RECIBIDAS
                decimal? aux = 0;
                for (int i = 0; i < guiaRemision.GuiaRemisionProducto.Count(); i++)
                    aux = aux + guiaRemision.GuiaRemisionProducto[i].cantidadRecibida;
                if (aux > 0)
                    return true;
                else
                    return false;
            }
            return false;
        }

        public void saveGuiaRemision(Object obj)
        {
            if (!camposObligatorios())
                MessageBox.Show("Complete todos los datos obligatorios", "Guia de Remision", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            else
            {
                if (guiaRemision.fechaRecepcion < guiaRemision.fechaEmision)
                    MessageBox.Show("La Fecha de Recepcion no puede ser menor que la Fecha de Emision", "Guia de Remision", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                else
                {
                    bool exito = false;
                    if (guiaRemision.id > 0)//Si existe
                    {
                        ComunService.idVentana(37);
                        if (!MC_GuiaRemisionService.enviarCambios())
                            MessageBox.Show("La Guia de Remision no se pudo actualizar", "Guia de Remision", MessageBoxButton.OK, MessageBoxImage.Error);
                        else
                        {
                            MessageBox.Show("La Guia de Remision se guardo con exito", "Guia de Remision", MessageBoxButton.OK, MessageBoxImage.Information);
                            if (guiaRemision.DocumentoCompra.id_solicitud_compra != null)
                                // guiaRemision.DocumentoCompra.SolicitudCompra.estado = 2;


                                for (int i = 0; i < guiaRemision.DocumentoCompra.DocumentoCompraProducto.Count(); i++)
                                    MC_AtenderSolicitudViewModel.actualizaestado(guiaRemision.Tienda, guiaRemision.DocumentoCompra.DocumentoCompraProducto[i].Producto);

                            exito = true;
                        }
                    }
                    else
                    {
                        ComunService.idVentana(36);
                        if (!MC_GuiaRemisionService.insertarGuiaRemision(guiaRemision))
                            MessageBox.Show("La Guia de Remision no se pudo agregar", "Guia de Remision", MessageBoxButton.OK, MessageBoxImage.Error);

                        else
                        {
                            MessageBox.Show("La Guia de Remision se agrego con exito", "Guia de Remision", MessageBoxButton.OK, MessageBoxImage.Information);
                            if (guiaRemision.DocumentoCompra.id_solicitud_compra != null)
                              //  guiaRemision.DocumentoCompra.SolicitudCompra.estado = 2;
                                for (int i = 0; i < guiaRemision.DocumentoCompra.DocumentoCompraProducto.Count(); i++)
                                    MC_AtenderSolicitudViewModel.actualizaestado(guiaRemision.Tienda, guiaRemision.DocumentoCompra.DocumentoCompraProducto[i].Producto);
                            
                            exito = true;
                        }
                    }
                    if (exito)
                    {
                        NotifyPropertyChanged("listaGuiasRemision");
                       
                        this.statusTab = Tab.BUSQUEDA;
                    }
                } 
            }                       
        }

        public void cancelGuiaRemision(Object obj)
        {
            MessageBoxResult result = MessageBox.Show("Al salir, perderá todos los datos ingresados. ¿Desea continuar?",
       "ATENCIÓN", MessageBoxButton.OKCancel, MessageBoxImage.Warning, MessageBoxResult.Cancel);
            if (result == MessageBoxResult.OK)
            {
                listaGuiasRemision = ComunService.db.GuiaRemision;
                this.statusTab = Tab.BUSQUEDA;                
            }
        }


        private String _ordenCompraCod;
        public String ordenCompraCod
        {
            get
            {
                try
                {
                    if (guiaRemision.id > 0)
                        return guiaRemision.DocumentoCompra.codigo;
                    else
                        return _ordenCompraCod;
                }
                catch (Exception e)
                {
                    return _ordenCompraCod;
                }
            }
            set
            {
                _ordenCompraCod = value;
                NotifyPropertyChanged("ordenCompraCod");
            }
        }


        public void cargarOC(Object id)
        {
            DocumentoCompra buscado = null;          
            try
            {
                buscado = ComunService.db.DocumentoCompra.Where(dc => dc.codigo.ToLower().Trim().Equals(this._ordenCompraCod.ToLower().Trim())).SingleOrDefault();
                //buscado = MC_DocumentoCompraService.obtenerDCByCodigo(this._ordenCompraCod);

                if (buscado != null)
                {
                    if (buscado.id_estado != 7)
                        MessageBox.Show("La Orden de Compra no se encuentra Facturada", "Orden de Compra", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    else
                    {
                        decimal? restante = 0;
                        int cont = buscado.DocumentoCompraProducto.Count();
                        for (int i = 0; i < cont; i++)
                            restante = restante + buscado.DocumentoCompraProducto[i].cantidadRestante;

                        if (restante == 0)
                            MessageBox.Show("La Orden de Compra ya fue recibida en su totalidad", "Orden de Compra", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        else
                        {
                            documentoCompra = buscado;
                            this.guiaRemision.DocumentoCompra = documentoCompra;

                            cont = documentoCompra.DocumentoCompraProducto.Count();

                            for (int j = 0; j < cont; j++)
                            {
                                GuiaRemisionProducto guiaLinea = new GuiaRemisionProducto() { id_guia_detalle = documentoCompra.DocumentoCompraProducto[j].id, cantidadRecibida = 0, DocumentoCompraProducto = documentoCompra.DocumentoCompraProducto[j], GuiaRemision = guiaRemision };
                                guiaRemision.GuiaRemisionProducto.Add(guiaLinea);
                            }
                            ordenCompraCod = "";
                            NotifyPropertyChanged("ordenCompraCod");
                            NotifyPropertyChanged("guiaRemision");
                        }
                    }
                }
                else
                {
                    //documentoCompra = null;
                    MessageBox.Show("No se encontro ninguna Orden de Compra con el codigo proporcionado", "Orden de Compra", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
            catch (Exception e) 
            {
                MessageBox.Show(e.Message);
            }  
        }
        #endregion
    }
}
