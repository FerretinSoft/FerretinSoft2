
namespace pe.edu.pucp.ferretin.model
{

    public partial class DocumentoCompra
    {
        public string tipoDC
        {
            get
            {
                if (tipo == 1)
                {
                    return "Cotizacion";
                }
                else
                {
                    return "Orden de Compra";
                }
            }
            set
            {
                if (value == "Cotizacion")
                {
                    tipo = 1;
                }
                else
                {
                    tipo = 2;
                }
                this.SendPropertyChanged("tipo");
            }
        }
    }
}
