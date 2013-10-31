using pe.edu.pucp.ferretin.model;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Net;
using System.Net.NetworkInformation;

namespace pe.edu.pucp.ferretin.controller
{
    /// <summary>
    /// Esta es la Clase Base de Servicio, la cual contiene metodos comunes para el manejo
    /// de la Base de Datos
    /// </summary>
    public class BaseService
    {
        /// <summary>
        /// Usado internamente para hacer referencia al DataContext de la BD
        /// </summary>
        /// <remarks>
        /// Es usado para no crear la conexion a la Base de datos Mientras no se necesite
        /// </remarks>
        private static FerretinDataContext _db = null;
        
        /// <summary>
        /// Devuelve un DataContext para el manejo de la Base de Datos
        /// </summary>
        /// <remarks>
        /// Si aún no se ha creado la conexión, la crea.
        /// </remarks>
        /// <exception cref="Exception">No se puede crear la conexión</exception>
        public static FerretinDataContext db
        {
            get
            {
                if (_db == null)
                {
                    _db = new FerretinDataContext();
                }
                return _db;
            }
        }

        public static void refresh()
        {
            db.Refresh(RefreshMode.OverwriteCurrentValues);
        }

        /// <summary>
        /// Envia a la Base de Datos todos los cambios realizados en los objetos recuperados
        /// </summary>
        /// <returns>True si se guardo correctamente, False en caso hubo algun error</returns>
        public static bool enviarCambios()
        {
            try
            {
                db.SubmitChanges(ConflictMode.ContinueOnConflict);
                /***********************************************/
                try
                {
                    Transaccion transaccion = new Transaccion();
                    transaccion.nroIP = obtenerIp();
                    transaccion.nroMAC = obtenerMac();
                    DateTime today = DateTime.Today;
                    transaccion.fecha = today;
                    db.Transaccion.InsertOnSubmit(transaccion);
                    return true;
                }
                catch (ChangeConflictException ex)
                {
                    MessageBox.Show(ex.Message);
                    return false;
                }              

            }
            catch (ChangeConflictException e)
            {
                db.ChangeConflicts.ResolveAll(RefreshMode.KeepCurrentValues);  
                try
                {
                    db.SubmitChanges(ConflictMode.ContinueOnConflict);
                    return true;
                }
                catch (ChangeConflictException ex)
                {
                    MessageBox.Show(ex.Message);
                    return false;
                }
            }
        }
        /**********************************************************************/
        public static string obtenerIp()
        {
            IPAddress[] a = Dns.GetHostByName(Dns.GetHostName()).AddressList;
            string ip = a[0].ToString();            
            return ip;
        }
        /**********************************************************************/
        public static string obtenerMac()
        {
            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
            String sMacAddress = string.Empty;
            foreach (NetworkInterface adapter in nics)
            {
                if (sMacAddress == String.Empty)// only return MAC Address from first card  
                {
                    IPInterfaceProperties properties = adapter.GetIPProperties();
                    sMacAddress = adapter.GetPhysicalAddress().ToString();
                    sMacAddress = string.Join(":", (from z in adapter.GetPhysicalAddress().GetAddressBytes() select z.ToString("X2")).ToArray());
                }
            }
            string mac = sMacAddress.ToString();
            MessageBox.Show(mac);
            return mac;
        }

    }
}
