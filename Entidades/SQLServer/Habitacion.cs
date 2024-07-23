using System;

namespace Entidades.SQLServer
{
    public class Habitacion
    {
        #region Propiedades

        public int HabitacionID { get; set; }
        public int NumeroHabitacion { get; set; }
        public string TipoHabitacion { get; set; }
        public int Capacidad { get; set; }
        public decimal Precio { get; set; }

        #endregion

        #region Constructor

        public Habitacion()
        {
            HabitacionID = 0;
            NumeroHabitacion = 0;
            TipoHabitacion = string.Empty;
            Capacidad = 0;
            Precio = 0.0m;
        }

        #endregion
    }
}
