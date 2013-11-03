using System;
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

namespace pe.edu.pucp.ferretin.view.MSeguridad
{
        
    /**************************************************************************************************************/
    /// <summary>
    /// Lógica de interacción para MS_AdministrarPerfiles.xaml
    /// </summary>
    public partial class MS_AdministrarPerfiles : Window
    {
        public MS_AdministrarPerfiles()
        {
            InitializeComponent();
        }
           
        private void perfil_Click(object sender, RoutedEventArgs e)
        {
            MSeguridad.MS_AdministrarArbolPerfiles perW = new MSeguridad.MS_AdministrarArbolPerfiles();
            perW.Show();
        }

        private void nuevoPerfilBtn_Click(object sender, RoutedEventArgs e)
        {
            MSeguridad.MS_AdministrarArbolPerfiles perW = new MSeguridad.MS_AdministrarArbolPerfiles();
            perW.Show();
        }


    }
}
