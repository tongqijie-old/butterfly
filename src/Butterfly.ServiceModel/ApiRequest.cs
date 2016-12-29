using Petecat.Extending;

namespace Butterfly.ServiceModel
{
    public class ApiRequest
    {
        public KeyValueItem[] Items { get; set; }

        public Paging Paging { get; set; }

        public void Append(string key, string value)
        {
            Items = Items.Append(new KeyValueItem() { Key = key, Value = value });
        }

        public bool Contains(string key)
        {
            return Items.Exists(x => x.EqualsWith(key));
        }

        public T Get<T>(string key)
        {
            var item = Items.FirstOrDefault(x => x.Key.EqualsWith(key));
            if (item == null)
            {
                return typeof(T).GetDefaultValue<T>();
            }

            return item.Value.ConvertTo<T>();
        }
    }
}