﻿using pe.edu.pucp.ferretin.controller.MCompras;
using pe.edu.pucp.ferretin.model;
using pe.edu.pucp.ferretin.viewmodel.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace pe.edu.pucp.ferretin.viewmodel.MCompras
{
    public class MC_ProveedoresViewModel:ViewModelBase
    {
        #region Constructor
        public MC_ProveedoresViewModel()
        {
            _proveedor = new Proveedor();
        }
        #endregion

     

        #region lista de Proveedores
        private Proveedor _proveedor;
        public Proveedor proveedor
        {
            get
            {
                return _proveedor;
            }

            set
            {
                _proveedor = value;
               
                NotifyPropertyChanged("proveedor");
            }
        }
        private IEnumerable<Proveedor> _listaProveedores;
        public IEnumerable<Proveedor> listaProveedores
        {
            get
            {
                _listaProveedores = MC_ProveedorService.buscarProveedores(searchRuc, searchRazonSoc, searchRubro);
                return _listaProveedores;
            }
            set
            {
                _listaProveedores = value;
                NotifyPropertyChanged("listaProveedores");
            }

        }
        #endregion

        #region Valores para el cuadro de Búsqueda
        public String _searchRuc = "";
        public String searchRuc { get { return _searchRuc; } set { _searchRuc = value; NotifyPropertyChanged("searchRuc"); } }

        public String _searchRazonSoc = "";
        public String searchRazonSoc { get { return _searchRazonSoc; } set { _searchRazonSoc = value; NotifyPropertyChanged("searchRazonSoc"); } }

        public String _searchRubro = "";
        public String searchRubro { get { return _searchRubro; } set { _searchRubro = value; NotifyPropertyChanged("searchRubro"); } }

       
        #endregion

        #region RalayCommand
        RelayCommand _actualizarListaProveedoresCommand;
        public ICommand actualizarListaProveedoresCommand
        {
            get
            {
                if (_actualizarListaProveedoresCommand == null)
                {
                    _actualizarListaProveedoresCommand = new RelayCommand(param => NotifyPropertyChanged("listaProveedores"));
                }
                return _actualizarListaProveedoresCommand;
            }
        }

        RelayCommand _viewEditProveedoresCommand;



        public ICommand viewEditProveedoresCommand
        {
            get
            {
                if (_viewEditProveedoresCommand == null)
                {
                    _viewEditProveedoresCommand = new RelayCommand(viewEditProveedor);
                }
                return _viewEditProveedoresCommand;
            }
        }
        RelayCommand _saveProveedoresCommand;
        public ICommand saveProveedoresCommand
        {
            get
            {
                if (_saveProveedoresCommand == null)
                {
                    _saveProveedoresCommand = new RelayCommand(saveProveedor);
                }
                return _saveProveedoresCommand;
            }
        }
        RelayCommand _cancelProveedorCommand;
        public ICommand cancelProveedorCommand
        {
            get
            {
                if (_cancelProveedorCommand == null)
                {
                    _cancelProveedorCommand = new RelayCommand(cancelProveedor);
                }
                return _cancelProveedorCommand;
            }
        }

        RelayCommand _agregarProveedorCommand;
        public ICommand agregarProveedorCommand
        {
            get
            {
                if (_agregarProveedorCommand == null)
                {
                    _agregarProveedorCommand = new RelayCommand(p => statusTab = Tab.AGREGAR);
                }
                return _agregarProveedorCommand;
            }
        }
        #endregion

        #region Comandos

        public void viewEditProveedor(Object id)
        {
            try
            {
                this.proveedor= listaProveedores.Single(proveedor => proveedor.id == (int)id);
                //if (this.cliente.id_ubigeo != null)
                //{
                //    selectedProvincia = this.cliente.UbigeoDistrito.UbigeoProvincia;
                //    selectedDepartamento = selectedProvincia.UbigeoDepartamento;
                //}
                this.statusTab = Tab.MODIFICAR;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        public void saveProveedor(Object obj)
        {

            if (proveedor.id > 0)//Si existe
            {
                if (!MC_ProveedorService.enviarCambios())
                {
                    MessageBox.Show("No se pudo actualizar al Proveedor");
                }
                else
                {
                    MessageBox.Show("El Proveedor fue guardado con éxito");
                }
            }
            else
            {
                if (!MC_ProveedorService.insertarProveedor(proveedor))
                {
                    MessageBox.Show("No se pudo agregar el nuevo proveedor");
                }
                else
                {
                    MessageBox.Show("El proveedor fue agregado con éxito");
                }
            }
        }
        public void cancelProveedor(Object obj)
        {
            this.statusTab = Tab.BUSQUEDA;
            listaProveedores = MC_ProveedorService.listaProveedores;
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
                    case Tab.BUSQUEDA: detallesTabHeader = "Agregar"; proveedor = new Proveedor(); break;
                    case Tab.AGREGAR: detallesTabHeader = "Agregar"; proveedor = new Proveedor(); break;
                    case Tab.MODIFICAR: detallesTabHeader = "Modificar"; break;
                    case Tab.DETALLES: detallesTabHeader = "Detalles"; break;
                    default: detallesTabHeader = "Agregar"; proveedor = new Proveedor(); break;
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

    }
}
