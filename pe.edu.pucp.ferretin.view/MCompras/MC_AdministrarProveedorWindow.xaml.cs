using pe.edu.pucp.ferretin.view.MVentas;
using pe.edu.pucp.ferretin.viewmodel.MCompras;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace pe.edu.pucp.ferretin.view.MCompras
{
    /// <summary>
    /// Interaction logic for MC_AdministrarProveedorWindow.xaml
    /// </summary>
    public partial class MC_AdministrarProveedorWindow : Window
    {
        public MC_AdministrarProveedorWindow()
        {
            InitializeComponent();
        }

        #region validación de campos

        private void cod_PreviewTextInput(object sender, TextCompositionEventArgs e)
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
        private void cod_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            //Validaciones para que acepte solo numeros
            if (((e.Key >= Key.D0 && e.Key <= Key.D9) || (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9) || e.Key == Key.Back || e.Key == Key.Tab))
                e.Handled = false;
            else
                e.Handled = true;
        }
        #endregion

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //  var vm = DataContext as MV_RegistrarVentaViewModel;
           // var buscador = new MV_BuscadorProductos(this, vm.usuarioLogueado.Empleado.tiendaActual);

            var vm = DataContext as MC_ProveedoresViewModel;
            var buscador = new MV_BuscadorProductos(this, vm.usuarioLogueado.Empleado.tiendaActual);
        }
        /****************************************************/
        private void codigoProveedor_PreviewTextInput(object sender, TextCompositionEventArgs e)
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
        /****************************************************/
        private void razonSocialProveedor_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (Regex.IsMatch(e.Text, "[a-zA-Z0-9]"))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }
        /****************************************************/
        private void codigoProveedor_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Space)
                e.Handled = false;
            else
                e.Handled = true;
        }
        /****************************************************/
        private void razonSocialProveedor_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Space)
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
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

        private void nombre_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (Regex.IsMatch(e.Text, "[a-zA-Z.]"))
             {
                 e.Handled = false;
             }
             else
             {
                 e.Handled = true;
             }
        }

        private void TextBox_PreviewTextInput_1(object sender, TextCompositionEventArgs e)
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

        private void nombreCont_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (Regex.IsMatch(e.Text, "[a-zA-Z]"))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void telfCont_PreviewTextInput(object sender, TextCompositionEventArgs e)
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

        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            if (this.Owner != null)
            {
                try
                {
                    MC_ProveedoresViewModel my_DataContext = this.DataContext as MC_ProveedoresViewModel;

                    if (Owner is MC_AdministrarOCCotizacionWindow)
                    {
                        MC_AdministrarOCCotizacionWindow padre = Owner as MC_AdministrarOCCotizacionWindow;
                        if (my_DataContext.soloSeleccionarProveedor)
                        {
                            MC_CotizacionesOCViewModel padre_DataContext = padre.DataContext as MC_CotizacionesOCViewModel;
                            padre_DataContext.proveedorNombre = my_DataContext.proveedor.razonSoc;
                            this.Close();
                        }
                    }
                }
                catch
                {
                }
            }
        }
    }
}
