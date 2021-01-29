using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SzkolnikAPI.Code
{
    public static class JSONReader
    {
        public static T Deserialize<T>(string json) => JsonConvert.DeserializeObject<T>(json);

        public static string Serialize(object obj) => JsonConvert.SerializeObject(obj);
    }
}