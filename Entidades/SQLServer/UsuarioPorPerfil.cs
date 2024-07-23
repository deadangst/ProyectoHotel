using System;
using System.Collections.Generic;
using System.Text;

namespace Entidades.SQLServer
{
    public class UsuarioPorPerfil
    {
        public Usuario usuario { get; set; }
        public Perfil perfil { get; set; }
        public DateTime fechaAsociacion { get; set; }

        public UsuarioPorPerfil()
        {
            usuario = new Usuario();
            perfil = new Perfil();
            fechaAsociacion = DateTime.Now;
        }
    }
}
