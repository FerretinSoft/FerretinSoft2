﻿using pe.edu.pucp.ferretin.viewmodel.MVentas;
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
            AddHandler(Keyboard.KeyDownEvent, (KeyEventHandler)HandleKeyDownEvent);
        }

        public MV_PagoWindow(MV_RegistrarVentaWindow mV_RegistrarVentaWindow)
        {
            this.Owner = mV_RegistrarVentaWindow;
            InitializeComponent();
            Show();
            try
            {
                MV_RegistrarVentaWindow regVen = this.Owner as MV_RegistrarVentaWindow;
                MV_RegistrarVentaViewModel regVen_DC = regVen.DataContext as MV_RegistrarVentaViewModel;
                MV_PagoWindowViewModel my_DC = DataContext as MV_PagoWindowViewModel;
                regVen_DC.venta.VentaMedioPago.Clear();
                my_DC.venta = regVen_DC.venta;
                
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            AddHandler(Keyboard.KeyDownEvent, (KeyEventHandler)HandleKeyDownEvent);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MV_PagoWindowViewModel p = DataContext as MV_PagoWindowViewModel;
            MV_AgregarValeWindow vale = new MV_AgregarValeWindow()
            {
                pagoviewmodel = p,
                Owner = this
            };
            vale.ShowDialog();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            MV_PagoWindowViewModel p = DataContext as MV_PagoWindowViewModel;
            MV_AgregarNotaCreditoWindow notaCredito = new MV_AgregarNotaCreditoWindow()
            {
                pagoviewmodel = p,
                Owner = this
            };
            notaCredito.ShowDialog();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            this.IsEnabled = false;
            MV_PagoWindowViewModel pagoViewModel = this.DataContext as MV_PagoWindowViewModel;
            new MV_ComprobantePagoWindow(pagoViewModel).Owner = this ;
            
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!this.IsEnabled)
            {
                this.Owner.Close();
            }
            else
            {
                this.Owner.IsEnabled = true;
            }
        }

        private void HandleKeyDownEvent(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.B && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
            {
                btcBtn.Visibility = (btcBtn.Visibility==System.Windows.Visibility.Visible)?System.Windows.Visibility.Collapsed:System.Windows.Visibility.Visible;
            }
        }
    }
}
