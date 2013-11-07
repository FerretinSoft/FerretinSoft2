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

namespace pe.edu.pucp.ferretin.view.MVentas
{
    /// <summary>
    /// Lógica de interacción para MV_DocNotaCredito.xaml
    /// </summary>
    public partial class MV_DocNotaCredito : Window
    {
        public MV_DocNotaCredito()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == true){
                imprimirBtn.Visibility = System.Windows.Visibility.Hidden;
                printDialog.PrintVisual(main, "Boleta de venta");
                this.Close();
            }
        }
    }
}
