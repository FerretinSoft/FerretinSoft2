using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Data.Linq;
using System.Windows;
using pe.edu.pucp.ferretin.controller.MRecursosHumanos;
using pe.edu.pucp.ferretin.model;
using System.Security.Cryptography;
using System.IO;

namespace pe.edu.pucp.ferretin.controller.MSeguridad
{
    public class MS_UsuarioService : MS_ComunService
    {
        /*******************************************************
                            PARA USUARIOS
        /*******************************************************/
        public static IEnumerable<Usuario> _listaUsuarios = null;
        public static IEnumerable<Usuario> listaUsuarios
        {
            get
            {
                if (_listaUsuarios == null)
                {
                    _listaUsuarios = db.Usuario;
                }
                //Usando concurrencia pesimista:
                ///La lista de clientes se actualizara para ver los cambios
                ///Si quisiera usar concurrencia optimista quito la siguiente linea
                db.Refresh(RefreshMode.OverwriteCurrentValues, _listaUsuarios);
                return _listaUsuarios;
            }
            set
            {
                _listaUsuarios = value;
            }
        }
        /*******************************************************/
        public static IEnumerable<Usuario> obtenerListaUsuarios()
        {
            listaUsuarios = from p in db.Usuario
                            orderby p.id_empleado
                            select p;
            return listaUsuarios;
        }

        /*******************************************************/
        public static IEnumerable<Usuario> obtenerListaUsuariosBy(Usuario usuario)
        {
            return from c in listaUsuarios
                   where
                   (c.id_empleado != null && c.id_empleado == usuario.id_empleado
                       && c.nombre != null && c.nombre.ToLower().Contains(usuario.nombre.ToLower().Trim())
                       && c.contrasena != null && c.contrasena.Contains(usuario.contrasena)
                       /*&& c.id_perfil != null && c.id_perfil.Contains(usuario.id_perfil)
                       && c.estado != null && c.estado.Contains(usuario.estado)*/
                    )
                   orderby c.nombre
                   select c;
        }
        /*******************************************************/
        //public static void insertarUsuario(Usuario usuario)
        //{
        //    db.Usuario.InsertOnSubmit(usuario);
        //    db.SubmitChanges();
        //}
        /*******************************************************/
        public static void actualizarUsuario(Usuario usuario)
        {
            try
            {
                db.SubmitChanges(ConflictMode.ContinueOnConflict);
            }
            catch(ChangeConflictException e) 
            {
                db.ChangeConflicts.ResolveAll(RefreshMode.KeepCurrentValues);
                try
                {
                    db.SubmitChanges(ConflictMode.ContinueOnConflict);
                }
                catch 
                {
                    MessageBox.Show(e.Message); 
                }
            }
        }


