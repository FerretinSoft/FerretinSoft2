using System.Windows;
using pe.edu.pucp.ferretin.controller;

namespace pe.edu.pucp.ferretin.view.MCompras
{
    /// <summary>
    /// Interaction logic for MC_AtenderSolicitudWindow.xaml
    /// </summary>
    public partial class MC_AtenderSolicitudWindow : Window
    {
        public MC_AtenderSolicitudWindow()
        {
            InitializeComponent();
        }

        private void consolidarBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void solAbs_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ComunService.Clean();
        }
    }
}
