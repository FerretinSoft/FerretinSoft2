﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pe.edu.pucp.ferretin.model
{
    public partial class SolicitudAbastecimiento
    {
        public static String generateCode()
        {
            DateTime today = DateTime.Now;
            return today.ToString("yyyyMMddHHmmssFF"); // 16 characters code
        }
    }
}
