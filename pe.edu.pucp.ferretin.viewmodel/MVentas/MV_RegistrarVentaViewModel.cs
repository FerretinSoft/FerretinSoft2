using pe.edu.pucp.ferretin.controller;
using pe.edu.pucp.ferretin.controller.MAlmacen;
using pe.edu.pucp.ferretin.controller.MSeguridad;
using pe.edu.pucp.ferretin.controller.MVentas;
using pe.edu.pucp.ferretin.model;
using pe.edu.pucp.ferretin.viewmodel.Helper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace pe.edu.pucp.ferretin.viewmodel.MVentas
{
    public class MV_RegistrarVentaViewModel : ViewModelBase
    {

        public MV_RegistrarVentaViewModel()
        {
            venta = new Venta()
            {
                fecha = DateTime.Now,
                igvActual = MS_SharedService.obtenerIGV(),
                Usuario = MS_SharedService.usuarioL
            };
            venta.VentaProducto.ListChanged += actualizarMontosVenta;
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

        private ImageSource _clienteImagen;
        public ImageSource clienteImagen
        {
            get
            {
                if (venta!=null && venta.Cliente != null && venta.Cliente.imagen != null)
                {
                    MemoryStream strm = new MemoryStream();
                    strm.Write(venta.Cliente.imagen.ToArray(), 0, venta.Cliente.imagen.Length);
                    strm.Position = 0;
                    System.Drawing.Image img = System.Drawing.Image.FromStream(strm);

                    BitmapImage bitmapImage = new BitmapImage();
                    bitmapImage.BeginInit();
                    MemoryStream memoryStream = new MemoryStream();
                    img.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Bmp);
                    memoryStream.Seek(0, SeekOrigin.Begin);
                    bitmapImage.StreamSource = memoryStream;
                    bitmapImage.EndInit();

                    _clienteImagen = bitmapImage;
                }
                return _clienteImagen;
            }
            set
            {
                _clienteImagen = value;
                NotifyPropertyChanged("clienteImagen");
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

        RelayCommand _pagarCommand;
        public ICommand pagarCommand
        {
            get
            {
                if (_pagarCommand == null)
                {
                    _pagarCommand = new RelayCommand(pagar,canPagar);
                }
                return _pagarCommand;
            }
        }
        #endregion

        #region Comandos

        public void pagar(object param)
        {
            
        }

        public bool canPagar(object param)
        {
            return this.venta != null && (venta.total > 0 || (venta.Cliente!=null && venta.puntosCanjeados > 0 && venta.puntosCanjeados <= venta.Cliente.puntosActual ) );
        }

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
            NotifyPropertyChanged("clienteImagen");
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
                    if (venta.VentaProducto.Count(vp => vp.Producto.id == producto.id) == 1)
                    {
                        venta.VentaProducto.Single(vp => vp.Producto.id == producto.id).cantidad++;
                    }
                    else
                    {
                        VentaProducto ventaProducto = new VentaProducto();
                        ventaProducto.canjeado = false;
                        ventaProducto.montoParcial = producto.precioLista;
                        ventaProducto.Venta = venta;
                        ventaProducto.Producto = producto;
                        ventaProducto.cantidad = 1;
                        ventaProducto.PromocionActual = MV_PromocionService.ultimaPromocionPorProducto(producto);
                        ventaProducto.PropertyChanged += actualizarMontosVenta;
                        
                        venta.VentaProducto.Add(ventaProducto);

                        
                    }
                    NotifyPropertyChanged("venta");
                }
            }
        }

        void actualizarMontosVenta(object sender, object e)
        {
            //elimino si algun producto tiene cantidad = 0
            foreach(var vp in venta.VentaProducto){
                if(vp.cantidad==0){
                    venta.VentaProducto.Remove(vp);
                }
            }
            
            //Actualizo el total
            venta.total = Decimal.Round(venta.VentaProducto.Sum(vp => vp.canjeado.Value ? 0 : vp.montoParcial).Value,2);

            //Actualizo los puntos canjeados
            venta.puntosCanjeados = (venta.VentaProducto.Sum(vp => vp.canjeado.Value ? vp.cantidad*vp.Producto.precioPuntos : 0));        

            //Actualizo los puntos ganados
            venta.puntosGanados = (venta.VentaProducto.Sum(vp => (vp.Producto!= null && vp.canjeado.Value == false) ? (vp.cantidad * vp.Producto.ganarPuntos) : 0));        
        }

        #endregion
    }
}
