using Newtonsoft.Json;


namespace TelegramUtils
{
    public class JsonAdapter<T> where T : class
    {
        protected JsonSerializerSettings settings;
        public JsonAdapter()
        {
            settings = new JsonSerializerSettings();
            settings.Formatting = Formatting.Indented;
            settings.TypeNameHandling = TypeNameHandling.All;
        }

        public string Dump(T bitField)
        {
            return JsonConvert.SerializeObject(bitField, settings);
        }

        public T Restore(string dumped)
        {
            return JsonConvert.DeserializeObject(dumped, settings) as T;
        }
    }
}