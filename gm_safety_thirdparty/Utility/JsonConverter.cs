using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace gm_safety_thirdparty.Utility
{
    public class DateOnlyJsonConverter : JsonConverter<DateOnly>
    {
        private readonly string _format = "dd-MM-yyyy";

        public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var str = reader.GetString();
            if (!string.IsNullOrEmpty(str) &&
                DateOnly.TryParseExact(str, _format, CultureInfo.InvariantCulture, DateTimeStyles.None, out var date))
                return date;
            return default;
        }

        public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
            => writer.WriteStringValue(value == default ? null : value.ToString(_format));
    }

    public class StringToNullableIntConverter : JsonConverter<int?>
    {
        public override int? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            try
            {
                if (reader.TokenType == JsonTokenType.String)
                {
                    var str = reader.GetString();
                    if (int.TryParse(str, out var val)) return val;
                    return null;
                }
                if (reader.TokenType == JsonTokenType.Number) return reader.GetInt32();
                if (reader.TokenType == JsonTokenType.Null) return null;
            }
            catch { return null; }
            return null;
        }

        public override void Write(Utf8JsonWriter writer, int? value, JsonSerializerOptions options)
        {
            if (value.HasValue) writer.WriteNumberValue(value.Value);
            else writer.WriteNullValue();
        }
    }

    //public class StringToBoolConverter : JsonConverter<bool>
    //{
    //    public override bool Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    //    {
    //        var s = reader.GetString()?.Trim().ToLowerInvariant();
    //        return s is "active" or "true" or "yes" or "1";
    //    }

    //    public override void Write(Utf8JsonWriter writer, bool value, JsonSerializerOptions options)
    //        => writer.WriteStringValue(value ? "Active" : "Inactive");
    //}
}