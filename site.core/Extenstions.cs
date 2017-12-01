using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace site.core
{
    public static class Extenstions
    {
        public static T FromJson<T>(this string self) where T : class
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(self);
            }
            catch
            {
                return null;
            }
        }

        public static List<List<T>> ChunkBy<T>(this List<T> source, int chunkSize)
        {
            return source
                .Select((x, i) => new { Index = i, Value = x })
                .GroupBy(x => x.Index / chunkSize)
                .Select(x => x.Select(v => v.Value).ToList())
                .ToList();
        }
    }
}