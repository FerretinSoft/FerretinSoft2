using pe.edu.pucp.ferretin.controller;
using pe.edu.pucp.ferretin.controller.MRecursosHumanos;
using pe.edu.pucp.ferretin.model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using pe.edu.pucp.ferretin.view.MVentas;
using pe.edu.pucp.ferretin.viewmodel.MVentas;
using pe.edu.pucp.ferretin.viewmodel.MRecursosHumanos;


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


        private void dni_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (Regex.IsMatch(e.Text, "[0-9]"))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtDNI_KeyDown(object sender, KeyEventArgs e)
        {
            //Validaciones para que acepte solo numeros
            if (((e.Key >= Key.D0 && e.Key <= Key.D9) || (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9) || e.Key == Key.Back || e.Key == Key.Tab))
                e.Handled = false;
            else
                e.Handled = true;     
        }


        private void nombre_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (Regex.IsMatch(e.Text, @"^([A-Z][a-z ]+)(\s[A-Z][a-z]+)*$"))
            {
                e.Handled = true;
            }
            else
            {
                e.Handled = false;
            }       
        }

        private void txtNombre_KeyDown(object sender, KeyEventArgs e)
        {
            //Validaciones para textbox de solo letras

            //if (((e.Key >= Key.A && e.Key <= Key.Z) || e.Key == Key.Back || e.Key == Key.Tab || e.Key!=Key.Space ))
            //    e.Handled = false;
            //else
            //    e.Handled = true;


            //if (((e.Key >= Key.A && e.Key <= Key.Z) || e.Key == Key.Back || e.Key == Key.Tab))
            //    e.Handled = false;
            //else
            //    e.Handled = true;
        }

         private void apPaterno_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
           if (Regex.IsMatch(e.Text, @"^([A-Z][a-z ]+)(\s[A-Z][a-z]+)*$"))
            {
                e.Handled = true;
            }
            else
            {
                e.Handled = false;
            } 
        }

         private void txtApPaterno_KeyDown(object sender, KeyEventArgs e)
         {
             //Validaciones para textbox de solo letras
            //if (!(e.Key >= Key.A && e.Key <= Key.Z)) e.Handled = true;
         }


         private void apMaterno_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (Regex.IsMatch(e.Text, @"^([A-Z][a-z ]+)(\s[A-Z][a-z]+)*$"))
            {
                e.Handled = true;
            }
            else
            {
                e.Handled = false;
            } 
        
        }

         private void txtApMaterno_KeyDown(object sender, KeyEventArgs e)
         {
             //Validaciones para textbox de solo letras
           // if (!(e.Key >= Key.A && e.Key <= Key.Z)) e.Handled = true;

         }


        private void direccion_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (Regex.IsMatch(e.Text, @"^[0-9]+(\s[a-zA-Z]+)+$")) 
            {
                e.Handled = true;
            }
            else
            {
                e.Handled = false;
            }
        }

         private void txtDireccion_KeyDown(object sender, KeyEventArgs e)
         {

         }


        private void telf1_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (Regex.IsMatch(e.Text, "[0-9]"))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtTelf1_KeyDown (object sender, KeyEventArgs e)
        {
            if (((e.Key >= Key.D0 && e.Key <= Key.D9) || (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9) || e.Key == Key.Back || e.Key == Key.Tab))
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void telf2_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            //Validaciones para que acepte solo numeros
            if (Regex.IsMatch(e.Text, "[0-9]"))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }
        private void txtTelf2_KeyDown(object sender, KeyEventArgs e)
        {
            if (((e.Key >= Key.D0 && e.Key <= Key.D9) || (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9) || e.Key == Key.Back || e.Key == Key.Tab))
                e.Handled = false;
            else
                e.Handled = true;
        }
        private void email_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            //Validaciones para textbox de solo letras

            String theEmailPattern = @"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*"
                                   + "@"
                                   + @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$";

            if (Regex.IsMatch(correo.Text, theEmailPattern))
                e.Handled = true;
            else 
                e.Handled = false;
            
            
            //if (Regex.IsMatch(e.Text, @"/[-0-9a-zA-Z.+_]+@[-0-9a-zA-Z.+_]+\.[a-zA-Z]{2,4}/"))
            //{
            //    e.Handled = true;
            //}
            //else
            //{
            //    e.Handled = false;
            //}

           // e.Handled = Regex.IsMatch(correo.Text, "/[-0-9a-zA-Z.+_]+@[-0-9a-zA-Z.+_]+\.[a-zA-Z]{2,4}/");

        }

        private void txtEmail_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Space)
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void sueldo_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if ((Regex.IsMatch(sueldo.Text, @"^[0-9]+(\.{0}[0-9]+)?$")) && (e.Text != ",") || (Regex.IsMatch(e.Text, "[0-9]")))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
          

        }
        private void txtSueldo_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.Key != Key.Space)
                e.Handled = false;
            else
                e.Handled = true;

        }

         private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (this.Owner != null)//O sea que proviene de un padre
            {
                try
                {
                    MV_DevolucionesWindow padre = this.Owner as MV_DevolucionesWindow;
                    MR_AdministrarPersonalViewModel my_DataContext = this.main.DataContext as MR_AdministrarPersonalViewModel;
                    MV_DevolucionesViewModel padre_DataContext = padre.main.DataContext as MV_DevolucionesViewModel;

                    padre_DataContext.nombreVendedor = my_DataContext.empleado.nombreCompleto;
                    padre_DataContext.searchVendedor = my_DataContext.empleado.dni;

                    this.Close();
                }
                catch
                {
                    try
                    {
                        MV_AdministrarVentasWindow padre = this.Owner as MV_AdministrarVentasWindow;
                        MR_AdministrarPersonalViewModel my_DataContext = this.main.DataContext as MR_AdministrarPersonalViewModel;
                        MV_VentasViewModel padre_DataContext = padre.main.DataContext as MV_VentasViewModel;

                        padre_DataContext.nombreVendedor = my_DataContext.empleado.nombreCompleto;
                        padre_DataContext.searchVendedor = my_DataContext.empleado.dni;

                        this.Close();
                    }
                    catch
                    {
                        try
                        {
                            MV_AdministrarNotaCreditoWindow padre = this.Owner as MV_AdministrarNotaCreditoWindow;
                            MR_AdministrarPersonalViewModel my_DataContext = this.main.DataContext as MR_AdministrarPersonalViewModel;
                            MV_NotaCreditoViewModel padre_DataContext = padre.main.DataContext as MV_NotaCreditoViewModel;

                            padre_DataContext.nombreVendedor = my_DataContext.empleado.nombreCompleto;
                            padre_DataContext.searchVendedor = my_DataContext.empleado.dni;

                            this.Close();
                        }
                        catch
                        { }
                    }
                }
            }
    }
}
    }