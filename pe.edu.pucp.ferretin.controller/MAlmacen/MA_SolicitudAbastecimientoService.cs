using pe.edu.pucp.ferretin.model;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pe.edu.pucp.ferretin.controller.MAlmacen
{
    public class MA_SolicitudAbastecimientoService : MA_ComunService
    {

        public class ProductoPorSolicitudTienda
        {
            public ProductoPorSolicitudTienda(SolicitudAbastecimientoProducto productoPorSolicitud, ProductoAlmacen productoPorAlmacen)
            {
                this.productoPorAlmacen = productoPorAlmacen;
                this.productoPorSolicitud = productoPorSolicitud;
            }

            public SolicitudAbastecimientoProducto productoPorSolicitud { get; set; }
            public ProductoAlmacen productoPorAlmacen { get; set; }
        }
        
        public static IEnumerable<SolicitudAbastecimiento> _listaSolicitudes;
        public static IEnumerable<SolicitudAbastecimiento> listaSolicitudes
        {
            get
            {
                if (_listaSolicitudes == null)
                {
                    _listaSolicitudes = db.SolicitudAbastecimiento;
                }
                //Usando concurrencia pesimista:
                ///La lista de clientes se actualizara para ver los cambios
                ///Si quisiera usar concurrencia optimista quito la siguiente linea
                db.Refresh(RefreshMode.OverwriteCurrentValues, _listaSolicitudes);
                return _listaSolicitudes;
            }
            set
            {
                _listaSolicitudes = value;
            }
        }


        public static SolicitudAbastecimiento obtenerSolicitudByCodigo(String codigo)
        {
            IEnumerable<SolicitudAbastecimiento> solicitudes = (from s in listaSolicitudes
                                           where s.codigo != null && s.codigo.Equals(codigo)
                                           select s);
            if (solicitudes.Count() > 0)
                return solicitudes.First();
            else
                return null;
        }

        public static void actualizarSolicitud(SolicitudAbastecimiento solicitud)
        {
            db.SubmitChanges();
        }

        public static IEnumerable<SolicitudAbastecimiento> buscar(Tienda tienda, SolicitudAbastecimientoEstado estado, DateTime fechaDesde, DateTime fechaHasta)
        {
            return listaSolicitudes
                .Where(m => (tienda == null) || m.Tienda == tienda)
                .Where(m => (estado == null) || (estado.id <= 0) || (m.SolicitudAbastecimientoEstado == estado))
                .Where(m => (m.fecha >= fechaDesde) && (m.fecha <= fechaHasta))
                .OrderBy(m => m.fecha);
            
        }

        public static IEnumerable<SolicitudAbastecimiento> buscar(Tienda almacen, Tienda tienda, SolicitudAbastecimientoEstado estado, DateTime fechaDesde, DateTime fechaHasta)
        {
            return listaSolicitudes
                .Where(m => (m.Tienda != null && m.Tienda.Tienda1 == almacen)) //obtener las solicitudes de todas las tiendas a las que abastezco
                .Where(m => (tienda == null) || m.Tienda == tienda)
                .Where(m => (estado == null) || (estado.id <= 0) || (m.SolicitudAbastecimientoEstado == estado))
                .Where(m => (m.fecha >= fechaDesde) && (m.fecha <= fechaHasta))
                .OrderBy(m => m.fecha);

        }

        public static IEnumerable<ProductoPorSolicitudTienda> buscarProductosPorSolicitud(Tienda almacen, SolicitudAbastecimiento solicitud)
        {
            List<ProductoPorSolicitudTienda> result = new List<ProductoPorSolicitudTienda>();
            if (solicitud == null) return result;
            for (int i = 0; i < solicitud.SolicitudAbastecimientoProducto.Count; i++)
            {
                ProductoAlmacen pa = MA_ProductoAlmacenService.ObtenerProductoAlmacenPorTiendaProducto(almacen, 
                                                                    solicitud.SolicitudAbastecimientoProducto[i].Producto);
                result.Add(new ProductoPorSolicitudTienda(solicitud.SolicitudAbastecimientoProducto[i], pa));
            }
            
            return result;
        }        

        /*public static bool atenderSolicitud(Tienda almacen, SolicitudAbastecimiento solicitud)
        {
            var productos = (from prodAlmacen in db.ProductoAlmacen
                            where prodAlmacen.Tienda == almacen
                            select prodAlmacen);
            for (int i = 0; i < solicitud.SolicitudAbastecimientoProducto.Count; i++)
            {
                var stock = (from prod in productos
                             where prod.Producto == solicitud.SolicitudAbastecimientoProducto[i].Producto
                             select prod.stock).First();
                if ((decimal)stock < solicitud.SolicitudAbastecimientoProducto[i].cantidad) return false;
            }
            return true;
        }*/        

        public static bool insertarSolicitud(SolicitudAbastecimiento solicitud)
        {
            if (!db.SolicitudAbastecimiento.Contains(solicitud))
            {
                //if (!db.TiendaHorario.Equals(almacen.TiendaHorario))
                //    db.TiendaHorario.InsertAllOnSubmit(almacen.tiendasH);

                if (solicitud.id <= 0) db.SolicitudAbastecimiento.InsertOnSubmit(solicitud);
                return enviarCambios();
            }
            else
            {
                return false;
            }
        }

        public static bool validarAtencionSolicitud(Tienda proveedor, SolicitudAbastecimiento solicitud)
        {
            var productos = (from prodAlmacen in db.ProductoAlmacen
                             where prodAlmacen.Tienda == proveedor
                             select prodAlmacen);
            for (int i = 0; i < solicitud.SolicitudAbastecimientoProducto.Count; i++)
            {
                var stock = (from prod in productos
                             where prod.Producto == solicitud.SolicitudAbastecimientoProducto[i].Producto
                             select prod.stock).First();
                if ((decimal)stock < solicitud.SolicitudAbastecimientoProducto[i].cantidad) return false;
            }
            return true;
        }

        public static bool atenderSolicitud(Tienda proveedor, SolicitudAbastecimiento solicitud)
        {
            String errores = registrarTransferenciaAbastecimiento(proveedor, solicitud.Tienda, solicitud.SolicitudAbastecimientoProducto);
            return (errores == "") ? true : false;
        }
    
    }
}
