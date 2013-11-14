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

    }
}
