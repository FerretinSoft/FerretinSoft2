using pe.edu.pucp.ferretin.controller;
using pe.edu.pucp.ferretin.controller.MSeguridad;
using pe.edu.pucp.ferretin.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            if(usuarioLog.contrasena == MS_UsuarioService.encrypt(conActual.Password) && nuevaCon.Password == confirmarCon.Password && !String.IsNullOrEmpty(nuevaCon.Password))
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

            

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
