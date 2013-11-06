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
            VentaMedioPago ventaMP = new VentaMedioPago()
            {
                Venta = venta,
                moneda = 0
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
                        ventaMP.montoReadOnly = true;
                        ventaMP.monedaReadOnly = true;
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
                            
                            ventaMP.montoReadOnly = true;
                            ventaMP.monedaReadOnly = true;

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
                            ventaMP.moneda = 1;//TODO siempre en soles?
                            ventaMP.monto = notaCreditoSeleccionado.importe;

                            ventaMP.montoReadOnly = true;
                            ventaMP.monedaReadOnly = true;

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
            venta.cobrado = venta.VentaMedioPago.Sum(vmp => vmp.moneda == 0 ? vmp.monto : (decimal)MS_SharedService.obtenerTipodeCambio() * vmp.monto);
        }
        #endregion

        
    }
}
