using System;
using System.Net;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.ComponentModel;

using pe.edu.pucp.ferretin.controller;
using pe.edu.pucp.ferretin.model;
using pe.edu.pucp.ferretin.controller.MSeguridad;
using pe.edu.pucp.ferretin.viewmodel.MSeguridad;
using System.Net.NetworkInformation;
using System.Management;

namespace pe.edu.pucp.ferretin.view.MSeguridad
{
    /************************************************************************************************************/
    /// <summary>
    /// Lógica de interacción para MS_AdministrarUsuarios.xaml
    /// </summary>
    public partial class MS_AdministrarUsuarios : Window
    {
        public MS_AdministrarUsuarios()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        //private void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    //IPHostEntry hostEntry = Dns.GetHostEntry(Dns.GetHostName()); 
        //    //IPAddress[] address = hostEntry.AddressList; 
        //    ////textBox1.Text = address.GetValue(1).ToString(); 
        //    //MessageBox.Show(address.Single().MapToIPv4().ToString());
        //    try
        //    {
        //        IPAddress[] a = Dns.GetHostByName(Dns.GetHostName()).AddressList;
        //        string ip = a[0].ToString();
        //        //MessageBox.Show(ip);

        //        NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
        //        String sMacAddress = string.Empty;
        //        foreach (NetworkInterface adapter in nics)
        //        {
        //            if (sMacAddress == String.Empty)// only return MAC Address from first card  
        //            {
        //                IPInterfaceProperties properties = adapter.GetIPProperties();
        //                sMacAddress = adapter.GetPhysicalAddress().ToString();
        //                sMacAddress = string.Join(":", (from z in adapter.GetPhysicalAddress().GetAddressBytes() select z.ToString("X2")).ToArray());                        
        //            }                    
        //        }           

        //        MessageBox.Show(sMacAddress);
        //    }
        //    catch (Exception f)
        //    {
        //        MessageBox.Show(f.Message);
        //    }

 

        //} 
    }
    
}


