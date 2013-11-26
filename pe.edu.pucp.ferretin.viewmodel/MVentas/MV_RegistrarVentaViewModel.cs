﻿using pe.edu.pucp.ferretin.controller;
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
        ~MV_RegistrarVentaViewModel()
        {
        }
        public MV_RegistrarVentaViewModel()
        {
            venta = new Venta()
            {
                fecha = DateTime.Now,
                igvPorcentaje = (decimal)MS_SharedService.obtenerIGV(),
                tipoCambio = (decimal)MS_SharedService.obtenerTipodeCambio(),
                tipoMoneda = 0,//Soles
                estado = 0,
                Usuario = MS_SharedService.usuarioL,
                Tienda = MS_SharedService.usuarioL.Empleado.tiendaActual
            };

            venta.VentaProducto.ListChanged += actualizarMontosVenta;
            
        }

        private long? _nroDocSeleccionado;
        public long? nroDocSeleccionado
        {
            get
            {
                return _nroDocSeleccionado;
            }
            set
            {
                _nroDocSeleccionado = value;
                if ( (value <= 99999999 && value >= 10000000) || ( value >= 10000000000 && value <= 99999999999) )
                {
                    cargarCliente(null);
                }else if(value==null || value==0)
                {
                    if (venta != null && venta.Cliente != null)
                    {
                        venta.Cliente = null;
                        NotifyPropertyChanged("widthClienteBar");
                    }
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

        public void agregarProducto(Object param)
        {
            if (!String.IsNullOrEmpty(codProdAgregar) || param!=null)
            {
                ProductoAlmacen productoAlmacen = null;
                Producto producto = null;
                try{
                    if (param is ProductoAlmacen)
                    {
                        productoAlmacen = param as ProductoAlmacen;
                        producto = productoAlmacen.Producto;
                    }
                    else
                    {
                        producto = MA_SharedService.obtenerProductoxCodigo(codProdAgregar);
                        productoAlmacen = producto.ProductoAlmacen.First(pa => pa.id_almacen.Equals(venta.Tienda.id));
                    }
                }catch{}
                
                //Validar si lo encuentra y si tiene como estado activo, el producto y el producto de un almacen
                if (productoAlmacen != null && producto !=null && productoAlmacen.estado>0 && producto.estado>0)
                {

                    var stockDisponible = 0;

                    try
                    {
                        stockDisponible = (int)productoAlmacen.stock;
                    }
                    catch { }

                    if (stockDisponible <= 0)
                    {
                        MessageBox.Show("No se cuenta con Stock de este producto:\n" + producto.nombre.ToUpper(), "Mensaje", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        return;
                    }
                    if (!producto.precioLista.HasValue || producto.precioLista <= 0)
                    {
                        MessageBox.Show("El producto no tiene un precio asignado:\n" + producto.nombre.ToUpper(), "Mensaje", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        return;
                    }

                    if (venta.VentaProducto.Count(vp => vp.id_producto.Equals(producto.id)) == 1)
                    {
                        var prod = venta.VentaProducto.Single( vp => vp.id_producto.Equals(producto.id) );
                        if (prod.vieneDeProforma.Value)
                        {
                            MessageBox.Show("El producto seleccionado, viene de una proforma, no se puede modificar las cantidades:\n" + producto.nombre.ToUpper(), "Mensaje", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        }
                        else
                        {
                            if (prod.cantidad + 1 > prod.stockDisponible)
                            {
                                MessageBox.Show("No se tiene más stock de este producto:\n" + producto.nombre.ToUpper(), "Mensaje", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                            }
                            else
                            {
                                prod.cantidad++;
                            }
                        }
                    }
                    else
                    {
                        if (stockDisponible > 0)
                        {
                            VentaProducto ventaProducto = new VentaProducto();
                            ventaProducto.PromocionActual = MV_PromocionService.ultimaPromocionPorProducto(productoAlmacen);
                            ventaProducto.canjeado = false;
                            ventaProducto.tipoCambio = venta.tipoCambio.Value;
                            ventaProducto.puntosCanejado = 0;
                            ventaProducto.Producto = producto;
                            ventaProducto.puntosGanado = producto.ganarPuntos;
                            ventaProducto.precioUnitario = producto.precioLista;
                            ventaProducto.moneda = producto.moneda;
                            ventaProducto.precioPuntos = producto.precioPuntos;
                            ventaProducto.Venta = venta;
                            
                            ventaProducto.cantidad = 1;
                            ventaProducto.stockDisponible = stockDisponible;

                            ventaProducto.PropertyChanged += actualizarMontosVenta;

                            venta.VentaProducto.Add(ventaProducto);

                            actualizarMontosVenta(null, null);
                        }
                        else
                        {
                            MessageBox.Show("No hay stock de este producto:\n" + producto.nombre.ToUpper(), "Mensaje", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        }

                    }
                    NotifyPropertyChanged("venta");
                }
                else
                {
                    if(productoAlmacen == null || producto ==null)
                        MessageBox.Show("Este producto no existe.", "Mensaje", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    else
                        MessageBox.Show("Este producto no esta disponible para su venta.", "Mensaje", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
        }

        public void actualizarMontosVenta(object sender, object e)
        {
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
