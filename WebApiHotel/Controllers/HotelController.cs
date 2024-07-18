using Entidades.MongoDB;
using Entidades.SQLServer;
using Microsoft.AspNetCore.Mvc;
using Negocio.Interfaces;
using System;
using System.Collections.Generic;
using BCrypt.Net;

namespace WebApiHotel.Controllers
{
    [ApiController]
    [Route("api/Hotel")]
    public class HotelController : Controller
    {
        private readonly IMongoDBLN _iMongoDBLN;
        private readonly ISQLServerLN _sQLServerLN;

        public HotelController(IMongoDBLN iMongoDBLN, ISQLServerLN sQLServerLN)
        {
            _iMongoDBLN = iMongoDBLN;
            _sQLServerLN = sQLServerLN;
        }

        public IActionResult Index()
        {
            return View();
        }

        #region Métodos de Usuarios

        [HttpPost]
        [Route(nameof(AgregarUsuario))]
        public IActionResult AgregarUsuario(Usuario usuario)
        {
            try
            {
                // Hashear la contraseña antes de guardarla
                usuario.PasswordHash = BCrypt.Net.BCrypt.HashPassword(usuario.PasswordHash);

                var resultado = _sQLServerLN.AgregarUsuario(usuario);
                if (resultado)
                {
                    _iMongoDBLN.AgregarBitacora(new Bitacora
                    {
                        TipoAccion = "AgregarUsuario",
                        UsuarioID = usuario.UsuarioID,
                        FechaAccion = DateTime.Now
                    });
                    return Ok("Usuario agregado exitosamente.");
                }
                else
                {
                    return StatusCode(500, "Error al agregar el usuario.");
                }
            }
            catch (Exception ex)
            {
                if (ex.Message == "El usuario con ese email ya existe.")
                {
                    return Conflict(ex.Message);
                }
                return StatusCode(500, "Error al agregar el usuario.");
            }
        }

        [HttpPut]
        [Route(nameof(ModificarUsuario))]
        public IActionResult ModificarUsuario([FromHeader] int usuarioID, [FromBody] Usuario usuario)
        {
            try
            {
                // Hashear la nueva contraseña si se proporciona
                if (!string.IsNullOrEmpty(usuario.PasswordHash))
                {
                    usuario.PasswordHash = BCrypt.Net.BCrypt.HashPassword(usuario.PasswordHash);
                }

                var resultado = _sQLServerLN.ModificarUsuario(new Usuario
                {
                    UsuarioID = usuarioID,
                    Nombre = usuario.Nombre,
                    Apellido = usuario.Apellido,
                    Email = usuario.Email,
                    Telefono = usuario.Telefono,
                    TipoUsuarioID = usuario.TipoUsuarioID,
                    PasswordHash = usuario.PasswordHash
                });

                if (resultado)
                {
                    _iMongoDBLN.AgregarBitacora(new Bitacora
                    {
                        TipoAccion = "ModificarUsuario",
                        UsuarioID = usuario.UsuarioID,
                        FechaAccion = DateTime.Now
                    });
                    return Ok("Usuario modificado exitosamente.");
                }
                else
                {
                    return StatusCode(500, "Error al modificar el usuario.");
                }
            }
            catch (Exception ex)
            {
                if (ex.Message == "El usuario con ese email ya existe.")
                {
                    return Conflict(ex.Message);
                }
                return StatusCode(500, "Error al modificar el usuario.");
            }
        }

