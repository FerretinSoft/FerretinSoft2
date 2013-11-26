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
using pe.edu.pucp.ferretin.model;
using pe.edu.pucp.ferretin.viewmodel.MCompras;

namespace pe.edu.pucp.ferretin.view.MCompras
{
    /// <summary>
    /// Interaction logic for MC_BuscadorCotizacionesWindow.xaml
    /// </summary>
    public partial class MC_BuscadorCotizacionesWindow : Window
    {
        public MC_BuscadorCotizacionesWindow(Window padre)
        {
            //dpfechaDesde.SelectedDate = DateTime.Now;
            //dpfechaHasta.SelectedDate = DateTime.Now;
            this.Owner = padre;
            InitializeComponent();
            var myDC = DataContext as MC_BuscadorCotizacionesViewModel;
            myDC.buscarCotizaciones(null);
            this.Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            bool exito = false; ;
            if (this.Owner is MC_AdministrarGuiaRemiWindow)
            {
                DocumentoCompra aux = null;
                var vmpadre = this.Owner.DataContext as MC_GuiaRemisionViewModel;
                var seleccionados = listaCotizaciones.SelectedItems;
                foreach (DocumentoCompra seleccionado in seleccionados)
                {
                    aux = seleccionado;
                    break;
                }

                if (aux.id_estado != 7)
                {
                    exito = false;
                    MessageBox.Show("La Orden de Compra no se encuentra Facturada", "Orden de Compra", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
                else
                {
                    decimal? restante = 0;
                    int cont = aux.DocumentoCompraProducto.Count();
                    for (int i = 0; i < cont; i++)
                        restante = restante + aux.DocumentoCompraProducto[i].cantidadRestante;

                    if (restante == 0)
                    {
                        exito = false;
                        MessageBox.Show("La Orden de Compra ya fue recibida en su totalidad", "Orden de Compra", MessageBoxButton.OK, MessageBoxImage.Exclamation);                       
                    }
                    else
                    {
                        vmpadre.guiaRemision.DocumentoCompra = aux;
                        cont = aux.DocumentoCompraProducto.Count();

                        for (int j = 0; j < cont; j++)
                        {
                            GuiaRemisionProducto guiaLinea = new GuiaRemisionProducto() { id_guia_detalle = aux.DocumentoCompraProducto[j].id, cantidadRecibida = 0, DocumentoCompraProducto = aux.DocumentoCompraProducto[j], GuiaRemision = vmpadre.guiaRemision };
                            vmpadre.guiaRemision.GuiaRemisionProducto.Add(guiaLinea);
                        }
                        vmpadre.ordenCompraCod = aux.codigo;
                        vmpadre.refrescarGuia();
                        exito = true;
                    }
                        
                }
            }
            if (exito)
            {
                this.Owner.Focus();
                this.Close();
            }           
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
