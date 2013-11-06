using pe.edu.pucp.ferretin.controller.MAlmacen;
using pe.edu.pucp.ferretin.model;
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
    /// Lógica de interacción para MA_ColoresProductosWindow.xaml
    /// </summary>
    public partial class MA_ColoresProductosWindow : Window
    {
        Int16 codigo { get; set; }
        Int16 idColor { get; set; }

        public MA_ColoresProductosWindow(Int16 cod)
        {   
            InitializeComponent();
            codigo = cod;
        }

        private void btnAddColor_Click(object sender, RoutedEventArgs e)
        {
            ProductoColor pc=new ProductoColor();
            pc.id_producto=codigo;
            pc.id_color = (Int16)cmbColores.SelectedValue;
            bool res=MA_ProductoService.agregarColorProducto(pc);

            if (res)
                MessageBox.Show("El color ha sido agregado con éxito");
            else
                MessageBox.Show("El color asignado ya existe para este producto");
        }


    }
}