        [HttpDelete]
        [Route(nameof(EliminarUsuario))]
        public IActionResult EliminarUsuario([FromHeader] int usuarioID)
        {
            try
            {
                var resultado = _sQLServerLN.EliminarUsuario(usuarioID);
                if (resultado)
                {
                    _iMongoDBLN.AgregarBitacora(new Bitacora
                    {
                        TipoAccion = "EliminarUsuario",
                        UsuarioID = usuarioID,
                        FechaAccion = DateTime.Now
                    });
                    return Ok("Usuario eliminado exitosamente.");
                }
                else
                {
                    return StatusCode(500, "Error al eliminar el usuario.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error al eliminar el usuario.");
            }
        }

        [HttpGet]
        [Route(nameof(ConsultarUsuarios))]
        public IActionResult ConsultarUsuarios()
        {
            return Ok(_sQLServerLN.ConsultarUsuarios());
        }

        [HttpGet]
        [Route(nameof(ConsultarUsuariosFiltrados))]
        public IActionResult ConsultarUsuariosFiltrados([FromHeader] int tipoUsuarioID)
        {
            return Ok(_sQLServerLN.ConsultarUsuariosFiltrados(tipoUsuarioID));
        }

        #endregion

        #region Métodos de Tarjetas de Crédito

        [HttpPost]
        [Route(nameof(AgregarTarjetaCredito))]
        public IActionResult AgregarTarjetaCredito(TarjetaCredito tarjeta)
        {
            try
            {
                var resultado = _sQLServerLN.AgregarTarjetaCredito(tarjeta);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error al agregar la tarjeta de crédito.");
            }
        }

        [HttpPut]
        [Route(nameof(ModificarTarjetaCredito))]
        public IActionResult ModificarTarjetaCredito([FromHeader] int tarjetaID, [FromBody] TarjetaCredito tarjeta)
        {
            try
            {
                var resultado = _sQLServerLN.ModificarTarjetaCredito(new TarjetaCredito
                {
                    TarjetaID = tarjetaID,
                    NumeroTarjeta = tarjeta.NumeroTarjeta,
                    FechaExpiracion = tarjeta.FechaExpiracion,
                    CodigoSeguridad = tarjeta.CodigoSeguridad,
                    EsValida = tarjeta.EsValida
                });

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error al modificar la tarjeta de crédito.");
            }
        }

        [HttpDelete]
        [Route(nameof(EliminarTarjetaCredito))]
        public IActionResult EliminarTarjetaCredito([FromHeader] int tarjetaID)
        {
            try
            {
                var resultado = _sQLServerLN.EliminarTarjetaCredito(tarjetaID);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error al eliminar la tarjeta de crédito.");
            }
        }

        [HttpGet]
        [Route(nameof(ConsultarTarjetasCredito))]
        public IActionResult ConsultarTarjetasCredito()
        {
            return Ok(_sQLServerLN.ConsultarTarjetasCredito());
        }

        #endregion

        #region Métodos de Reservas

        [HttpPost]
        [Route(nameof(AgregarReserva))]
        public IActionResult AgregarReserva(Reserva reserva)
        {
            try
            {
                var resultado = _sQLServerLN.AgregarReserva(reserva);
                if (resultado)
                {
                    _iMongoDBLN.AgregarBitacora(new Bitacora
                    {
                        TipoAccion = "AgregarReserva",
                        UsuarioID = reserva.UsuarioID,
                        FechaAccion = DateTime.Now
                    });
                }
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route(nameof(ModificarReserva))]
        public IActionResult ModificarReserva([FromHeader] int reservaID, [FromBody] Reserva reserva)
        {
            try
            {
                var resultado = _sQLServerLN.ModificarReserva(new Reserva
                {
                    ReservaID = reservaID,
                    HabitacionID = reserva.HabitacionID,
                    UsuarioID = reserva.UsuarioID,
                    TarjetaID = reserva.TarjetaID,
                    FechaInicio = reserva.FechaInicio,
                    FechaFin = reserva.FechaFin,
                    MontoTotal = reserva.MontoTotal,
                    EstadoID = reserva.EstadoID
                });

                if (resultado)
                {
                    _iMongoDBLN.AgregarBitacora(new Bitacora
                    {
                        TipoAccion = "ModificarReserva",
                        UsuarioID = reserva.UsuarioID,
                        FechaAccion = DateTime.Now
                    });
                }
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error al modificar la reserva.");
            }
        }

        //[HttpDelete]
        //[Route(nameof(EliminarReserva))]
        //public IActionResult EliminarReserva([FromHeader] int reservaID)
        //{
        //    try
        //    {
        //        var resultado = _sQLServerLN.EliminarReserva(reservaID);
        //        if (resultado)
        //        {
        //            _iMongoDBLN.AgregarBitacora(new Bitacora
        //            {
        //                TipoAccion = "EliminarReserva",
        //                UsuarioID = reservaID,
        //                FechaAccion = DateTime.Now
        //            });
        //        }
        //        return Ok(resultado);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, "Error al eliminar la reserva.");
        //    }
        //}

        [HttpDelete]
        [Route(nameof(EliminarReserva))]
        public IActionResult EliminarReserva([FromHeader] int reservaID)
        {
            try
            {
                var resultado = _sQLServerLN.EliminarReserva(reservaID);
                if (resultado)
                {
                    _iMongoDBLN.AgregarBitacora(new Bitacora
                    {
                        TipoAccion = "EliminarReserva",
                        UsuarioID = reservaID,
                        FechaAccion = DateTime.Now
                    });
                    return Ok("Reserva eliminada exitosamente.");
                }
                else
                {
                    return StatusCode(500, "Error al eliminar la reserva.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error al eliminar la reserva: " + ex.Message);
            }
        }

        [HttpGet]
        [Route(nameof(ConsultarReservas))]
        public IActionResult ConsultarReservas()
        {
            return Ok(_sQLServerLN.ConsultarReservas());
        }

        [HttpGet]
        [Route(nameof(ConsultarReservasFiltradas))]
        public IActionResult ConsultarReservasFiltradas([FromHeader] int estadoID)
        {
            return Ok(_sQLServerLN.ConsultarReservasFiltradas(estadoID));
        }

        #endregion

        #region Métodos de Pagos

        [HttpPost]
        [Route(nameof(AgregarPago))]
        public IActionResult AgregarPago(Pago pago)
        {
            try
            {
                var resultado = _sQLServerLN.AgregarPago(pago);
                if (resultado)
                {
                    _iMongoDBLN.AgregarBitacora(new Bitacora
                    {
                        TipoAccion = "AgregarPago",
                        UsuarioID = pago.ReservaID,
                        FechaAccion = DateTime.Now
                    });
                }
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error al agregar el pago.");
            }
        }

        [HttpPut]
        [Route(nameof(ModificarPago))]
        public IActionResult ModificarPago([FromHeader] int pagoID, [FromBody] Pago pago)
        {
            try
            {
                var resultado = _sQLServerLN.ModificarPago(new Pago
                {
                    PagoID = pagoID,
                    ReservaID = pago.ReservaID,
                    Monto = pago.Monto,
                    FechaPago = pago.FechaPago,
                    MetodoPagoID = pago.MetodoPagoID
                });

                if (resultado)
                {
                    _iMongoDBLN.AgregarBitacora(new Bitacora
                    {
                        TipoAccion = "ModificarPago",
                        UsuarioID = pago.ReservaID,
                        FechaAccion = DateTime.Now
                    });
                }
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error al modificar el pago.");
            }
        }

        [HttpDelete]
        [Route(nameof(EliminarPago))]
        public IActionResult EliminarPago([FromHeader] int pagoID)
        {
            try
            {
                var resultado = _sQLServerLN.EliminarPago(pagoID);
                if (resultado)
                {
                    _iMongoDBLN.AgregarBitacora(new Bitacora
                    {
                        TipoAccion = "EliminarPago",
                        UsuarioID = pagoID,
                        FechaAccion = DateTime.Now
                    });
                }
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error al eliminar el pago.");
            }
        }

        [HttpGet]
        [Route(nameof(ConsultarPagos))]
        public IActionResult ConsultarPagos()
        {
            return Ok(_sQLServerLN.ConsultarPagos());
        }

        #endregion

        #region Métodos de Habitaciones

        [HttpPost]
        [Route(nameof(AgregarHabitacion))]
        public IActionResult AgregarHabitacion(Habitacion habitacion)
        {
            try
            {
                var resultado = _sQLServerLN.AgregarHabitacion(habitacion);
                if (resultado)
                {
                    return Ok("Habitacion agregada exitosamente.");
                }
                else
                {
                    return StatusCode(500, "Error al agregar la habitacion.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error al agregar la habitacion: " + ex.Message);
            }
        }

        [HttpPut]
        [Route(nameof(ModificarHabitacion))]
        public IActionResult ModificarHabitacion([FromHeader] int habitacionID, [FromBody] Habitacion habitacion)
        {
            try
            {
                habitacion.HabitacionID = habitacionID;
                var resultado = _sQLServerLN.ModificarHabitacion(habitacion);
                if (resultado)
                {
                    return Ok("Habitacion modificada exitosamente.");
                }
                else
                {
                    return StatusCode(500, "Error al modificar la habitacion.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error al modificar la habitacion: " + ex.Message);
            }
        }

        [HttpDelete]
        [Route(nameof(EliminarHabitacion))]
        public IActionResult EliminarHabitacion([FromHeader] int habitacionID)
        {
            try
            {
                var resultado = _sQLServerLN.EliminarHabitacion(habitacionID);
                if (resultado)
                {
                    return Ok("Habitacion eliminada exitosamente.");
                }
                else
                {
                    return StatusCode(500, "Error al eliminar la habitacion.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error al eliminar la habitacion: " + ex.Message);
            }
        }

        [HttpGet]
        [Route(nameof(ConsultarHabitaciones))]
        public IActionResult ConsultarHabitaciones()
        {
            return Ok(_sQLServerLN.ConsultarHabitaciones());
        }

        [HttpGet]
        [Route(nameof(ConsultarHabitacionPorID))]
        public IActionResult ConsultarHabitacionPorID([FromHeader] int habitacionID)
        {
            return Ok(_sQLServerLN.ConsultarHabitacionPorID(habitacionID));
        }

        #endregion

        #region Métodos de Bitácora

        [HttpGet]
        [Route(nameof(ConsultarBitacora))]
        public IActionResult ConsultarBitacora()
        {
            List<Bitacora> bitacora = _iMongoDBLN.ConsultarBitacora();
            return Ok(bitacora);
        }

        #endregion
    }
}
