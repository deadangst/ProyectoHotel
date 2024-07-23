using System;
using System.Collections.Generic;
using System.Text;

namespace Entidades.SQLServer
{
    public class Perfil
    {
        public int codigoPerfil { get; set; }
        public string descripcion { get; set; }
        public bool estado { get; set; }
        public string email { get; set; }

        public Perfil()
        {
            codigoPerfil = 0;
            descripcion = string.Empty;
            estado = true;
            email = string.Empty;
        }
    }
}
