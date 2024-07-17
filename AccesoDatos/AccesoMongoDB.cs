using AccesoDatos.Interfaces;
using Entidades.MongoDB;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System.Collections.Generic;

namespace AccesoDatos
{
    public class AccesoMongoDB : IAccesoMongoDB
    {
        private readonly IConfiguration _iConfiguracion;
        private MongoClient _InstanciaBD;
        private IMongoDatabase _BaseDatos;
        private const string NombreBD = "BitacoraHotel";

        public AccesoMongoDB(IConfiguration iConfiguracion)
        {
            _iConfiguracion = iConfiguracion;
            var cadenaConexion = _iConfiguracion.GetConnectionString("ConexionMongoDBAtlas");
            _InstanciaBD = new MongoClient(cadenaConexion);
            _BaseDatos = _InstanciaBD.GetDatabase(NombreBD);
        }

        // Método para agregar una bitácora
        public bool AgregarBitacora(Bitacora P_Entidad)
        {
            try
            {
                var coleccion = _BaseDatos.GetCollection<Bitacora>("Bitacora");
                coleccion.InsertOne(P_Entidad);
                return true;
            }
            catch
            {
                return false;
            }
        }

        // Método para consultar la bitácora
        public List<Bitacora> ConsultarBitacora()
        {
            try
            {
                var coleccion = _BaseDatos.GetCollection<Bitacora>("Bitacora");
                return coleccion.Find(_ => true).ToList();
            }
            catch
            {
                return new List<Bitacora>();
            }
        }
    }
}
