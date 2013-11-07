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
using System.Text.RegularExpressions;
using pe.edu.pucp.ferretin.controller;


namespace pe.edu.pucp.ferretin.view.MSeguridad
{

    
    public partial class MS_LoginWindow : Window
    {
        #region Variables
        //Variables
        private String nombreUsuario; 
        private String contrasena;
        private bool existeUsuario;

        public List<Parametro> listaParametros;
        public int intentos;
        #endregion

        #region Constructor
        public MS_LoginWindow()
        {

            InitializeComponent();
            if (Console.CapsLock)
            {
                mayusStatus.Content = "Mayusculas Activadas";
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
        #endregion

        #region Boton Iniciar Sesion
        private void iniSesionBtn_Click(object sender, RoutedEventArgs e)
        {
            login();
        }
        #endregion

        #region Eventos de Textbox
        private void tboxNombreUsuario_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.nombreUsuario = tboxNombreUsuario.Text.ToString();

        }

        private void pwboxContrasena_PasswordChanged(object sender, RoutedEventArgs e)
        {
            this.contrasena = pwboxContrasena.Password.ToString();
        }
        #endregion

        #region Evento para verificar Mayusculas
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (Console.CapsLock)
            {
                mayusStatus.Content = "Mayusculas Activadas";
            }

            if (!Console.CapsLock)
            {
                mayusStatus.Content = "";
            }

            if (Keyboard.IsKeyDown(Key.Enter)) login();
        }
        #endregion

        #region Metodo Login
        private void login()
        {
            //Usuario que se va a loguear
            Usuario usuarioLog;
            //Lista de Usuarios
            IEnumerable<Usuario> listaUsuarios = MS_UsuarioService.obtenerListaUsuarios();
            existeUsuario = false;

            //Va a verificar cada usuario para encontrar el usuario que esta intentando loguear
            foreach(Usuario value in listaUsuarios){

                //Si el nombre de usuario y la contraseña coinciden.
                if (value.nombre == this.nombreUsuario && value.contrasena == MS_UsuarioService.encrypt(this.contrasena))
                {

                    //Verifica si la contraseña coincide con "ferretinSoft". Si coincide entonces es la primera vez que esta logueando.
                    if (MS_UsuarioService.encrypt(this.contrasena) == MS_UsuarioService.encrypt("ferretinSoft"))
                    {

                        usuarioLog = value;
                        ComunService.usuarioLo(value);
                        MS_CambiarContraseñaUsuario cc = new MS_CambiarContraseñaUsuario(usuarioLog);
                        MessageBox.Show("Inicio de sesión exitoso. A continuación cambie su contraseña.");
                        cc.Show();
                        this.Close();
                        break;

                    }

                    //Verifica el estado del usuario. De ser inactivo no se le deja iniciar sesion.
                    if (value.estado == 0)
                    {
                        MessageBox.Show("Su usuario está bloqueado, contactese con administración para solucionar este problema.");
                        this.Close();
                    }
                    else
                    {
                        listaParametros = MS_ParametroService.obtenerListaParametros().ToList();
                        //Inicia sesion correctamente.
                        value.intentosCon = Convert.ToInt16(listaParametros[0].valor);  //Se restablece el numero de intentos del usuario.
                        MS_UsuarioService.actualizarUsuario(value);                     
                        
                        usuarioLog = value;
                         
                        MainWindow mainW = new MainWindow(usuarioLog);
                        this.Close();
                        mainW.Show();
                    }

                }
                else
                {
                    

                    //Aqui va el codigo para cuando el nombre de usuario y contraseña son incorrectos
                    //Si el nombre de usuario existe
                    if (value.nombre == this.nombreUsuario)
                    {
                        existeUsuario = true;
                        //Si su estado es inactivo
                        if (value.estado == 0)
                        {
                            MessageBox.Show("Su usuario está bloqueado, contactese con administración para solucionar este problema.");
                            this.Close();
                        }

                        //Se reduce y actualiza el numero de intentos del usuario
                        intentos = (int)value.intentosCon;
                        if (value.intentosCon == 0) break;
                        value.intentosCon--;
                        intentos = (int)value.intentosCon;
                        MS_UsuarioService.actualizarUsuario(value);
                        
                    }

                    //No ingreso ningun dato
                    if (String.IsNullOrEmpty(this.nombreUsuario) && String.IsNullOrEmpty(this.contrasena))
                    {

                        lbLoginError.Content = "Ingrese un nombre de usuario y contraseña.";

                    }
                    else if (String.IsNullOrEmpty(this.nombreUsuario))          //No ingreso nombre de usuario
                    {

                        lbLoginError.Content = "Ingrese un nombre de usuario.";

                    }
                    else if (String.IsNullOrEmpty(this.contrasena))             //No ingreso Contraseña
                    {

                        lbLoginError.Content = "Ingrese una contraseña.";

                    }
                    else if (!String.IsNullOrEmpty(this.nombreUsuario) && !String.IsNullOrEmpty(this.contrasena))   //Si no ingreso Ambos
                    {

                        lbLoginError.Content = "Nombre de Usuario y Contraseña invalidos.";
                        
                    }
                    
                    if (!String.IsNullOrEmpty(this.nombreUsuario))
                    {

                        if (existeUsuario)
                        {
                            numIntentos.Content = "Número de intentos restantes: " + intentos;
                        }
                        else
                        {
                            numIntentos.Content = "   El Nombre de Usuario no existe.";
                        }
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
            //Fin Foreach



        }
        #endregion

        #region Validaciones
        private void tboxNombreUsuario_PreviewTextInput(object sender, TextCompositionEventArgs e)
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

        private void pwboxContrasena_PreviewTextInput(object sender, TextCompositionEventArgs e)
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

        #region Restringir tecla Espacio para cada Textbox
        private void tboxNombreUsuario_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Space)
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void pwboxContrasena_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Space)
                e.Handled = false;
            else
                e.Handled = true;
        }
        #endregion
    }
}
