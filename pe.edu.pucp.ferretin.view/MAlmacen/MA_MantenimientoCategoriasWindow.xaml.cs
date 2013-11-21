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
using pe.edu.pucp.ferretin.controller.MAlmacen;
using pe.edu.pucp.ferretin.controller;

namespace pe.edu.pucp.ferretin.view.MAlmacen
{
    /// <summary>
    /// Lógica de interacción para MA_MantenimientoCategoriasWindow.xaml
    /// </summary>
    public partial class MA_MantenimientoCategoriasWindow : Window
    {
        public MA_MantenimientoCategoriasWindow()
        {
            InitializeComponent();
        }

        private void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (((e.Key >= Key.A && e.Key <= Key.Z) || e.Key == Key.Back || e.Key == Key.Tab || (e.Key == Key.Space)))
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (txtNombreCategoria.Text != "" || txtNombreCategoria != null)
            {
                ComunService.idVentana(19);
                Categoria cat = new Categoria();
                cat.nombre = txtNombreCategoria.Text;
                cat.id_padre = null;
                cat.descripcion = txtNombreCategoria.Text;
                cat.nivel = 1;
                if (!MA_CategoriaService.insertarCategoria(cat))
                {
                    MessageBox.Show("No se pudo agregar");
                }
                else {
                    MessageBox.Show("Se agrego con exito");
                }
            }
            else {

                MessageBox.Show("Ingresar los campos");
            }
            
        }

    }
}
