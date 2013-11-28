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
    /// Lógica de interacción para MV_TiposServiciosWindow.xaml
    /// </summary>
    public partial class MV_TiposServiciosWindow : Window
    {

        FerretinDataContext midb = new FerretinDataContext();

        public MV_TiposServiciosWindow()
        {
            InitializeComponent();
            tiposServicio.ItemsSource = midb.ServicioTipo;
        }



        /// <summary>
        /// Cerrar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Seleccionar o guardar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
