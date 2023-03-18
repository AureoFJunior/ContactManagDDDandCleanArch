using Newtonsoft.Json;

namespace ContactManag.Domain.Utils
{
    public static class SharedFunctions
    {

        #region Utils
        public static bool IsNotNullOrEmpty<T>(IEnumerable<T> enumerable)
        {
            return enumerable != null && enumerable.Any();
        }

        public static T SafeConvertToNumber<T>(string input) where T : struct
        {
            if (string.IsNullOrEmpty(input))
                return default(T);
            if (typeof(T) == typeof(int))
            {
                if (int.TryParse(input, out int result))
                    return (T)(object)result;
            }
            else if (typeof(T) == typeof(short))
            {
                if (short.TryParse(input, out short result))
                    return (T)(object)result;
            }
            else if (typeof(T) == typeof(long))
            {
                if (long.TryParse(input, out long result))
                    return (T)(object)result;
            }
            return default(T);
        }

        public static bool IsDateBetween(DateTime input, DateTime start, DateTime end)
        {
            return input >= start && input <= end;
        }

        public static T ConvertObject<T>(object obj)
        {
            if (obj == null) return default(T);

            var json = JsonConvert.SerializeObject(obj);
            return JsonConvert.DeserializeObject<T>(json);
        }

        public static T IsNullOrZero<T>(this T variable, T defaultValue)
        {
            if (defaultValue == null) throw new ArgumentException("default value can't be null", "defaultValue");
            if (variable == null || variable.Equals(default(T)))
                return defaultValue;
            return variable;
        }

        public static bool ValidateBracketOrder(string s)
        {
            var opening = new List<char>();
            var mapBrackets = new Dictionary<char, char> { { '(', ')' }, { '[', ']' }, { '{', '}' } };

            foreach (char c in s)
            {
                if (mapBrackets.ContainsKey(c))
                    opening.Add(c);

                else if (mapBrackets.ContainsValue(c))
                {
                    if (opening.Count == 0 || mapBrackets[opening.Last()] != c)
                        return false;

                    opening.RemoveAt(opening.Count - 1);
                }
            }

            return opening.Count == 0;
        }

        #endregion
    }
}
