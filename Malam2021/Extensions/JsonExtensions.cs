using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Malam2021.Extensions
{
    public static class JsonExtensions
    {

        readonly static JsonSerializerSettings newtonSettingsIdented;
        readonly static JsonSerializerSettings newtonSettingsNotIdented;
        static JsonExtensions()
        {
            newtonSettingsIdented = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new CamelCaseNamingStrategy()
                },
                Formatting = Newtonsoft.Json.Formatting.Indented,
                DateFormatString = "yyyy-MM-dd HH:mm:ss"
            };
            newtonSettingsNotIdented = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new CamelCaseNamingStrategy()
                },
                //  Formatting = Newtonsoft.Json.Formatting.Indented,
                DateFormatString = "yyyy-MM-dd HH:mm:ss"
            };
        }

        static JsonSerializerSettings Settings(bool isIdented)
        {
            return (isIdented) ? newtonSettingsIdented : newtonSettingsNotIdented;
        }

        public static string ToJson<T>(this T that, bool isIdented = true) where T : new()
        {
            try
            {
                if (that == null) return null;
                return JsonConvert.SerializeObject(that, Settings(isIdented));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}\n{ex.StackTrace}");
                return null;
            }
        }
        public static string ToJson(this object that, Type t, bool isIdented = true)
        {
            try
            {
                if (that == null) return null;
                return JsonConvert.SerializeObject(that, t, Settings(isIdented));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}\n{ex.StackTrace}");
                return null;
            }

        }
        public static T FromJson<T>(this string that, bool isIdented = true) where T : new()
        {
            try
            {
                if (that?.Length < 4) return default(T);
                return JsonConvert.DeserializeObject<T>(that, Settings(isIdented));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}\n{ex.StackTrace}");
                return default(T);
            }
        }
        public static object FromJson(this string that, Type t, bool isIdented = true)
        {
            try
            {
                if (that?.Length < 4) return null;
                return JsonConvert.DeserializeObject(that, t, Settings(isIdented));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}\n{ex.StackTrace}");
                return null;
            }
        }
        public static T[] ArrayFromJson<T>(this string json, bool isIdented = true) where T : new()
        {
            if (json.StartsWith('['))
            {
                return JsonConvert.DeserializeObject<T[]>(json, Settings(isIdented));
            }
            else if (json.StartsWith('{'))
            {
                T item = json.FromJson<T>();
                return new[] { item };
            }
            return new T[0];
        }
    }


}
