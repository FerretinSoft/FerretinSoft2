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
                _listaDocumentosCompra = MC_DocumentoCompraService.buscarDocumentosCompra(searchCodigo, searchProveedor, searchTipoDocumento, searchFechaDesde, searchFechaHasta, searchEstado.id);

                return _listaDocumentosCompra;
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
                            default:
                                return true;
                        }
                    }
                }
                return true;
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
                        return _proveedorNombre;

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
                        //listaProductosDC = new List<DocumentoCompraProducto>(); 
                        usuarioIngreso = MC_ComunService.usuarioL;
                        break;

                    case Tab.MODIFICAR: 
                        detallesTabHeader = "Modificar"; 
                        //listaProductosDC = MC_DocumentoCompraService.buscarProductosDC(documentoCompra).ToList(); 
                        usuarioIngreso = documentoCompra.Usuario1;
                        usuarioAprobacion = documentoCompra.Usuario;
                        break;

                    case Tab.DETALLES: 
                        detallesTabHeader = "Detalles"; 
                        //listaProductosDC = MC_DocumentoCompraService.buscarProductosDC(documentoCompra).ToList(); 
                        usuarioIngreso = documentoCompra.Usuario1;
                        usuarioAprobacion = documentoCompra.Usuario;
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
                this.documentoCompra = listaDocumentosCompra.Single(documentoCompra => documentoCompra.id == (long)id);               
                if (this.documentoCompra.tipoDC == "Cotizacion")
                {
                    prepararLabels(1);
                }
                if (this.documentoCompra.tipoDC == "Orden de Compra")
                {
                    prepararLabels(2);
                }
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
                statusTab = Tab.AGREGAR;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }     

        public void aprobarDocumento(Object id)
        {
            try
            {
                if (documentoCompra.tipo == 1)//ES COTIZACION
                {
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
                    documentoCompra.Usuario = MC_ComunService.usuarioL;
                    documentoCompra.DocumentoCompraEstado = MC_DocumentoCompraService.obtenerEstado(3);
                    ComunService.idVentana(36);
                    MC_DocumentoCompraService.insertarDocumentoCompra(ocGenerada);
                }
                else//ES ORDEN DE COMPRA
                {
                    documentoCompra.DocumentoCompraEstado = MC_DocumentoCompraService.obtenerEstado(6);
                    documentoCompra.Usuario = MC_ComunService.usuarioL;
                }                   

                ComunService.idVentana(37);

                if (!MC_DocumentoCompraService.enviarCambios())
                {
                    if (documentoCompra.tipo == 1)//ES COTIZACION
                        MessageBox.Show("La Cotizacion no se pudo aprobar");
                    else
                        MessageBox.Show("La Orden de Compra no se pudo aprobar");
                }
                else
                {
                    if (documentoCompra.tipo == 1)//ES COTIZACION
                        MessageBox.Show("La Cotizacion se aprobo con exito");
                    else
                        MessageBox.Show("La Orden de Compra se aprobo con exito");
                }

                NotifyPropertyChanged("listaDocumentosCompra");
                this.statusTab = Tab.BUSQUEDA;
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
            if (documentoCompra.id > 0)//Si existe
            {
                cont = documentoCompra.DocumentoCompraProducto.Count();
                subTotal = 0;
                for (i = 0; i < cont; i++)
                    subTotal = subTotal + documentoCompra.DocumentoCompraProducto[i].montoParcial;
                documentoCompra.total = subTotal;
                documentoCompra.subTotal = documentoCompra.total / (((decimal)MS_SharedService.obtenerIGV() / (100)) + 1);
                documentoCompra.igv = documentoCompra.total - documentoCompra.subTotal;
                if (documentoCompra.tipo == 2 && documentoCompra.DocumentoCompraEstado.nombre.Equals("Emitida") && documentoCompra.nroFactura != null && documentoCompra.fechaVencimiento != null)
                    documentoCompra.DocumentoCompraEstado = MC_DocumentoCompraService.obtenerEstado(7);

                ComunService.idVentana(37);
                if (!MC_DocumentoCompraService.enviarCambios())
                {
                    if (documentoCompra.tipo == 1)//ES COTIZACION
                        MessageBox.Show("La Cotizacion no se pudo actualizar");
                    else
                        MessageBox.Show("La Orden de Compra no se pudo actualizar");
                }
                else
                {
                    if (documentoCompra.tipo == 1)//ES COTIZACION
                        MessageBox.Show("La Cotizacion se guardo con exito");
                    else
                        MessageBox.Show("La Orden de Compra se guardo con exito");
                }
            }
            else
            {
                prepararDC(tipoDC);
                cont = documentoCompra.DocumentoCompraProducto.Count();
                subTotal = 0;
                for (i = 0; i < cont; i++)
                    subTotal = subTotal + documentoCompra.DocumentoCompraProducto[i].montoParcial;
                documentoCompra.total = subTotal;
                documentoCompra.subTotal = documentoCompra.total / (((decimal)MS_SharedService.obtenerIGV() / (100)) + 1);
                documentoCompra.igv = documentoCompra.total - documentoCompra.subTotal;
                documentoCompra.codigo = MC_DocumentoCompraService.generarCodigoDC(documentoCompra.tipo);
                ComunService.idVentana(36);
                if (!MC_DocumentoCompraService.insertarDocumentoCompra(documentoCompra))
                {
                    if (documentoCompra.tipo == 1)//ES COTIZACION
                        MessageBox.Show("La Cotizacion no se pudo agregar");
                    else
                        MessageBox.Show("La Orden de Compra no se pudo agregar");
                }
                else
                {
                    if(documentoCompra.tipo == 1)//ES COTIZACION
                        MessageBox.Show("La Cotizacion se agrego con exito");
                    else
                        MessageBox.Show("La Orden de Compra se agrego con exito");
                }
            }
            NotifyPropertyChanged("listaDocumentosCompra");
            this.statusTab = Tab.BUSQUEDA;
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
                buscado = MC_ProveedorService.buscarProveedorByName(this._proveedorNombre);
                documentoCompra.Proveedor = buscado;
                proveedorNombre = "";
                NotifyPropertyChanged("proveedorNombre");
                NotifyPropertyChanged("documentoCompra");
            }
            catch { }

            if (buscado == null)
            {
                MessageBox.Show("No se encontro ninguna Proveedor", "No se encontro", MessageBoxButton.OK, MessageBoxImage.Question);
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
                    codigo = MC_DocumentoCompraService.generarCodigoDC(2),
                    fechaEmision = DateTime.Now,
                    DocumentoCompra1 = this.documentoCompra,
                    DocumentoCompraEstado = MC_DocumentoCompraService.obtenerEstado(1),
                    Proveedor = this.documentoCompra.Proveedor,
                    Usuario1 = MC_ComunService.usuarioL,
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
                documentoCompra.DocumentoCompraEstado = MC_DocumentoCompraService.obtenerEstado(1);
                documentoCompra.Usuario1 = usuarioIngreso;
            }
            else
            {
                documentoCompra.tipo = 2;
                documentoCompra.DocumentoCompraEstado = MC_DocumentoCompraService.obtenerEstado(5);
                documentoCompra.Usuario1 = usuarioIngreso;
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

        #endregion 
    }
}
