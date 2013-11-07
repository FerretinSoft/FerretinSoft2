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
            igv = Decimal.Round((this.total * (decimal)igvActual / 100).Value, 2);
            subTotal = this.total - this.igv;
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
