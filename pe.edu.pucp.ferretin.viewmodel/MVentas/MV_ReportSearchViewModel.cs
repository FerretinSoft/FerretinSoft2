using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using pe.edu.pucp.ferretin.controller;
using pe.edu.pucp.ferretin.model;
using pe.edu.pucp.ferretin.viewmodel.Helper;

namespace pe.edu.pucp.ferretin.viewmodel.MVentas
{
    class MV_ReportSearchViewModel : ViewModelBase
    {
        public string searchNombre
        {
            get;
            set;
        }

        private Tienda _tienda;
        public Tienda tienda
        {
            get
            {
                return _tienda;
            }
            set
            {
                _tienda = value;
                NotifyPropertyChanged("tienda");
            }
        }

        private IEnumerable<Producto> _listaProductos;
        public IEnumerable<Producto> listaProductos
        {
            get
            {

                if (_listaProductos == null)
                {
                    if (tienda != null)
                    {
                        var productos = ComunService.db.Producto;
                        _listaProductos = productos;
                    }
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
