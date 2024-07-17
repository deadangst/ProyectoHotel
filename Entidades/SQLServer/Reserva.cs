using System;

namespace Entidades.SQLServer
{
    public class Reserva
    {
        #region Propiedades

        public int ReservaID { get; set; }
        public int HabitacionID { get; set; }
        public int UsuarioID { get; set; }
        public int TarjetaID { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public decimal MontoTotal { get; set; }
        public int EstadoID { get; set; }

        #endregion

        #region Constructor

        public Reserva()
        {
            ReservaID = 0;
            HabitacionID = 0;
            UsuarioID = 0;
            TarjetaID = 0;
            FechaInicio = DateTime.MinValue;
            FechaFin = DateTime.MinValue;
            MontoTotal = 0.0m;
            EstadoID = 1; // Asumiendo que el estado por defecto es activo
        }

        #endregion
    }
}
