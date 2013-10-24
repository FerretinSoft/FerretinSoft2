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

namespace pe.edu.pucp.ferretin.view.MSeguridad
{
    /// <summary>
    /// Lógica de interacción para MS_ParametrosWindow.xaml
    /// </summary>
    public partial class MS_ParametrosWindow : Window
    {
        List<Parametro> listaParametros;

        public MS_ParametrosWindow()
        {
            InitializeComponent();

            listaParametros = MS_ParametroService.obtenerListaParametros().ToList();

            intContrasena.Text = listaParametros[0].valor.ToString();
            tMaxSesion.Text = listaParametros[1].valor.ToString();
            durClave.Text = listaParametros[2].valor.ToString();
            tipCambio.Text = listaParametros[3].valor.ToString();
            igv.Text = listaParametros[4].valor.ToString();

            vigProforma.Text = listaParametros[5].valor.ToString();
            vigNotaCredito.Text = listaParametros[6].valor.ToString();
            solesPunto.Text = listaParametros[7].valor.ToString();
            
        }

        
        
    }
}
