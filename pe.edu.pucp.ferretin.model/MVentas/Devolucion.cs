using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pe.edu.pucp.ferretin.model
{
    public partial class Devolucion
    {
        public string fecEmisionString
        {
            get
            {
                if (fecEmision != null)
                {
                    string fechaString = fecEmision.ToString();
                    DateTime fecha = Convert.ToDateTime(fechaString);
                    fechaString = fecha.ToString("d");
                    return fechaString;
                }
                else
                    return null;
            }

        }

        public string totalString
        {
            get
            {
                if (total != null)
                {
                    return "S/. " + total;
                }
                else
                    return null;
            }

        }

        public string subTotalString
        {
            get
            {
                if (subTotal != null)
                {
                    return "S/. " + subTotal;
                }
                else
                    return null;
            }

        }

        public string igvString
        {
            get
            {
                if (igv != null)
                {
                    return "S/. " + igv;
                }
                else
                    return null;
            }

        }
    }
}
