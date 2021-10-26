using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackSugar.Entity;

namespace BlackSugar.Repository
{
    public interface IJsonParser
    {
        string Parse(object value);
    }

    public class JsonParser : IJsonParser
    {
        private const string JSON_CONTENT = "\"$$name$$\": \"$$value$$\"";
        private const string JSON_VALUE = "$$value$$";
        private const string JSON_NAME = "$$name$$";

        public string Parse(object value)
        {
            var result = string.Empty;
            if (IsRoop(value))
            {
                var ary = value as IEnumerable;
                foreach(var item in ary)
                {
                    result += ParseSimple(item);
                    result += ",\n";
                }
            }
            else
            {
                result = ParseSimple(value);
                result += ",\n";
            }

            return "[\n" + result.Substring(0, result.Length - 2) + "\n]";
        }

        private string ParseSimple(object value)
        {
            var result = string.Empty;
            foreach (var prop in value.GetType().GetProperties())
            {
                result += JSON_CONTENT
                      .Replace(JSON_NAME, Escape(ToLowerFirst(prop.Name)))
                      .Replace(JSON_VALUE, Escape(Convert.ToString(prop.GetValue(value))));

                result += ", ";
            }

            return "    {" + result.Substring(0, result.Length - 2) + "}";
        }

        private string ToLowerFirst(string value)
        {
            var vs = value.ToCharArray();
            vs[0] = char.ToLower(vs[0]);
            return new string(vs);

        }

        private bool IsRoop(object value)
        {
            return typeof(IEnumerable).IsAssignableFrom(value.GetType());
        }

        private string Escape(string value)
        {
            return value
                    .Replace("\"", "\\\"")
                    .Replace("\\", "\\\\")
                    .Replace("/", "\\/")
                    .Replace("\b", "\\\b")
                    .Replace("\f", "\\\f")
                    .Replace("\n", "\\\n")
                    .Replace("\r", "\\\r")
                    .Replace("\t", "\\\t");
        }
    }
}
