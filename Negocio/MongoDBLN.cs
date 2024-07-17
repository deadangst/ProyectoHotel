using Entidades.MongoDB;
using Negocio.Interfaces;
using AccesoDatos.Interfaces;
using System.Collections.Generic;

namespace Negocio
{
    public class MongoDBLN : IMongoDBLN
    {
        private readonly IAccesoMongoDB _AccesoMongoDB;

        public MongoDBLN(IAccesoMongoDB accesoMongoDB)
        {
            _AccesoMongoDB = accesoMongoDB;
        }

        // Método para agregar una bitácora
        public bool AgregarBitacora(Bitacora P_Entidad)
        {
            return _AccesoMongoDB.AgregarBitacora(P_Entidad);
        }

        // Método para consultar la bitácora
        public List<Bitacora> ConsultarBitacora()
        {
            return _AccesoMongoDB.ConsultarBitacora();
        }
    }
}
