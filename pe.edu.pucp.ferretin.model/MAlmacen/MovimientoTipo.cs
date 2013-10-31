using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pe.edu.pucp.ferretin.model
{
    public partial class MovimientoTipo : IDataErrorInfo
    {
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
                    case "nombre":
                        if (String.IsNullOrEmpty(this.nombre))
                        {
                            errorMessage = "El nombre del tipo de movimiento no debe estar vacío.";
                        }

                        break;
                    case "categoria":
                        if (this.categoria != 'S' && this.categoria != 'E')
                        {
                            errorMessage = "Los valores permitidos son 'E' para los movimientos de entrada y 'S' para los de salida.";
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
    }
}
