using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pe.edu.pucp.ferretin.viewmodel.Helper;
using pe.edu.pucp.ferretin.model;
using pe.edu.pucp.ferretin.controller.MAlmacen;
using System.Windows.Input;

namespace pe.edu.pucp.ferretin.viewmodel.MAlmacen
{
    public class MA_MantenimientoProductos_SeleccionColorViewModel : ViewModelBase
    {
        private Int16 idColor { set; get; }

        private IEnumerable<Color> _listaColores;
        public IEnumerable<Color> listaColores
        {
            get
            {
                _listaColores = MA_ProductoService.obtenerTodosColores();
                return _listaColores;
            }
            set
            {
                _listaColores = value;
                OnPropertyChanged("listaColores");
            }
        }


        #region RelayCommand


        RelayCommand _checkTreeCommand;
        public ICommand checkTreeCommand
        {
            get
            {
                if (_checkTreeCommand == null)
                {
                    _checkTreeCommand = new RelayCommand(guardarColorProducto);
                }
                return _checkTreeCommand;
            }
        }
        #endregion 

        private void guardarColorProducto(object obj)
        {
            ProductoColor pc = new ProductoColor();
            
            //pc.id_producto=;
            pc.id_color=idColor;

        }

        public void sayHello()
        {
            Console.WriteLine("Hola...");
        }


    }
}
