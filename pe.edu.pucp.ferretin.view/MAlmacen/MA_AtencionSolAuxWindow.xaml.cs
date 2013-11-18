using pe.edu.pucp.ferretin.viewmodel.MAlmacen;
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

namespace pe.edu.pucp.ferretin.view.MAlmacen
{
    /// <summary>
    /// Lógica de interacción para MA_AtencionSolAuxWindow.xaml
    /// </summary>
    public partial class MA_AtencionSolAuxWindow : Window
    {
        public MA_AtencionSolAuxWindow()
        {
            InitializeComponent();
        }

        private void aceptarBtn_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Esta seguro que desea atender la solicitud?", "Confirmación de Atención", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                MA_AtencionSolAbastecimientoWindow parent = this.Owner as MA_AtencionSolAbastecimientoWindow;
                MA_AtencionSolAbastecimientoViewModel parentVM = parent.main.DataContext as MA_AtencionSolAbastecimientoViewModel;
                MA_AtencionSolAuxViewModel thisVM = this.main.DataContext as MA_AtencionSolAuxViewModel;
                parentVM.listaAtencion = thisVM.listadoProductos;
                this.Close();
            }
            else
            {
                
            }
        }
    }
}
