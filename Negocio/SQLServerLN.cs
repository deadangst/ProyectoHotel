using Entidades.SQLServer;
using Negocio.Interfaces;
using AccesoDatos.Interfaces;
using System.Collections.Generic;
using System;

namespace Negocio
{
    public class SQLServerLN : ISQLServerLN
    {
        private readonly IAccesoSQL _AccesoSQL;

        public SQLServerLN(IAccesoSQL accesoSQL)
        {
            _AccesoSQL = accesoSQL;
        }
        #region Métodos Usuarios
        // Método para agregar un usuario
        public bool AgregarUsuario(Usuario P_Entidad)
        {
            return _AccesoSQL.AgregarUsuario(P_Entidad);
        }

        //Método para Autorizar Perfil por Usuario
        public List<Perfil> AutorizacionesPorUsuarios(Usuario P_Entidad)
        {
            return _AccesoSQL.AutorizacionesPorUsuarios(P_Entidad);
        }

        // Método para modificar un usuario
        public bool ModificarUsuario(Usuario P_Entidad)
        {
            return _AccesoSQL.ModificarUsuario(P_Entidad);
        }

        // Método para eliminar un usuario
        public bool EliminarUsuario(int usuarioID)
        {
            return _AccesoSQL.EliminarUsuario(usuarioID);
        }

        // Método para consultar usuarios
        public List<Usuario> ConsultarUsuarios()
        {
            return _AccesoSQL.ConsultarUsuarios();
        }

        // Método para consultar usuarios filtrados por tipo de usuario
        public List<Usuario> ConsultarUsuariosFiltrados(int tipoUsuarioID)
        {
            return _AccesoSQL.ConsultarUsuariosFiltrados(tipoUsuarioID);
        }
        #endregion

        #region Métodos Tarjetas
        // Método para agregar una tarjeta de crédito
        public bool AgregarTarjetaCredito(TarjetaCredito P_Entidad)
        {
            return _AccesoSQL.AgregarTarjetaCredito(P_Entidad);
        }

        // Método para modificar una tarjeta de crédito
        public bool ModificarTarjetaCredito(TarjetaCredito P_Entidad)
        {
            return _AccesoSQL.ModificarTarjetaCredito(P_Entidad);
        }

        // Método para eliminar una tarjeta de crédito
        public bool EliminarTarjetaCredito(int tarjetaID)
        {
            return _AccesoSQL.EliminarTarjetaCredito(tarjetaID);
        }

        // Método para consultar tarjetas de crédito
        public List<TarjetaCredito> ConsultarTarjetasCredito()
        {
            return _AccesoSQL.ConsultarTarjetasCredito();
        }

        // Método para agregar una reserva
        public bool AgregarReserva(Reserva P_Entidad)
        {
            return _AccesoSQL.AgregarReserva(P_Entidad);
        }
        #endregion

        #region Métodos Reservas
        // Método para modificar una reserva
        public bool ModificarReserva(Reserva P_Entidad)
        {
            return _AccesoSQL.ModificarReserva(P_Entidad);
        }

        // Método para eliminar una reserva
        public bool EliminarReserva(int reservaID)
        {
            return _AccesoSQL.EliminarReserva(reservaID);
        }

        // Método para consultar reservas
        public List<Reserva> ConsultarReservas()
        {
            return _AccesoSQL.ConsultarReservas();
        }

        // Método para consultar reservas filtradas por estado
        public List<Reserva> ConsultarReservasFiltradas(int estadoID)
        {
            return _AccesoSQL.ConsultarReservasFiltradas(estadoID);
        }
        #endregion

        #region Métodos Pagos
        // Método para agregar un pago
        public bool AgregarPago(Pago P_Entidad)
        {
            return _AccesoSQL.AgregarPago(P_Entidad);
        }

        // Método para modificar un pago
        public bool ModificarPago(Pago P_Entidad)
        {
            return _AccesoSQL.ModificarPago(P_Entidad);
        }

        // Método para eliminar un pago
        public bool EliminarPago(int pagoID)
        {
            return _AccesoSQL.EliminarPago(pagoID);
        }

        // Método para consultar pagos
        public List<Pago> ConsultarPagos()
        {
            return _AccesoSQL.ConsultarPagos();
        }
        #endregion
        #region Habitaciones
        
        public bool AgregarHabitacion(Habitacion P_Entidad)
        {
            return _AccesoSQL.AgregarHabitacion(P_Entidad);
        }

        public bool ModificarHabitacion(Habitacion P_Entidad)
        {
            return _AccesoSQL.ModificarHabitacion(P_Entidad);
        }

        public bool EliminarHabitacion(int habitacionID)
        {
            return _AccesoSQL.EliminarHabitacion(habitacionID);
        }

        public List<Habitacion> ConsultarHabitaciones()
        {
            return _AccesoSQL.ConsultarHabitaciones();
        }

        public Habitacion ConsultarHabitacionPorID(int habitacionID)
        {
            return _AccesoSQL.ConsultarHabitacionPorID(habitacionID);
        }

        // Método para verificar la disponibilidad de una habitación
        public bool VerificarDisponibilidad(int habitacionID, DateTime fechaInicio, DateTime fechaFin)
        {
            return _AccesoSQL.VerificarDisponibilidad(habitacionID, fechaInicio, fechaFin);
        }
        #endregion
    }
}
