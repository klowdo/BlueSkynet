using System;

namespace BlueSkynet.Domain.Services
{
    public interface ISerializer
    {
        string SerializeObject(object obj);

        T DeserializeObject<T>(T obj);

        object DeserializeObject(string eData, Type getType);
    }
}