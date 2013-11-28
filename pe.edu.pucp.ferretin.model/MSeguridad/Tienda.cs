using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace pe.edu.pucp.ferretin.model
{
    public partial class Tienda : IDataErrorInfo
    {       

        #region Zona de Validaciones

        #region Código por default de la interfaz heredada
        public string Error
        {
            get { throw new NotImplementedException(); }
        }

        protected Dictionary<string, string> _errors = new Dictionary<string, string>();
        public IDictionary<string, string> Errors
        {
            get { return _errors; }
        }
        #endregion

        public string this[string columnName]
        {
            get
            {
                string errorMessage = string.Empty;
                this.Errors.Remove(columnName);

                switch (columnName)
                {
                    case "codigoTienda":
                        if (String.IsNullOrEmpty(this.codigo))
                        {
                            errorMessage = "El código de la tienda no debe estar vacio.";
                        }

                        break;
                    case "nombreTienda":
                        if (String.IsNullOrEmpty(this.nombre))
                        {
                            errorMessage = "Debe ingresar un nombre de Tienda.";
                        }
                            /*
                        else
                        {
                            Int64 nro = 0;
                            Int64.TryParse(nroDoc, out nro);
                            if (tipoDocumento == "DNI" || tipoDocumento == "RUC")
                            {
                                if (tipoDocumento == "DNI" && (nro < 10000000 || nro > 99999999))
                                {
                                    errorMessage = "El DNI debe tener 8 números";
                                }
                                if (tipoDocumento == "RUC" && (nro < 10000000000 || nro > 99999999999))
                                {
                                    errorMessage = "El RUC debe tener 11 números";
                                }
                            }
                            else
                            {
                                errorMessage = "Debe seleccionar un tipo de documento";
                            }
                        }
                             */
                        break;
                    case "direccionTienda":
                        if (String.IsNullOrEmpty(this.direccion))
                        {
                            errorMessage = "Debe ingresar una dirección de Tienda.";
                        }
                        /*
                        if (String.IsNullOrEmpty(nombre))
                        {
                            if (tipoDocumento == "DNI")
                                errorMessage = "Debe ingresar un Nombre para el Cliente";
                            else if (tipoDocumento == "RUC")
                                errorMessage = "Debe Ingresar la Razón Social de la Empresa";
                            else
                                errorMessage = "Debe seleccionar un tipo de documento";
                        }
                         */
                        break;
                    case "telefono1":
                        if (String.IsNullOrEmpty(this.telefono1))
                        {
                            errorMessage = "El Teléfono 1 no debe estar vacio.";
                        }

                        break;

                        /*
                    case "apPaterno":
                        if (tipoDocumento == "DNI" && String.IsNullOrEmpty(apPaterno))
                        {
                            errorMessage = "Debe ingresar un Apellido Paterno para el Cliente";
                        }
                        break;
                    case "apMaterno":
                        if (tipoDocumento == "DNI" && String.IsNullOrEmpty(apMaterno))
                        {
                            errorMessage = "Debe ingresar un Apellido Materno para el Cliente";
                        }
                        break;
                    case "UbigeoDistrito":
                        if (UbigeoDistrito == null || String.IsNullOrEmpty(UbigeoDistrito.id))
                        {
                            errorMessage = "Debe seleccionar un Pais, Provincia, Ciudad y Distrito";
                        }
                        break;
                         */
                }

                if (!string.IsNullOrEmpty(errorMessage))
                {
                    this.Errors.Add(columnName, errorMessage);
                }

                return errorMessage;
            }
        }

        #endregion

        public String tipoAux
        {
            get
            {
                if (estado == 1) return "Venta";
                else
                    return "Venta/Compra";
            }
        }

        public String estadoAux
        {
            get
            {
                if (estado == 0) return "Inactivo";
                else
                    return "Activo";
            }
        }
    }
}
