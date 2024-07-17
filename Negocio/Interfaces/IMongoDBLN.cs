using Entidades.MongoDB;
using System.Collections.Generic;

namespace Negocio.Interfaces
{
    public interface IMongoDBLN
    {
        bool AgregarBitacora(Bitacora P_Entidad);
        List<Bitacora> ConsultarBitacora();
    }
}
