using Cursos.Domain.Cursos;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace Cursos.Infrastructure.Serializers;

public class CapacidadCursoSerializer : SerializerBase<CapacidadCurso>
{
    public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, CapacidadCurso value)
    {
        context.Writer.WriteInt32(value.Value);
    }

    public override CapacidadCurso Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
    {
        var value = context.Reader.ReadInt32();
        return new CapacidadCurso(value);
    }


}