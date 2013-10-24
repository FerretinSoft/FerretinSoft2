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
    /// Lógica de interacción para MV_MainWindow.xaml
    /// </summary>
    public partial class MV_MainWindow : Window
    {
        private static MV_MainWindow INSTANCE;
        public static MV_MainWindow instance
        {
            get
            {
                if (INSTANCE == null)
                {
                    INSTANCE = new MV_MainWindow();
                }
                return INSTANCE;
            }
        }

        public MV_MainWindow()
        {
            InitializeComponent();
        }

        private void homeBtn_Click(object sender, RoutedEventArgs e)
        {
            if (this.OwnedWindows.Count == 0) //si no tiene ninguna ventana hija 
            {
                this.Close();
            }
        }

        private void registrarVentaBtn_Click(object sender, RoutedEventArgs e)
        {
            MV_RegistrarVentaWindow w = new MV_RegistrarVentaWindow();
            w.Owner = this;
            w.Show();
        }

        private void admClientesBtn_Click(object sender, RoutedEventArgs e)
        {
            MV_ClientesWindow cw = new MV_ClientesWindow();
            cw.Owner = this;
            cw.Show();
        }

        private void repVentasBtn_Click(object sender, RoutedEventArgs e)
        {
            MV_ReportesVentasWindow repW = new MV_ReportesVentasWindow();
            repW.Owner = this;
            repW.Show();
        }



        private void administrarProformasBtn_Click(object sender, RoutedEventArgs e)
        {
            MV_AdministrarProformasWindow w = new MV_AdministrarProformasWindow();
            w.Owner = this;
            w.Show();
        }

        private void regresarVentasBtn_Click(object sender, RoutedEventArgs e)
        {
            ventasMenu.Visibility = System.Windows.Visibility.Visible;
            postVentaMenu.Visibility = System.Windows.Visibility.Hidden;
        }
        private void anularVentaBtn_Click(object sender, RoutedEventArgs e)
        {
            MV_AdministrarVentasWindow pw = new MV_AdministrarVentasWindow();
            pw.Show();
        }

        private void administrarDevolucionBtn_Click(object sender, RoutedEventArgs e)
        {
            MV_DevolucionesWindow pw = new MV_DevolucionesWindow();
            pw.Show();
        }


        private void administrarNotaCreditoBtn_Click(object sender, RoutedEventArgs e)
        {
            MV_AdministrarNotaCreditoWindow w = new MV_AdministrarNotaCreditoWindow();
            w.Show();
        }

        private void postVentaBtn_Click(object sender, RoutedEventArgs e)
        {
            ventasMenu.Visibility = System.Windows.Visibility.Hidden;
            postVentaMenu.Visibility = System.Windows.Visibility.Visible;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            INSTANCE = null;
        }

        private void promocionesBtn_Click(object sender, RoutedEventArgs e)
        {
            MV_AdministrarPromocionesWindow w = new MV_AdministrarPromocionesWindow();
            w.Show();
        }

        private void admValesBtn_Click(object sender, RoutedEventArgs e)
        {
            MV_AdministrarValesWindow w = new MV_AdministrarValesWindow();
            w.Show();
        }
    }
}
