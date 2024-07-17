using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Entidades.MongoDB
{
    public class Bitacora
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ID { get; set; }
        [BsonElement("actionType")]
        public string TipoAccion { get; set; }
        [BsonElement("userId")]
        public int UsuarioID { get; set; }
        [BsonElement("timestamp")]
        public DateTime FechaAccion { get; set; }

        public Bitacora()
        {
            ID = string.Empty;
            TipoAccion = string.Empty;
            UsuarioID = 0;
            FechaAccion = DateTime.MinValue;
        }
    }
}
