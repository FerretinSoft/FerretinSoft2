using System;
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
        public MS_AdministrarUsuarios()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        #region Validaciones Campos de Texto

        private void txtNomUsuario_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        #endregion

        private void tipoCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

    }
    
}


