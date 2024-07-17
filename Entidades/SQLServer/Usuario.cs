using System;

namespace Entidades.SQLServer
{
    public class Usuario
    {
        #region Propiedades

        public int UsuarioID { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public int TipoUsuarioID { get; set; }
        public string PasswordHash { get; set; }

        #endregion

        #region Constructor

        public Usuario()
        {
            UsuarioID = 0;
            Nombre = string.Empty;
            Apellido = string.Empty;
            Email = string.Empty;
            Telefono = string.Empty;
            TipoUsuarioID = 4; // Asumiendo que el tipo por defecto es Cliente
            PasswordHash = string.Empty;
        }

        #endregion
    }
}
