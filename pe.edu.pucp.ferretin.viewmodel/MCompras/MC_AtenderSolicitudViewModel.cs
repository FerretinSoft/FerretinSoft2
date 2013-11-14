using pe.edu.pucp.ferretin.controller;
using pe.edu.pucp.ferretin.controller.MAlmacen;
using pe.edu.pucp.ferretin.controller.MCompras;
using pe.edu.pucp.ferretin.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pe.edu.pucp.ferretin.viewmodel.Helper;
using System.Windows.Input;

namespace pe.edu.pucp.ferretin.viewmodel.MCompras
{
    public class MC_AtenderSolicitudViewModel : ViewModelBase
    {
        #region lista Productos de la  Solicitud
        //private ProductoAlmacen _productoSol;
        //public ProductoAlmacen productoSol
        //{
        //    get
        //    {
        //        return _productoSol;
        //    }
        //    set
        //    {
        //        _productoSol = value;
        //        NotifyPropertyChanged("productoSol");
        //    }
        //}
        //private Dictionary<ProductoAlmacen,decimal> _listaProductosSol;
        //public Dictionary<ProductoAlmacen, decimal> listaProductosSol
        //{
        //    get
        //    {   Usuario usuario = ComunService.usuarioL;
        //        Tienda tienda = usuario.Empleado.tiendaActual;
        //        _listaProductosSol = MA_SharedService.obtenerProductosPorAbastecer(tienda);
        //        return _listaProductosSol;
        //    }
        //    set
        //    {
        //        _listaProductosSol = value;
        //        NotifyPropertyChanged("listaProductosSol");
                

        //    }

        //}

        private ProductoSol _productoSol;
        public ProductoSol productoSol
        {
            get
            {
                return _productoSol;
            }
            set
            {
                _productoSol = value;
                NotifyPropertyChanged("productoSol");
            }
        }
        private List<ProductoSol> _listaProductosSol;
        public List<ProductoSol> listaProductosSol
        {
            get
            {
                _listaProductosSol = new List<ProductoSol>();
                Usuario usuario = ComunService.usuarioL;
                Tienda tienda = usuario.Empleado.tiendaActual;
                ProductoSol actual;
                Dictionary<ProductoAlmacen,decimal> diccionario = MA_SharedService.obtenerProductosPorAbastecer(tienda);
                foreach (var entry in diccionario)
                {
                    actual = new ProductoSol();
                    actual.cantidad = entry.Value;
                    actual.producto = entry.Key;
                    _listaProductosSol.Add(actual);
                    // do something with entry.Value or entry.Key                   
                }
                int i;
                for (i = 0; i < _listaProductosSol.Count(); i++)
                {
                    _listaProductosSol[i].posiProveedor = MC_ProveedorService.obtenerPosiblesProveedores(_listaProductosSol[i].producto);
                }

                return _listaProductosSol;
            }
            set
            {
                _listaProductosSol = value;
                NotifyPropertyChanged("listaProductosSol");
            }
        }

        #endregion

        RelayCommand _generarOCSCommand;
        public ICommand generarOCSCommand
        {
            get
            {
                if (_generarOCSCommand == null)
                {
                    _generarOCSCommand = new RelayCommand(generarOCS);
                    NotifyPropertyChanged("productoSol");
                }
                return _generarOCSCommand;

            }
        }

        public void generarOCS(Object id)
        {
            int i;
            for (i = 0; i < this.listaProductosSol.Count(); i++)
            {
                Proveedor p = listaProductosSol[i].selectedProveedor;
            }
            //this._productoSol;
            //Proveedor buscado = null;
            //int i;
            //try
            //{
            //    buscado = MC_ProveedorService.buscarProveedorByName(this._proveedorNombre);
            //    documentoCompra.Proveedor = buscado;
            //    NotifyPropertyChanged("documentoCompra");
            //}
            //catch { }

            //if (buscado == null)
            //{
            //    MessageBox.Show("No se encontro ninguna Proveedor", "No se encontro", MessageBoxButton.OK, MessageBoxImage.Question);
            //}
        }

    }
} 