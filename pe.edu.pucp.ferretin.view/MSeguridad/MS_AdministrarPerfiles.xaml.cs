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
    /// Lógica de interacción para MS_AdministrarPerfiles.xaml
    /// </summary>
    public partial class MS_AdministrarPerfiles : Window
    {
        public MS_AdministrarPerfiles()
        {
            InitializeComponent();
        }

        private void nuevoPerfilBtn_click(object sender, RoutedEventArgs e)
        {
            perfilesTab.SelectedIndex = 1;
        }

        private void edPerfilBtn_Click(object sender, RoutedEventArgs e)
        {
            perfilesTab.SelectedIndex = 1;
        }     
    }
}
