﻿using pe.edu.pucp.ferretin.controller;
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
                    case Tab.BUSQUEDA:
                        {
                            ComunService.Clean();
                            detallesTabHeader = soloSeleccionarPromocion ? "Detalles" : "Agregar"; break;//Si es agregar, creo un nuevo objeto Promocion
                        }
                    case Tab.AGREGAR: 
                        {
                            detallesTabHeader = "Agregar"; 
                            var p = new Promocion();
                            foreach (var tienda in MS_SharedService.tiendas)
                            {

                                p.PromocionTienda.Add(new PromocionTienda()
                                {
                                    activo = false,
                                    Tienda = tienda
                                });
                            }
                            promocion = p;
                            break;
                        }
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

        public string codProdAgregar { get; set; }

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

        public IEnumerable<PromocionTipo> tiposPromocion
        {
            get
            {
                
                return MV_PromocionService.db.PromocionTipo.ToList();
            }
        }

        #region RelayCommand
        RelayCommand _agregarProductoCommand;
        public ICommand agregarProductoCommand
        {
            get
            {
                if (_agregarProductoCommand == null)
                {
                    _agregarProductoCommand = new RelayCommand(agregarProducto);
                }
                return _agregarProductoCommand;
            }
        }
        
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

        RelayCommand _viewEditPromocionCommand;
        public ICommand viewEditPromocionCommand
        {
            get
            {
                if (_viewEditPromocionCommand == null)
                {
                    _viewEditPromocionCommand = new RelayCommand(viewEditPromocion);
                }
                return _viewEditPromocionCommand;
            }
        }

        RelayCommand _savePromocionCommand;
        public ICommand savePromocionCommand
        {
            get
            {
                if (_savePromocionCommand == null)
                {
                    _savePromocionCommand = new RelayCommand(savePromocion);
                }
                return _savePromocionCommand;
            }
        }

        RelayCommand _cancelPromocionCommand;
        public ICommand cancelPromocionCommand
        {
            get
            {
                if (_cancelPromocionCommand == null)
                {
                    _cancelPromocionCommand = new RelayCommand(cancelPromocion);
                }
                return _cancelPromocionCommand;
            }
        }

        
        #endregion

        #region Comandos

        public void viewEditPromocion(Object id)
        {
            try
            {
                this.promocion = MV_PromocionService.db.Promocion.Single(promocion => promocion.id == (int)id);
                foreach (var pp in this.promocion.PromocionProducto)
                {
                    pp.PropertyChanged += promocion_PropertyChanged;
                }
                if (soloSeleccionarPromocion)
                    this.statusTab = Tab.DETALLES;
                else
                    this.statusTab = Tab.MODIFICAR;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        public void savePromocion(Object obj)
        {
            if (soloSeleccionarPromocion)
            {

            }
            else
            {
                String result = String.Empty;
                if (promocion != null)
                {
                    if (String.IsNullOrEmpty(promocion.nombre))
                    {
                        result = "Ingrese un nombre para la promoción";
                    }
                    else
                    {
                        if (promocion.fechaDesde != null && promocion.fechaHasta != null)
                        {
                            if (DateTime.Compare(promocion.fechaDesde.Value, promocion.fechaHasta.Value) > 0)
                            {
                                result = "La fecha desde debe ser menor que la fecha hasta";
                            }
                            else
                            {
                                if (promocion.PromocionTienda.Count <= 0)
                                {
                                    result = "Debe seleccionar al menos una tienda";
                                }
                                else
                                {
                                    if (promocion.PromocionProducto.Count <= 0)
                                    {
                                        result = "Debe agregar al menos un producto";
                                    }
                                    else
                                    {
                                        result = String.Empty;
                                        var pps = promocion.PromocionProducto;
                                        foreach (var pp in pps)
                                        {
                                            if (result.Length > 0) break;
                                            if (pp.PromocionTipo == null)
                                            {
                                                result = "El producto no tiene un tipo de promoción seleccionado:\n" + pp.Producto.nombre.ToUpper();
                                            }
                                        }
                                            
                                    }
                                }
                            }
                        }
                        else
                        {
                            result = "Debe seleccionar un rango de duración de la promoción válido";

                            
                        }
                    }
                }
                if (result.Length > 0)
                {
                    MessageBox.Show(result, "Mensaje", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return;
                }

                if (promocion.id > 0)//Si existe
                {
                    ComunService.idVentana(48);
                    if (!MV_PromocionService.enviarCambios())
                    {
                        MessageBox.Show("No se pudo actualizar el promocion");
                    }
                    else
                    {
                        MessageBox.Show("La promoción fue guardada con éxito");
                        listaPromociones = null;
                        statusTab = Tab.BUSQUEDA;
                    }
                }
                else
                {
                    ComunService.idVentana(48);
                    if (!MV_PromocionService.insertarPromocion(promocion))
                    {
                        MessageBox.Show("No se pudo agregar el nuevo promocion");
                    }
                    else
                    {
                        listaPromociones = null;
                        statusTab = Tab.BUSQUEDA;
                        MessageBox.Show("La promoción fue agregado con éxito");
                    }
                }
            }
        }
        public void cancelPromocion(Object obj)
        {
            MessageBoxResult result =MessageBox.Show("Al salir, perderá todos los datos ingresados. ¿Desea continuar?",
                                        "ATENCIÓN", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
            if (result == MessageBoxResult.OK)
            {
                ComunService.Clean();
                listaPromociones = null;
                this.statusTab = Tab.BUSQUEDA;
            }
        }

        

        public void agregarProducto(Object id)
        {
            if (codProdAgregar != null && codProdAgregar.Length > 0)
            {
                Producto producto = null;
                try
                {
                    producto = MA_SharedService.obtenerProductoxCodigo(codProdAgregar);
                }
                catch { }

                if (producto != null)
                {
                    PromocionProducto promocionProducto = null;
                    if (promocion.PromocionProducto.Count(vp => vp.Producto.id == producto.id) == 1)
                    {
                        MessageBox.Show("Este producto ya se encuentra en la lista");
                        //promocion.PromocionProducto.Single(vp => vp.Producto.id == producto.id).stockTotal++;
                    }
                    else
                    {
                        promocionProducto = new PromocionProducto()
                        {
                            Producto = producto,
                            cantMulUnidades = 0,
                            descuentoPorcentaje = 0,
                            Promocion = this.promocion,
                            stockActual = 0,
                            stockTotal = 1,
                            maxPromVenta = 1
                        };
                        promocionProducto.PropertyChanged += promocion_PropertyChanged;
                        promocion.PromocionProducto.Add(promocionProducto);
                    }
                    NotifyPropertyChanged("promocion");
                }
                else
                {
                    MessageBox.Show("No se encontro ningun producto con el código proporcionado");
                }
            }
        }

        /// <summary>
        /// Cuando cambia una propiedad de PromocionProducto
        /// </summary>
        /// <param name="sender">PromocionProducto</param>
        /// <param name="e"></param>
        void promocion_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            var pp = sender as PromocionProducto;
            if (pp.PromocionTipo != null)
            {
                pp.cantMulUnidades = pp.PromocionTipo.cantidadUnidades??0;
                pp.descuentoPorcentaje = pp.PromocionTipo.descuento??0;
            }
            NotifyPropertyChanged("promocion");
        }
        #endregion
    }
}
