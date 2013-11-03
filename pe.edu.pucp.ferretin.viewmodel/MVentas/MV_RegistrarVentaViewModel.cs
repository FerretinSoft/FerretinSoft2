using pe.edu.pucp.ferretin.controller.MAlmacen;
using pe.edu.pucp.ferretin.controller.MVentas;
using pe.edu.pucp.ferretin.model;
using pe.edu.pucp.ferretin.viewmodel.Helper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace pe.edu.pucp.ferretin.viewmodel.MVentas
{
    public class MV_RegistrarVentaViewModel : ViewModelBase
    {
        public MV_RegistrarVentaViewModel()
        {
            venta = new Venta()
            {
                fecha = DateTime.Now
            };
            
        }

        private string _nroDocSeleccionado = "";
        public string nroDocSeleccionado
        {
            get
            {
                return _nroDocSeleccionado;
            }
            set
            {
                _nroDocSeleccionado = value;
                if (value.Length == 8 || value.Length == 11)
                {
                    cargarCliente(null);
                }
                NotifyPropertyChanged("nroDocSeleccionado");
            }
        }
        public string codProdAgregar { get; set; }

        private Venta _venta;
        public Venta venta
        {
            get
            {
                return _venta;
            }
            set
            {
                _venta = value;
            }
        }

        public GridLength widthClienteBar
        {
            get
            {
                return venta.Cliente == null ? new GridLength(0) : GridLength.Auto;
            }
        }

        #region RalayCommand
        RelayCommand _cargarClienteCommand;
        public ICommand cargarClienteCommand
        {
            get
            {
                if (_cargarClienteCommand == null)
                {
                    _cargarClienteCommand = new RelayCommand(cargarCliente);
                }
                return _cargarClienteCommand;
            }
        }

        RelayCommand _agregarProductoCommand;
        public ICommand agregarProductoCommand
        {
            get
            {
                if (_agregarProductoCommand == null)
                {
                    _agregarProductoCommand = new RelayCommand(agregarProducto);
                }
                return _agregarProductoCommand;
            }
        }
        #endregion

        #region Comandos

        public void cargarCliente(Object id)
        {
            Cliente buscado = null;
            try
            {
                buscado = MV_ClienteService.obtenerClienteByNroDoc(nroDocSeleccionado);
            }catch{}

            if(buscado==null){
                MessageBox.Show("No se encontro ningún Cliente con el número de documento proporcionado","No se encontro",MessageBoxButton.OK,MessageBoxImage.Question);   
            }
            venta.Cliente = buscado;
            NotifyPropertyChanged("widthClienteBar");
        }

        public void agregarProducto(Object id)
        {
            if (codProdAgregar != null && codProdAgregar.Length > 0)
            {
                Producto producto = null;
                try{
                    producto = MA_SharedService.obtenerProductoxCodigo(codProdAgregar);
                }catch{}

                if(producto !=null){
                    VentaProducto ventaProducto = new VentaProducto(){
                        Producto = producto,
                        Venta = this.venta,
                        cantidad = 1,
                        montoParcial = producto.precioLista                        
                    };
                    venta.VentaProducto.Add(ventaProducto);
                    NotifyPropertyChanged("venta");
                }
            }
        }

        #endregion
    }
}
