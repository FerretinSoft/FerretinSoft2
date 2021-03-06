﻿using System;
using System.Net;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.ComponentModel;

using pe.edu.pucp.ferretin.controller;
using pe.edu.pucp.ferretin.model;
using pe.edu.pucp.ferretin.controller.MSeguridad;
using pe.edu.pucp.ferretin.viewmodel.MSeguridad;
using System.Net.NetworkInformation;
using System.Management;
using System.Text.RegularExpressions;

namespace pe.edu.pucp.ferretin.view.MSeguridad
{
    /************************************************************************************************************/
    /// <summary>
    /// Lógica de interacción para MS_AdministrarUsuarios.xaml
    /// </summary>
    public partial class MS_AdministrarUsuarios : Window
    {
        /****************************************************/
        public MS_AdministrarUsuarios()
        {
            InitializeComponent();
        }
        /************************************************/
        #region Validaciones Campos de Texto
        /****************************************************
         * Validacion para dni del usuario en la tabla empleado
        ****************************************************/        
        private void dniEmp_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            //Validaciones para que acepte solo numeros
            if (((e.Key >= Key.D0 && e.Key <= Key.D9) || (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9) || e.Key == Key.Back || e.Key == Key.Tab))
                e.Handled = false;
            else
                e.Handled = true;
        }
        /****************************************************/
        private void dniEmp_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            //Validaciones para que acepte solo numeros
            if (Regex.IsMatch(e.Text, "[0-9]"))            
                e.Handled = false;            
            else            
                e.Handled = true;            
        }
        /****************************************************
        * Validacion para nombre del usuario (username) a crear
        ****************************************************/
        private void nombreUsuarioTxtBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            //Validaciones para que acepte solo alfanumericos
            if (Regex.IsMatch(e.Text, "[a-zA-Z0-9]"))            
                e.Handled = false;            
            else            
                e.Handled = true;            
        }
        /****************************************************/
        private void nombreUsuarioTxtBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            //Validaciones para que no acepte espacio en blanco
            if (e.Key != Key.Space)
                e.Handled = false;
            else
                e.Handled = true;
        }        
        #endregion

        private void nombreUsuarioSearchTxtBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
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

        private void nombresTxtBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (Regex.IsMatch(e.Text, "[a-zA-Z]"))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }

        }

        private void apellidoPatTxtBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (Regex.IsMatch(e.Text, "[a-zA-Z]"))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }

        }

        private void apellidoMatTxtBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (Regex.IsMatch(e.Text, "[a-zA-Z]"))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }

        }

        private void Windows_Closing(object sender, CancelEventArgs e)
        {
            ComunService.Clean();
        }        

    }
    
}


