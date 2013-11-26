using pe.edu.pucp.ferretin.controller.MAlmacen;
using pe.edu.pucp.ferretin.controller.MSeguridad;
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
    public class MA_MantenimientoProductosEdicionStockViewModel:ViewModelBase
    {
        public Producto _producto;
        public Producto producto
        {
            get
            {
                return _producto;
            }
            set
            {
                _producto = value;
                MA_ProductoService.crearStockProductoAlmacen(_producto.id);
                productoAlmacen = MA_ProductoService.obtenerStockProductoAlmacen(_producto.id).ToList();
                NotifyPropertyChanged("producto");
            }

        }

        public List<ProductoAlmacen> _productoAlmacen;
        public List<ProductoAlmacen> productoAlmacen
        {
            get
            {   
                return _productoAlmacen;
            }
            set
            {   
                _productoAlmacen = value;
                NotifyPropertyChanged("productoAlmacen");
            }
        }

        public IEnumerable<Tienda> _listaTiendas;
        public IEnumerable<Tienda> listaTiendas
        {
            get
            {
                _listaTiendas = MS_TiendaService.listaTiendas;
                return _listaTiendas;
            }

            set
            {
                _listaTiendas = value;
                NotifyPropertyChanged("listaTiendas");
            }
        }

        RelayCommand _guardarStockCommand;
        public ICommand guardarStockCommand
        {
            get
            {
                if (_guardarStockCommand == null)
                {
                    _guardarStockCommand = new RelayCommand(guardarStockBtn);
                }
                return _guardarStockCommand;
            }
        }

        public void guardarStockBtn(Object obj)
        {
            MA_ProductoService.guardarStockProductoAlmacen();
            Window w = (Window)obj;
            MessageBox.Show("Stock guardado con éxito");
            w.Close();
        }
        
    }
}
