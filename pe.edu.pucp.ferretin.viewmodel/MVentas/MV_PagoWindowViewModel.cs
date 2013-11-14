using pe.edu.pucp.ferretin.controller;
using pe.edu.pucp.ferretin.controller.MAlmacen;
using pe.edu.pucp.ferretin.controller.MSeguridad;
using pe.edu.pucp.ferretin.controller.MVentas;
using pe.edu.pucp.ferretin.model;
using pe.edu.pucp.ferretin.viewmodel.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace pe.edu.pucp.ferretin.viewmodel.MVentas
{
    public class MV_PagoWindowViewModel : ViewModelBase
    {

        public IEnumerable<MedioPago> mediosPago
        {
            get
            {
                return MV_ComunService.db.MedioPago;
            }
        }

        public List<String> tiposMoneda
        {
            get
            {
                return new List<string>() {"Soles", "Dolares" };
            }
        }

        private Venta _venta;
        public Venta venta {
            get
            {
                return _venta;
            }
            set
            {
                _venta = value;
                venta.VentaMedioPago.ListChanged += VentaMedioPago_ListChanged;
                NotifyPropertyChanged("venta");
            }
        }

        #region RalayCommand
        RelayCommand _imprimirDocumentoCommand;
        public ICommand imprimirDocumentoCommand
        {
            get
            {
                if (_imprimirDocumentoCommand == null)
                {
                    _imprimirDocumentoCommand = new RelayCommand(imprimirDocumento, canImprimirDocumento);
                }
                return _imprimirDocumentoCommand;
            }
        }
        RelayCommand _agregarPagoCommand;
        public ICommand agregarPagoCommand
        {
            get
            {
                if (_agregarPagoCommand == null)
                {
                    _agregarPagoCommand = new RelayCommand(agregarPago,canAgregarPago);
                }
                return _agregarPagoCommand;
            }
        }
        #endregion
        public Vale _valeSeleccionado = null;
        public Vale valeSeleccionado { get; set; }
        public NotaCredito _notaCreditoSeleccionado = null;
        public NotaCredito notaCreditoSeleccionado { get; set; }

        #region Comandos
        public void agregarPago(object param)
        {
            if (venta.diferencia <= 0)
            {
                return;
            }
            int tipoPago = int.Parse(param.ToString());
            var ventaMP = new VentaMedioPago()
            {
                Venta = venta,
                moneda = 0,
                tipoCambio = (decimal)MS_SharedService.obtenerTipodeCambio()
            };
            ventaMP.PropertyChanged += ventaMP_PropertyChanged;

            switch (tipoPago)
            {
                case 1://Pago Efectivo
                    {
                        ventaMP.MedioPago = mediosPago.ElementAt(0);
                        ventaMP.monto = venta.diferencia;
                        break;
                    };
                case 2: //Pago Tarjeta
                    {
                        ventaMP.MedioPago = mediosPago.ElementAt(1);
                        ventaMP.detalle = "# Transacción " + new Random(100).Next(11111111, 99999999).ToString() + "";
                        ventaMP.monto = venta.diferencia;
                        break;
                    };
                case 3: //Pago Vale
                    {
                        if (valeSeleccionado != null)
                        {
                            ventaMP.Vale = valeSeleccionado;
                            ventaMP.MedioPago = mediosPago.ElementAt(2);
                            ventaMP.detalle = "Vale N° " + valeSeleccionado.codigo.ToString();
                            ventaMP.moneda = valeSeleccionado.LoteVale.moneda-1;
                            ventaMP.monto = valeSeleccionado.LoteVale.monto;
                            valeSeleccionado = null;
                        }
                        break;
                    };
                case 4: //Pago Nota de Crédito
                    {
                        if (notaCreditoSeleccionado != null)
                        {
                            ventaMP.NotaCredito = notaCreditoSeleccionado;
                            ventaMP.MedioPago = mediosPago.ElementAt(3);
                            ventaMP.detalle = "Nota de Crédito N° " + notaCreditoSeleccionado.codigo.ToString();
                            ventaMP.moneda = 0;//TODO siempre en soles?
                            ventaMP.monto = notaCreditoSeleccionado.importe;
                            notaCreditoSeleccionado = null;
                        }
                        break;
                    };
            }
            if(ventaMP!=null)
                venta.VentaMedioPago.Add(ventaMP);
            
            NotifyPropertyChanged("venta");
        }

        void VentaMedioPago_ListChanged(object sender, System.ComponentModel.ListChangedEventArgs e)
        {
            actualizarCobrado();
        }

        public void imprimirDocumento(object param)
        {
            string result = String.Empty;
            try
            {
                try
                {
                    venta.nroDocumento = MV_VentaService.generarNroDoc((venta.Cliente==null?false:(venta.Cliente.tipo==2)));
                    venta.tipoDocumento = ((venta.Cliente == null ? 0 : (venta.Cliente.tipo == 2?1:0)));
                    if (venta.Cliente != null)
                    {
                        venta.Cliente.puntosActual -= venta.Cliente.puntosActual == null ? 0 : venta.Cliente.puntosActual - venta.puntosCanjeados;
                        venta.Cliente.puntosUsados += venta.Cliente.puntosUsados == null ? venta.puntosCanjeados : venta.Cliente.puntosUsados + venta.puntosCanjeados;
                        venta.Cliente.puntosActual += venta.Cliente.puntosActual == null ? venta.puntosGanados : venta.Cliente.puntosActual + venta.puntosGanados;
                        venta.Cliente.puntosGanados += venta.Cliente.puntosGanados == null ? venta.puntosGanados : venta.Cliente.puntosGanados + venta.puntosGanados;
                        venta.Cliente.ultimaCompra = venta.fecha;
                        venta.Cliente.totalCompras = venta.Cliente.totalCompras == null ? 1 : venta.Cliente.totalCompras + 1;
                    }
                    foreach (var vmp in venta.VentaMedioPago)
                    {
                        if (vmp.Vale != null)
                        {
                            vmp.Vale.estado = 1;
                        }
                        if (vmp.NotaCredito != null)
                        {
                            vmp.NotaCredito.estado = 1;
                        }
                    }
                    foreach (var vp in venta.VentaProducto)
                    {
                        if (vp.PromocionActual != null)
                        {
                            vp.PromocionActual.stockActual--;
                        }
                    }
                }
                catch { }
                ComunService.idVentana(42);
                result = MA_SharedService.registrarVenta(venta.Usuario.Empleado.tiendaActual, venta.VentaProducto);
                if (result.Length <= 0)//si resulto bien
                {
                    
                }
            }
            catch (Exception e)
            {
                result = "Error al registrar venta: "+ e.Message;
            }
            if (result.Trim().Length>0)
            {
                MessageBox.Show(result);
            }
        }

        private bool canAgregarPago(object param)
        {
            return venta!=null && venta.diferencia > 0;
        }

        private bool canImprimirDocumento(object param)
        {
            return venta != null && venta.diferencia <= 0;
        }

        void ventaMP_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            actualizarCobrado();
        }

        void actualizarCobrado()
        {
            
            venta.cobrado = Decimal.Round(venta.VentaMedioPago.Sum(vmp => vmp.moneda == 0 ? vmp.monto : vmp.tipoCambio * vmp.monto).Value, 2);
        }
        #endregion

        
    }
}
