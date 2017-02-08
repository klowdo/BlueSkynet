using BlueSkynet.Domain.Services;
using Newtonsoft.Json;
using System;

namespace BlueSkynet.Infrastructure.Services
{
    public class JsonSerializer : ISerializer
    {
        public string SerializeObject(object obj) => JsonConvert.SerializeObject(obj);

        public T DeserializeObject<T>(string obj) => JsonConvert.DeserializeObject<T>(obj);

        public object DeserializeObject(string eData, Type getType) => JsonConvert.DeserializeObject(eData, getType);
    }
}