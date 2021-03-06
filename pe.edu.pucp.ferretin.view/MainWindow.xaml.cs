﻿using pe.edu.pucp.ferretin.model;
using pe.edu.pucp.ferretin.viewmodel;
using pe.edu.pucp.ferretin.controller;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace pe.edu.pucp.ferretin.view
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Variables
        Usuario usuarioLog;
        #endregion 

        #region Constructor
        public MainWindow(Usuario usuario)
        {
            InitializeComponent();
            usuarioLog = usuario;
            ComunService.usuarioLo(usuario);

            try
            {
                ComunService.obtenerPermisos(usuario);
            }
            catch { }

            try
            {
                usuarioMenu.Header = ComunService.usuarioL.Empleado.tiendaActual.nombre + ", " + ComunService.usuarioL.Empleado.nombre + " " + ComunService.usuarioL.Empleado.apPaterno;
            }
            catch(Exception e) 
            {
                return;
            }

        }
        #endregion

        #region Boton Compras
        private void comprasBtn_Click(object sender, RoutedEventArgs e)
        {
            if (ComunService.usuarioLpermisos[14].estado == true)
            {
                MCompras.MC_MainWindow Mainw = new MCompras.MC_MainWindow();
                Mainw.ShowDialog();
            }
            else
            {
                MessageBox.Show("Usted no cuenta con el permiso necesario.");
            }   
                
        }
        #endregion

        #region Boton Seguridad
        private void confBtn_Click(object sender, RoutedEventArgs e)
        {
            if (ComunService.usuarioLpermisos[27].estado == true)
            {
                MSeguridad.MS_MainWindow MSWindow = new MSeguridad.MS_MainWindow();
                MSWindow.ShowDialog();
            }
            else
            {
                MessageBox.Show("Usted no cuenta con el permiso necesario.");
            }
                
        }
        #endregion

        #region Boton Ventas
        private void ventasBtn_Click(object sender, RoutedEventArgs e)
        {
            if (ComunService.usuarioLpermisos[3].estado == true)
            {

                MVentas.MV_MainWindow MVWindow = MVentas.MV_MainWindow.instance;
                MVWindow.ShowDialog();
                MVWindow.Focus();
            }
            else
            {
                MessageBox.Show("Usted no cuenta con el permiso necesario.");
            }     
           
        }
        #endregion

        #region Boton RRHH
        private void rrhhBtn_Click(object sender, RoutedEventArgs e)
        {
            if (ComunService.usuarioLpermisos[1].estado == true)
            {
                MRecursosHumanos.MR_MainWindow MRWindow = new MRecursosHumanos.MR_MainWindow();
                MRWindow.ShowDialog();
            }
            else
            {
                MessageBox.Show("Usted no cuenta con el permiso necesario.");
            }             
            
        }
        #endregion

        #region Boton Almacen
        private void almacenBtn_Click(object sender, RoutedEventArgs e)
        {

            if (ComunService.usuarioLpermisos[20].estado == true)
            {
                MAlmacen.MA_MainWindow maMain = new MAlmacen.MA_MainWindow();
                maMain.ShowDialog();
            }
            else
            {
                MessageBox.Show("Usted no cuenta con el permiso necesario.");
            }  

            
        }
        #endregion

        #region Botones Cambiar Contraseña y Cerrar Sesion
        private void cambiarPasswMenuItem_Click(object sender, RoutedEventArgs e)
        {
            MSeguridad.MS_CambiarContraseñaUsuario wCambiar = new MSeguridad.MS_CambiarContraseñaUsuario(usuarioLog, 0);
            wCambiar.ShowDialog();
        }

        private void cerrarSesionMenuItem_Click(object sender, RoutedEventArgs e)
        {
            for (int i = App.Current.Windows.Count - 1; i >= 0; i--)
            {
                if (App.Current.Windows[i] != this)
                {
                    App.Current.Windows[i].Close();
                }
            }

            MSeguridad.MS_LoginWindow lw = new MSeguridad.MS_LoginWindow();
            this.Close();
            lw.ShowDialog();
        }
        #endregion
        
        #region Boton Reportes
        private void reportesBtn_Click(object sender, RoutedEventArgs e)
        {
            ReportesWindow RPWindow = new ReportesWindow();
            RPWindow.ShowDialog();
        }
        #endregion
    }
}
