using System;
using System.IO;

namespace WorkspanCloner.Readers
{
    public static class JsonReader
    {
        public static T Load<T>(string filePath) where T: class
        {
            T result = null;
            var json = File.ReadAllText(filePath);
            result = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(json);

            if (result != null)
                return result;
            else
                throw new Exception("JSON file not formatted properly.");
        }
    }
}
