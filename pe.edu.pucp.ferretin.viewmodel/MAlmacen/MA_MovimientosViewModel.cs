﻿using pe.edu.pucp.ferretin.controller.MAlmacen;
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
    public class MA_MovimientosViewModel : ViewModelBase
    {
        #region Lista Movimientos y Edicion de Movimientos
        private Movimiento _movimiento;
        public Movimiento movimiento
        {
            get
            {
                return _movimiento;
            }
            set
            {
                _movimiento = value;
                /*if (value.id_ubigeo != null)
                {
                    String id_distrito = value.id_ubigeo;
                    String id_provincia = value.UbigeoDistrito.id_ubig_provincia;
                    String id_departamento = value.UbigeoDistrito.UbigeoProvincia.id_ubig_departamento;
                    distritos = MV_ClienteService.distritos.Where(distrito => distrito.id_ubig_provincia.Equals(id_provincia));
                }*/
                NotifyPropertyChanged("movimiento");
            }
        }

        private IEnumerable<Movimiento> _listaMovimientos;
        public IEnumerable<Movimiento> listaMovimientos
        {
            get
            {
                //String searchTipoDocumento = this.searchTipoDocumento == 1 ? "DNI" : (this.searchTipoDocumento == 2 ? "RUC" : "");
                Dictionary<String, Object> parametros = new Dictionary<String, Object>();
                //armar diccionario de parametros
                _listaMovimientos = MA_MovimientosService.ObtenerListaMovimientos(parametros);

                return _listaMovimientos;
            }
            set
            {
                _listaMovimientos = value;
                NotifyPropertyChanged("listaMovimientos");
            }
        }
        #endregion

        #region Constructor
        public MA_MovimientosViewModel()
        {
            _movimiento = new Movimiento();
        }
        #endregion

        #region Valores para el cuadro de Búsqueda
        public Tienda _searchAlmacen;
        public Tienda searchAlmacen { get { return _searchAlmacen; } set { _searchAlmacen = value; NotifyPropertyChanged("searchAlmacen"); } }

        public MovimientoEstado _searchEstado;
        public MovimientoEstado searchEstado { get { return _searchEstado; } set { _searchEstado = value; NotifyPropertyChanged("searchEstado"); } }
        
        public DateTime _searchFechaDesde = DateTime.MinValue;
        public DateTime searchFechaDesde { get { return _searchFechaDesde; } set { _searchFechaDesde= value; NotifyPropertyChanged("searchFechaDesde"); } }

        public DateTime _searchFechaHasta = DateTime.MaxValue;
        public DateTime searchFechaHasta { get { return _searchFechaHasta; } set { _searchFechaHasta= value; NotifyPropertyChanged("searchFechaHasta"); } }

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
                    case Tab.BUSQUEDA: detallesTabHeader = "Agregar"; movimiento = new Movimiento(); break;//Si es agregar, creo un nuevo objeto Cliente
                    case Tab.AGREGAR: detallesTabHeader = "Agregar"; movimiento = new Movimiento(); break;//Si es agregar, creo un nuevo objeto Cliente
                    case Tab.MODIFICAR: detallesTabHeader = "Modificar"; break;
                    case Tab.DETALLES: detallesTabHeader = "Detalles"; break;
                    default: detallesTabHeader = "Agregar"; movimiento = new Movimiento(); break;//Si es agregar, creo un nuevo objeto Cliente
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
        RelayCommand _actualizarListaMovimientosCommand;
        public ICommand actualizarListaMovimientosCommand
        {
            get
            {
                if (_actualizarListaMovimientosCommand == null)
                {
                    _actualizarListaMovimientosCommand = new RelayCommand(param => NotifyPropertyChanged("listaMovimientos"));
                }
                return _actualizarListaMovimientosCommand;
            }
        }
        RelayCommand _viewEditMovimientoCommand;
        public ICommand viewEditMovimientoCommand
        {
            get
            {
                if (_viewEditMovimientoCommand == null)
                {
                    _viewEditMovimientoCommand = new RelayCommand(viewEditMovimiento);
                }
                return _viewEditMovimientoCommand;
            }
        }
        RelayCommand _saveMovimientoCommand;
        public ICommand saveMovimientoCommand
        {
            get
            {
                if (_saveMovimientoCommand == null)
                {
                    _saveMovimientoCommand = new RelayCommand(saveMovimiento);
                }
                return _saveMovimientoCommand;
            }
        }
        RelayCommand _cancelMovimientoCommand;
        public ICommand cancelMovimientoCommand
        {
            get
            {
                if (_cancelMovimientoCommand == null)
                {
                    _cancelMovimientoCommand = new RelayCommand(cancelMovimiento);
                }
                return _cancelMovimientoCommand;
            }
        }

        RelayCommand _newMovimientoCommand;
        public ICommand newMovimientoCommand
        {
            get
            {
                if (_newMovimientoCommand == null)
                {
                    _newMovimientoCommand = new RelayCommand(newMovimiento);
                }
                return _newMovimientoCommand;
            }
        }
        #endregion

        #region Comandos

        public void viewEditMovimiento(Object id)
        {
            try
            {
                this.movimiento = listaMovimientos.Single(mov => mov.id == (int)id);
                //aqui preparar las condiciones para cambiar el tab this.statusTab = Tab.MODIFICAR;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public void newMovimiento(Object id)
        {
            try
            {
                //aqui preparar las condiciones para cambiar el tab this.statusTab = Tab.MODIFICAR;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public void saveMovimiento(Object obj)
        {

            if (movimiento.id > 0)//Si existe
            {
                if (!MA_MovimientosService.enviarCambios())
                {
                    MessageBox.Show("No se pudo actualizar el movimiento");
                }
                else
                {
                    MessageBox.Show("El movimiento fue guardado con éxito");
                }
            }
            else
            {
                if (!MA_MovimientosService.InsertarMovimiento(movimiento))
                {
                    MessageBox.Show("No se pudo agregar el nuevo movimiento");
                }
                else
                {
                    MessageBox.Show("El movimiento fue agregado con éxito");
                }
            }
        }

        public void cancelMovimiento(Object obj)
        {
            //ir a la ventana de busqueda this.statusTab = Tab.BUSQUEDA;
            listaMovimientos = MA_MovimientosService.ListaMovimientos;
        }
        #endregion
    }
}
