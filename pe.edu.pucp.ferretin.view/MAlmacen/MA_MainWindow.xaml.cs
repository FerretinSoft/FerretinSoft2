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

namespace pe.edu.pucp.ferretin.view.MAlmacen
{
    /// <summary>
    /// 
    /// Lógica de interacción para MA_MainWindow.xaml
    /// </summary>
    public partial class MA_MainWindow : Window
    {
        public MA_MainWindow()
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

        private void invProdBtn_Click(object sender, RoutedEventArgs e)
        {
            if (ComunService.usuarioLpermisos[21].estado == true)
            {
                MA_InventarioProductosWindow invWindow = new MA_InventarioProductosWindow();
                invWindow.ShowDialog();
            }
            else
            {
                MessageBox.Show("Usted no cuenta con el permiso necesario.");
            }
        }

        private void prodBtn_Click(object sender, RoutedEventArgs e)
        {
            if (ComunService.usuarioLpermisos[22].estado == true)
            {
                MA_MantenimientoProductosWindow prodWindow = new MA_MantenimientoProductosWindow();
                prodWindow.ShowDialog();
            }
            else
            {
                MessageBox.Show("Usted no cuenta con el permiso necesario.");
            }
        }

        private void movBtn_Click(object sender, RoutedEventArgs e)
        {
            if (ComunService.usuarioLpermisos[24].estado == true)
            {
                MA_MovimientosWindow movWindow = new MA_MovimientosWindow();
                movWindow.ShowDialog();
            }
            else
            {
                MessageBox.Show("Usted no cuenta con el permiso necesario.");
            }
        }

        private void repAlmacenBtn_Click(object sender, RoutedEventArgs e)
        {
            if (ComunService.usuarioLpermisos[26].estado == true)
            {
                MA_ReportesWindow repWindow = new MA_ReportesWindow();
                repWindow.ShowDialog();
            }
            else
            {
                MessageBox.Show("Usted no cuenta con el permiso necesario.");
            }
        }

        private void solAbastecimientoBtn_Click(object sender, RoutedEventArgs e)
        {
            if (ComunService.usuarioLpermisos[25].estado == true)
            {
                MA_RegistroSolAbastecimientoWindow solWindow = new MA_RegistroSolAbastecimientoWindow();
                solWindow.ShowDialog();
            }
            else
            {
                MessageBox.Show("Usted no cuenta con el permiso necesario.");
            }
        }

        private void mantCatBtn_Click(object sender, RoutedEventArgs e)
        {
            if (ComunService.usuarioLpermisos[23].estado == true)
            {
                MA_MantenimientoCategoriasWindow catWindow = new MA_MantenimientoCategoriasWindow();
                catWindow.ShowDialog();
            }
            else
            {
                MessageBox.Show("Usted no cuenta con el permiso necesario.");
            }
        }

        private void atencionSolicitudesBtn_Click(object sender, RoutedEventArgs e)
        {
            if (ComunService.usuarioLpermisos[25].estado == true)
            {
                MA_AtencionSolAbastecimientoWindow atenSol = new MA_AtencionSolAbastecimientoWindow();
                atenSol.ShowDialog();
            }
            else
            {
                MessageBox.Show("Usted no cuenta con el permiso necesario.");
            }
        }
    }
}
