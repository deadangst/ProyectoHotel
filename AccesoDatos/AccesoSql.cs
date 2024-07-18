using AccesoDatos.Interfaces;
using Dapper;
using Entidades.SQLServer;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace AccesoDatos
{
    public class AccesoSQL : IAccesoSQL
    {
        private readonly IConfiguration _iConfiguracion;

        public AccesoSQL(IConfiguration iConfiguracion)
        {
            _iConfiguracion = iConfiguracion;
        }

        #region Métodos de Usuarios

        // Método para agregar un usuario
        public bool AgregarUsuario(Usuario P_Entidad)
        {
            // Hashear la contraseña antes de guardarla
            P_Entidad.PasswordHash = BCrypt.Net.BCrypt.HashPassword(P_Entidad.PasswordHash);

            DynamicParameters parametros = new DynamicParameters();
            parametros.Add("@Nombre", P_Entidad.Nombre, DbType.String, ParameterDirection.Input, 25);
            parametros.Add("@Apellido", P_Entidad.Apellido, DbType.String, ParameterDirection.Input, 50);
            parametros.Add("@Email", P_Entidad.Email, DbType.String, ParameterDirection.Input, 50);
            parametros.Add("@Telefono", P_Entidad.Telefono, DbType.String, ParameterDirection.Input, 20);
            parametros.Add("@TipoUsuarioID", P_Entidad.TipoUsuarioID, DbType.Int32, ParameterDirection.Input);
            parametros.Add("@PasswordHash", P_Entidad.PasswordHash, DbType.String, ParameterDirection.Input);

            using (var conexionSQL = new SqlConnection(_iConfiguracion.GetConnectionString("ConexionSQLServer")))
            {
                try
                {
                    return conexionSQL.Execute("PA_AgregarUsuario", parametros, commandType: CommandType.StoredProcedure) > 0;
                }
                catch (SqlException ex) when (ex.Number == 2627) // Error de clave duplicada
                {
                    throw new Exception("El usuario con ese email ya existe.");
                }
            }
        }

        // Método para modificar un usuario
        public bool ModificarUsuario(Usuario P_Entidad)
        {
            // Hashear la nueva contraseña antes de guardarla, si es que se proporcionó una nueva contraseña
            if (!string.IsNullOrEmpty(P_Entidad.PasswordHash))
            {
                P_Entidad.PasswordHash = BCrypt.Net.BCrypt.HashPassword(P_Entidad.PasswordHash);
            }

            DynamicParameters parametros = new DynamicParameters();
            parametros.Add("@UsuarioID", P_Entidad.UsuarioID, DbType.Int32, ParameterDirection.Input);
            parametros.Add("@Nombre", P_Entidad.Nombre, DbType.String, ParameterDirection.Input, 25);
            parametros.Add("@Apellido", P_Entidad.Apellido, DbType.String, ParameterDirection.Input, 50);
            parametros.Add("@Email", P_Entidad.Email, DbType.String, ParameterDirection.Input, 50);
            parametros.Add("@Telefono", P_Entidad.Telefono, DbType.String, ParameterDirection.Input, 20);
            parametros.Add("@TipoUsuarioID", P_Entidad.TipoUsuarioID, DbType.Int32, ParameterDirection.Input);
            parametros.Add("@PasswordHash", P_Entidad.PasswordHash, DbType.String, ParameterDirection.Input);

            using (var conexionSQL = new SqlConnection(_iConfiguracion.GetConnectionString("ConexionSQLServer")))
            {
                return conexionSQL.Execute("PA_ModificarUsuario", parametros, commandType: CommandType.StoredProcedure) > 0;
            }
        }

        // Método para eliminar un usuario
        public bool EliminarUsuario(int usuarioID)
        {
            DynamicParameters parametros = new DynamicParameters();
            parametros.Add("@UsuarioID", usuarioID, DbType.Int32, ParameterDirection.Input);

            using (var conexionSQL = new SqlConnection(_iConfiguracion.GetConnectionString("ConexionSQLServer")))
            {
                return conexionSQL.Execute("PA_EliminarUsuario", parametros, commandType: CommandType.StoredProcedure) > 0;
            }
        }

        // Método para consultar usuarios
        public List<Usuario> ConsultarUsuarios()
        {
            using (var conexionSQL = new SqlConnection(_iConfiguracion.GetConnectionString("ConexionSQLServer")))
            {
                return (List<Usuario>)conexionSQL.Query<Usuario>("PA_ConsultarUsuarios", commandType: CommandType.StoredProcedure);
            }
        }

        // Método para consultar usuarios filtrados por tipo de usuario
        public List<Usuario> ConsultarUsuariosFiltrados(int tipoUsuarioID)
        {
            DynamicParameters parametros = new DynamicParameters();
            parametros.Add("@TipoUsuarioID", tipoUsuarioID, DbType.Int32, ParameterDirection.Input);

            using (var conexionSQL = new SqlConnection(_iConfiguracion.GetConnectionString("ConexionSQLServer")))
            {
                return (List<Usuario>)conexionSQL.Query<Usuario>("PA_ConsultarUsuariosFiltrados", parametros, commandType: CommandType.StoredProcedure);
            }
        }

        // Método para consultar usuario por correo electrónico
        public Usuario ConsultarUsuarioPorEmail(string email)
        {
            DynamicParameters parametros = new DynamicParameters();
            parametros.Add("@Email", email, DbType.String, ParameterDirection.Input, 50);

            using (var conexionSQL = new SqlConnection(_iConfiguracion.GetConnectionString("ConexionSQLServer")))
            {
                return conexionSQL.Query<Usuario>("PA_ConsultarUsuarioPorEmail", parametros, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
        }

        #endregion

        #region Métodos de Tarjetas de Crédito

        // Método para agregar una tarjeta de crédito
        public bool AgregarTarjetaCredito(TarjetaCredito P_Entidad)
        {
            DynamicParameters parametros = new DynamicParameters();
            parametros.Add("@UsuarioID", P_Entidad.UsuarioID, DbType.Int32, ParameterDirection.Input);
            parametros.Add("@NumeroTarjeta", P_Entidad.NumeroTarjeta, DbType.String, ParameterDirection.Input, 20);
            parametros.Add("@FechaExpiracion", P_Entidad.FechaExpiracion, DbType.Date, ParameterDirection.Input);
            parametros.Add("@CodigoSeguridad", P_Entidad.CodigoSeguridad, DbType.String, ParameterDirection.Input, 4);
            parametros.Add("@EsValida", P_Entidad.EsValida, DbType.Boolean, ParameterDirection.Input);

            using (var conexionSQL = new SqlConnection(_iConfiguracion.GetConnectionString("ConexionSQLServer")))
            {
                return conexionSQL.Execute("PA_AgregarTarjetaCredito", parametros, commandType: CommandType.StoredProcedure) > 0;
            }
        }

        // Método para modificar una tarjeta de crédito
        public bool ModificarTarjetaCredito(TarjetaCredito P_Entidad)
        {
            DynamicParameters parametros = new DynamicParameters();
            parametros.Add("@TarjetaID", P_Entidad.TarjetaID, DbType.Int32, ParameterDirection.Input);
            parametros.Add("@NumeroTarjeta", P_Entidad.NumeroTarjeta, DbType.String, ParameterDirection.Input, 20);
            parametros.Add("@FechaExpiracion", P_Entidad.FechaExpiracion, DbType.Date, ParameterDirection.Input);
            parametros.Add("@CodigoSeguridad", P_Entidad.CodigoSeguridad, DbType.String, ParameterDirection.Input, 4);
            parametros.Add("@EsValida", P_Entidad.EsValida, DbType.Boolean, ParameterDirection.Input);

            using (var conexionSQL = new SqlConnection(_iConfiguracion.GetConnectionString("ConexionSQLServer")))
            {
                return conexionSQL.Execute("PA_ModificarTarjetaCredito", parametros, commandType: CommandType.StoredProcedure) > 0;
            }
        }

        // Método para eliminar una tarjeta de crédito
        public bool EliminarTarjetaCredito(int tarjetaID)
        {
            DynamicParameters parametros = new DynamicParameters();
            parametros.Add("@TarjetaID", tarjetaID, DbType.Int32, ParameterDirection.Input);

            using (var conexionSQL = new SqlConnection(_iConfiguracion.GetConnectionString("ConexionSQLServer")))
            {
                return conexionSQL.Execute("PA_EliminarTarjetaCredito", parametros, commandType: CommandType.StoredProcedure) > 0;
            }
        }

        // Método para consultar tarjetas de crédito
        public List<TarjetaCredito> ConsultarTarjetasCredito()
        {
            using (var conexionSQL = new SqlConnection(_iConfiguracion.GetConnectionString("ConexionSQLServer")))
            {
                return (List<TarjetaCredito>)conexionSQL.Query<TarjetaCredito>("PA_ConsultarTarjetasCredito", commandType: CommandType.StoredProcedure);
            }
        }

        #endregion

        #region Métodos de Reservas

        // Método para agregar una reserva
        public bool AgregarReserva(Reserva P_Entidad)
        {
            bool disponible = VerificarDisponibilidad(P_Entidad.HabitacionID, P_Entidad.FechaInicio, P_Entidad.FechaFin);
            if (!disponible)
            {
                throw new Exception("La habitación no está disponible en las fechas seleccionadas.");
            }

            DynamicParameters parametros = new DynamicParameters();
            parametros.Add("@HabitacionID", P_Entidad.HabitacionID, DbType.Int32, ParameterDirection.Input);
            parametros.Add("@UsuarioID", P_Entidad.UsuarioID, DbType.Int32, ParameterDirection.Input);
            parametros.Add("@TarjetaID", P_Entidad.TarjetaID, DbType.Int32, ParameterDirection.Input);
            parametros.Add("@FechaInicio", P_Entidad.FechaInicio, DbType.Date, ParameterDirection.Input);
            parametros.Add("@FechaFin", P_Entidad.FechaFin, DbType.Date, ParameterDirection.Input);
            parametros.Add("@MontoTotal", P_Entidad.MontoTotal, DbType.Decimal, ParameterDirection.Input);
            parametros.Add("@EstadoID", P_Entidad.EstadoID, DbType.Int32, ParameterDirection.Input);

            using (var conexionSQL = new SqlConnection(_iConfiguracion.GetConnectionString("ConexionSQLServer")))
            {
                return conexionSQL.Execute("PA_AgregarReserva", parametros, commandType: CommandType.StoredProcedure) > 0;
            }
        }

        // Método para modificar una reserva
        public bool ModificarReserva(Reserva P_Entidad)
        {
            DynamicParameters parametros = new DynamicParameters();
            parametros.Add("@ReservaID", P_Entidad.ReservaID, DbType.Int32, ParameterDirection.Input);
            parametros.Add("@HabitacionID", P_Entidad.HabitacionID, DbType.Int32, ParameterDirection.Input);
            parametros.Add("@UsuarioID", P_Entidad.UsuarioID, DbType.Int32, ParameterDirection.Input);
            parametros.Add("@TarjetaID", P_Entidad.TarjetaID, DbType.Int32, ParameterDirection.Input);
            parametros.Add("@FechaInicio", P_Entidad.FechaInicio, DbType.Date, ParameterDirection.Input);
            parametros.Add("@FechaFin", P_Entidad.FechaFin, DbType.Date, ParameterDirection.Input);
            parametros.Add("@MontoTotal", P_Entidad.MontoTotal, DbType.Decimal, ParameterDirection.Input);
            parametros.Add("@EstadoID", P_Entidad.EstadoID, DbType.Int32, ParameterDirection.Input);

            using (var conexionSQL = new SqlConnection(_iConfiguracion.GetConnectionString("ConexionSQLServer")))
            {
                return conexionSQL.Execute("PA_ModificarReserva", parametros, commandType: CommandType.StoredProcedure) > 0;
            }
        }

        //// Método para eliminar una reserva
        //public bool EliminarReserva(int reservaID)
        //{
        //    using (var conexionSQL = new SqlConnection(_iConfiguracion.GetConnectionString("ConexionSQLServer")))
        //    {
        //        var parametros = new DynamicParameters();
        //        parametros.Add("@ReservaID", reservaID, DbType.Int32, ParameterDirection.Input);

        //        try
        //        {
        //            // Obtén la información de la reserva antes de eliminarla
        //            var reserva = conexionSQL.QueryFirstOrDefault<Reserva>("PA_ConsultarReservaPorID", parametros, commandType: CommandType.StoredProcedure);

        //            // Calcula la diferencia en días entre la fecha de hoy y la fecha de inicio de la reserva
        //            var diasAntesDeInicio = (reserva.FechaInicio - DateTime.Today).Days;

        //            // Si la reserva se cancela dentro de los 15 días antes de la fecha de inicio, aplica la penalización
        //            if (diasAntesDeInicio < 15)
        //            {
        //                var montoPenalizacion = reserva.MontoTotal * 0.2m;

        //                var parametrosPago = new DynamicParameters();
        //                parametrosPago.Add("@ReservaID", reservaID, DbType.Int32, ParameterDirection.Input);
        //                parametrosPago.Add("@Monto", montoPenalizacion, DbType.Decimal, ParameterDirection.Input);
        //                parametrosPago.Add("@FechaPago", DateTime.Now, DbType.Date, ParameterDirection.Input);
        //                parametrosPago.Add("@MetodoPagoID", 1, DbType.Int32, ParameterDirection.Input); // Asume un método de pago por defecto

        //                conexionSQL.Execute("PA_AgregarPago", parametrosPago, commandType: CommandType.StoredProcedure);
        //            }

        //            // Elimina la reserva y los pagos asociados que no son multas
        //            conexionSQL.Execute("PA_EliminarReserva", parametros, commandType: CommandType.StoredProcedure);
        //            return true;
        //        }
        //        catch (SqlException ex)
        //        {
        //            // Manejo de errores según sea necesario
        //            throw new Exception("Error al eliminar la reserva: " + ex.Message);
        //        }
        //    }
        //}

        // Método para eliminar una reserva
        public bool EliminarReserva(int reservaID)
        {
            using (var conexionSQL = new SqlConnection(_iConfiguracion.GetConnectionString("ConexionSQLServer")))
            {
                var parametros = new DynamicParameters();
                parametros.Add("@ReservaID", reservaID, DbType.Int32, ParameterDirection.Input);

                try
                {
                    // Elimina la reserva utilizando el procedimiento almacenado que maneja la lógica de penalización
                    conexionSQL.Execute("PA_EliminarReserva", parametros, commandType: CommandType.StoredProcedure);
                    return true;
                }
                catch (SqlException ex)
                {
                    // Manejo de errores según sea necesario
                    throw new Exception("Error al eliminar la reserva: " + ex.Message);
                }
            }
        }

        // Método para consultar reservas
        public List<Reserva> ConsultarReservas()
        {
            using (var conexionSQL = new SqlConnection(_iConfiguracion.GetConnectionString("ConexionSQLServer")))
            {
                return (List<Reserva>)conexionSQL.Query<Reserva>("PA_ConsultarReservas", commandType: CommandType.StoredProcedure);
            }
        }

        // Método para consultar reservas filtradas por estado
        public List<Reserva> ConsultarReservasFiltradas(int estadoID)
        {
            DynamicParameters parametros = new DynamicParameters();
            parametros.Add("@EstadoID", estadoID, DbType.Int32, ParameterDirection.Input);

            using (var conexionSQL = new SqlConnection(_iConfiguracion.GetConnectionString("ConexionSQLServer")))
            {
                return (List<Reserva>)conexionSQL.Query<Reserva>("PA_ConsultarReservasFiltradas", parametros, commandType: CommandType.StoredProcedure);
            }
        }

        // Método para consultar una reserva por ID
        public Reserva ConsultarReservaPorID(int reservaID)
        {
            DynamicParameters parametros = new DynamicParameters();
            parametros.Add("@ReservaID", reservaID, DbType.Int32, ParameterDirection.Input);

            using (var conexionSQL = new SqlConnection(_iConfiguracion.GetConnectionString("ConexionSQLServer")))
            {
                return conexionSQL.Query<Reserva>("PA_ConsultarReservaPorID", parametros, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
        }

        #endregion

        #region Métodos de Pagos

        // Método para agregar un pago
        public bool AgregarPago(Pago P_Entidad)
        {
            DynamicParameters parametros = new DynamicParameters();
            parametros.Add("@ReservaID", P_Entidad.ReservaID, DbType.Int32, ParameterDirection.Input);
            parametros.Add("@Monto", P_Entidad.Monto, DbType.Decimal, ParameterDirection.Input);
            parametros.Add("@FechaPago", P_Entidad.FechaPago, DbType.Date, ParameterDirection.Input);
            parametros.Add("@MetodoPagoID", P_Entidad.MetodoPagoID, DbType.Int32, ParameterDirection.Input);

            using (var conexionSQL = new SqlConnection(_iConfiguracion.GetConnectionString("ConexionSQLServer")))
            {
                return conexionSQL.Execute("PA_AgregarPago", parametros, commandType: CommandType.StoredProcedure) > 0;
            }
        }

        // Método para modificar un pago
        public bool ModificarPago(Pago P_Entidad)
        {
            DynamicParameters parametros = new DynamicParameters();
            parametros.Add("@PagoID", P_Entidad.PagoID, DbType.Int32, ParameterDirection.Input);
            parametros.Add("@ReservaID", P_Entidad.ReservaID, DbType.Int32, ParameterDirection.Input);
            parametros.Add("@Monto", P_Entidad.Monto, DbType.Decimal, ParameterDirection.Input);
            parametros.Add("@FechaPago", P_Entidad.FechaPago, DbType.Date, ParameterDirection.Input);
            parametros.Add("@MetodoPagoID", P_Entidad.MetodoPagoID, DbType.Int32, ParameterDirection.Input);

            using (var conexionSQL = new SqlConnection(_iConfiguracion.GetConnectionString("ConexionSQLServer")))
            {
                return conexionSQL.Execute("PA_ModificarPago", parametros, commandType: CommandType.StoredProcedure) > 0;
            }
        }

        // Método para eliminar un pago
        public bool EliminarPago(int pagoID)
        {
            DynamicParameters parametros = new DynamicParameters();
            parametros.Add("@PagoID", pagoID, DbType.Int32, ParameterDirection.Input);

            using (var conexionSQL = new SqlConnection(_iConfiguracion.GetConnectionString("ConexionSQLServer")))
            {
                return conexionSQL.Execute("PA_EliminarPago", parametros, commandType: CommandType.StoredProcedure) > 0;
            }
        }

        // Método para consultar pagos
        public List<Pago> ConsultarPagos()
        {
            using (var conexionSQL = new SqlConnection(_iConfiguracion.GetConnectionString("ConexionSQLServer")))
            {
                return (List<Pago>)conexionSQL.Query<Pago>("PA_ConsultarPagos", commandType: CommandType.StoredProcedure);
            }
        }

        #endregion
        #region Métodos Habitaciones
        
        public bool AgregarHabitacion(Habitacion P_Entidad)
        {
            DynamicParameters parametros = new DynamicParameters();
            parametros.Add("@NumeroHabitacion", P_Entidad.NumeroHabitacion, DbType.Int32, ParameterDirection.Input);
            parametros.Add("@TipoHabitacion", P_Entidad.TipoHabitacion, DbType.String, ParameterDirection.Input, 50);
            parametros.Add("@Capacidad", P_Entidad.Capacidad, DbType.Int32, ParameterDirection.Input);
            parametros.Add("@Precio", P_Entidad.Precio, DbType.Decimal, ParameterDirection.Input);

            using (var conexionSQL = new SqlConnection(_iConfiguracion.GetConnectionString("ConexionSQLServer")))
            {
                return conexionSQL.Execute("PA_AgregarHabitacion", parametros, commandType: CommandType.StoredProcedure) > 0;
            }
        }

        public bool ModificarHabitacion(Habitacion P_Entidad)
        {
            DynamicParameters parametros = new DynamicParameters();
            parametros.Add("@HabitacionID", P_Entidad.HabitacionID, DbType.Int32, ParameterDirection.Input);
            parametros.Add("@NumeroHabitacion", P_Entidad.NumeroHabitacion, DbType.Int32, ParameterDirection.Input);
            parametros.Add("@TipoHabitacion", P_Entidad.TipoHabitacion, DbType.String, ParameterDirection.Input, 50);
            parametros.Add("@Capacidad", P_Entidad.Capacidad, DbType.Int32, ParameterDirection.Input);
            parametros.Add("@Precio", P_Entidad.Precio, DbType.Decimal, ParameterDirection.Input);

            using (var conexionSQL = new SqlConnection(_iConfiguracion.GetConnectionString("ConexionSQLServer")))
            {
                return conexionSQL.Execute("PA_ModificarHabitacion", parametros, commandType: CommandType.StoredProcedure) > 0;
            }
        }

        public bool EliminarHabitacion(int habitacionID)
        {
            DynamicParameters parametros = new DynamicParameters();
            parametros.Add("@HabitacionID", habitacionID, DbType.Int32, ParameterDirection.Input);

            using (var conexionSQL = new SqlConnection(_iConfiguracion.GetConnectionString("ConexionSQLServer")))
            {
                return conexionSQL.Execute("PA_EliminarHabitacion", parametros, commandType: CommandType.StoredProcedure) > 0;
            }
        }

        public List<Habitacion> ConsultarHabitaciones()
        {
            using (var conexionSQL = new SqlConnection(_iConfiguracion.GetConnectionString("ConexionSQLServer")))
            {
                return (List<Habitacion>)conexionSQL.Query<Habitacion>("PA_ConsultarHabitaciones", commandType: CommandType.StoredProcedure);
            }
        }

        public Habitacion ConsultarHabitacionPorID(int habitacionID)
        {
            DynamicParameters parametros = new DynamicParameters();
            parametros.Add("@HabitacionID", habitacionID, DbType.Int32, ParameterDirection.Input);

            using (var conexionSQL = new SqlConnection(_iConfiguracion.GetConnectionString("ConexionSQLServer")))
            {
                return conexionSQL.Query<Habitacion>("PA_ConsultarHabitacionPorID", parametros, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
        }
        #endregion

        #region Métodos de Disponibilidad

        // Método para verificar la disponibilidad de una habitación
        public bool VerificarDisponibilidad(int habitacionID, DateTime fechaInicio, DateTime fechaFin)
        {
            DynamicParameters parametros = new DynamicParameters();
            parametros.Add("@FechaInicio", fechaInicio, DbType.Date, ParameterDirection.Input);
            parametros.Add("@FechaFin", fechaFin, DbType.Date, ParameterDirection.Input);
            parametros.Add("@HabitacionID", habitacionID, DbType.Int32, ParameterDirection.Input);

            using (var conexionSQL = new SqlConnection(_iConfiguracion.GetConnectionString("ConexionSQLServer")))
            {
                return conexionSQL.Query<int>("PA_VerificarDisponibilidad", parametros, commandType: CommandType.StoredProcedure).Single() == 1;
            }
        }

        #endregion
    }
}
