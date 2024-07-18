using Entidades.SQLServer;
using System;
using System.Collections.Generic;

namespace Negocio.Interfaces
{
    public interface ISQLServerLN
    {
        bool AgregarUsuario(Usuario P_Entidad);
        bool ModificarUsuario(Usuario P_Entidad);
        bool EliminarUsuario(int usuarioID);
        List<Usuario> ConsultarUsuarios();
        List<Usuario> ConsultarUsuariosFiltrados(int tipoUsuarioID);

        bool AgregarTarjetaCredito(TarjetaCredito P_Entidad);
        bool ModificarTarjetaCredito(TarjetaCredito P_Entidad);
        bool EliminarTarjetaCredito(int tarjetaID);
        List<TarjetaCredito> ConsultarTarjetasCredito();

        bool AgregarReserva(Reserva P_Entidad);
        bool ModificarReserva(Reserva P_Entidad);
        bool EliminarReserva(int reservaID);
        List<Reserva> ConsultarReservas();
        List<Reserva> ConsultarReservasFiltradas(int estadoID);

        bool AgregarPago(Pago P_Entidad);
        bool ModificarPago(Pago P_Entidad);
        bool EliminarPago(int pagoID);
        List<Pago> ConsultarPagos();

        // Métodos de Habitaciones
        bool AgregarHabitacion(Habitacion P_Entidad);
        bool ModificarHabitacion(Habitacion P_Entidad);
        bool EliminarHabitacion(int habitacionID);
        List<Habitacion> ConsultarHabitaciones();
        Habitacion ConsultarHabitacionPorID(int habitacionID);

        bool VerificarDisponibilidad(int habitacionID, DateTime fechaInicio, DateTime fechaFin);
    }
}
