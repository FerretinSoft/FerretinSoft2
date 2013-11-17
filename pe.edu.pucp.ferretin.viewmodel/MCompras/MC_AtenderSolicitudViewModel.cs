using pe.edu.pucp.ferretin.controller;
using pe.edu.pucp.ferretin.controller.MAlmacen;
using pe.edu.pucp.ferretin.controller.MCompras;
using pe.edu.pucp.ferretin.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pe.edu.pucp.ferretin.viewmodel.Helper;
using System.Windows.Input;
using pe.edu.pucp.ferretin.controller.MSeguridad;
using System.Windows;

namespace pe.edu.pucp.ferretin.viewmodel.MCompras
{
    public class MC_AtenderSolicitudViewModel : ViewModelBase
    {
        #region lista Productos de la  Solicitud


        private IEnumerable<SolicitudCompra> _listaProductosSol;
        public IEnumerable<SolicitudCompra> listaProductosSol
        {
            get
            {
                
                var usuario = ComunService.usuarioL;
                var tienda = usuario.Empleado.tiendaActual;
                Dictionary<ProductoAlmacen,decimal> diccionario = MA_SharedService.obtenerProductosPorAbastecer(tienda);

                //Productos atentidos pendientes
                var _listaProductosPendientes = from sc in MC_ComunService.db.SolicitudCompra where sc.estado == 1 select sc;

                //Productos vistos pero no atendidos
                _listaProductosSol = from sc in MC_ComunService.db.SolicitudCompra where sc.estado == 0 select sc;

                bool huboCambio = false;
                foreach (var entry in diccionario)
                {
                    if (!_listaProductosSol.Any(p => (p.Producto.id == entry.Key.Producto.id) ) && 
                        !_listaProductosPendientes.Any(p => (p.Producto.id == entry.Key.Producto.id) ) )
                    {
                        var nuevo = new SolicitudCompra()
                        {
                            cantidad = (int)entry.Value,
                            estado = 0,
                            Producto = entry.Key.Producto,
                            Tienda = entry.Key.Tienda,
                        };
                        MC_ComunService.db.SolicitudCompra.InsertOnSubmit(nuevo);
                        huboCambio = true;
                    }
                }
                if(huboCambio)
                    MC_ComunService.db.SubmitChanges(System.Data.Linq.ConflictMode.ContinueOnConflict);

                int i;
                for (i = 0; i < _listaProductosSol.Count(); i++)
                {
                    _listaProductosSol.ElementAt(i).posiProveedor = MC_ProveedorService.obtenerPosiblesProveedores(_listaProductosSol.ElementAt(i).Producto);
                }
                
                return _listaProductosSol;
            }
            set
            {
                _listaProductosSol = value;
                NotifyPropertyChanged("listaProductosSol");
            }
        }

        #endregion

        RelayCommand _generarOCSCommand;
        public ICommand generarOCSCommand
        {
            get
            {
                if (_generarOCSCommand == null)
                {
                    _generarOCSCommand = new RelayCommand(generarOCS);
                    NotifyPropertyChanged("productoSol");
                }
                return _generarOCSCommand;

            }
        }
        
        public void generarOCS(Object id)
        {

            var seleccionados = _listaProductosSol.Where(l => l.isSelected!=null && l.isSelected==true);
            List<DocumentoCompra> documentosCompra = new List<DocumentoCompra>();

            if (seleccionados.Count() > 0)
            {
                var dce = MC_ComunService.db.DocumentoCompraEstado.First(i => i.nombre.ToLower().Contains("ingresada") && i.tipo.Value == true);

                foreach (var seleccionado in seleccionados)
                {
                    seleccionado.estado = 1;
                    DocumentoCompra dc;
                    if (documentosCompra.Any(d => d.Proveedor.id == seleccionado.Proveedor.id))
                    {
                        dc = documentosCompra.First(d => d.Proveedor.id == seleccionado.Proveedor.id);
                    }
                    else
                    {
                        dc = new DocumentoCompra()
                        {
                            codigo = MC_DocumentoCompraService.generarCodigoDC(2),
                            DocumentoCompraEstado = dce,
                            fechaEmision = DateTime.Now,
                            Usuario1 = MS_SharedService.usuarioL,
                            Proveedor = seleccionado.Proveedor,
                            total = 0,
                            SolicitudCompra = seleccionado,
                            tipo = 2,
                            subTotal = 0
                        };
                    }
                    var dcp = new DocumentoCompraProducto()
                        {
                            cantidad = seleccionado.cantidad,
                            DocumentoCompra = dc,
                            Producto = seleccionado.Producto,
                            precioUnit = seleccionado.Producto.precioLista,
                            montoParcial = seleccionado.cantidad * seleccionado.Proveedor.ProveedorProducto.First(l=>l.Producto.id == seleccionado.Producto.id).precio,
                            estado = 1,
                            UnidadMedida = seleccionado.Producto.UnidadMedida
                        };
                    dc.total += Decimal.Round(dcp.montoParcial.Value,2);
                    dc.subTotal = Decimal.Round((dc.total / (1 + (decimal)MS_SharedService.obtenerIGV() / 100)).Value,2);
                    dc.igv = Decimal.Round((dc.total - dc.subTotal).Value, 2);
                    
                    dc.DocumentoCompraProducto.Add(dcp);
                    if (!documentosCompra.Contains(dc))
                    {
                        documentosCompra.Add(dc);
                    }
                }//end for

                if (documentosCompra.Count() > 0)
                {
                    MC_ComunService.db.DocumentoCompra.InsertAllOnSubmit(documentosCompra);
                    MC_ComunService.db.SubmitChanges();
                    MessageBox.Show("La orden se agrego correctamente");
                }
            }
            else
            {
                MessageBox.Show("Seleccione algún elemento");
            }
            //this._productoSol;
            //Proveedor buscado = null;
            //int i;
            //try
            //{
            //    buscado = MC_ProveedorService.buscarProveedorByName(this._proveedorNombre);
            //    documentoCompra.Proveedor = buscado;
            //    NotifyPropertyChanged("documentoCompra");
            //}
            //catch { }

            //if (buscado == null)
            //{
            //    MessageBox.Show("No se encontro ninguna Proveedor", "No se encontro", MessageBoxButton.OK, MessageBoxImage.Question);
            //}
        }


    }
} 