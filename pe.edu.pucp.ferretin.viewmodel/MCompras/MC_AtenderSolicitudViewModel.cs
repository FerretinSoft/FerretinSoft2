using pe.edu.pucp.ferretin.controller;
using pe.edu.pucp.ferretin.controller.MAlmacen;
using pe.edu.pucp.ferretin.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                foreach (var entry in MA_SharedService.obtenerProductosPorAbastecer(tienda))
                {
                    actual = new ProductoSol();
                    actual.cantidad = entry.Value;
                    actual.producto = entry.Key;
                    _listaProductosSol.Add(actual);
                    // do something with entry.Value or entry.Key
                    
                }
               // MA_SharedService.obtenerProductosPorAbastecer(tienda);
                return _listaProductosSol;
            }
            set
            {
                _listaProductosSol = value;
                NotifyPropertyChanged("listaProductosSol");


            }

        }

        #endregion
    }
} 