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

        public bool AgregarUsuario(Usuario P_Entidad)
        {
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

        public bool ModificarUsuario(Usuario P_Entidad)
        {
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

        public bool EliminarUsuario(int usuarioID)
        {
            DynamicParameters parametros = new DynamicParameters();

            parametros.Add("@UsuarioID", usuarioID, DbType.Int32, ParameterDirection.Input);

            using (var conexionSQL = new SqlConnection(_iConfiguracion.GetConnectionString("ConexionSQLServer")))
            {
                return conexionSQL.Execute("PA_EliminarUsuario", parametros, commandType: CommandType.StoredProcedure) > 0;
            }
        }

        public List<Usuario> ConsultarUsuarios()
        {
            using (var conexionSQL = new SqlConnection(_iConfiguracion.GetConnectionString("ConexionSQLServer")))
            {
                return (List<Usuario>)conexionSQL.Query<Usuario>("PA_ConsultarUsuarios", commandType: CommandType.StoredProcedure);
            }
        }

        public List<Usuario> ConsultarUsuariosFiltrados(int tipoUsuarioID)
        {
            DynamicParameters parametros = new DynamicParameters();
            parametros.Add("@TipoUsuarioID", tipoUsuarioID, DbType.Int32, ParameterDirection.Input);

            using (var conexionSQL = new SqlConnection(_iConfiguracion.GetConnectionString("ConexionSQLServer")))
            {
                return (List<Usuario>)conexionSQL.Query<Usuario>("PA_ConsultarUsuariosFiltrados", parametros, commandType: CommandType.StoredProcedure);
            }
        }

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

        public bool EliminarTarjetaCredito(int tarjetaID)
        {
            DynamicParameters parametros = new DynamicParameters();
            parametros.Add("@TarjetaID", tarjetaID, DbType.Int32, ParameterDirection.Input);

            using (var conexionSQL = new SqlConnection(_iConfiguracion.GetConnectionString("ConexionSQLServer")))
            {
                return conexionSQL.Execute("PA_EliminarTarjetaCredito", parametros, commandType: CommandType.StoredProcedure) > 0;
            }
        }

        public List<TarjetaCredito> ConsultarTarjetasCredito()
        {
            using (var conexionSQL = new SqlConnection(_iConfiguracion.GetConnectionString("ConexionSQLServer")))
            {
                return (List<TarjetaCredito>)conexionSQL.Query<TarjetaCredito>("PA_ConsultarTarjetasCredito", commandType: CommandType.StoredProcedure);
            }
        }

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

        public bool EliminarReserva(int reservaID)
        {
            DynamicParameters parametros = new DynamicParameters();
            parametros.Add("@ReservaID", reservaID, DbType.Int32, ParameterDirection.Input);

            using (var conexionSQL = new SqlConnection(_iConfiguracion.GetConnectionString("ConexionSQLServer")))
            {
                return conexionSQL.Execute("PA_EliminarReserva", parametros, commandType: CommandType.StoredProcedure) > 0;
            }
        }

        public List<Reserva> ConsultarReservas()
        {
            using (var conexionSQL = new SqlConnection(_iConfiguracion.GetConnectionString("ConexionSQLServer")))
            {
                return (List<Reserva>)conexionSQL.Query<Reserva>("PA_ConsultarReservas", commandType: CommandType.StoredProcedure);
            }
        }

        public List<Reserva> ConsultarReservasFiltradas(int estadoID)
        {
            DynamicParameters parametros = new DynamicParameters();
            parametros.Add("@EstadoID", estadoID, DbType.Int32, ParameterDirection.Input);

            using (var conexionSQL = new SqlConnection(_iConfiguracion.GetConnectionString("ConexionSQLServer")))
            {
                return (List<Reserva>)conexionSQL.Query<Reserva>("PA_ConsultarReservasFiltradas", parametros, commandType: CommandType.StoredProcedure);
            }
        }

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

        public bool EliminarPago(int pagoID)
        {
            DynamicParameters parametros = new DynamicParameters();
            parametros.Add("@PagoID", pagoID, DbType.Int32, ParameterDirection.Input);

            using (var conexionSQL = new SqlConnection(_iConfiguracion.GetConnectionString("ConexionSQLServer")))
            {
                return conexionSQL.Execute("PA_EliminarPago", parametros, commandType: CommandType.StoredProcedure) > 0;
            }
        }

        public List<Pago> ConsultarPagos()
        {
            using (var conexionSQL = new SqlConnection(_iConfiguracion.GetConnectionString("ConexionSQLServer")))
            {
                return (List<Pago>)conexionSQL.Query<Pago>("PA_ConsultarPagos", commandType: CommandType.StoredProcedure);
            }
        }

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
    }
}
