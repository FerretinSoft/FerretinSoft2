using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace pe.edu.pucp.ferretin.model
{
    /// <summary>
    /// Clase extendida de la entidad Cliente, con la cual también se podrán hacer validaciones
    /// </summary>
    public partial class Cliente : IDataErrorInfo
    {

        #region Zona de atributos

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

        public string tipoDocString
        {
            get
            {
                return (tipoDocumento == "RUC" ? "FACTURA " : "BOLETA ") + tipoDocumento + " " + nroDoc.ToUpper(); 
            }
        }

        public string direccionDistrito
        {
            get
            {
                return direccion.ToUpper() + " " + UbigeoDistrito.nombre.ToUpper();
            }
        }
        public string nombreMayusCompleto
        {
            get{
                return nombreCompleto.ToUpper();
            }
        }
        public String nombreCompleto
        {
            get
            {
                return String.Join(" ", nombre, apPaterno, apMaterno);
            }
        }

        public bool isBoleta
        {
            get
            {
                return this.tipo == 1;
            }
        }

        public bool isFactura
        {
            get
            {
                return this.tipo == 2;
            }
        }

        public string nombreDoc
        {
            get
            {
                return tipo==1?"DNI":"RUC";
            }
        }

        public string nombreNombre
        {
            get
            {
                return tipo == 1 ? "Nombre Completo" : "Razón Social";
            }
        }

        
        #endregion
        
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
                    case "telefono1":
                        if (String.IsNullOrEmpty(this.telefono1))
                        {
                            errorMessage = "El Teléfono 1 no debe estar vacio.";
                        }

                        break;
                    case "nroDoc":
                        if (String.IsNullOrEmpty(this.nroDoc))
                        {
                            errorMessage = "Debe ingresar un número de documento.";
                        }
                        else
                        {
                            Int64 nro=0;
                            Int64.TryParse(nroDoc, out nro);
                            if (tipoDocumento == "DNI" || tipoDocumento == "RUC")
                            {
                                if (tipoDocumento=="DNI" && ( nro < 10000000 || nro > 99999999) )
                                {
                                    errorMessage = "El DNI debe tener 8 números";
                                }
                                if (tipoDocumento == "RUC" && ( nro < 10000000000 || nro > 99999999999) )
                                {
                                    errorMessage = "El RUC debe tener 11 números";
                                }
                            }
                            else
                            {
                                errorMessage = "Debe seleccionar un tipo de documento";
                            }
                        }
                        break;
                    case "nombre":
                        if (String.IsNullOrEmpty(nombre))
                        {
                            if(tipoDocumento=="DNI")
                                errorMessage = "Debe ingresar un Nombre para el Cliente";
                            else if (tipoDocumento == "RUC")
                                errorMessage = "Debe Ingresar la Razón Social de la Empresa";
                            else
                                errorMessage = "Debe seleccionar un tipo de documento";
                        }
                        break;
                    case "apPaterno":
                        if (tipoDocumento == "DNI" && String.IsNullOrEmpty(apPaterno))
                        {
                            errorMessage = "Debe ingresar un Apellido Paterno para el Cliente";
                        }
                        break;
                    case "apMaterno":
                        if (tipoDocumento=="DNI" && String.IsNullOrEmpty(apMaterno))
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
                }

                if (!string.IsNullOrEmpty(errorMessage))
                {
                    this.Errors.Add(columnName, errorMessage);
                }

                return errorMessage;
            }
        }

        #endregion

    }
}
