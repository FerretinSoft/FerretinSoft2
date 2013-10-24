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
    /// Lógica de interacción para MS_LoginWindow.xaml
    /// </summary>
    public partial class MS_LoginWindow : Window
    {
        MainWindow mainW;   
        public MS_LoginWindow()
        {
            InitializeComponent();
        }

        private void iniSesionBtn_Click(object sender, RoutedEventArgs e)
        {
            //IList listaClientes = ClienteService.obtenerListaClientes();
            if (mainW == null)
            {
                mainW = new MainWindow();
            }
            this.Close();
            mainW.Show();

        }
    }
}
