using pe.edu.pucp.ferretin.controller;
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
            if (ComunService.usuarioLpermisos[4].estado == true)
            {
                MV_RegistrarVentaWindow w = new MV_RegistrarVentaWindow();
                w.Owner = this;
                w.Show();
            }
            else
            {
                MessageBox.Show("Usted no cuenta con el permiso necesario");
            }
        }

        private void admClientesBtn_Click(object sender, RoutedEventArgs e)
        {
            if (ComunService.usuarioLpermisos[7].estado == true)
            {
                MV_ClientesWindow cw = new MV_ClientesWindow();
                cw.Owner = this;
                cw.Show();
            }
            else
            {
                MessageBox.Show("Usted no cuenta con el permiso necesario");
            }
        }

        private void repVentasBtn_Click(object sender, RoutedEventArgs e)
        {
            if (ComunService.usuarioLpermisos[13].estado == true)
            {
                MV_ReportesVentasWindow repW = new MV_ReportesVentasWindow();
                repW.Owner = this;
                repW.Show();
            }
            else
            {
                MessageBox.Show("Usted no cuenta con el permiso necesario");
            }
        }



        private void administrarProformasBtn_Click(object sender, RoutedEventArgs e)
        {
            if (ComunService.usuarioLpermisos[11].estado == true)
            {
                MV_AdministrarProformasWindow w = new MV_AdministrarProformasWindow();
                w.Owner = this;
                w.Show();
            }
            else
            {
                MessageBox.Show("Usted no cuenta con el permiso necesario");
            }
        }

        private void regresarVentasBtn_Click(object sender, RoutedEventArgs e)
        {
            ventasMenu.Visibility = System.Windows.Visibility.Visible;
            postVentaMenu.Visibility = System.Windows.Visibility.Hidden;
        }
        private void anularVentaBtn_Click(object sender, RoutedEventArgs e)
        {
            if (ComunService.usuarioLpermisos[5].estado == true)
            {
                MV_AdministrarVentasWindow pw = new MV_AdministrarVentasWindow();
                pw.Show();
            }
            else
            {
                MessageBox.Show("Usted no cuenta con el permiso necesario");
            }
        }

        private void administrarDevolucionBtn_Click(object sender, RoutedEventArgs e)
        {
            if (ComunService.usuarioLpermisos[9].estado == true)
            {
                MV_DevolucionesWindow pw = new MV_DevolucionesWindow();
                pw.Show();
            }
            else
            {
                MessageBox.Show("Usted no cuenta con el permiso necesario");
            }
        }


        private void administrarNotaCreditoBtn_Click(object sender, RoutedEventArgs e)
        {
            if (ComunService.usuarioLpermisos[10].estado == true)
            {
                MV_AdministrarNotaCreditoWindow w = new MV_AdministrarNotaCreditoWindow();
                w.Show();
            }
            else
            {
                MessageBox.Show("Usted no cuenta con el permiso necesario");
            }
        }

        private void postVentaBtn_Click(object sender, RoutedEventArgs e)
        {
            if (ComunService.usuarioLpermisos[8].estado == true)
            {
                ventasMenu.Visibility = System.Windows.Visibility.Hidden;
                postVentaMenu.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                MessageBox.Show("Usted no cuenta con el permiso necesario");
            }
        }

        private void preVentaBtn_Click(object sender, RoutedEventArgs e)
        {
            if (ComunService.usuarioLpermisos[8].estado == true)
            {
                ventasMenu.Visibility = System.Windows.Visibility.Hidden;
                preVentaMenu.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                MessageBox.Show("Usted no cuenta con el permiso necesario");
            }
        }

        private void precioProdBtn_Click(object sender, RoutedEventArgs e)
        {
            //if (ComunService.usuarioLpermisos[8].estado == true)
            //{
            MV_AdministrarPrecioProductos w = new MV_AdministrarPrecioProductos();
            w.Show();
            //}
            //else
            //{
            //    MessageBox.Show("Usted no cuenta con el permiso necesario");
            //}
        }


        private void Window_Closed(object sender, EventArgs e)
        {
            INSTANCE = null;
        }

        private void promocionesBtn_Click(object sender, RoutedEventArgs e)
        {
            if (ComunService.usuarioLpermisos[6].estado == true)
            {
                MV_AdministrarPromocionesWindow w = new MV_AdministrarPromocionesWindow();
                w.Show();
            }
            else
            {
                MessageBox.Show("Usted no cuenta con el permiso necesario");
            }
        }

        private void admValesBtn_Click(object sender, RoutedEventArgs e)
        {
            if (ComunService.usuarioLpermisos[12].estado == true)
            {
                MV_AdministrarValesWindow w = new MV_AdministrarValesWindow();
                w.Show();
            }
            else
            {
                MessageBox.Show("Usted no cuenta con el permiso necesario");
            }
        }
    }
}
