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
    /// Lógica de interacción para MV_AgregarNotaCreditoWindow.xaml
    /// </summary>
    public partial class MV_AgregarNotaCreditoWindow : Window
    {
        public viewmodel.MVentas.MV_PagoWindowViewModel pagoviewmodel;

        public MV_AgregarNotaCreditoWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            pagoviewmodel.notaCreditoSeleccionado = null;
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            pagoviewmodel.notaCreditoSeleccionado = notaCreditoSeleccionado;
            this.Close();
        }
        private NotaCredito notaCreditoSeleccionado = null;
        IEnumerable<NotaCredito> listaNotaCreditos = null;

        private void codigoNotaCredito_LostFocus(object sender, RoutedEventArgs e)
        {
            if (listaNotaCreditos == null) listaNotaCreditos = MV_NotaCreditoService.listaNotasCredito.ToList<NotaCredito>();
            if (listaNotaCreditos.Count(v => v.codigo.Equals(codigoNotaCredito.Text)) > 0)
            {

                NotaCredito notaCredito = listaNotaCreditos.First(v => v.codigo.Equals(codigoNotaCredito.Text));
                if (notaCredito.estado == 0)//TODO estado 0 es activo?
                {
                    notaCreditoSeleccionado = notaCredito;
                    //TODO Si la nota de credito solo es en soles o podría ser en dolares
                    valorNotaCredito.Text = notaCreditoSeleccionado.importe.ToString() + " Soles";
                    errorMensaje.Text = "";
                    aceptar.IsEnabled = true;
                }
                else
                {
                    notaCreditoSeleccionado = null;
                    valorNotaCredito.Text = "";
                    errorMensaje.Text = "Este Nota de Crédito ya fue utilizado";
                    aceptar.IsEnabled = false;
                }
            }
            else
            {
                valorNotaCredito.Text = "";
                notaCreditoSeleccionado = null;
                if (codigoNotaCredito.Text.Length >= 8) errorMensaje.Text = "Código de Nota de Crédito no existe";
                aceptar.IsEnabled = false;
            }
        }
    }
}
