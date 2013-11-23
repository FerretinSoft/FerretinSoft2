using pe.edu.pucp.ferretin.controller;
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
    /// Lógica de interacción para MS_CambiarContraseñaUsuario.xaml
    /// </summary>
    public partial class MS_CambiarContraseñaUsuario : Window
    {
        #region Variables
        
        Usuario usuarioLog;                     //Usuario que inicio sesión.
        int primeraVez;                         //Indica si es la primera vez que el usuario ha iniciado sesión.
        int renovar;
        Regex r = new Regex("^[a-zA-Z0-9]*$");

        #endregion

        #region Constructor
        public MS_CambiarContraseñaUsuario(Usuario usuario, int renovacion)
        {
            usuarioLog = usuario;
            renovar = renovacion;
            //Verifica si es la primera vez que esta iniciando sesión.
            if (usuarioLog.contrasena == MS_UsuarioService.encrypt("ferretinSoft"))
            {
                primeraVez = 1;
            }
            else
            {
                primeraVez = 0;
            }

            InitializeComponent();
        }
        #endregion

        #region Boton Aceptar
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            cambiarContraseña();
        }
        #endregion

        #region Boton Cancelar
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
             MessageBoxResult result =MessageBox.Show("Al salir, perderá todos los datos ingresados. ¿Desea continuar?",
            "ATENCIÓN", MessageBoxButton.OKCancel, MessageBoxImage.Warning, MessageBoxResult.Cancel);
             if (result == MessageBoxResult.OK)
             {
                 this.Close();
             }
        }
        #endregion

        #region Cambiar Contraseña
        public void cambiarContraseña()
        {

            if (nuevaCon.Password == "ferretinSoft")
            {
                MessageBox.Show("Por motivos de seguridad no puede usar está contraseña.");
            }
            else
            {
                if (usuarioLog.contrasena == MS_UsuarioService.encrypt(conActual.Password) && nuevaCon.Password == confirmarCon.Password && !String.IsNullOrEmpty(nuevaCon.Password) && nuevaCon.Password.Length >= 6 && r.IsMatch(nuevaCon.Password))
                {
                    usuarioLog.contrasena = MS_UsuarioService.encrypt(nuevaCon.Password);
                    usuarioLog.ultimoCambioContrasena = DateTime.Now;
                    MS_UsuarioService.actualizarUsuario(usuarioLog);

                    if (primeraVez == 1 || renovar == 1)
                    {
                        if (renovar == 0)
                        {
                            MessageBox.Show("Contraseña Cambiada Correctamente. Bienvenido a ferretinSoft.");
                        }
                        else
                        {
                            MessageBox.Show("Contraseña Cambiada Correctamente.");
                        }
                        MainWindow mw = new MainWindow(usuarioLog);
                        mw.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Contraseña Cambiada Correctamente");
                    }

                    this.Close();
                }
                else
                {

                    if (usuarioLog.contrasena != MS_UsuarioService.encrypt(conActual.Password))
                    {
                        MessageBox.Show("La contraseña actual no es correcta.");

                    }
                    else if (!(nuevaCon.Password.Length >= 6))
                    {

                        MessageBox.Show("La contraseña debe tener como mínimo 6 caracteres");

                    }
                    else if (nuevaCon.Password != confirmarCon.Password)
                    {
                        MessageBox.Show("La contraseña nueva y la confirmación de contraseña no coinciden");

                    }
                    else if (!r.IsMatch(nuevaCon.Password))
                    {
                        MessageBox.Show("La contraseña tiene que ser alfanumerica.");
                    }
                    else if (!String.IsNullOrEmpty(nuevaCon.Password) || !String.IsNullOrEmpty(conActual.Password) || !String.IsNullOrEmpty(confirmarCon.Password))
                    {
                        MessageBox.Show("Verifique que se hayan llenado todos los campos.");
                    }


                }
            }
        }
        #endregion

        #region Validaciones
        private void conActual_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (Regex.IsMatch(e.Text, "[a-zA-Z0-9]"))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void nuevaCon_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (Regex.IsMatch(e.Text, "[a-zA-Z0-9]"))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void confirmarCon_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (Regex.IsMatch(e.Text, "[a-zA-Z0-9]"))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }
        #endregion

        #region Restringir Barra Espaciadora para cada textbox

        private void conActual_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Space)
                e.Handled = false;
            else
                e.Handled = true;
        }


        private void nuevaCon_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Space)
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void confirmarCon_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Space)
                e.Handled = false;
            else
                e.Handled = true;
        }
        #endregion        
    }
}
