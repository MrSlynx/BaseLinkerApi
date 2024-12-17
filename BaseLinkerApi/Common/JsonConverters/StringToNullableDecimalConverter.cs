using System;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BaseLinkerApi.Common.JsonConverters;

internal class StringToNullableDecimalConverter : JsonConverter<decimal?>
{
    public override decimal? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        switch (reader.TokenType)
        {
            case JsonTokenType.Number:
                return reader.GetDecimal();
            case JsonTokenType.String:
            {
                if (Decimal.TryParse(reader.GetString(), NumberStyles.None, CultureInfo.InvariantCulture, out Decimal dec))
                {
                    return dec;
                }
                return null;
            }
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public override void Write(Utf8JsonWriter writer, decimal? value, JsonSerializerOptions options)
    {
        if (value.HasValue)
        {
            writer.WriteNumberValue(value.Value);
        }
        else
        {
            writer.WriteNullValue();
        }
    }
}