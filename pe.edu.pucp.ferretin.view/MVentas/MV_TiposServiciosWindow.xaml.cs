using pe.edu.pucp.ferretin.model;
using pe.edu.pucp.ferretin.viewmodel.MVentas;
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
        private MV_ServiciosWindow mV_ServiciosWindow;

        public MV_TiposServiciosWindow()
        {
            InitializeComponent();
            tiposServicio.ItemsSource = midb.ServicioTipo;
        }

        public MV_TiposServiciosWindow(MV_ServiciosWindow mV_ServiciosWindow)
        {
            InitializeComponent();
            tiposServicio.ItemsSource = midb.ServicioTipo;
            guardarBtn.Content = "SELECCIONAR";
            this.mV_ServiciosWindow = mV_ServiciosWindow;
            tiposServicio.IsReadOnly = true;
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
        private void guardarBtn_Click(object sender, RoutedEventArgs e)
        {
            //Viene del buscador de servicios
            if (this.Owner!=null && mV_ServiciosWindow != null)
            {
                var vmServ = mV_ServiciosWindow.DataContext as MV_ServiciosViewModel;
                var selected = tiposServicio.SelectedItems;
                if (selected.Count > 0)
                {
                    foreach (ServicioTipo servT in selected)
                    {
                        vmServ.codServTipoAgregar = servT.codigo;
                        vmServ.agregarServicioTipo(null);
                    }
                    vmServ.codServTipoAgregar = "";

                    this.Close();
                }
                else
                {
                    MessageBox.Show("Debe Seleccionar al menos un tipo de servicio para agregar");
                }
            }
            else
            {
                string messageBoxText = "¿Desea confirmar la transacción? Se procederá a almacenar la información ingresada";
                string caption = "Mensaje de confirmación";
                MessageBoxButton button = MessageBoxButton.OKCancel;
                MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button);
                if (result == MessageBoxResult.OK)
                {
                    midb.SubmitChanges();
                    this.Close();
                }
            }
        }
    }
}
