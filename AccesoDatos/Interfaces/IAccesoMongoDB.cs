using Entidades.MongoDB;
using System.Collections.Generic;

namespace AccesoDatos.Interfaces
{
    public interface IAccesoMongoDB
    {
        bool AgregarBitacora(Bitacora P_Entidad);
        List<Bitacora> ConsultarBitacora();
    }
}
