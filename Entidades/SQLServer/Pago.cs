using System;

namespace Entidades.SQLServer
{
    public class Pago
    {
        #region Propiedades

        public int PagoID { get; set; }
        public int ReservaID { get; set; }
        public decimal Monto { get; set; }
        public DateTime FechaPago { get; set; }
        public int MetodoPagoID { get; set; }

        #endregion

        #region Constructor

        public Pago()
        {
            PagoID = 0;
            ReservaID = 0;
            Monto = 0.0m;
            FechaPago = DateTime.MinValue;
            MetodoPagoID = 1; // Asumiendo que el método de pago por defecto es tarjeta
        }

        #endregion
    }
}
