using pe.edu.pucp.ferretin.viewmodel.MVentas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
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
    /// Lógica de interacción para prueba.xaml
    /// </summary>
    public partial class MV_ComprobantePagoWindow : Window
    {
        private viewmodel.MVentas.MV_PagoWindowViewModel pagoViewModel;

        public MV_ComprobantePagoWindow()
        {
            InitializeComponent();
        }

        public MV_ComprobantePagoWindow(viewmodel.MVentas.MV_PagoWindowViewModel pagoViewModel)
        {
            InitializeComponent();

            this.pagoViewModel = pagoViewModel;

            var myDC = this.main.DataContext as pruebaViewModel;
            myDC.venta = pagoViewModel.venta;
            var n = pagoViewModel.venta.id;
            this.Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == true)
            {
                printDialog.PrintVisual(printZone, "Boleta de venta");
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                this.Owner.Owner.Close();
            }
            catch { }
        }
    }
}
