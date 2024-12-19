// StringToDecimalConverter.cs is a part of BaseLinkerApi project.
// 
// Created by Slynx on 19/12/2024 21:12.

using System;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BaseLinkerApi.Common.JsonConverters
{
    internal class StringToDecimalConverter : JsonConverter<Decimal>
    {
        public override Decimal Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
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
                    return 0m;
                }
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public override void Write(Utf8JsonWriter writer, Decimal value, JsonSerializerOptions options)
        {
            writer.WriteNumberValue(value);
        }
    }
}