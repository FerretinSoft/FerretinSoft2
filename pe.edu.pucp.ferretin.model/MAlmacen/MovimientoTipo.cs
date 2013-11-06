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
        public enum CategoriaMovimiento { ENTRADA, SALIDA, TRANSFERENCIA, NINGUNA};

        public CategoriaMovimiento categoriaEnum
        {
            get 
            {
                switch (categoria)
                {
                    case 'E': 
                        return CategoriaMovimiento.ENTRADA;
                    case 'S':
                        return CategoriaMovimiento.SALIDA;
                    case 'T':
                        return CategoriaMovimiento.TRANSFERENCIA;
                    default:
                        return CategoriaMovimiento.NINGUNA;
                }
            }
            set 
            {
                switch (value)
                {
                    case CategoriaMovimiento.ENTRADA:
                        categoria = 'E';
                        break;
                    case CategoriaMovimiento.SALIDA:
                        categoria = 'S';
                        break;
                    case CategoriaMovimiento.TRANSFERENCIA:
                        categoria = 'T';
                        break;
                    default:
                        categoria = ' ';
                        break;
                }
            }
        }

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
