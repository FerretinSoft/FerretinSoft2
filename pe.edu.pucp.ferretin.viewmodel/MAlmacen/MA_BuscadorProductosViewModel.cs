using pe.edu.pucp.ferretin.controller;
using pe.edu.pucp.ferretin.model;
using pe.edu.pucp.ferretin.viewmodel.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace pe.edu.pucp.ferretin.viewmodel.MAlmacen
{
    public class MA_BuscadorProductosViewModel : ViewModelBase
    {
        public string searchNombre
        {
            get;
            set;
        }

        private IEnumerable<Producto> _listaProductos;
        public IEnumerable<Producto> listaProductos
        {
            get
            {
                if (_listaProductos == null)
                {
                    var productos = ComunService.db.Producto.OrderBy(pa => pa.id);
                    _listaProductos = productos;                    
                }
                if (_listaProductos != null)
                    return _listaProductos.Where(p => p.nombre.ToLower().Contains(searchNombre == null ? "" : searchNombre.ToLower()));
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
