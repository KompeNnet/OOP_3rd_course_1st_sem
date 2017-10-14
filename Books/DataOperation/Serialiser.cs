﻿using Newtonsoft.Json;

namespace Books.DataOperation
{
    public class Serialiser
    {
        class Serializer
        {
            public static string Serialize<T>(T smth)
            {
                return JsonConvert.SerializeObject(smth);
            }

            public static dynamic Deserialize<T>(string smth)
            {
                return JsonConvert.DeserializeObject<T>(smth);
            }
        }
    }
}