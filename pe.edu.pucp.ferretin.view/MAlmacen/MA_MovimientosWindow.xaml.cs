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
using System.ComponentModel;
using pe.edu.pucp.ferretin.model;
using pe.edu.pucp.ferretin.controller;
using System.Threading;
using System.Windows.Threading;
using pe.edu.pucp.ferretin.controller.MSeguridad;
using pe.edu.pucp.ferretin.controller.MAlmacen;
using pe.edu.pucp.ferretin.viewmodel;
using pe.edu.pucp.ferretin.viewmodel.MAlmacen;

namespace pe.edu.pucp.ferretin.view.MAlmacen
{
    /// <summary>
    /// Lógica de interacción para MA_MovimientosWindow.xaml
    /// </summary>
    public partial class MA_MovimientosWindow : Window
    {
        
        public MA_MovimientosWindow()
        {
            InitializeComponent();
        }

        private void DataGrid_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            if ((string)e.Column.Header != "Estado")
            {
                // If this is the new row, entity is not yet attached to the context.
                if (((MovimientoTipo)e.Row.Item).id >0)
                {
                    e.Cancel = true;
                }
            }
        }

        private void visualizarDocumentoBtn_Click(object sender, RoutedEventArgs e)
        {
            MA_DocumentoMovimiento w = new MA_DocumentoMovimiento();
            MA_MovimientosViewModel movVM = this.main.DataContext as MA_MovimientosViewModel;
            MA_DocumentoMovimientoViewModel docVM = w.main.DataContext as MA_DocumentoMovimientoViewModel;
            docVM.movimiento = movVM.movimiento;
            w.Show();
        }        


        
    }
}
