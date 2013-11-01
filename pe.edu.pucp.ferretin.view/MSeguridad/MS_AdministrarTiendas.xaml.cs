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
using pe.edu.pucp.ferretin.model;
using pe.edu.pucp.ferretin.controller;
using System.Threading;
using System.Windows.Threading;
using System.ComponentModel;
using pe.edu.pucp.ferretin.controller.MSeguridad;
using pe.edu.pucp.ferretin.controller.MRecursosHumanos;
using pe.edu.pucp.ferretin.viewmodel.MSeguridad;

namespace pe.edu.pucp.ferretin.view.MSeguridad
{
    /// <summary>
    /// Interaction logic for MS_AdministrarTiendas.xaml
    /// </summary>
    public partial class MS_AdministrarTiendas : Window
    {
        
        public MS_AdministrarTiendas()
        {
            InitializeComponent();
        }

        private void estadoCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        #region Validaciones Campos de Texto

        private void txtDNI_KeyDown(object sender, KeyEventArgs e)
        {
            //Validaciones para que acepte solo numeros
            if (((e.Key >= Key.D0 && e.Key <= Key.D9) || (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9) || e.Key == Key.Back || e.Key == Key.Tab))
                e.Handled = false;
            else
                e.Handled = true;
        }

        #endregion

        private void tipoCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

    }
}
