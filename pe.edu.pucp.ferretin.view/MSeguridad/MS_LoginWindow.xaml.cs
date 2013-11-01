using pe.edu.pucp.ferretin.model;
using pe.edu.pucp.ferretin.view;
using System;
using System.Collections;
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
using pe.edu.pucp.ferretin.controller.MSeguridad;


namespace pe.edu.pucp.ferretin.view.MSeguridad
{

    /// <summary>
    /// Interaction logic for MS_LoginWindow.xaml
    /// </summary>
    public partial class MS_LoginWindow : Window
    {
        private String nombreUsuario;
        private String contrasena;

        public List<Parametro> listaParametros;
        public int intentos;


        public MS_LoginWindow()
        {

            InitializeComponent();
            if (Console.CapsLock)
            {
                statusLabel.Content = "Mayusculas Activadas";
            }

            fechaHora.Content = System.DateTime.Now.Date;

            listaParametros = MS_ParametroService.obtenerListaParametros().ToList();
            try
            {
                intentos = Convert.ToInt16(listaParametros[0].valor);
            }
            catch
            {
                intentos = 0;
            }
        }

        private void iniSesionBtn_Click(object sender, RoutedEventArgs e)
        {
            login();
        }

        private void tboxNombreUsuario_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.nombreUsuario = tboxNombreUsuario.Text.ToString();

        }

        private void pwboxContrasena_PasswordChanged(object sender, RoutedEventArgs e)
        {
            this.contrasena = pwboxContrasena.Password.ToString();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (Console.CapsLock)
            {
                statusLabel.Content = "Mayusculas Activadas";
            }

            if (!Console.CapsLock)
            {
                statusLabel.Content = "";
            }

            if (Keyboard.IsKeyDown(Key.Enter)) login();
        }

        private void login()
        {
            Usuario usuarioLog;

            IEnumerable<Usuario> listaUsuarios = MS_UsuarioService.obtenerListaUsuarios();

            foreach(Usuario value in listaUsuarios){

                if (value.nombre == this.nombreUsuario && value.contrasena == MS_UsuarioService.encrypt(this.contrasena))
                {
                    
                    if (MS_UsuarioService.encrypt(this.contrasena) == MS_UsuarioService.encrypt("ferretinSoft"))
                    {

                        usuarioLog = value;
                        MS_CambiarContraseñaUsuario cc = new MS_CambiarContraseñaUsuario(usuarioLog);
                        cc.Show();
                        this.Close();
                        break;


                    }

                    if (value.estado == 0)
                    {
                        MessageBox.Show("Su usuario está bloqueado, contactese con administración para solucionar este problema.");
                        this.Close();
                    }
                    else
                    {

                        value.intentosCon = Convert.ToInt16(listaParametros[0].valor);
                        MS_UsuarioService.actualizarUsuario(value);
                        usuarioLog = value;
                        MainWindow mainW = new MainWindow(usuarioLog);
                        this.Close();
                        mainW.Show();
                    }

                }
                else
                {


                    if (value.nombre == this.nombreUsuario)
                    {

                        if (value.estado == 0)
                        {
                            MessageBox.Show("Su usuario está bloqueado, contactese con administración para solucionar este problema.");
                            this.Close();
                        }

                        intentos = (int)value.intentosCon;
                        if (value.intentosCon == 0) break;

                        value.intentosCon--;
                        intentos = (int)value.intentosCon;
                        MS_UsuarioService.actualizarUsuario(value);
                    }

                    if (String.IsNullOrEmpty(this.nombreUsuario) && String.IsNullOrEmpty(this.contrasena))
                    {

                        lbLoginError.Content = "Ingrese un nombre de usuario y contraseña.";

                    }
                    else if (String.IsNullOrEmpty(this.nombreUsuario))
                    {

                        lbLoginError.Content = "Ingrese un nombre de usuario.";

                    }
                    else if (String.IsNullOrEmpty(this.contrasena))
                    {

                        lbLoginError.Content = "Ingrese una contraseña.";

                    }
                    else if (!String.IsNullOrEmpty(this.nombreUsuario) && !String.IsNullOrEmpty(this.contrasena))
                    {

                        lbLoginError.Content = "Nombre de Usuario y Contraseña invalidos.";
                        
                    }

                    if (!String.IsNullOrEmpty(this.nombreUsuario))
                    {

                        numIntentos.Content = "Número de intentos restantes: " + intentos;

                        if (intentos == 0)
                        {
                            MessageBox.Show("Este usuario sera bloqueado, por favor comuniquese con el area de administración");
                            value.estado = 0;
                            MS_UsuarioService.actualizarUsuario(value);
                            this.Close();
                            break;
                        }

                    }


                }

            }

            


        }



    }
}
