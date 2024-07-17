using System;

namespace Entidades.SQLServer
{
    public class TarjetaCredito
    {
        #region Propiedades

        public int TarjetaID { get; set; }
        public int UsuarioID { get; set; }
        public string NumeroTarjeta { get; set; }
        public DateTime FechaExpiracion { get; set; }
        public string CodigoSeguridad { get; set; }
        public bool EsValida { get; set; }

        #endregion

        #region Constructor

        public TarjetaCredito()
        {
            TarjetaID = 0;
            UsuarioID = 0;
            NumeroTarjeta = string.Empty;
            FechaExpiracion = DateTime.MinValue;
            CodigoSeguridad = string.Empty;
            EsValida = false;
        }

        #endregion
    }
}
