using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace Recipe.DataStructures
{
    public static class Utility
    {
        public static string ToJsonString<T>(this T obj)
        {
            using (MemoryStream stream = new System.IO.MemoryStream())
            {
                // Serialize into the stream.
                DataContractJsonSerializer serializer
                    = new DataContractJsonSerializer(typeof(object));
                serializer.WriteObject(stream, obj);
                stream.Flush();

                // Get the result as text.
                stream.Seek(0, SeekOrigin.Begin);
                using (StreamReader reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }
        public static T FromJsonString<T>(this string json)
        {
            // Make a stream to read from.
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(json);
            writer.Flush();
            stream.Position = 0;

            // Deserialize from the stream.
            DataContractJsonSerializer serializer
                = new DataContractJsonSerializer(typeof(T));
            T result = (T)serializer.ReadObject(stream);

            // Return the result.
            return result;          
        }

    }
}
