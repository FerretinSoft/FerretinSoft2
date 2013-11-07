using pe.edu.pucp.ferretin.viewmodel.MVentas;
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
    /// Lógica de interacción para prueba.xaml
    /// </summary>
    public partial class prueba : Window
    {
        private viewmodel.MVentas.MV_PagoWindowViewModel pagoViewModel;

        public prueba()
        {
            InitializeComponent();
        }

        public prueba(viewmodel.MVentas.MV_PagoWindowViewModel pagoViewModel)
        {
            InitializeComponent();

            this.pagoViewModel = pagoViewModel;

            var myDC = this.main.DataContext as pruebaViewModel;
            myDC.venta = pagoViewModel.venta;

            this.Show();
        }
    }
}
