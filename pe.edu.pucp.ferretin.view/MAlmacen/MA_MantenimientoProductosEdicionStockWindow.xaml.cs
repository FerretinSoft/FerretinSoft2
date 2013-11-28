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
    /// Lógica de interacción para MA_MantenimientoProductosEdicionStock.xaml
    /// </summary>
    public partial class MA_MantenimientoProductosEdicionStockWindow : Window
    {
        TabControl tabMain;
        public MA_MantenimientoProductosEdicionStockWindow(TabControl tab)
        {
            InitializeComponent();
            tabMain = tab;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ComunService.Clean();
            tabMain.SelectedIndex = 0;
        }
    }
}
