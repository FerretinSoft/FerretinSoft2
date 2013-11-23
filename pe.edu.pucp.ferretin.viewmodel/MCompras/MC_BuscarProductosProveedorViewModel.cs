using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using pe.edu.pucp.ferretin.controller;
using pe.edu.pucp.ferretin.controller.MCompras;
using pe.edu.pucp.ferretin.model;
using pe.edu.pucp.ferretin.viewmodel.Helper;

namespace pe.edu.pucp.ferretin.viewmodel.MCompras
{
    public class MC_BuscarProductosProveedorViewModel : ViewModelBase
    {
        #region Valores para los controles de la ventana

        public Proveedor _proveedor;
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

        //public string _searchProveedor = "";
        //public string searchProveedor 
        //{ 
        //    get 
        //    { 
        //        return _searchProveedor; 
        //    } 
        //    set 
        //    { 
        //        _searchProveedor = value; 
        //        NotifyPropertyChanged("searchProveedor"); 
        //    } 
        //}

        public string _searchProducto = "";
        public string searchProducto 
        { 
            get 
            { 
                return _searchProducto; 
            } 
            set 
            { 
                _searchProducto = value; 
                NotifyPropertyChanged("searchProducto"); 
            } 
        }

        #endregion

        #region Lista de Productos del Proveedor y otras listas

        //private ProveedorProducto _productoProveedor;
        //public ProveedorProducto productoProveedor
        //{
        //    get
        //    {
        //        return _productoProveedor;
        //    }
        //    set
        //    {
        //        _productoProveedor = value;
        //        NotifyPropertyChanged("productoProveedor");
        //    }
        //}

        private IEnumerable<ProveedorProducto> _listaProductosProveedor;
        public IEnumerable<ProveedorProducto> listaProductosProveedor
        {
            get
            {
                if (proveedor == null)
                    _listaProductosProveedor = null;
                else
                    _listaProductosProveedor = ComunService.db.ProveedorProducto.Where(pp => pp.Proveedor == proveedor && pp.Producto.nombre.ToLower().Trim().Contains(searchProducto.ToLower().Trim())).OrderBy(pp => pp.Producto.id);  
           
                return _listaProductosProveedor;
            }
        }


        //public IEnumerable<ProveedorProducto> listaProductosProveedorFinal
        //{
        //    get
        //    {
        //        var sequence = new List<ProveedorProducto>();
        //        if (listaProductosProveedor != null)
        //        {

        //            List<ProveedorProducto> listAux = listaProductosProveedor.ToList();
        //            int cont = listaProductosProveedor.Count();
        //            for (int i = 0; i < cont; i++)
        //            {
        //                if (listAux[i].isSelected)
        //                {
        //                    sequence.Add(listAux[i]);
        //                }
        //            }

        //        }
        //        return sequence;
        //    }
        //}

        #endregion

        #region Relay Commands

        RelayCommand _actualizarListaProductosProveedorCommand;
        public ICommand actualizarListaProductosProveedorCommand
        {
            get
            {
                if (_actualizarListaProductosProveedorCommand == null)
                {
                    _actualizarListaProductosProveedorCommand = new RelayCommand(buscarProductos);
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

        //RelayCommand _agregarListaProductosProveedorCommand;
        //public ICommand agregarListaProductosProveedorCommand
        //{
        //    get
        //    {
        //        if (_agregarListaProductosProveedorCommand == null)
        //        {
        //            _agregarListaProductosProveedorCommand = new RelayCommand(agregarListaProductosProveedor);
        //        }
        //        return _agregarListaProductosProveedorCommand;
        //    }
        //}

        #endregion 

        public void buscarProductos(object obj)
        {
            NotifyPropertyChanged("listaProductosProveedor");
        }
    }
}