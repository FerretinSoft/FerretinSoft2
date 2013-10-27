using pe.edu.pucp.ferretin.model;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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
                return true;
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

    }
}
