using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pe.edu.pucp.ferretin.model
{
    public partial class NotaCredito
    {
        public string estadoString
        {
            get
            {
                return estado == 1 ? "EXPIRADA" : "APLICABLE";
            }
        }
    }
}
