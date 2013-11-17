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
using System.Diagnostics;
using System.Text.RegularExpressions;

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
            MS_AdministrarArbolPerfiles perW = new MS_AdministrarArbolPerfiles();
            ///***********************************/
            //perW.Owner = this;            
            var viewModel = perW.main.DataContext as MS_AdministrarArbolPerfilesViewModel;
            MS_AdministrarPerfilesViewModel my_DataContext = this.main.DataContext as MS_AdministrarPerfilesViewModel;
            //viewModel.nombrePerfil = "BASTARDO";

            viewModel.nombrePerfil = my_DataContext.searchDescripcion;
            //viewModel.descripcion = my_DataContext.listaPerfiles.ElementAt(1).descripcion;
            
            

            //MS_AdministrarArbolPerfiles padre = this.Owner as MS_AdministrarArbolPerfiles;
            //MS_AdministrarPerfiles my_DataContext = this.main.DataContext as MS_AdministrarPerfiles;
            //MS_AdministrarArbolPerfilesViewModel padre_DataContext = padre.main.DataContext as MS_AdministrarArbolPerfilesViewModel;

            //padre_DataContext.nombrePerfil2 = my_DataContext.;            
            //MS_AdministrarPerfilesViewModel my_DataContext = this.main.DataContext as MS_AdministrarPerfilesViewModel;
            //MS_AdministrarArbolPerfilesViewModel padre_DataContext = perW.main.DataContext as MS_AdministrarArbolPerfilesViewModel;
                        
            perW.Show();           
        }

        private void nuevoPerfilBtn_Click(object sender, RoutedEventArgs e)
        {            
            MS_AdministrarArbolPerfiles perW = new MS_AdministrarArbolPerfiles();
            MS_AdministrarPerfilesViewModel my_DataContext = this.main.DataContext as MS_AdministrarPerfilesViewModel;
            MS_AdministrarArbolPerfilesViewModel padre_DataContext = perW.main.DataContext as MS_AdministrarArbolPerfilesViewModel;
            ///***********************************/
            //perW.Owner = this;
            //var viewModel = perW.main.DataContext as MS_AdministrarArbolPerfiles;
            ////viewModel.soloSeleccionarCliente = true;
            
            perW.Show();           
        }
        #region Validaciones
        private void descripcionTxtBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
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



    }
}
