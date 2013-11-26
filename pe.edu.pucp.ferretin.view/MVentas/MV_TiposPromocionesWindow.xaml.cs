using pe.edu.pucp.ferretin.controller;
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
    /// Lógica de interacción para MV_TiposPromocionesWindow.xaml
    /// </summary>
    public partial class MV_TiposPromocionesWindow : Window
    {
        FerretinDataContext midb = new FerretinDataContext();

        public MV_TiposPromocionesWindow()
        {
            InitializeComponent();
            this.tiposPromocion.ItemsSource = midb.PromocionTipo;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string messageBoxText = "¿Desea confirmar la transacción? Se procederá a almacenar la información ingresada";
                string caption = "Mensaje de confirmación";
                MessageBoxButton button = MessageBoxButton.OKCancel;
                MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button);
                if(result==MessageBoxResult.OK)
                {
                    midb.SubmitChanges();
                    this.Close();
                }
        }
    }
}
