using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pe.edu.pucp.ferretin.model
{

    public partial class Proveedor : INotifyPropertyChanged, IDataErrorInfo
    {
        #region atributos
        public int tipoProv
        {
            get
            {
                if (tipo == "EMPRESA")
                {
                    return 1;
                }
                else if (tipo == "PERSONA NATURAL")
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
                    tipo = "EMPRESA";
                }
                else if (value == 2)
                {
                    tipo = "PERSONA NATURAL";
                }
                else
                    tipo = "";
                this.SendPropertyChanged("tipo");
            }
        }

        public string stado 
        {
            get
            {
                if (estado == 1)
                {
                    return "Activo";
                }
                else if (estado == 2)
                {
                    return "Inactivo";
                }
                else
                    return "NO jala";
            }
            set
            {
                if (value == "Activo")
                {
                    estado = 1;
                }
                else if (value == "Inactivo")
                {
                    estado = 2;
                }
                else
                    estado = 0;
                this.SendPropertyChanged("estado");
            }
        }
        public int estado1
        {
            get
            {
                
                return (int)estado -1;
            }
            set
            {
               // estado = ()value + 1;   
            }

        }

        public string nombreNombre
        {
            get
            {
                return tipoProv == 2 ? "Nombre Completo" : "Razón Social";
            }
        }
        public string nombreDoc
        {
            get
            {
                return tipoProv == 2 ? "DNI" : "RUC";
            }
        }
        #endregion

        #region zona de validaciones

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
                    
                    case "ruc":
                        if (String.IsNullOrEmpty(this.ruc))
                        {
                            errorMessage = "Debe ingresar un número de documento.";
                        }
                        else
                        {
                            Int64 nro = 0;
                            Int64.TryParse(ruc, out nro);
                            if (tipo == "EMPRESA" || tipo == "PERSONA NATURAL")
                            {
                                if (tipo == "PERSONA NATURAL" && (nro < 10000000 || nro > 99999999))
                                {
                                    errorMessage = "El DNI debe tener 8 números";
                                }
                                if (tipo == "EMPRESA" && (nro < 10000000000 || nro > 99999999999))
                                {
                                    errorMessage = "El RUC debe tener 11 números";
                                }
                            }
                            else
                            {
                                errorMessage = "Debe seleccionar un tipo de Proveedor";
                            }
                        }
                        break;
                    case "razonSoc":
                        if (String.IsNullOrEmpty(razonSoc))
                        {
                            if (tipo == "PERSONA NATURAL")
                                errorMessage = "Debe ingresar el Nombre del Proveedor";
                            else if (tipo == "EMPRESA")
                                errorMessage = "Debe Ingresar la Razón Social de la Empresa";
                            else
                                errorMessage = "Debe seleccionar un tipo de documento";
                        }
                        break;
                  
                    case "UbigeoDistrito":
                        if (UbigeoDistrito == null || String.IsNullOrEmpty(UbigeoDistrito.id))
                        {
                            errorMessage = "Debe seleccionar un Pais, Provincia, Ciudad y Distrito";
                        }
                        break;

                    case "direccion":
                        if (String.IsNullOrEmpty(direccion))
                        {
                        
                                errorMessage = "Debe ingresar la Dirección del Proveedor";
                           
                        }
                        break;
                    case "id_rubro":
                        
                        if (id_rubro <1 || id_rubro==null || id_rubro >6)
                        {

                            errorMessage = "Debe seleccionar un Rubro";

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
