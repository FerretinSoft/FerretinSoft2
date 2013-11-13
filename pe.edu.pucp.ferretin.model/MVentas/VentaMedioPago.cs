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

        public string montoString
        {
            get
            {
                return (this.moneda == 0 ? " S/. " : "US $ ") + Decimal.Round(this.monto.Value, 2,MidpointRounding.ToEven).ToString();
            }
        }

        public decimal? tipoCambio { get; set; }

        partial void OnmonedaChanged()
        {
            if ( tipoCambio!=null && monto!=null && tipoCambio>0  && monto>0)
            {
                var newMonto = monto;
                if (moneda == 0)//Soles
                {
                    newMonto *= tipoCambio;
                }
                else if (moneda == 1)//Dolares
                {
                    newMonto /= tipoCambio;
                }
                monto = Decimal.Round(newMonto.Value, 2);
            }
        }

        partial void OnmontoChanged()
        {
            if (monto < 0)
            {
                monto = 0;
            }
            
        }

        public bool montoReadOnly
        {
            get
            {
                if (Vale != null) return true;
                if (NotaCredito != null) return true;
                return false;
            }
        }

        
        public bool monedaReadOnly
        {
            get
            {
                return montoReadOnly;
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
