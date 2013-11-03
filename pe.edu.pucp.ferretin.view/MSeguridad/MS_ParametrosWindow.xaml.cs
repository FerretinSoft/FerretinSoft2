using pe.edu.pucp.ferretin.controller.MSeguridad;
using pe.edu.pucp.ferretin.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace pe.edu.pucp.ferretin.view.MSeguridad
{
    /// <summary>
    /// Lógica de interacción para MS_ParametrosWindow.xaml
    /// </summary>
    public partial class MS_ParametrosWindow : Window
    {
        #region Variables
        //Lista de Parametros
        
        List<Parametro> listaParametros;

        //Variables auxiliares para jalar los Parametros
        Parametro intentosC;
        Parametro tiempoSesion;
        Parametro duracionClave;
        Parametro tipoDeCambio;
        Parametro IGV;
        Parametro vigenciaPro;
        Parametro vigenciaNota;
        Parametro solesPorPunto;
        
        #endregion
        
        #region Constructor
        //Constructor ParametrosWindow
        public MS_ParametrosWindow()
        {
            InitializeComponent();
            
            //Obtiene todos los parametros.
            listaParametros = MS_ParametroService.obtenerListaParametros().ToList();

            //Se asigna a cada textbox su valor respectivo de la base de datos.
            intContrasena.Text = listaParametros[0].valor;
            intentosC = listaParametros[0];

            tMaxSesion.Text = listaParametros[1].valor;
            tiempoSesion = listaParametros[1];

            durClave.Text = listaParametros[2].valor;
            duracionClave = listaParametros[2];

            tipCambio.Text = listaParametros[3].valor;
            tipoDeCambio = listaParametros[3];

            igv.Text = listaParametros[4].valor;
            IGV = listaParametros[4];

            vigProforma.Text = listaParametros[5].valor;
            vigenciaPro = listaParametros[5];

            vigNotaCredito.Text = listaParametros[6].valor;
            vigenciaNota = listaParametros[6];

            solesPunto.Text = listaParametros[7].valor;
            solesPorPunto = listaParametros[7];
            
        }
        #endregion

        #region Eventos Textboxs
        //Evento de cambio de texto en Textbox
        private void intContrasena_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                  intentosC.valor = intContrasena.Text;
            }
            catch { }
            
            
        }

        private void tMaxSesion_TextChanged(object sender, TextChangedEventArgs e)
        {
   
            try
            {
                tiempoSesion.valor = tMaxSesion.Text;
            }
            catch { }

        }

        private void durClave_TextChanged(object sender, TextChangedEventArgs e)
        {
            
            try
            {
                duracionClave.valor = durClave.Text;
            }
            catch { }
            
        }

        private void tipCambio_TextChanged(object sender, TextChangedEventArgs e)
        {
           
            try
            {
                tipoDeCambio.valor = tipCambio.Text;
            }
            catch { }
        }

        private void igv_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                IGV.valor = igv.Text;
            }
            catch { }
        }

        private void vigProforma_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                vigenciaPro.valor = vigProforma.Text;
            }
            catch { }
        }

        private void vigNotaCredito_TextChanged(object sender, TextChangedEventArgs e)
        {
            
            try
            {
                vigenciaNota.valor = vigNotaCredito.Text;
            }
            catch { }
        }

        private void solesPunto_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                solesPorPunto.valor = solesPunto.Text;
            }
            catch { }
        }
        //Fin de Eventos de Textbox
        #endregion

        #region Boton Guardar
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("¿Está seguro que desea guardar estos cambios?", "Confirmación",
                                MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                if (!String.IsNullOrEmpty(intContrasena.Text))
                {
                    MS_ParametroService.actualizarParametro(intentosC);
                }

                if (!String.IsNullOrEmpty(tMaxSesion.Text))
                {
                    MS_ParametroService.actualizarParametro(tiempoSesion);
                }

                if (!String.IsNullOrEmpty(durClave.Text))
                {
                    MS_ParametroService.actualizarParametro(duracionClave);
                }

                if (!String.IsNullOrEmpty(tipCambio.Text))
                {
                    MS_ParametroService.actualizarParametro(tipoDeCambio);
                }

                if (!String.IsNullOrEmpty(igv.Text))
                {
                    MS_ParametroService.actualizarParametro(IGV);
                }

                if (!String.IsNullOrEmpty(vigProforma.Text))
                {
                    MS_ParametroService.actualizarParametro(vigenciaPro);
                }

                if (!String.IsNullOrEmpty(vigNotaCredito.Text))
                {
                    MS_ParametroService.actualizarParametro(vigenciaNota);
                }

                if (!String.IsNullOrEmpty(solesPunto.Text))
                {
                    MS_ParametroService.actualizarParametro(solesPorPunto);
                }

                if (String.IsNullOrEmpty(intContrasena.Text) && String.IsNullOrEmpty(tMaxSesion.Text) && String.IsNullOrEmpty(durClave.Text)
                    && String.IsNullOrEmpty(tipCambio.Text) && String.IsNullOrEmpty(igv.Text) && String.IsNullOrEmpty(vigProforma.Text)
                    && String.IsNullOrEmpty(vigNotaCredito.Text) && String.IsNullOrEmpty(solesPunto.Text))
                {
                    MessageBox.Show("No hay parametros validos.");

                }
                else
                {
                    MessageBox.Show("Los Parametros Validos han sido Actualizados Correctamente");
                }

            }
        }
        #endregion

        #region Boton Cancelar
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("¿Está seguro que desea cerrar esta ventana?", "Confirmación",
                                MessageBoxButton.YesNo) == MessageBoxResult.Yes) this.Close();
            
        }
        #endregion

        #region Validaciones
        private void intContrasena_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (Regex.IsMatch(e.Text, "[0-9]"))
            {
                e.Handled = false;
            }else{
                e.Handled = true;
            }
        }

        private void tMaxSesion_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (Regex.IsMatch(e.Text, "[0-9]"))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void durClave_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (Regex.IsMatch(e.Text, "[0-9]"))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void tipCambio_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if ((Regex.IsMatch(tipCambio.Text, @"^[0-9]+(\.{0}[0-9]+)?$")) && (e.Text != ",") || (Regex.IsMatch(e.Text, "[0-9]")))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void igv_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if ((Regex.IsMatch(igv.Text, @"^[0-9]+(\.{0}[0-9]+)?$")) && (e.Text != ",") || (Regex.IsMatch(e.Text, "[0-9]")))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void vigProforma_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (Regex.IsMatch(e.Text, "[0-9]"))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void vigNotaCredito_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (Regex.IsMatch(e.Text, "[0-9]"))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void solesPunto_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if ((Regex.IsMatch(solesPunto.Text, @"^[0-9]+(\.{0}[0-9]+)?$")) && (e.Text != ",") || (Regex.IsMatch(e.Text, "[0-9]")))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        #endregion

        #region Restringir la Tecla Espacio para cada Textbox
        private void intContrasena_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Space)
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void tMaxSesion_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Space)
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void durClave_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Space)
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void tipCambio_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Space)
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void igv_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Space)
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void vigProforma_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Space)
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void vigNotaCredito_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Space)
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void solesPunto_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Space)
                e.Handled = false;
            else
                e.Handled = true;
        }

        #endregion
    }
}
