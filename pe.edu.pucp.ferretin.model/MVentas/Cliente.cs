using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace pe.edu.pucp.ferretin.model
{
    /// <summary>
    /// Clase extendida de la entidad Cliente, con la cual también se podrán hacer validaciones
    /// </summary>
    public partial class Cliente : INotifyPropertyChanged, IDataErrorInfo
    {

        #region Zona de atributos

        private ImageSource _imagenMostrar;
        public ImageSource imagenMostrar
        {
            get
            {
                if (imagen != null)
                {
                    MemoryStream strm = new MemoryStream();
                    strm.Write(imagen.ToArray(), 0, imagen.Length);
                    strm.Position = 0;
                    System.Drawing.Image img = System.Drawing.Image.FromStream(strm);

                    BitmapImage bitmapImage = new BitmapImage();
                    bitmapImage.BeginInit();
                    MemoryStream memoryStream = new MemoryStream();
                    img.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Bmp);
                    memoryStream.Seek(0, SeekOrigin.Begin);
                    bitmapImage.StreamSource = memoryStream;
                    bitmapImage.EndInit();
                    _imagenMostrar = bitmapImage;
                }
                return _imagenMostrar;
            }
        }

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
                this.SendPropertyChanged("isBoleta");
            }
        }

        public string tipoDocString
        {
            get
            {
                return (tipoDocumento == "RUC" ? "FACTURA " : "BOLETA ") + tipoDocumento + " " + nroDoc.ToString(); 
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
                return String.Join(" ", nombre, apPaterno, apMaterno).ToUpper();
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

        #region Zona de Eventos
        partial void OntipoDocumentoChanged()
        {
            if (tipo == 2)
            {
                apPaterno = "";
                apMaterno = "";
            }
            SendPropertyChanged("nroDoc");
            SendPropertyChanged("apPaterno");
            SendPropertyChanged("apMaterno");
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
                    case "nroDoc":
                        if (tipoDocumento == "DNI" || tipoDocumento == "RUC")
                        {
                            if (nroDoc == null)
                            {
                                errorMessage = "Debe ingresar un número de documento";
                            }
                            if (tipoDocumento=="DNI" && (nroDoc < 10000000 || nroDoc > 99999999) )
                            {
                                errorMessage = "El DNI debe tener 8 números";
                            }
                            if (tipoDocumento == "RUC" && (nroDoc == null || nroDoc < 10000000000 || nroDoc > 99999999999) )
                            {
                                errorMessage = "El RUC debe tener 11 números";
                            }
                        }
                        else
                        {
                            errorMessage = "Debe seleccionar un tipo de documento";
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
                            errorMessage = "Debe seleccionar una Provincia, Ciudad y un Distrito";
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

        private string _mensajeError;
        public string mensajeError
        {
            get
            {
                return _mensajeError;
            }
            set
            {
                if (!value.Equals(_mensajeError))
                {
                    _mensajeError = value;
                    SendPropertyChanged("mensajeError");
                }
            }
        }
    }
}
