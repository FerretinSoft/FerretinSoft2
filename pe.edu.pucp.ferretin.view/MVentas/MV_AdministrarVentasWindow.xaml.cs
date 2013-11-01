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
using pe.edu.pucp.ferretin.controller.MVentas;
using pe.edu.pucp.ferretin.model;
using pe.edu.pucp.ferretin.viewmodel.MVentas;

namespace pe.edu.pucp.ferretin.view.MVentas
{
    /// <summary>
    /// Lógica de interacción para MV_AdministrarVentasWindow.xaml
    /// </summary>
    public partial class MV_AdministrarVentasWindow : Window
    {
        

        public int selectedCodVenta = 0;

        public MV_AdministrarVentasWindow()
        {
            InitializeComponent();
            rowSelectVentaLista.Height = new GridLength(0);
            rowSelectVentaDetalle.Height = new GridLength(0);
            
        }

        


        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            Cliente cliente = MV_ClienteService.obtenerClienteByNroDoc(searchNroDoc.Text);
            if (cliente != null)
                nombreCliente.Text = cliente.nombreCompleto;
            else
                nombreCliente.Text = "";
        }


    }
}
