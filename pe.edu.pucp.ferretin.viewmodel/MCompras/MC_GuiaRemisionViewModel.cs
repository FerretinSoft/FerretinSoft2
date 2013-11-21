using System;
using System.Collections.Generic;
using System.Linq;
using pe.edu.pucp.ferretin.model;
using pe.edu.pucp.ferretin.viewmodel.Helper;
using pe.edu.pucp.ferretin.controller.MCompras;
using System.Windows.Input;
using System.Windows;
using pe.edu.pucp.ferretin.controller;

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

        //private IEnumerable<GuiaRemisionProducto> _listaGuiaRemisionProducto = null;
        //public IEnumerable<GuiaRemisionProducto> listaGuiaRemisionProducto
        //{
        //    get
        //    {
        //        if (guiaRemision.id >0)
        //            _listaGuiaRemisionProducto = MC_GuiaRemisionService.buscarProductosGuiaRemision(guiaRemision);
        //        return _listaGuiaRemisionProducto;
        //    }
        //    set
        //    {
        //        _listaGuiaRemisionProducto = value;
        //        NotifyPropertyChanged("listaGuiaRemisionProducto");
        //    }
        //}

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
                        ordenCompraCod = ""; 
                        guiaRemision = new GuiaRemision(); 
                        //listaGuiaRemisionProducto = null;
                        guiaRemision.Tienda = MC_ComunService.usuarioL.Empleado.tiendaActual;break;

                    case Tab.MODIFICAR: 
                        detallesTabHeader = "Modificar"; 
                        break;

                    case Tab.DETALLES: 
                        detallesTabHeader = "Detalles";  
                        break;

                    default: 
                        detallesTabHeader = "Agregar"; 
                        guiaRemision = new GuiaRemision(); 
                        break;//Si es agregar, creo un nuevo objeto Guia de Remision
                }
                NotifyPropertyChanged("statusTab");
                //Cuando se cambia el status, tambien se tiene que actualizar el currentIndex del tab
                NotifyPropertyChanged("currentIndexTab"); //Hace que cambie el tab automaticamente
                NotifyPropertyChanged("guiaRemision");
                //NotifyPropertyChanged("listaGuiaRemisionProducto");
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
                //NotifyPropertyChanged("listaGuiaRemisionProducto");
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
                //NotifyPropertyChanged("guiaRemision");
                //NotifyPropertyChanged("listaGuiaRemisionProducto");
                //NotifyPropertyChanged("ordenCompraCod");
                this.statusTab = Tab.MODIFICAR;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public void saveGuiaRemision(Object obj)
        {
            bool exito = false;
            if (guiaRemision.id > 0)//Si existe
            {
                ComunService.idVentana(37);
                if (!MC_GuiaRemisionService.enviarCambios())
                {
                    MessageBox.Show("No se pudo actualizar la guia de remision");
                }
                else
                {
                    MessageBox.Show("La guia de remision fue guardado con éxito");
                    exito = true;
                }
            }
            else
            {
                ComunService.idVentana(36);
                if (!MC_GuiaRemisionService.insertarGuiaRemision(guiaRemision))
                {
                    MessageBox.Show("No se pudo agregar la nueva guia de remision");
                }
                else
                {
                    MessageBox.Show("La guia de remision se agrego con exito");
                    exito = true;
                }
            }
            if(exito)
            {
                NotifyPropertyChanged("listaGuiasRemision");
            this.statusTab = Tab.BUSQUEDA;
            }           
        }

        public void cancelGuiaRemision(Object obj)
        {
            MessageBoxResult result = MessageBox.Show("Al salir, perderá todos los datos ingresados. ¿Desea continuar?",
       "ATENCIÓN", MessageBoxButton.OKCancel, MessageBoxImage.Warning, MessageBoxResult.Cancel);
            if (result == MessageBoxResult.OK)
            {
                this.statusTab = Tab.BUSQUEDA;
                listaGuiasRemision = MC_GuiaRemisionService.listaGuiasRemision;
            }
        }


        private String _ordenCompraCod;
        public String ordenCompraCod
        {
            get
            {
                try
                {
                    if ("".Equals(this.guiaRemision.DocumentoCompra.codigo) || this.guiaRemision.DocumentoCompra.codigo == null)
                        return _ordenCompraCod;
                    else
                        return guiaRemision.DocumentoCompra.codigo;
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
                buscado = MC_DocumentoCompraService.obtenerDCByCodigo(this._ordenCompraCod);

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
