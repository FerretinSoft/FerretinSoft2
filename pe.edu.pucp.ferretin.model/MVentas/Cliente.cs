using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace pe.edu.pucp.ferretin.model
{
    public partial class Cliente
    {

        public int tipo
        {
            get
            {
                if (tipoDocumento == "DNI")
                {
                    return 1;
                }
                else if (tipoDocumento == "RUC")
                {
                    return 2;
                }
                else
                    return 0;
            }
            set
            {
                if (value == 1)
                {
                    tipoDocumento = "DNI";
                }
                else if (value == 2)
                {
                    tipoDocumento = "RUC";
                }
                else
                    tipoDocumento = "";
                this.SendPropertyChanged("tipo");
            }
        }

        public String nombreCompleto
        {
            get
            {
                return String.Join(" ", nombre, apPaterno, apMaterno);
            }
        }

        partial void Ontelefono1Changing(String value)
        {
            Regex telefonoNumero = new Regex(@"^[2-9]\d{2}-\d{4}$");
            if (telefonoNumero.IsMatch(value) == false)
            {
                throw new Exception("Numero de teléfono no válido");
            }
        }

        partial void OnnroDocChanging(String value)
        {
            Int64 nroDoc = Int64.Parse(value);
            if (nroDoc <= 0)
            {
                throw new Exception("El número de documento debe contener un valor numérico");
            }
            if (tipoDocumento.Equals("DNI") && (nroDoc>99999999 || nroDoc<10000000))
            {
                throw new Exception("El DNI debe tener exactamente 8 números");
            }
            if (tipoDocumento.Equals("RUC") && (nroDoc>99999999999 || nroDoc<10000000000))
            {
                throw new Exception("El RUC debe tener exactamente 11 números");
            }
        }
    }
}
