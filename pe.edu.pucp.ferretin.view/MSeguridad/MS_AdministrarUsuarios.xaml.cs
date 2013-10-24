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
    /// Lógica de interacción para MS_AdministrarUsuarios.xaml
    /// </summary>
    public partial class MS_AdministrarUsuarios : Window
    {
        public MS_AdministrarUsuarios()
        {
            InitializeComponent();
        }

        private void nuevoUsuarioBtn_Click(object sender, RoutedEventArgs e)
        {
            usuariosTab.SelectedIndex = 1;
        }

        private void edUsuarioBtn_Click(object sender, RoutedEventArgs e)
        {
            usuariosTab.SelectedIndex = 1;
        }

        private void privilegiosBtn_Click(object sender, RoutedEventArgs e)
        {
            //PrivilegiosWindow pw = new PrivilegiosWindow();
            //pw.ShowDialog();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MS_GenerarContraseñaWindow cw = new MS_GenerarContraseñaWindow();
            cw.ShowDialog();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
