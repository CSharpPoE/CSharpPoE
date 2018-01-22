﻿using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace PathOfExile.Model
{
    internal class SockatableConverter : JsonConverter
    {
        public override bool CanWrite => false;


        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
            JsonSerializer serializer)
        {
            dynamic arr = JToken.Load(reader);
            var list = new List<SocketableItem>();

            foreach (var obj in arr)
            {
                switch (obj.category)
                {
                    case JValue value:
                        list.Add(obj.ToObject<Gem>());
                        break;
                    case JObject value:
                        list.Add(obj.ToObject<AbyssJewel>());
                        break;
                }
            }

            return list;
        }

        public override bool CanConvert(Type objectType) => objectType == typeof(SocketableItem);
    }
}