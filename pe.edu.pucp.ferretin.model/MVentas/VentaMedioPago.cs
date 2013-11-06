using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace pe.edu.pucp.ferretin.model
{
    public partial class VentaMedioPago
    {

       

        partial void OnmontoChanged()
        {
            //if (monto == 0)
           // {
            //    Venta.VentaMedioPago.Remove(this);
            //}
            
        }

        private bool _montoReadOnly = false;
        public bool montoReadOnly
        {
            get
            {
                return _montoReadOnly;
            }
            set
            {
                _montoReadOnly = value;
            }
        }

        private bool _monedaReadOnly = false;
        public bool monedaReadOnly
        {
            get
            {
                return _monedaReadOnly;
            }
            set
            {
                _monedaReadOnly = value;
            }
        }

        public bool monedaIsEnable
        {
            get
            {
                return !monedaReadOnly;
            }
        }
    }
}
