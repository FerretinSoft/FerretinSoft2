using pe.edu.pucp.ferretin.controller.MVentas;
using pe.edu.pucp.ferretin.model;
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
    /// Lógica de interacción para MV_AgregarValeWindow.xaml
    /// </summary>
    public partial class MV_AgregarValeWindow : Window
    {
        public viewmodel.MVentas.MV_PagoWindowViewModel pagoviewmodel;

        public MV_AgregarValeWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            pagoviewmodel.valeSeleccionado = null;
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            pagoviewmodel.valeSeleccionado = valeSeleccionado;
            this.Close();
        }
        private Vale valeSeleccionado = null;
        IEnumerable<Vale> listaVales = null;

        private void codigoVale_LostFocus(object sender, RoutedEventArgs e)
        {
            if (listaVales == null) listaVales = MV_ValeService.listaVales.ToList<Vale>();
            if (listaVales.Count(v => v.codigo.Equals(codigoVale.Text)) > 0)
            {

                Vale vale = listaVales.First(v => v.codigo.Equals(codigoVale.Text));
                if (vale.estado == 0)
                {
                    valeSeleccionado = vale;
                    valorVale.Text = valeSeleccionado.LoteVale.monto.ToString() + " " + valeSeleccionado.LoteVale.monedaString;
                    errorMensaje.Text = "";
                    aceptar.IsEnabled = true;
                }
                else
                {
                    valeSeleccionado = null;
                    valorVale.Text = "";
                    errorMensaje.Text = "Este vale ya fue utilizado";
                    aceptar.IsEnabled = false;
                }
            }
            else
            {
                valorVale.Text = "";
                valeSeleccionado = null;
                if (codigoVale.Text.Length >= 8) errorMensaje.Text = "Código de vale no existe";
                aceptar.IsEnabled = false;
            }
        }

    }
}
