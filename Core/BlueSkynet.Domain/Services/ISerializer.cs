using System;

namespace BlueSkynet.Domain.Services
{
    public interface ISerializer
    {
        string SerializeObject(object obj);

        T DeserializeObject<T>(string obj);

        object DeserializeObject(string eData, Type getType);
    }
}