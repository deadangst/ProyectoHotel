using Entidades.SQLServer;
using System;
using System.Collections.Generic;

namespace AccesoDatos.Interfaces
{
    public interface IAccesoSQL
    {
        bool AgregarUsuario(Usuario P_Entidad);
        bool ModificarUsuario(Usuario P_Entidad);
        bool EliminarUsuario(int usuarioID);
        List<Usuario> ConsultarUsuarios();
        List<Usuario> ConsultarUsuariosFiltrados(int tipoUsuarioID);
        Usuario ConsultarUsuarioPorEmail(string email); // Nuevo método para consultar usuario por email

        bool AgregarTarjetaCredito(TarjetaCredito P_Entidad);
        bool ModificarTarjetaCredito(TarjetaCredito P_Entidad);
        bool EliminarTarjetaCredito(int tarjetaID);
        List<TarjetaCredito> ConsultarTarjetasCredito();

        bool AgregarReserva(Reserva P_Entidad);
        bool ModificarReserva(Reserva P_Entidad);
        bool EliminarReserva(int reservaID);
        List<Reserva> ConsultarReservas();
        List<Reserva> ConsultarReservasFiltradas(int estadoID);
        Reserva ConsultarReservaPorID(int reservaID); // Nuevo método para consultar reserva por ID

        bool AgregarPago(Pago P_Entidad);
        bool ModificarPago(Pago P_Entidad);
        bool EliminarPago(int pagoID);
        List<Pago> ConsultarPagos();

        bool VerificarDisponibilidad(int habitacionID, DateTime fechaInicio, DateTime fechaFin);
    }
}
