using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pe.edu.pucp.ferretin.model
{
    public partial class Proforma
    {

        public double igvActual = 0;

        partial void OntotalChanged()
        {
            this.subTotal = Decimal.Round(this.total.Value / (1 + ((decimal)igvActual / 100)), 2);
            this.igv = Decimal.Round(this.total.Value - this.subTotal.Value, 2);
        }

        public string totalString
        {
            get
            {
                return "S/. " + total.ToString();
            }
        }
    }
}
