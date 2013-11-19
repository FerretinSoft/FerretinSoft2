using System;
using System.Collections.Generic;
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
using pe.edu.pucp.ferretin.model;

namespace pe.edu.pucp.ferretin.view.MVentas
{
    /// <summary>
    /// Lógica de interacción para MV_AdministrarPrecioProductos.xaml
    /// </summary>
    public partial class MV_AdministrarPrecioProductos : Window
    {

    

        public MV_AdministrarPrecioProductos()
        {
            InitializeComponent();
        }

        private void validarPrecioLista(object sender, TextCompositionEventArgs e)
        {
            if ((Regex.IsMatch(PrecioListaText.Text, @"^[0-9]+(\.{0}[0-9]+)?$")) && (e.Text != ",") || (Regex.IsMatch(e.Text, "[0-9]")))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void validarPrecioLista_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Space)
                e.Handled = false;
            else
                e.Handled = true;
        }


        private void validarPrecioPuntos(object sender, TextCompositionEventArgs e)
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

        private void validarPrecioPuntos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Space)
                e.Handled = false;
            else
                e.Handled = true;
        }
    }
}
