using Newtonsoft.Json;

namespace Books.DataOperation
{
    public static class Serialiser
    {
        public static string Serialize<T>(T smth)
        {
            return JsonConvert.SerializeObject(smth);
        }

        public static T Deserialize<T>(string smth)
        {
            return JsonConvert.DeserializeObject<T>(smth);
        }
    }
}
