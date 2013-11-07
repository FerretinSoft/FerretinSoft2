﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pe.edu.pucp.ferretin.model;
using pe.edu.pucp.ferretin.viewmodel.Helper;
using pe.edu.pucp.ferretin.controller.MCompras;
using System.Windows.Input;
using System.Windows;

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

        private IEnumerable<GuiaRemision> _listaGuiasRemision;
        public IEnumerable<GuiaRemision> listaGuiasRemision
        {
            get
            {
                _listaGuiasRemision = MC_GuiaRemisionService.buscarGuiasRemision(searchCodigo, searchProveedor, searchFechaDesde, searchFechaHasta);

                return _listaGuiasRemision;
            }
            set
            {
                _listaGuiasRemision = value;
                NotifyPropertyChanged("listaGuiasRemision");
            }
        }

        //private IEnumerable<GuiaRemisionProducto> _listaProductosGuia;
        //public IEnumerable<GuiaRemisionProducto> listaProductosGuia
        //{
        //    get
        //    {
        //        //_listaGuiasRemision = MC_DocumentoCompraService.buscarGuiasRemision();

        //        return _listaProductosGuia;
        //    }
        //    set
        //    {
        //        _listaProductosGuia = value;
        //        NotifyPropertyChanged("listaProductosGuia");
        //    }
        //}
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
                _statusTab = value;
                //Si cambió el estado de las pestañas también cambio los Header
                //Si la pestaña es para agregar nuevo, limpio los input
                switch (_statusTab)
                {
                    case Tab.BUSQUEDA: detallesTabHeader = "Agregar"; guiaRemision = new GuiaRemision(); break;
                    case Tab.AGREGAR: detallesTabHeader = "Agregar"; guiaRemision = new GuiaRemision(); break;
                    case Tab.MODIFICAR: detallesTabHeader = "Modificar"; break;
                    case Tab.DETALLES: detallesTabHeader = "Detalles"; break;
                    default: detallesTabHeader = "Agregar"; guiaRemision = new GuiaRemision(); break;//Si es agregar, creo un nuevo objeto Guia de Remision
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
                {
                    _actualizarListaGuiasRemisionCommand = new RelayCommand(param => NotifyPropertyChanged("listaGuiasRemision"));
                }
                return _actualizarListaGuiasRemisionCommand;
            }
        }

        RelayCommand _viewEditGuiaRemisionCommand;
        public ICommand viewEditGuiaRemisionCommand
        {
            get
            {
                if (_viewEditGuiaRemisionCommand == null)
                {
                    _viewEditGuiaRemisionCommand = new RelayCommand(viewEditGuiaRemision);
                }
                return _viewEditGuiaRemisionCommand;
            }
        }

        RelayCommand _saveGuiaRemisionCommand;
        public ICommand saveGuiaRemisionCommand
        {
            get
            {
                if (_saveGuiaRemisionCommand == null)
                {
                    _saveGuiaRemisionCommand = new RelayCommand(saveGuiaRemision);
                }
                return _saveGuiaRemisionCommand;
            }
        }

        RelayCommand _cancelGuiaRemisionCommand;
        public ICommand cancelGuiaRemisionCommand
        {
            get
            {
                if (_cancelGuiaRemisionCommand == null)
                {
                    _cancelGuiaRemisionCommand = new RelayCommand(cancelGuiaRemision);
                }
                return _cancelGuiaRemisionCommand;
            }
        }

        RelayCommand _cargarOCCommand;
        public ICommand cargarOCCommand
        {
            get
            {
                if (_cargarOCCommand == null)
                {
                    _cargarOCCommand = new RelayCommand(cargarOC);
                }
                return _cargarOCCommand;
            }
        }
        #endregion

        #region commands

        public void viewEditGuiaRemision(Object id)
        {
            try
            {
                this.guiaRemision = listaGuiasRemision.Single(guiaRemision => guiaRemision.id == (int)id);
                this.statusTab = Tab.MODIFICAR;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public void saveGuiaRemision(Object obj)
        {
            //if (guiaRemision.id > 0)//Si existe
            //{
            //    if (!MC_GuiaRemisionService.enviarCambios())
            //    {
            //        MessageBox.Show("No se pudo actualizar el documento de compra");
            //    }
            //    else
            //    {
            //        MessageBox.Show("El documento de compra fue guardado con éxito");
            //    }
            //}
            //else
            //{
            //    if (!MC_DocumentoCompraService.insertarDocumentoCompra(documentoCompra))
            //    {
            //        MessageBox.Show("No se pudo agregar el nuevo documento de compra");
            //    }
            //    else
            //    {
            //        MessageBox.Show("El documento de compra fue agregado con éxito");
            //    }
            //}
        }

        public void cancelGuiaRemision(Object obj)
        {
            //this.statusTab = Tab.BUSQUEDA;
            //listaGuiasRemision = MC_GuiaRemisionService.listaGuiasRemision;
        }

        public void cargarOC(Object id)
        {
            DocumentoCompra buscado = null;
            int i;
            try
            {
                buscado = MC_DocumentoCompraService.obtenerDCByCodigo(searchOC);
                documentoCompra = buscado;

                this.guiaRemision.DocumentoCompra = documentoCompra;
                
                
                for (i = 0; i < documentoCompra.DocumentoCompraProducto.Count(); i++)
                {
                    GuiaRemisionProducto guiaLinea = new GuiaRemisionProducto();
                    guiaLinea.id_guia_detalle = documentoCompra.DocumentoCompraProducto[i].id;
                    guiaLinea.cantidadRecibida = 0;
                    guiaLinea.DocumentoCompraProducto = documentoCompra.DocumentoCompraProducto[i];
                    guiaRemision.GuiaRemisionProducto.Add(guiaLinea);
                    
                }
                NotifyPropertyChanged("guiaRemision");

                string nombrecito = guiaRemision.GuiaRemisionProducto[1].DocumentoCompraProducto.Producto.nombre;
                }
            catch { }

            if (buscado == null)
            {
                documentoCompra = null;
                MessageBox.Show("No se encontro ninguna Orden de Compra con el número de documento proporcionado", "No se encontro", MessageBoxButton.OK, MessageBoxImage.Question);
            }
        }

        //public void viewDetailVenta(Object id)
        //{
        //    try
        //    {
        //        this.venta = listaVentas.Single(venta => venta.id == (long)id);
        //        this.listaProductos = MV_VentaService.obtenerProductosbyIdVenta((long)id);
        //        this.listaMedioPago = MV_VentaService.obtenerMedioDePagobyIdVenta((long)id);
        //        selectedTab = 1;
        //    }
        //    catch (Exception e)
        //    {
        //        MessageBox.Show(e.Message);
        //    }
        //}

        //public void cargarCliente(Object id)
        //{
        //    Cliente buscado = null;
        //    try
        //    {
        //        buscado = MV_ClienteService.obtenerClienteByNroDoc(searchNroDocCliente);
        //    }
        //    catch { }

        //    if (buscado == null)
        //    {
        //        MessageBox.Show("No se encontro ningún Cliente con el número de documento proporcionado", "No se encontro", MessageBoxButton.OK, MessageBoxImage.Question);
        //        nombreCliente = "";
        //        searchNroDocCliente = "";
        //    }
        //    else
        //    {
        //        nombreCliente = buscado.nombreCompleto;
        //        searchNroDocCliente = buscado.nroDoc;
        //    }
        //    NotifyPropertyChanged("nombreCliente");
        //    NotifyPropertyChanged("searchNroDocCliente");
        //}

        #endregion
    }
}
