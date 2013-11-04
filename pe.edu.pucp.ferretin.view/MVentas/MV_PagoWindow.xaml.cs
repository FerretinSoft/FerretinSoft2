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
    /// Lógica de interacción para MV_PagoWindow.xaml
    /// </summary>
    public partial class MV_PagoWindow : Window
    {
        
        public MV_PagoWindow()
        {
            InitializeComponent();
        }

        public MV_PagoWindow(MV_RegistrarVentaWindow mV_RegistrarVentaWindow)
        {
            this.Owner = mV_RegistrarVentaWindow;
            InitializeComponent();
            Show();
            try
            {
                MV_RegistrarVentaWindow regVen = this.Owner as MV_RegistrarVentaWindow;
                MV_RegistrarVentaViewModel regVen_DC = regVen.main.DataContext as MV_RegistrarVentaViewModel;
                
            }
            catch { }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
