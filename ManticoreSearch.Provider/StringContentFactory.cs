using Newtonsoft.Json;
using System.Text;

namespace ManticoreSearch.Provider
{
    internal static class StringContentFactory
    {
        public static StringContent Create<TData>(TData data, string contentType, JsonSerializerSettings settings)
        {
            var json = JsonConvert.SerializeObject(data, settings);
            return new StringContent(json, Encoding.UTF8, contentType!);
        }

        public static StringContent Create(string data, string contentType)
        {
            return new StringContent(data, Encoding.UTF8, contentType);
        }

        public static StringContent Create(string text)
        {
            return new StringContent(text);
        }
    }
}
