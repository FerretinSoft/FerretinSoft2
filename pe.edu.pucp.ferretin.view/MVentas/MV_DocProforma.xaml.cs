using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Packaging;
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
using System.Windows.Xps;
using System.Windows.Xps.Packaging;

namespace pe.edu.pucp.ferretin.view.MVentas
{
    /// <summary>
    /// Lógica de interacción para MV_DocProforma.xaml
    /// </summary>
    public partial class MV_DocProforma : Window
    {
        public MV_DocProforma()
        {
            InitializeComponent();
        }
        public void imprimir(){
            this.Show();
            PrintDialog printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == true)
            {
                printDialog.PrintVisual(this, "Proforma");
                this.Close();
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {

            

            
        }
    }
}
