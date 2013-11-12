﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using pe.edu.pucp.ferretin.controller.MCompras;
using pe.edu.pucp.ferretin.model;
using pe.edu.pucp.ferretin.viewmodel.Helper;

namespace pe.edu.pucp.ferretin.viewmodel.MCompras
{
    public class MC_BuscarProductosProveedorViewModel : ViewModelBase
    {
        #region Constructor
        public MC_BuscarProductosProveedorViewModel()
        {
            _productoProveedor = new ProveedorProducto();  
        }
        #endregion

        #region Valores para los controles de la ventana

        public string _searchProveedor = "";
        public string searchProveedor { get { return _searchProveedor; } set { _searchProveedor = value; NotifyPropertyChanged("searchProveedor"); } }

        public string _searchProducto = "";
        public string searchProducto { get { return _searchProducto; } set { _searchProducto = value; NotifyPropertyChanged("searchProducto"); } }

        #endregion

        #region Lista de Productos del Proveedor y otras listas

        private ProveedorProducto _productoProveedor;
        public ProveedorProducto productoProveedor
        {
            get
            {
                return _productoProveedor;
            }
            set
            {
                _productoProveedor = value;
                NotifyPropertyChanged("productoProveedor");
            }
        }

        private IEnumerable<ProveedorProducto> _listaProductosProveedor = null;
        public IEnumerable<ProveedorProducto> listaProductosProveedor
        {
            get
            {
                if (searchProducto != "" || searchProveedor != "")
                {
                    if (searchProveedor == "")
                    {
                        MessageBox.Show("Debe ingresar el Proveedor");
                    }
                    else
                        _listaProductosProveedor = MC_ProveedorService.obtenerProductosProveedor(searchProveedor, searchProducto);
                }
                if (searchProveedor == "" && searchProducto == "")
                    _listaProductosProveedor = null;
                
                return _listaProductosProveedor;
            }
            set
            {
                _listaProductosProveedor = value;
                NotifyPropertyChanged("listaProductosProveedor");
            }
        }

        private IEnumerable<ProveedorProducto> _listaProductosProveedorFinal = null;
        public IEnumerable<ProveedorProducto> listaProductosProveedorFinal
        {
            get
            {
                return _listaProductosProveedorFinal;
            }
            set
            {
                _listaProductosProveedorFinal = value;
                NotifyPropertyChanged("listaProductosProveedorFinal");
            }
        }
        #endregion

        #region Relay Commands

        RelayCommand _actualizarListaProductosProveedorCommand;
        public ICommand actualizarListaProductosProveedorCommand
        {
            get
            {
                if (_actualizarListaProductosProveedorCommand == null)
                {
                    _actualizarListaProductosProveedorCommand = new RelayCommand(param => NotifyPropertyChanged("listaProductosProveedor"));
                }
                return _actualizarListaProductosProveedorCommand;
            }
        }

        //RelayCommand _agregarListaProductosProveedorCommand;
        //public ICommand agregarListaProductosProveedorCommand
        //{
        //    get
        //    {
        //        if (_agregarListaProductosProveedorCommand == null)
        //        {
        //            _agregarListaProductosProveedorCommand = new RelayCommand(param => NotifyPropertyChanged("listaProductosProveedorFinal"));
        //        }
        //        return _agregarListaProductosProveedorCommand;
        //    }
        //}

        RelayCommand _agregarListaProductosProveedorCommand;
        public ICommand agregarListaProductosProveedorCommand
        {
            get
            {
                if (_agregarListaProductosProveedorCommand == null)
                {
                    _agregarListaProductosProveedorCommand = new RelayCommand(agregarListaProductosProveedor);
                }
                return _agregarListaProductosProveedorCommand;
            }
        }

        #endregion 

        #region Commands

        public void agregarListaProductosProveedor(Object id)
        {
            int i;
            try
            {
                if (listaProductosProveedor != null)
                {
                    var sequence = new List<ProveedorProducto>();
                    List<ProveedorProducto> listAux = listaProductosProveedor.ToList();
                    for (i = 0; i < listaProductosProveedor.Count(); i++)
                    {
                        ProveedorProducto linea;
                        if (listAux[i].isSelected)
                        {
                            linea = new ProveedorProducto() { Producto = listAux[i].Producto, Proveedor = listAux[i].Proveedor, id = listAux[i].id, UnidadMedida = listAux[i].UnidadMedida, estado = listAux[i].estado, precio = listAux[i].precio, tiempoEntrega = listAux[i].tiempoEntrega, isSelected = listAux[i].isSelected };
                            sequence.Add(linea);
                        }                          
                    }
                    listaProductosProveedorFinal = sequence;
                }
                NotifyPropertyChanged("listaProductosProveedorFinal");
            }
            catch { }
        }

        #endregion
    }
}