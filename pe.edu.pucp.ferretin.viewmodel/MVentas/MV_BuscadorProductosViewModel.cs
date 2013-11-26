using pe.edu.pucp.ferretin.controller;
using pe.edu.pucp.ferretin.model;
using pe.edu.pucp.ferretin.viewmodel.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace pe.edu.pucp.ferretin.viewmodel.MVentas
{
    public class MV_BuscadorProductosViewModel : ViewModelBase
    {
        public string searchNombre
        {
            get;
            set;
        }

        private Tienda _tienda;
        public Tienda tienda
        {
            get{
                return _tienda;
            }
            set
            {
                _tienda = value;
                NotifyPropertyChanged("tienda");
            }
        }

        private IEnumerable<ProductoAlmacen> _listaProductos;
        public IEnumerable<ProductoAlmacen> listaProductos
        {
            get
            {
                
                if (_listaProductos == null)
                {
                    if (tienda != null)
                    {
                        var productos = ComunService.db.ProductoAlmacen.Where(pa => pa.Tienda.id.Equals(tienda.id) && pa.estado.HasValue && pa.estado == 1 && pa.Producto != null && pa.Producto.estado == 1).OrderBy(pa=>pa.id);
                        _listaProductos = productos;
                    }
                }
                if (_listaProductos != null)
                    return _listaProductos.Where(p => p.Producto.nombre.ToLower().Contains(searchNombre == null ? "" : searchNombre.ToLower()));
                else
                    return null;

            }
        }






        RelayCommand _buscarProductoCommand;
        public ICommand buscarProductoCommand
        {
            get
            {
                if (_buscarProductoCommand == null)
                {
                    _buscarProductoCommand = new RelayCommand(buscarProductos);
                }
                return _buscarProductoCommand;
            }
        }

        public void buscarProductos(object obj)
        {
            NotifyPropertyChanged("listaProductos");
        }
    }
}
