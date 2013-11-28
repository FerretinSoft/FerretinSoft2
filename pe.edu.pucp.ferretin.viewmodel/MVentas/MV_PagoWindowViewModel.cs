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
        private IEnumerable<MedioPago> _mediosPago=null;
        public IEnumerable<MedioPago> mediosPago
        {
            get
            {
                if(_mediosPago==null)_mediosPago=MV_VentaService.dbVenta.MedioPago;
                return _mediosPago;
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
            VentaMedioPago ventaMP = null;
            
            ventaMP = new VentaMedioPago()
            {
                moneda = 0,
                tipoCambio = (decimal)MS_SharedService.obtenerTipodeCambio()
            };
            ventaMP.PropertyChanged += ventaMP_PropertyChanged;

            switch (tipoPago)
            {
                case 1://Pago Efectivo
                    {
                        ventaMP.Venta = venta;
                        ventaMP.MedioPago = mediosPago.ElementAt(0);
                        ventaMP.monto = venta.diferencia;
                        break;
                    };
                case 2: //Pago Tarjeta
                    {
                        ventaMP.Venta = venta;
                        ventaMP.MedioPago = mediosPago.ElementAt(1);
                        ventaMP.detalle = "# Transacción " + new Random(100).Next(11111111, 99999999).ToString() + "";
                        ventaMP.monto = venta.diferencia;
                        break;
                    };
                case 3: //Pago Vale
                    {
                        if (valeSeleccionado != null)
                        {
                            ventaMP.Venta = venta;
                            ventaMP.Vale = valeSeleccionado;
                            ventaMP.MedioPago = mediosPago.ElementAt(2);
                            ventaMP.detalle = "Vale N° " + valeSeleccionado.codigo.ToString();
                            ventaMP.moneda = valeSeleccionado.LoteVale.moneda;
                            ventaMP.monto = valeSeleccionado.LoteVale.monto;
                            valeSeleccionado = null;
                        }
                        else
                        {
                            ventaMP = null;
                        }
                        break;
                    };
                case 4: //Pago Nota de Crédito
                    {
                        if (notaCreditoSeleccionado != null)
                        {
                            ventaMP.Venta = venta;
                            ventaMP.NotaCredito = notaCreditoSeleccionado;
                            ventaMP.MedioPago = mediosPago.ElementAt(3);
                            ventaMP.detalle = "Nota de Crédito N° " + notaCreditoSeleccionado.codigo.ToString();
                            ventaMP.moneda = 0;//TODO siempre en soles?
                            ventaMP.monto = notaCreditoSeleccionado.importe;
                            notaCreditoSeleccionado = null;
                        }
                        else
                        {
                            ventaMP = null;
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
                
                ComunService.idVentana(42);

                var productosMover = venta.VentaProducto.Where(vp=>!vp.isService);
                if (productosMover.Count()>0)
                {
                    result = MA_SharedService.registrarVenta(venta.Usuario.Empleado.tiendaActual, productosMover);
                }
                if (result.Length <= 0)//si resulto bien
                {
                    venta.fecha = DateTime.Now;
                    try
                    {
                        if (venta.Proforma != null)
                        {
                            venta.Proforma.finalizado = true;
                        }
                        venta.nroDocumento = MV_VentaService.generarNroDoc((venta.Cliente == null ? false : (venta.Cliente.tipo == 2)));
                        venta.tipoDocumento = ((venta.Cliente == null ? 0 : (venta.Cliente.tipo == 2 ? 1 : 0)));
                        if (venta.Cliente != null)
                        {
                            int puntosCanjeados = venta.puntosCanjeados.Value;
                            int puntosGanados = venta.puntosGanados.Value;

                            venta.Cliente.puntosActual = venta.Cliente.puntosActual ?? 0;
                            venta.Cliente.puntosUsados = venta.Cliente.puntosUsados ?? 0;
                            venta.Cliente.puntosGanados = venta.Cliente.puntosGanados ?? 0;
                            //TODO
                            venta.Cliente.puntosActual += puntosGanados - puntosCanjeados;
                            venta.Cliente.puntosUsados += puntosCanjeados;
                            venta.Cliente.puntosGanados += puntosGanados;
                            
                            venta.Cliente.ultimaCompra = venta.fecha;
                            venta.Cliente.totalCompras = (venta.Cliente.totalCompras == null) ? 1 : (venta.Cliente.totalCompras + 1);
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
                            if (vp.PromocionActual != null && vp.prodConDesc>0)
                            {
                                vp.PromocionActual.stockActual -= (int)(vp.prodConDesc/vp.PromocionActual.cantMulUnidades);
                            }
                            if (vp.isService && vp.servicioSeleccionado != null)
                            {
                                vp.servicioSeleccionado.estado = 2;//Facturado
                            }
                        }
                        MV_VentaService.enviarCambios();
                    }
                    catch (Exception ex) {
                        MessageBox.Show("Error en ventas:" + ex.Message);
                    }
                    finally
                    {
                        
                    }
                }
            }
            catch (Exception e)
            {
                result = "Error al registrar en almacen: "+ e.Message;
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
