using pe.edu.pucp.ferretin.controller;
using pe.edu.pucp.ferretin.controller.MRecursosHumanos;
using pe.edu.pucp.ferretin.model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Lógica de interacción para MR_AdministrarPersonalWindow.xaml
    /// </summary>
    public partial class MR_AdministrarPersonalWindow : Window
    {
       
        public MR_AdministrarPersonalWindow()
        {
            InitializeComponent();
                  
        }


        private void txtDNI_KeyDown(object sender, KeyEventArgs e)
        {
     
            //Validaciones para que acepte solo numeros
            if (((e.Key >= Key.D0 && e.Key <= Key.D9) || (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9) || e.Key == Key.Back || e.Key == Key.Tab))
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void txtNombre_KeyDown(object sender, KeyEventArgs e)
        {
            //Validaciones para textbox de solo letras
            if (!(e.Key > Key.A && e.Key < Key.Z)) e.Handled = true;

        }
        private void txtApPaterno_KeyDown(object sender, KeyEventArgs e)
        {
            //Validaciones para textbox de solo letras
            if (!(e.Key > Key.A && e.Key < Key.Z)) e.Handled = true;
        }
        private void txtApMaterno_KeyDown(object sender, KeyEventArgs e)
        {
            //Validaciones para textbox de solo letras
            if (!(e.Key > Key.A && e.Key < Key.Z)) e.Handled = true;
        }
        private void txtDireccion_KeyDown(object sender, KeyEventArgs e)
        {
            //Validaciones para textbox de solo letras
            if (!(e.Key > Key.A && e.Key < Key.Z)) e.Handled = true;
        }
        private void txtTelf1_KeyDown(object sender, KeyEventArgs e)
        {
            //Validaciones para que acepte solo numeros
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
                e.Handled = false;
            else
                e.Handled = true;
        }
        private void txtTelf2_KeyDown(object sender, KeyEventArgs e)
        {
            //Validaciones para que acepte solo numeros
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
                e.Handled = false;
            else
                e.Handled = true;
        }
        private void txtEmail_KeyDown(object sender, KeyEventArgs e)
        {
            //Validaciones para que acepte solo numeros
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void txtSueldo_KeyDown(object sender, KeyEventArgs e)
        {
            //Validaciones para textbox tipo precio
            if (e.Key == Key.OemComma || e.Key == Key.OemPeriod)
            {
                
            }
            else
            {
                if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
                    e.Handled = false;
                else
                    e.Handled = true;
            }
        }



    }
}
