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
    public class MC_CotizacionesOCViewModel : ViewModelBase
    {
        #region Constructor
        public MC_CotizacionesOCViewModel()
        {
            _documentoCompra = new DocumentoCompra();
            _documentoCompra.DocumentoCompraProducto.ListChanged += actualizarMontosDC;
        }
        #endregion

        public string codProdAgregar { get; set; }

        #region Valores de los controles - PRIMERA PESTANA

        public int tipoDC;

        public String _searchCodigo = "";
        public String searchCodigo { get { return _searchCodigo; } set { _searchCodigo = value; NotifyPropertyChanged("searchCodigo"); } }

        public String _searchProveedor = "";
        public String searchProveedor { get { return _searchProveedor; } set { _searchProveedor = value; NotifyPropertyChanged("searchProveedor"); } }

        public DateTime? _searchFechaDesde = null;
        public DateTime? searchFechaDesde { get { return _searchFechaDesde; } set { _searchFechaDesde = value; NotifyPropertyChanged("searchFechaDesde"); } }

        public DateTime? _searchFechaHasta = null;
        public DateTime? searchFechaHasta { get { return _searchFechaHasta; } set { _searchFechaHasta = value; NotifyPropertyChanged("searchFechaHasta"); } }

        private DocumentoCompraEstado _searchEstado = new DocumentoCompraEstado();
        public DocumentoCompraEstado searchEstado
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

        //btnBusqProvEnable
        private bool _btnBusqProvEnable;
        public bool btnBusqProvEnable
        {
            get
            {
                return _btnBusqProvEnable;
            }
            set
            {
                _btnBusqProvEnable = value;
                NotifyPropertyChanged("btnBusqProvEnable");
            }
        }
        private IEnumerable<DocumentoCompraEstado> _listaEstadosDC;
        public IEnumerable<DocumentoCompraEstado> listaEstadosDC
        {
            get
            {
                return _listaEstadosDC;
            }
            set
            {
                _listaEstadosDC = value;
                NotifyPropertyChanged("listaEstadosDC");
            }
        }

        public int _searchTipoDocumento = 0;
        public int searchTipoDocumento
        {
            get
            {
                return _searchTipoDocumento;
            }
            set
            {
                _searchTipoDocumento = value;
                if (searchTipoDocumento != 0)
                {
                    listaEstadosDC = new DocumentoCompraEstado[] { new DocumentoCompraEstado { id = 0, nombre = "Todos" } };
                    listaEstadosDC = listaEstadosDC.Concat(MC_DocumentoCompraService.obtenerEstadosPorTipoDC(searchTipoDocumento));
                    _searchEstado = new DocumentoCompraEstado();
                }
                else
                {
                    listaEstadosDC = null;
                    _searchEstado = new DocumentoCompraEstado();
                }
                NotifyPropertyChanged("searchTipoDocumento");
            }
        }

        private DocumentoCompra _documentoCompra;
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

        private IEnumerable<DocumentoCompra> _listaDocumentosCompra;
        public IEnumerable<DocumentoCompra> listaDocumentosCompra
        {
            get
            {
                if (searchFechaDesde > searchFechaHasta)
                    MessageBox.Show("La 'Fecha Hasta' no puede ser menor que la 'Fecha Desde'", "Documento de Compra", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                else
                {
                    int idTienda = ComunService.usuarioL.Empleado.tiendaActual.id;
                    _listaDocumentosCompra = MC_DocumentoCompraService.buscarDocumentosCompra(searchCodigo, searchProveedor, searchTipoDocumento, searchFechaDesde, searchFechaHasta, searchEstado.id, idTienda).ToList();

                }
                    
                return _listaDocumentosCompra.ToList();
            }
            set
            {
                _listaDocumentosCompra = value;
                NotifyPropertyChanged("listaDocumentosCompra");
            }
        }

        #endregion

        #region Valores para mostrar u ocultar controles - SEGUNDA PESTANA

        public bool isCreating
        {
            get
            {
                if (statusTab == Tab.DETALLES || statusTab == Tab.MODIFICAR)
                {
                    if (documentoCompra.DocumentoCompraEstado.nombre == "Ingresada")
                        return true;
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    if (statusTab == Tab.AGREGAR || statusTab == Tab.BUSQUEDA)
                        return true;
                    else
                        return false;
                }
            }
        }

        public bool isCreatingFechaPago
        {
            get
            {
                if (documentoCompra.id > 0)
                {
                    if (documentoCompra.tipo == 1) // ES COTIZACION
                    {
                        switch (documentoCompra.DocumentoCompraEstado.nombre)
                        {
                            case "Aprobada":
                                return false;
                            default:
                                return true;
                        }
                    }
                    else // ES ORDEN DE COMPRA
                    {
                        switch (documentoCompra.DocumentoCompraEstado.nombre)
                        {
                            case "Facturada":
                                return false;
                            case "Ingresada":
                                return false;
                            case "Emitida":
                                return true;
                            default:
                                return false;
                        }
                    }
                }
                else
                {
                    if (tipoDC == 1)//ES COTIZACION
                        return true;
                    else
                        return false;
                }
                //return true;
            }
        }

        public string btnAprobarLabel
        {
            get
            {
                if (documentoCompra.id > 0)
                {
                    if (documentoCompra.tipo == 1) // ES COTIZACION
                    {
                        switch (documentoCompra.DocumentoCompraEstado.nombre)
                        {
                            case "Ingresada":
                                return "GENERAR OC";
                            default:
                                return "";
                        }
                    }
                    else // ES ORDEN DE COMPRA
                    {
                        switch (documentoCompra.DocumentoCompraEstado.nombre)
                        {
                            case "Ingresada":
                                return "APROBAR OC";
                            default:
                                return "";
                        }
                    }
                }
                return "";
            }
        }

        public Visibility generarAprobar
        {
            get
            {
                if (documentoCompra.id > 0)
                {
                    if (documentoCompra.tipo == 1) // ES COTIZACION
                    {
                        switch (documentoCompra.DocumentoCompraEstado.nombre)
                        {
                            case "Ingresada":
                                return Visibility.Visible;
                            default:
                                return Visibility.Hidden;
                        }
                    }
                    else // ES ORDEN DE COMPRA
                    {
                        switch (documentoCompra.DocumentoCompraEstado.nombre)
                        {
                            case "Ingresada":
                                return Visibility.Visible;
                            default:
                                return Visibility.Hidden;
                        }
                    }
                }
                return Visibility.Hidden;
            }
        }

        public Visibility ingresaFactura
        {
            get
            {
                if (documentoCompra.id > 0)
                {
                    if (documentoCompra.tipo == 2) // ES ORDEN DE COMPRA
                    {
                        switch (documentoCompra.DocumentoCompraEstado.nombre)
                        {
                            case "Emitida":
                                return Visibility.Visible;
                            case "Facturada":
                                return Visibility.Visible;
                            default:
                                return Visibility.Hidden;
                        }
                    }
                    else // ES COTIZACION
                        return Visibility.Hidden;
                    
                }
                return Visibility.Hidden;
            }
        }

        #endregion 

        #region Valores de los controles - SEGUNDA PESTANA      

        public Usuario _usuarioIngreso = null;
        public Usuario usuarioIngreso
        {
            get
            {
                return _usuarioIngreso;
            }
            set
            {
                _usuarioIngreso = value;
                NotifyPropertyChanged("usuarioIngreso");
            }
        }

        public Usuario _usuarioAprobacion = null;
        public Usuario usuarioAprobacion
        {
            get
            {
                return _usuarioAprobacion;
            }
            set
            {
                _usuarioAprobacion = value;
                NotifyPropertyChanged("usuarioAprobacion");
            }
        }

        public Tienda _tienda = null;
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
       
        private String _proveedorNombre;
        public String proveedorNombre
        {
            get
            {
                try
                {
                    if (documentoCompra.id > 0)
                        return documentoCompra.Proveedor.razonSoc;
                    else
                        return "";

                }
                catch (Exception e)
                {
                    return _proveedorNombre;
                }
            }
            set
            {
                _proveedorNombre = value;
                NotifyPropertyChanged("proveedorNombre");
            }
        }

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

        public string _labelFechaDC1 = null;
        public string labelFechaDC1
        {
            get
            {
                return _labelFechaDC1;
            }
            set
            {
                _labelFechaDC1 = value;
                NotifyPropertyChanged("labelFechaDC1");
            }
        }

        public string _labelFechaDC2 = null;
        public string labelFechaDC2
        {
            get
            {
                return _labelFechaDC2;
            }
            set
            {
                _labelFechaDC2 = value;
                NotifyPropertyChanged("labelFechaDC2");
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
                if (value == Tab.DETALLES && documentoCompra == null)
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
                        documentoCompra = new DocumentoCompra();
                        btnBusqProvEnable = true;
                        proveedorNombre = "";
                        //listaProductosDC = new List<DocumentoCompraProducto>(); 
                        usuarioIngreso = MC_ComunService.usuarioL;
                        tienda = MC_ComunService.usuarioL.Empleado.tiendaActual;
                        break;

                    case Tab.MODIFICAR: 
                        detallesTabHeader = "Modificar"; 
                        //listaProductosDC = MC_DocumentoCompraService.buscarProductosDC(documentoCompra).ToList(); 
                        usuarioIngreso = documentoCompra.Usuario1;
                        usuarioAprobacion = documentoCompra.Usuario;
                        tienda = documentoCompra.Tienda;
                        break;

                    case Tab.DETALLES: 
                        detallesTabHeader = "Detalles"; 
                        //listaProductosDC = MC_DocumentoCompraService.buscarProductosDC(documentoCompra).ToList(); 
                        usuarioIngreso = documentoCompra.Usuario1;
                        usuarioAprobacion = documentoCompra.Usuario;
                        tienda = documentoCompra.Tienda;
                        break;

                    default: 
                        detallesTabHeader = "Agregar"; 
                        documentoCompra = new DocumentoCompra(); 
                        //listaProductosDC = new List<DocumentoCompraProducto>(); 
                        break;
                }
                NotifyPropertyChanged("statusTab");
                //Cuando se cambia el status, tambien se tiene que actualizar el currentIndex del tab
                NotifyPropertyChanged("currentIndexTab"); //Hace que cambie el tab automaticamente
                NotifyPropertyChanged("isCreating");
                NotifyPropertyChanged("btnAprobarLabel");
                NotifyPropertyChanged("generarAprobar");
                NotifyPropertyChanged("isCreatingFechaPago");
                NotifyPropertyChanged("ingresaFactura");
                NotifyPropertyChanged("proveedorNombre");
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

        #region RalayCommand

        RelayCommand _actualizarListaDocumentosCompraCommand;
        public ICommand actualizarListaDocumentosCompraCommand
        {
            get
            {
                if (_actualizarListaDocumentosCompraCommand == null)
                {
                    _actualizarListaDocumentosCompraCommand = new RelayCommand(param => NotifyPropertyChanged("listaDocumentosCompra"));
                }
                return _actualizarListaDocumentosCompraCommand;
            }
        }

        RelayCommand _agregarCotizacionCommand;
        public ICommand agregarCotizacionCommand
        {
            get
            {
                if (_agregarCotizacionCommand == null)
                {
                    _agregarCotizacionCommand = new RelayCommand(agregarCotizacion);
                }
                return _agregarCotizacionCommand;

            }
        }
        
        RelayCommand _aprobarDocumentoCompraCommand;
        public ICommand aprobarDocumentoCompraCommand
        {
            get
            {
                if (_aprobarDocumentoCompraCommand == null)
                {
                    _aprobarDocumentoCompraCommand = new RelayCommand(aprobarDocumento);
                }
                return _aprobarDocumentoCompraCommand;

            }
        }

        RelayCommand _agregarOrdenCompraCommand;
        public ICommand agregarOrdenCompraCommand
        {
            get
            {
                if (_agregarOrdenCompraCommand == null)
                {
                    _agregarOrdenCompraCommand = new RelayCommand(agregarOrdenCompra);
                }
                return _agregarOrdenCompraCommand;
            }
        }

        RelayCommand _viewEditDocumentoCompraCommand;
        public ICommand viewEditDocumentoCompraCommand
        {
            get
            {
                if (_viewEditDocumentoCompraCommand == null)
                {
                    _viewEditDocumentoCompraCommand = new RelayCommand(viewEditDocumentoCompra);
                }
                return _viewEditDocumentoCompraCommand;
            }
        }

        RelayCommand _saveDocumentoCompraCommand;
        public ICommand saveDocumentoCompraCommand
        {
            get
            {
                if (_saveDocumentoCompraCommand == null)
                {
                    _saveDocumentoCompraCommand = new RelayCommand(saveDocumentoCompra);
                }
                return _saveDocumentoCompraCommand;
            }
        }

        RelayCommand _cancelDocumentoCompraCommand;
        public ICommand cancelDocumentoCompraCommand
        {
            get
            {
                if (_cancelDocumentoCompraCommand == null)
                {
                    _cancelDocumentoCompraCommand = new RelayCommand(cancelDocumentoCompra);
                }
                return _cancelDocumentoCompraCommand;
            }
        }

        RelayCommand _cargarProveedorCommand;
        public ICommand cargarProveedorCommand
        {
            get
            {
                if (_cargarProveedorCommand == null)
                {
                    _cargarProveedorCommand = new RelayCommand(cargarProveedor);
                }
                return _cargarProveedorCommand;
            }
        }

        #endregion

        #region Comandos

        public void viewEditDocumentoCompra(Object id)
        {
            try
            {
                this.documentoCompra = ComunService.db.DocumentoCompra.Where(p => p.id == (long)id).SingleOrDefault();
                //this.documentoCompra = ComunService.db.DocumentoCompra.Single(documentoCompra => documentoCompra.id == (long)id);
                for (int i = 0; i < this.documentoCompra.DocumentoCompraProducto.Count(); i++ )
                    this.documentoCompra.DocumentoCompraProducto[i].PropertyChanged += actualizarMontosDC;
                    if (this.documentoCompra.tipoDC == "Cotizacion")
                    {
                        prepararLabels(1);
                    }
                if (this.documentoCompra.tipoDC == "Orden de Compra")
                {
                    prepararLabels(2);
                }
                btnBusqProvEnable = false;
                this.statusTab = Tab.MODIFICAR;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public void agregarCotizacion(Object id)
        {
            try
            {                
                prepararLabels(1);
                tipoDC = 1;
                proveedorNombre = "";
                btnBusqProvEnable = true;
                statusTab = Tab.AGREGAR;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public void agregarOrdenCompra(Object id)
        {
            try
            {                
                prepararLabels(2);
                tipoDC = 2;
                proveedorNombre = "";
                btnBusqProvEnable = true;
                statusTab = Tab.AGREGAR;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public bool camposObligatoriosAprobacion()
        {
            if (documentoCompra.tipo == 1)// ES COTIZACION
            {
                //PRIMERO VALIDO LOS CAMPOS GENERALES
                if ((documentoCompra.fechaEmision != null) && (documentoCompra.fechaVencimiento != null) && (documentoCompra.Proveedor != null))
                {
                    //SI SE CUMPLE LO DE ARRIBA VERIFICO QUE SE HAYA INGRESADO FILAS AL DETALLE TE DEL DOCUMENTO
                    if (documentoCompra.DocumentoCompraProducto.Count() <= 0)
                        return false;
                    else
                        return true;
                }
                return false;
            }
            else// ES ORDEN DE COMPRA
            {
                if ((documentoCompra.fechaEmision != null) && (documentoCompra.Proveedor != null))
                {
                    //SI SE CUMPLE LO DE ARRIBA VERIFICO QUE SE HAYA INGRESADO FILAS AL DETALLE TE DEL DOCUMENTO
                    if (documentoCompra.DocumentoCompraProducto.Count() <= 0)
                        return false;
                    else
                        return true;
                }
                return false;
            }                      
        }

        public bool camposObligatoriosIngreso()
        {
            if (documentoCompra.tipo == 1)// ES COTIZACION
            {
                if ((documentoCompra.fechaEmision != null) && (documentoCompra.fechaVencimiento != null) && (documentoCompra.Proveedor != null))
                {
                    if (documentoCompra.DocumentoCompraProducto.Count() <= 0)
                        return false;
                    else
                        return true;
                }
                return false;
            }
            else// ES ORDEN DE COMPRA
            {
                if ((documentoCompra.fechaEmision != null) && (documentoCompra.Proveedor != null))
                {
                    //SI SE CUMPLE LO DE ARRIBA VERIFICO QUE SE HAYA INGRESADO FILAS AL DETALLE TE DEL DOCUMENTO
                    if (documentoCompra.DocumentoCompraProducto.Count() <= 0)
                        return false;
                    else
                    {
                        if(documentoCompra.id_estado != 6)
                            return true;
                        if ((documentoCompra.id_estado == 6) && (documentoCompra.fechaVencimiento != null) && (documentoCompra.nroFactura != null))
                            return true;
                        else
                            return false;
                    }
                }               
                return false;
            }   
        }
        public void aprobarDocumento(Object id)
        {
            bool pasoValidaciones = false;
            try
            {
                if (documentoCompra.tipo == 1)//ES COTIZACION
                {
                    if (!camposObligatoriosAprobacion())
                    {
                        MessageBox.Show("Complete todos los datos obligatorios", "Cotizacion", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    }
                    else
                    {
                        if ((documentoCompra.fechaVencimiento.Value.Date < documentoCompra.fechaEmision.Value.Date) || 
                            (documentoCompra.fechaEmision.Value.Date > DateTime.Today.Date))
                            MessageBox.Show("Ingrese fechas válidas", "Cotizacion", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        else
                        {
                            documentoCompra.Usuario = MC_ComunService.usuarioL;
                            documentoCompra.DocumentoCompraEstado = ComunService.db.DocumentoCompraEstado.Where(dce => dce.id == 3).SingleOrDefault();
                            //documentoCompra.DocumentoCompraEstado = MC_DocumentoCompraService.obtenerEstado(3);
                            ComunService.idVentana(55);
                            MC_DocumentoCompraService.enviarCambios();
                            int i;
                            int cont = this.documentoCompra.DocumentoCompraProducto.Count();
                            // CREO LA ORDEN DE COMPRA
                            DocumentoCompra ocGenerada = generarOCDeCotizacion();
                            for (i = 0; i < cont; i++)
                            {
                                DocumentoCompraProducto producto = new DocumentoCompraProducto()
                                {
                                    Producto = this.documentoCompra.DocumentoCompraProducto[i].Producto,
                                    UnidadMedida = this.documentoCompra.DocumentoCompraProducto[i].Producto.UnidadMedida,
                                    DocumentoCompra = ocGenerada,
                                    cantidad = this.documentoCompra.DocumentoCompraProducto[i].cantidad,
                                    cantidadRestante = this.documentoCompra.DocumentoCompraProducto[i].cantidadRestante,
                                    estado = this.documentoCompra.DocumentoCompraProducto[i].estado,
                                    montoParcial = this.documentoCompra.DocumentoCompraProducto[i].montoParcial,
                                    precioUnit = this.documentoCompra.DocumentoCompraProducto[i].precioUnit
                                };
                                ocGenerada.DocumentoCompraProducto.Add(producto);
                            }
                            ComunService.idVentana(33);
                            MC_DocumentoCompraService.insertarDocumentoCompra(ocGenerada);
                            pasoValidaciones = true;
                        }
                    }
                }
                else//ES ORDEN DE COMPRA
                {
                    if (!camposObligatoriosAprobacion())
                    {
                        MessageBox.Show("Complete todos los datos obligatorios", "Orden de Compra", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    }
                    else
                    {
                        if (documentoCompra.fechaEmision.Value.Date > DateTime.Today.Date)
                            MessageBox.Show("Ingrese fechas válidas", "Cotizacion", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        else
                        {
                            documentoCompra.DocumentoCompraEstado = ComunService.db.DocumentoCompraEstado.Where(dce => dce.id == 6).SingleOrDefault();
                            //documentoCompra.DocumentoCompraEstado = MC_DocumentoCompraService.obtenerEstado(6);
                            documentoCompra.Usuario = MC_ComunService.usuarioL;
                            ComunService.idVentana(34);
                            MC_DocumentoCompraService.enviarCambios();
                            pasoValidaciones = true;
                        }
                    }                 
                }

                if (pasoValidaciones)
                {
                    ComunService.idVentana(37);

                    if (!MC_DocumentoCompraService.enviarCambios())
                    {
                        if (documentoCompra.tipo == 1)//ES COTIZACION
                            MessageBox.Show("No se pudo generar la Orden de Compra", "Orden de Compra", MessageBoxButton.OK, MessageBoxImage.Error);
                        else
                            MessageBox.Show("La Orden de Compra no se pudo aprobar", "Orden de Compra", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        if (documentoCompra.tipo == 1)//ES COTIZACION
                            MessageBox.Show("Se genero la Orden de Compra con exito", "Orden de Compra", MessageBoxButton.OK, MessageBoxImage.Information);
                        else
                            MessageBox.Show("La Orden de Compra se aprobo con exito", "Orden de Compra", MessageBoxButton.OK, MessageBoxImage.Information);
                    }

                    NotifyPropertyChanged("listaDocumentosCompra");
                    this.statusTab = Tab.BUSQUEDA;
                }                
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public void saveDocumentoCompra(Object obj)
        {
            int i;
            int cont;
            decimal? subTotal;
            bool exito = false;
            if (documentoCompra.id > 0)//Si existe
            {
                if (!camposObligatoriosIngreso())
                {
                    MessageBox.Show("Complete todos los datos obligatorios", "Documento de Compra", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
                else
                {
                    if ((documentoCompra.fechaVencimiento != null && documentoCompra.fechaVencimiento.Value.Date < documentoCompra.fechaEmision.Value.Date) ||
                            (documentoCompra.fechaEmision.Value.Date > DateTime.Today.Date))
                        if(documentoCompra.tipo == 1)
                            MessageBox.Show("Ingrese fechas válidas", "Cotizacion", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        else
                            MessageBox.Show("Ingrese fechas válidas", "Orden de Compra", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    else
                    {
                        cont = documentoCompra.DocumentoCompraProducto.Count();
                        subTotal = 0;
                        for (i = 0; i < cont; i++)
                        {
                            subTotal = subTotal + documentoCompra.DocumentoCompraProducto[i].montoParcial;
                            documentoCompra.DocumentoCompraProducto[i].cantidadRestante = documentoCompra.DocumentoCompraProducto[i].cantidad;
                        }
                        documentoCompra.total = Decimal.Round(subTotal.Value, 2);
                        documentoCompra.subTotal = Decimal.Round((documentoCompra.total / (((decimal)MS_SharedService.obtenerIGV() / (100)) + 1)).Value, 2);
                        documentoCompra.igv = Decimal.Round((documentoCompra.total - documentoCompra.subTotal).Value, 2);
                        if (documentoCompra.tipo == 2 && documentoCompra.DocumentoCompraEstado.nombre.Equals("Emitida") && documentoCompra.nroFactura != null && documentoCompra.fechaVencimiento != null)
                            documentoCompra.DocumentoCompraEstado = ComunService.db.DocumentoCompraEstado.Where(dce => dce.id == 7).SingleOrDefault();
                        //documentoCompra.DocumentoCompraEstado = MC_DocumentoCompraService.obtenerEstado(7);

                        if (documentoCompra.tipo == 1)
                            ComunService.idVentana(55);
                        else
                            ComunService.idVentana(34);

                        if (!MC_DocumentoCompraService.enviarCambios())
                        {
                            if (documentoCompra.tipo == 1)//ES COTIZACION
                                MessageBox.Show("La Cotizacion no se pudo actualizar", "Cotizacion", MessageBoxButton.OK, MessageBoxImage.Error);
                            else
                                MessageBox.Show("La Orden de Compra no se pudo actualizar", "Orden de Compra", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        else
                        {
                            if (documentoCompra.tipo == 1)//ES COTIZACION
                                MessageBox.Show("La Cotizacion se guardo con exito", "Cotizacion", MessageBoxButton.OK, MessageBoxImage.Information);
                            else
                                MessageBox.Show("La Orden de Compra se guardo con exito", "Orden de Compra", MessageBoxButton.OK, MessageBoxImage.Information);
                            exito = true;
                        }
                    }
                }        
            }
            else
            {
                prepararDC(tipoDC);
                if (!camposObligatoriosIngreso())
                {
                    MessageBox.Show("Complete todos los datos obligatorios", "Documento de Compra", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
                else
                {
                    if ((documentoCompra.fechaVencimiento != null && documentoCompra.fechaVencimiento.Value.Date < documentoCompra.fechaEmision.Value.Date) ||
                            (documentoCompra.fechaEmision.Value.Date > DateTime.Today.Date))
                        MessageBox.Show("Ingrese fechas válidas", "Documento de Compra", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    else
                    {
                        cont = documentoCompra.DocumentoCompraProducto.Count();
                        subTotal = 0;
                        for (i = 0; i < cont; i++)
                        {
                            subTotal = subTotal + documentoCompra.DocumentoCompraProducto[i].montoParcial;
                            documentoCompra.DocumentoCompraProducto[i].cantidadRestante = documentoCompra.DocumentoCompraProducto[i].cantidad;
                        }

                        documentoCompra.total = Decimal.Round(subTotal.Value, 2);
                        documentoCompra.subTotal = Decimal.Round((documentoCompra.total / (((decimal)MS_SharedService.obtenerIGV() / (100)) + 1)).Value, 2);
                        documentoCompra.igv = Decimal.Round((documentoCompra.total - documentoCompra.subTotal).Value, 2);
                        documentoCompra.codigo = MC_DocumentoCompraService.generarCodigoDC_V2(documentoCompra.tipo);

                        if (documentoCompra.tipo == 1)
                            ComunService.idVentana(54);
                        else
                            ComunService.idVentana(33);

                        if (!MC_DocumentoCompraService.insertarDocumentoCompra(documentoCompra))
                        {
                            if (documentoCompra.tipo == 1)//ES COTIZACION
                                MessageBox.Show("La Cotizacion no se pudo agregar", "Cotizacion", MessageBoxButton.OK, MessageBoxImage.Error);
                            else
                                MessageBox.Show("La Orden de Compra no se pudo agregar", "Orden de Compra", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        else
                        {
                            if (documentoCompra.tipo == 1)//ES COTIZACION
                                MessageBox.Show("La Cotizacion se agrego con exito", "Cotizacion", MessageBoxButton.OK, MessageBoxImage.Information);
                            else
                                MessageBox.Show("La Orden de Compra se agrego con exito", "Orden de Compra", MessageBoxButton.OK, MessageBoxImage.Information);
                            exito = true;
                        }
                    }
                }               
            }
            if (exito)
            {
                NotifyPropertyChanged("listaDocumentosCompra");
                this.statusTab = Tab.BUSQUEDA;
            }            
        }

        public void cancelDocumentoCompra(Object obj)
        {
            MessageBoxResult result = MessageBox.Show("Al salir, perderá todos los datos ingresados. ¿Desea continuar?",
                "ATENCIÓN", MessageBoxButton.OKCancel, MessageBoxImage.Warning, MessageBoxResult.Cancel);
            if (result == MessageBoxResult.OK)
            {               
                listaDocumentosCompra = MC_DocumentoCompraService.listaDocumentosCompra;
                this.statusTab = Tab.BUSQUEDA;
            }
        }

        public void cargarProveedor(Object id)
        {
            Proveedor buscado = null;
            try
            {
                buscado = ComunService.db.Proveedor.Where(pv => pv.razonSoc.ToLower().Trim().Contains(this._proveedorNombre.ToLower().Trim())).SingleOrDefault();
                //buscado = MC_ProveedorService.buscarProveedorByName(this._proveedorNombre);
                documentoCompra.Proveedor = buscado;
                proveedorNombre = "";
                NotifyPropertyChanged("proveedorNombre");
                NotifyPropertyChanged("documentoCompra");
            }
            catch { }

            if (buscado == null)
            {
                MessageBox.Show("No se encontro ninguna Proveedor", "Proveedor", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        #endregion

        #region Metodos Auxiliares

        private DocumentoCompra generarOCDeCotizacion()
        {
            DocumentoCompra ocGenerada = null;
            try
            {
                ocGenerada = new DocumentoCompra()
                {
                    tipo = 2,
                    codigo = MC_DocumentoCompraService.generarCodigoDC_V2(2),
                    fechaEmision = DateTime.Now.Date,
                    DocumentoCompra1 = this.documentoCompra,
                    DocumentoCompraEstado = ComunService.db.DocumentoCompraEstado.Where(dce => dce.id == 5).SingleOrDefault(),
                    //DocumentoCompraEstado = MC_DocumentoCompraService.obtenerEstado(1),
                    Proveedor = this.documentoCompra.Proveedor,
                    Usuario1 = MC_ComunService.usuarioL,
                    Tienda = MC_ComunService.usuarioL.Empleado.tiendaActual,
                    igv = this.documentoCompra.igv,
                    subTotal = this.documentoCompra.subTotal,
                    total = this.documentoCompra.total,
                    DocumentoCompraProducto = new System.Data.Linq.EntitySet<DocumentoCompraProducto>(),
                };
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            return ocGenerada;
        }

        private void prepararLabels(int tipo)
        {
            if (tipo == 1)//ES COTIZACION
            {
                this.labelCodigo = "Cotizacion";
                this.labelFechaDC1 = "Fecha Emision";
                this.labelFechaDC2 = "Fecha Vencimiento";
            }
            else //ES ORDEN DE COMPRA
            {
                this.labelCodigo = "Orden Compra";
                this.labelFechaDC1 = "Fecha Emision";
                this.labelFechaDC2 = "Fecha Pago";
            }
        }

        private void prepararDC(int tipo)
        {           
            if (tipo == 1)
            {
                documentoCompra.tipo = 1;
                documentoCompra.DocumentoCompraEstado = ComunService.db.DocumentoCompraEstado.Where(dce => dce.id == 1).SingleOrDefault();
                //documentoCompra.DocumentoCompraEstado = MC_DocumentoCompraService.obtenerEstado(1);
                documentoCompra.Usuario1 = usuarioIngreso;
                documentoCompra.Tienda = tienda;
            }
            else
            {
                documentoCompra.tipo = 2;
                documentoCompra.DocumentoCompraEstado = ComunService.db.DocumentoCompraEstado.Where(dce => dce.id == 5).SingleOrDefault();
                //documentoCompra.DocumentoCompraEstado = MC_DocumentoCompraService.obtenerEstado(5);
                documentoCompra.Usuario1 = usuarioIngreso;
                documentoCompra.Tienda = tienda;
            }
        }

        public void actualizar()
        {
            NotifyPropertyChanged("documentoCompra");
        }

        public void actualizarMontosDC(object sender, object e)
        {
            //Actualizo el total
            documentoCompra.total = Decimal.Round(documentoCompra.DocumentoCompraProducto.Sum(p => p.montoParcial).Value, 2);
            documentoCompra.subTotal = Decimal.Round((documentoCompra.total / (((decimal)MS_SharedService.obtenerIGV() / (100)) + 1)).Value,2);
            documentoCompra.igv = documentoCompra.total - documentoCompra.subTotal;
        }

        public void agregarProducto(Object obj, Proveedor proveedor)
        {
            if (codProdAgregar != null && proveedor != null)
            {
                ProveedorProducto producto = null;
                try
                {
                    producto = ComunService.db.ProveedorProducto.Where(pp => pp.Producto.codigo == codProdAgregar && pp.Proveedor == proveedor).SingleOrDefault();
                    //producto = MC_ProveedorService.obtenerProductoProveedor(codProdAgregar, proveedor);
                }
                catch { }

                if (producto != null)
                {
                    DocumentoCompraProducto dcp = null;
                    if (documentoCompra.DocumentoCompraProducto.Count(vp => vp.Producto.id == producto.Producto.id) == 1)
                    {
                        NotifyPropertyChanged("documentoCompra");
                    }
                    else
                    {
                        dcp = new DocumentoCompraProducto()
                        {
                            Producto = producto.Producto,
                            UnidadMedida = producto.UnidadMedida,
                            precioUnit = producto.precio,
                            estado = 1,
                            cantidad = 0,
                        };
                        dcp.PropertyChanged += actualizarMontosDC;
                        documentoCompra.DocumentoCompraProducto.Add(dcp);
                        actualizarMontosDC(null, null);
                        //NotifyPropertyChanged("documentoCompra");
                    }                    
                }
                NotifyPropertyChanged("documentoCompra");
            }
            else
            {
                MessageBox.Show("No se encontro ningun producto con el código proporcionado");
            }
        }
        #endregion 
    }
}
