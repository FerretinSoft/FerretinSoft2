using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pe.edu.pucp.ferretin.model
{
    public partial class Movimiento
    {
        public static String generateCode()
        {
            DateTime today = DateTime.Now;
            return today.ToString("yyyyMMddHHmmssFF"); // 16 characters code
        }

        public String convertDate{get;set;}
        public MovimientoProducto mp { get; set; }

    }
}