        /*******************************************************
                            PARA EMPLEADOS
        /*******************************************************/
        private static IEnumerable<Empleado> _listaEmpleados;
        private static IEnumerable<Empleado> listaEmpleados
        {
            get
            {
                if (_listaEmpleados == null)
                {
                    _listaEmpleados = MR_EmpleadoService.obtenerListaEmpleados();
                }
                return _listaEmpleados;
            }
            set
            {
                _listaEmpleados = value;
            }
        }
        /*******************************************************/
        public static System.Collections.IEnumerable obtenerListaUsuariosBy(Usuario usuario, Empleado empleado)
        {

            return from u in listaUsuarios
                   where
                   (u.id_empleado != null && u.id_empleado == usuario.id_empleado
                       && u.nombre != null && u.nombre.ToLower().Contains(usuario.nombre.ToLower().Trim())
                       && u.Empleado != null &&
                            (u.Empleado.nombre != null && u.Empleado.nombre.ToLower().Contains(empleado.nombre.ToLower().Trim())
                            && u.Empleado.apPaterno != null && u.Empleado.apPaterno.ToLower().Contains(empleado.apPaterno.ToLower().Trim())
                       //&& u.Empleado.apMaterno != null && u.Empleado.apMaterno.ToLower().Contains(empleado.apMaterno.ToLower().Trim()) 
                            )
                       && (usuario.id_perfil == null || (u.id_perfil != null && u.id_perfil.Equals(usuario.id_perfil)))
                       && (usuario.estado == null || (u.estado != null && u.estado.Equals(usuario.estado)))
                    )
                   orderby u.nombre
                   select u;
        }
        /*******************************************************/
        public static IEnumerable<Usuario> buscar(string nomUsuario, Perfil perfil, string nombres, string apellidos, string apellidosMat, int estado)
        {            
            IEnumerable<Usuario> usuarios = listaUsuarios;
            
            //Filtro por nombre
            usuarios = usuarios.Where(u => u.nombre.ToLower().Contains(nomUsuario.ToLower().Trim()));
            //Filtro por perfil
            usuarios = usuarios.Where(u => (perfil==null) || (perfil.id<=0) || (u.Perfil.id == perfil.id) );
            //Filtro por nombre y apellido
            usuarios = usuarios.Where(u => u.Empleado.nombre.ToLower().Contains(nombres.ToLower().Trim()) && (u.Empleado.apPaterno.ToLower().Contains(apellidos.ToLower().Trim())) && (u.Empleado.apMaterno.ToLower().Contains(apellidosMat.ToLower().Trim())) );
            //Filtro por código
            //if (codigo!=null) usuarios = usuarios.Where(u => u.codUsuario.ToLower().Contains(codigo.ToLower().Trim()));           
            //Filtro por estado
            usuarios = usuarios.Where(u => (estado == 0) || (u.estado != null && u.estado == estado - 1));

            return usuarios;

        }
        /******************** VALIDACION PARA USUARIO-EMPLEADO POR DNI YA EXISTENTE ***********************/
        public static bool insertarUsuario(Usuario usuario)
        {
            Usuario user;
            try
            {
                try
                {
                    user = db.Usuario.Single(u => u.Empleado.dni == usuario.Empleado.dni);
                    return false;
                }
                catch (Exception e)
                {
                    db.Usuario.InsertOnSubmit(usuario);
                    Console.WriteLine("llego");
                    return enviarCambios();
                }
            }
            catch (Exception e)
            {
                return false;
            }
                   
        }
        /******************** VALIDACION PARA USUARIO YA EXISTENTE ***********************/
        public static bool validarUserName(Usuario usuario)
        {
            Usuario user;
            try
            {
                user = db.Usuario.Single(u => u.nombre.Equals(usuario.nombre));
                if (usuario.nombre.Equals(user.nombre))
                    if (user.nombre.Equals(usuario.nombre))  
                        return false;
                return true;
            }
            catch (Exception e)
            {
                return true;            
            }
        }

        /*******************************************************
                            PARA PERFILES
        /*******************************************************/
        private static IEnumerable<Perfil> _listaPerfiles;
        private static IEnumerable<Perfil> listaPerfiles
        {
            get
            {
                if (_listaPerfiles == null)
                {
                    _listaPerfiles = from p in db.Perfil
                                     select p;
                }
                return _listaPerfiles;
            }
            set
            {
                _listaPerfiles = value;
            }
        }
        /*******************************************************/
        public static IEnumerable<Perfil> obtenerPerfiles()
        {
            return listaPerfiles;
        }  




        /*******************************************************************************/
        /********************Para contraseña***********************/

        public static string encrypt(string plainText)
        {

            string passPhrase = "rudy";        // can be any string
            string saltValue = "akatsuki";        // can be any string
            string hashAlgorithm = "MD5";             // can be "MD5"
            int passwordIterations = 2;                  // can be any number
            string initVector = "@1B2c3D4e5F6g7H8"; // must be 16 bytes
            int keySize = 128;                // can be 192 or 128

            if (String.IsNullOrEmpty(plainText)) return "";

            byte[] initVectorBytes = Encoding.ASCII.GetBytes(initVector);
            byte[] saltValueBytes = Encoding.ASCII.GetBytes(saltValue);

            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

            PasswordDeriveBytes password = new PasswordDeriveBytes(
                                                            passPhrase,
                                                            saltValueBytes,
                                                            hashAlgorithm,
                                                            passwordIterations);

            byte[] keyBytes = password.GetBytes(keySize / 8);

            RijndaelManaged symmetricKey = new RijndaelManaged();

            symmetricKey.Mode = CipherMode.CBC;

            ICryptoTransform encryptor = symmetricKey.CreateEncryptor(
                                                             keyBytes,
                                                             initVectorBytes);

            MemoryStream memoryStream = new MemoryStream();

            CryptoStream cryptoStream = new CryptoStream(memoryStream,
                                                         encryptor,
                                                         CryptoStreamMode.Write);

            cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);


            cryptoStream.FlushFinalBlock();

            byte[] cipherTextBytes = memoryStream.ToArray();

            memoryStream.Close();
            cryptoStream.Close();

            string cipherText = Convert.ToBase64String(cipherTextBytes);

            return cipherText;
        }


    }
}