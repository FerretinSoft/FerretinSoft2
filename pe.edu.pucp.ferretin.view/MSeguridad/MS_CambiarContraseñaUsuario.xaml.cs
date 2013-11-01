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
        Usuario usuarioLog;
        int primeraVez;
        Regex r = new Regex("^[a-zA-Z0-9]*$");

        public MS_CambiarContraseñaUsuario(Usuario usuario)
        {
            usuarioLog = usuario;

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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (usuarioLog.contrasena == MS_UsuarioService.encrypt(conActual.Password) && nuevaCon.Password == confirmarCon.Password && !String.IsNullOrEmpty(nuevaCon.Password) && nuevaCon.Password.Length >= 6 && r.IsMatch(nuevaCon.Password))
            {
                usuarioLog.contrasena = MS_UsuarioService.encrypt(nuevaCon.Password);
                MS_UsuarioService.actualizarUsuario(usuarioLog);
                MessageBox.Show("Contraseña Cambiada Correctamente");

                if (primeraVez == 1)
                {
                    MS_LoginWindow lw = new MS_LoginWindow();
                    lw.Show();
                }

                this.Close();
            }
            else
            {

                if (usuarioLog.contrasena != MS_UsuarioService.encrypt(conActual.Password))
                {
                    MessageBox.Show("La contraseña actual no es correcta.");

                }else if(!(nuevaCon.Password.Length >= 6)){

                    MessageBox.Show("La contraseña debe tener como mínimo 6 caracteres");
                
                }else if(nuevaCon.Password != confirmarCon.Password)
                {
                    MessageBox.Show("La contraseña nueva y la confirmación de contraseña no coinciden");

                }
                else if (!r.IsMatch(nuevaCon.Password))
                {
                    MessageBox.Show("La contraseña tiene que ser alfanumerica.");
                }          
                else if(!String.IsNullOrEmpty(nuevaCon.Password) || !String.IsNullOrEmpty(conActual.Password) || !String.IsNullOrEmpty(confirmarCon.Password))
                {
                    MessageBox.Show("Verifique que se hayan llenado todos los campos.");
                }
                

            }

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        
    }
}
