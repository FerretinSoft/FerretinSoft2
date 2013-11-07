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
    public class MC_CotizacionesOCViewModel : ViewModelBase
    {
        #region Constructor
        public MC_CotizacionesOCViewModel()
        {
            _documentoCompra = new DocumentoCompra();
        }
        #endregion

        #region Valores para el cuadro de Búsqueda

        public String _searchCodigo = "";
        public String searchCodigo { get { return _searchCodigo; } set { _searchCodigo = value; NotifyPropertyChanged("searchCodigo"); } }

        public String _searchProveedor = "";
        public String searchProveedor { get { return _searchProveedor; } set { _searchProveedor = value; NotifyPropertyChanged("searchProveedor"); } }

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

        public DateTime? _searchFechaDesde = null;
        public DateTime? searchFechaDesde { get { return _searchFechaDesde; } set { _searchFechaDesde = value; NotifyPropertyChanged("searchFechaDesde"); } }

        public DateTime? _searchFechaHasta = null;
        public DateTime? searchFechaHasta { get { return _searchFechaHasta; } set { _searchFechaHasta = value; NotifyPropertyChanged("searchFechaHasta"); } }


        #endregion

        #region Valores para la segunda pestana

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
                _statusTab = value;
                //Si cambió el estado de las pestañas también cambio los Header
                //Si la pestaña es para agregar nuevo, limpio los input
                switch (_statusTab)
                {
                    case Tab.BUSQUEDA: detallesTabHeader = "Agregar"; documentoCompra = new DocumentoCompra(); break;//Si es agregar, creo un nuevo objeto Cliente
                    case Tab.AGREGAR: detallesTabHeader = "Agregar"; documentoCompra = new DocumentoCompra(); break;//Si es agregar, creo un nuevo objeto Cliente
                    case Tab.MODIFICAR: detallesTabHeader = "Modificar"; break;
                    case Tab.DETALLES: detallesTabHeader = "Detalles"; break;
                    default: detallesTabHeader = "Agregar"; documentoCompra = new DocumentoCompra(); break;//Si es agregar, creo un nuevo objeto Cliente
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

        #region Lista Documentos de Compra y Edicion de Documentos de Compra
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

        #endregion

        #region Comandos

        public void viewEditDocumentoCompra(Object id)
        {
            try
            {
                this.documentoCompra = listaDocumentosCompra.Single(documentoCompra => documentoCompra.id == (long)id);
                this.statusTab = Tab.MODIFICAR;
                if (this.documentoCompra.tipoDC == "Cotizacion")
                {
                    this.labelCodigo = "Cotizacion";
                    this.labelFechaDC1 = "Fecha Emision";
                    this.labelFechaDC2 = "Fecha Vencimiento";
                }
                if (this.documentoCompra.tipoDC == "Orden de Compra")
                {
                    this.labelCodigo = "Orden Compra";
                    this.labelFechaDC1 = "Fecha Emision";
                    this.labelFechaDC2 = "Fecha Pago";
                }

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
                this.statusTab = Tab.AGREGAR;
                this.labelCodigo = "Cotizacion";
                this.labelFechaDC1 = "Fecha Emision";
                this.labelFechaDC2 = "Fecha Vencimiento";

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
                this.statusTab = Tab.AGREGAR;
                this.labelCodigo = "Orden Compra";
                this.labelFechaDC1 = "Fecha Emision";
                this.labelFechaDC2 = "Fecha Pago";

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public void saveDocumentoCompra(Object obj)
        {
            //if (documentoCompra.id > 0)//Si existe
            //{
            //    if (!MC_DocumentoCompraService.enviarCambios())
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

        public void cancelDocumentoCompra(Object obj)
        {
            this.statusTab = Tab.BUSQUEDA;
            listaDocumentosCompra = MC_DocumentoCompraService.listaDocumentosCompra;
        }

        #endregion
    }
}
