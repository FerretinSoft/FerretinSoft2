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
        List<Parametro> listaParametros;

        Parametro intentosC;
        Parametro tiempoSesion;
        Parametro duracionClave;
        Parametro tipoDeCambio;
        Parametro IGV;
        Parametro vigenciaPro;
        Parametro vigenciaNota;
        Parametro solesPorPunto;

        int p1, p2, p3, p4, p5, p6, p7, p8;
        

        public MS_ParametrosWindow()
        {
            InitializeComponent();
            p1 = p2 = p3 = p4 = p5 = p6 = p6 = p7 = p8 = 0;
            listaParametros = MS_ParametroService.obtenerListaParametros().ToList();

            intContrasena.Text = listaParametros[0].valor.ToString();
            intentosC = listaParametros[0];

            tMaxSesion.Text = listaParametros[1].valor.ToString();
            tiempoSesion = listaParametros[1];

            durClave.Text = listaParametros[2].valor.ToString();
            duracionClave = listaParametros[2];

            tipCambio.Text = listaParametros[3].valor.ToString();
            tipoDeCambio = listaParametros[3];

            igv.Text = listaParametros[4].valor.ToString();
            IGV = listaParametros[4];

            vigProforma.Text = listaParametros[5].valor.ToString();
            vigenciaPro = listaParametros[5];

            vigNotaCredito.Text = listaParametros[6].valor.ToString();
            vigenciaNota = listaParametros[6];

            solesPunto.Text = listaParametros[7].valor.ToString();
            solesPorPunto = listaParametros[7];
        }

        private void intContrasena_TextChanged(object sender, TextChangedEventArgs e)
        {
            p1 = 1;
            try
            {
                if (Regex.IsMatch(intContrasena.Text, "[0-9]"))
                {
                    intentosC.valor = Convert.ToInt16(intContrasena.Text);
                }
                else if (!Regex.IsMatch(intContrasena.Text, "[0-9]") && !String.IsNullOrEmpty(intContrasena.Text))
                {
                    MessageBox.Show("Ingrese un número de intentos valido");
                    intContrasena.Text = "";
                }
            }
            catch { }
            
        }

        private void tMaxSesion_TextChanged(object sender, TextChangedEventArgs e)
        {
            p2 = 1;
            try
            {

                if (Regex.IsMatch(tMaxSesion.Text, "[0-9]"))
                {
                    tiempoSesion.valor = Convert.ToInt16(tMaxSesion.Text);
                }
                else if (!Regex.IsMatch(tMaxSesion.Text, "[0-9]") && !String.IsNullOrEmpty(tMaxSesion.Text))
                {
                    MessageBox.Show("Ingrese un tiempo de sesión valido");
                    tMaxSesion.Text = "";
                }
            }
            catch { }
        }

        private void durClave_TextChanged(object sender, TextChangedEventArgs e)
        {
            p3 = 1;
            try
            {

                if (Regex.IsMatch(durClave.Text, "[0-9]"))
                {
                    duracionClave.valor = Convert.ToInt16(durClave.Text);
                }
                else if (!Regex.IsMatch(durClave.Text, "[0-9]") && !String.IsNullOrEmpty(durClave.Text))
                {
                    MessageBox.Show("Ingrese un tiempo de duración valido");
                    durClave.Text = "";
                }

                
            }
            catch { }
        }

        private void tipCambio_TextChanged(object sender, TextChangedEventArgs e)
        {
            p4 = 1;

            try
            {

                if (Regex.IsMatch(durClave.Text, "[0-9]"))
                {
                    tipoDeCambio.valor = Convert.ToInt16(tipCambio.Text);
                }
                else if (!Regex.IsMatch(tipCambio.Text, "[0-9]") && !String.IsNullOrEmpty(tipCambio.Text))
                {
                    MessageBox.Show("Ingrese un número de intentos valido");
                    tipCambio.Text = "";
                }                
            }
            catch { }
        }

        private void igv_TextChanged(object sender, TextChangedEventArgs e)
        {
            p5 = 1;
            try
            {
                if (Regex.IsMatch(durClave.Text, "[0-9]"))
                {
                    IGV.valor = Convert.ToInt16(igv.Text);
                }
                else if (!Regex.IsMatch(igv.Text, "[0-9]") && !String.IsNullOrEmpty(igv.Text))
                {
                    MessageBox.Show("Ingrese un número de intentos valido");
                    igv.Text = "";
                }   
                               
            }
            catch { }
        }

        private void vigProforma_TextChanged(object sender, TextChangedEventArgs e)
        {
            p6 = 1;
            try
            {
                if (Regex.IsMatch(vigProforma.Text, "[0-9]"))
                {
                    vigenciaPro.valor = Convert.ToInt16(vigProforma.Text);
                }
                else if (!Regex.IsMatch(vigProforma.Text, "[0-9]") && !String.IsNullOrEmpty(vigProforma.Text))
                {
                    MessageBox.Show("Ingrese un número de intentos valido");
                    vigProforma.Text = "";
                }  

            }
            catch { }
        }

        private void vigNotaCredito_TextChanged(object sender, TextChangedEventArgs e)
        {
            p7 = 1;
            try
            {

                if (Regex.IsMatch(vigProforma.Text, "[0-9]"))
                {
                    vigenciaNota.valor = Convert.ToInt16(vigNotaCredito.Text);
                }
                else if (!Regex.IsMatch(vigNotaCredito.Text, "[0-9]") && !String.IsNullOrEmpty(vigNotaCredito.Text))
                {
                    MessageBox.Show("Ingrese un número de intentos valido");
                    vigNotaCredito.Text = "";
                }  
            }
            catch { }
        }

        private void solesPunto_TextChanged(object sender, TextChangedEventArgs e)
        {
            p8 = 1;
            try
            {
                if (Regex.IsMatch(vigProforma.Text, "[0-9]"))
                {
                    solesPorPunto.valor = Convert.ToInt16(solesPunto.Text);
                }
                else if (!Regex.IsMatch(solesPunto.Text, "[0-9]") && !String.IsNullOrEmpty(solesPunto.Text))
                {
                    MessageBox.Show("Ingrese un número de intentos valido");
                    solesPunto.Text = "";
                }
            }
            catch { }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (p1 == 1 && !String.IsNullOrEmpty(intContrasena.Text)) 
            {
                MS_ParametroService.actualizarParametro(intentosC);
            }

            if (p2 == 1 && !String.IsNullOrEmpty(tMaxSesion.Text))
            {
                MS_ParametroService.actualizarParametro(tiempoSesion);
            }

            if (p3 == 1 && !String.IsNullOrEmpty(durClave.Text))
            {
                MS_ParametroService.actualizarParametro(duracionClave);
            }

            if (p4 == 1 && !String.IsNullOrEmpty(tipCambio.Text))
            {
                MS_ParametroService.actualizarParametro(tipoDeCambio);
            }

            if (p5 == 1 && !String.IsNullOrEmpty(igv.Text))
            {
                MS_ParametroService.actualizarParametro(IGV);
            }

            if (p6 == 1 && !String.IsNullOrEmpty(vigProforma.Text))
            {
                MS_ParametroService.actualizarParametro(vigenciaPro);
            }

            if (p7 == 1 && !String.IsNullOrEmpty(vigNotaCredito.Text))
            {
                MS_ParametroService.actualizarParametro(vigenciaNota);
            }

            if (p8 == 1 && !String.IsNullOrEmpty(solesPunto.Text))
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

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public static bool esNumero(string cad)
        {
            Regex isnumber = new Regex("[^0-9]");
            return !isnumber.IsMatch(cad);
        }

        
    }
}
