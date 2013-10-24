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

namespace pe.edu.pucp.ferretin.view.MRecursosHumanos
{
    /// <summary>
    /// Lógica de interacción para MR_MainWindow.xaml
    /// </summary>
    public partial class MR_MainWindow : Window
    {
        public MR_MainWindow()
        {
            InitializeComponent();
        }

        private void homeBtn_Click(object sender, RoutedEventArgs e)
        {
            if (this.OwnedWindows.Count == 0)
            {
                this.Close();
            }
        }



        private void adminPersonalBtn_Click(object sender, RoutedEventArgs e)
        {
            MR_AdministrarPersonalWindow adminW = new MR_AdministrarPersonalWindow();
            adminW.Show();

        }
    }
}
