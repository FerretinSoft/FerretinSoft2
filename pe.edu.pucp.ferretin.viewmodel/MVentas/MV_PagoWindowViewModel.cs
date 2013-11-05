using pe.edu.pucp.ferretin.controller.MVentas;
using pe.edu.pucp.ferretin.model;
using pe.edu.pucp.ferretin.viewmodel.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                NotifyPropertyChanged("venta");
            }
        }

        #region RalayCommand
        RelayCommand _agregarPagoCommand;
        public ICommand agregarPagoCommand
        {
            get
            {
                if (_agregarPagoCommand == null)
                {
                    _agregarPagoCommand = new RelayCommand(agregarPago);
                }
                return _agregarPagoCommand;
            }
        }
        #endregion

        #region Comandos
        public void agregarPago(object param)
        {
            var ventaMP = new VentaMedioPago(){
                Venta = venta,
                monto = 0,
                moneda = 0,
                MedioPago = mediosPago.First(),
            };
            venta.VentaMedioPago.Add(ventaMP);
            NotifyPropertyChanged("venta");
        }
        #endregion
    }
}
