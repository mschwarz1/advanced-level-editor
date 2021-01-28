﻿using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
#if ALE_JSON
using System.Text;
using UnityEngine.Assertions;
using Newtonsoft.Json;
#endif

namespace Hertzole.ALE
{
    public static class LevelEditorSerializer
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void InitializeWritersReaders()
        {
            LevelEditorWriter<byte>.Write = (writer, value, name) => writer.Write(value, name);
            LevelEditorWriter<sbyte>.Write = (writer, value, name) => writer.Write(value, name);
            LevelEditorWriter<short>.Write = (writer, value, name) => writer.Write(value, name);
            LevelEditorWriter<ushort>.Write = (writer, value, name) => writer.Write(value, name);
            LevelEditorWriter<int>.Write = (writer, value, name) => writer.Write(value, name);
            LevelEditorWriter<uint>.Write = (writer, value, name) => writer.Write(value, name);
            LevelEditorWriter<long>.Write = (writer, value, name) => writer.Write(value, name);
            LevelEditorWriter<ulong>.Write = (writer, value, name) => writer.Write(value, name);
            LevelEditorWriter<float>.Write = (writer, value, name) => writer.Write(value, name);
            LevelEditorWriter<double>.Write = (writer, value, name) => writer.Write(value, name);
            LevelEditorWriter<decimal>.Write = (writer, value, name) => writer.Write(value, name);
            LevelEditorWriter<string>.Write = (writer, value, name) => writer.Write(value, name);
            LevelEditorWriter<char>.Write = (writer, value, name) => writer.Write(value, name);
            LevelEditorWriter<bool>.Write = (writer, value, name) => writer.Write(value, name);
            LevelEditorWriter<Vector2>.Write = (writer, value, name) => writer.Write(value, name);
            LevelEditorWriter<Vector2Int>.Write = (writer, value, name) => writer.Write(value, name);
            LevelEditorWriter<Vector3>.Write = (writer, value, name) => writer.Write(value, name);
            LevelEditorWriter<Vector3Int>.Write = (writer, value, name) => writer.Write(value, name);
            LevelEditorWriter<Vector4>.Write = (writer, value, name) => writer.Write(value, name);
            LevelEditorWriter<Quaternion>.Write = (writer, value, name) => writer.Write(value, name);
            LevelEditorWriter<Component>.Write = (writer, value, name) => writer.Write(value, name);
            LevelEditorWriter<Color>.Write = (writer, value, name) => writer.Write(value, name);
            LevelEditorWriter<Color32>.Write = (writer, value, name) => writer.Write(value, name);

            LevelEditorReader<byte>.Read = (reader) => reader.ReadByte();
            LevelEditorReader<sbyte>.Read = (reader) => reader.ReadSByte();
            LevelEditorReader<short>.Read = (reader) => reader.ReadShort();
            LevelEditorReader<ushort>.Read = (reader) => reader.ReadUShort();
            LevelEditorReader<int>.Read = (reader) => reader.ReadInt();
            LevelEditorReader<uint>.Read = (reader) => reader.ReadUInt();
            LevelEditorReader<long>.Read = (reader) => reader.ReadLong();
            LevelEditorReader<ulong>.Read = (reader) => reader.ReadULong();
            LevelEditorReader<float>.Read = (reader) => reader.ReadFloat();
            LevelEditorReader<double>.Read = (reader) => reader.ReadDouble();
            LevelEditorReader<decimal>.Read = (reader) => reader.ReadDecimal();
            LevelEditorReader<string>.Read = (reader) => reader.ReadString();
            LevelEditorReader<char>.Read = (reader) => reader.ReadChar();
            LevelEditorReader<bool>.Read = (reader) => reader.ReadBool();
            LevelEditorReader<Vector2>.Read = (reader) => reader.ReadVector2();
            LevelEditorReader<Vector2Int>.Read = (reader) => reader.ReadVector2Int();
            LevelEditorReader<Vector3>.Read = (reader) => reader.ReadVector3();
            LevelEditorReader<Vector3Int>.Read = (reader) => reader.ReadVector3Int();
            LevelEditorReader<Vector4>.Read = (reader) => reader.ReadVector4();
            LevelEditorReader<Quaternion>.Read = (reader) => reader.ReadQuaternion();
            LevelEditorReader<Component>.Read = (reader) => reader.ReadComponent();
            LevelEditorReader<Color>.Read = (reader) => reader.ReadColor();
            LevelEditorReader<Color32>.Read = (reader) => reader.ReadColor32();
        }

        private static readonly Dictionary<string, Action<LevelEditorWriter, object, string>> writers = new Dictionary<string, Action<LevelEditorWriter, object, string>>()
        {
            { "System.Byte", (writer, value, name) => writer.Write((byte)value, name) },
            { "System.SByte", (writer, value, name) => writer.Write((sbyte)value, name) },
            { "System.Int16", (writer, value, name) => writer.Write((short)value, name) },
            { "System.UInt16", (writer, value, name) => writer.Write((ushort)value, name) },
            { "System.Int32", (writer, value, name) => writer.Write((int)value, name) },
            { "System.UInt32", (writer, value, name) => writer.Write((uint)value, name) },
            { "System.Int64", (writer, value, name) => writer.Write((long)value, name) },
            { "System.UInt64", (writer, value, name) => writer.Write((ulong)value, name) },
            { "System.Single", (writer, value, name) => writer.Write((float)value, name) },
            { "System.Double", (writer, value, name) => writer.Write((double)value, name) },
            { "System.Decimal", (writer, value, name) => writer.Write((decimal)value, name) },
            { "System.String", (writer, value, name) => writer.Write((string)value, name) },
            { "System.Char", (writer, value, name) => writer.Write((char)value, name) },
            { "System.Boolean", (writer, value, name) => writer.Write((bool)value, name) },
            { "UnityEngine.Vector2", (writer, value, name) => writer.Write((Vector2)value, name) },
            { "UnityEngine.Vector2Int", (writer, value, name) => writer.Write((Vector2Int)value, name) },
            { "UnityEngine.Vector3", (writer, value, name) => writer.Write((Vector3)value, name) },
            { "UnityEngine.Vector3Int", (writer, value, name) => writer.Write((Vector3Int)value, name) },
            { "UnityEngine.Vector4", (writer, value, name) => writer.Write((Vector4)value, name) },
            { "UnityEngine.Quaternion", (writer, value, name) => writer.Write((Quaternion)value, name) },
            { "UnityEngine.Component", (writer, value, name) => writer.Write((Component)value, name) },
            { "UnityEngine.Color", (writer, value, name) => writer.Write((Color)value, name) },
            { "UnityEngine.Color32", (writer, value, name) => writer.Write((Color32)value, name) },
        };

        private static readonly Dictionary<string, Func<LevelEditorReader, object>> readers = new Dictionary<string, Func<LevelEditorReader, object>>()
        {
            { "System.Byte", (reader) => reader.ReadByte() },
            { "System.SByte", (reader) => reader.ReadSByte() },
            { "System.Int16", (reader) => reader.ReadShort() },
            { "System.UInt16", (reader) => reader.ReadUShort() },
            { "System.Int32", (reader) => reader.ReadInt() },
            { "System.UInt32", (reader) => reader.ReadUInt() },
            { "System.Int64", (reader) => reader.ReadLong() },
            { "System.UInt64", (reader) => reader.ReadULong() },
            { "System.Single", (reader) => reader.ReadFloat() },
            { "System.Double", (reader) => reader.ReadDouble() },
            { "System.Decimal", (reader) => reader.ReadDecimal() },
            { "System.String", (reader) => reader.ReadString() },
            { "System.Char", (reader) => reader.ReadChar() },
            { "System.Boolean", (reader) => reader.ReadBool() },
            { "UnityEngine.Vector2", (reader) => reader.ReadVector2() },
            { "UnityEngine.Vector2Int", (reader) => reader.ReadVector2Int() },
            { "UnityEngine.Vector3", (reader) => reader.ReadVector3() },
            { "UnityEngine.Vector3Int", (reader) => reader.ReadVector3Int() },
            { "UnityEngine.Vector4", (reader) => reader.ReadVector4() },
            { "UnityEngine.Quaternion", (reader) => reader.ReadQuaternion() },
            { "UnityEngine.Component", (reader) => reader.ReadComponent() },
            { "UnityEngine.Color", (reader) => reader.ReadColor() },
            { "UnityEngine.Color32", (reader) => reader.ReadColor32() },
        };

        public static byte[] SerializeBinary(LevelEditorSaveData data)
        {
            MemoryStream stream = new MemoryStream();
            LevelEditorWriter writer = new LevelEditorWriter(new BinaryWriter(stream));

            Serialize(data, writer);

            byte[] bytes = stream.ToArray();
            writer.Dispose();
            stream.Dispose();

            return bytes;
        }

#if !ALE_JSON
        [Obsolete("SerializeJson can only be used with the Json.Net package. Install 'com.unity.nuget-newtonsoft-json' package to use Json.")]
#endif
        public static string SerializeJson(LevelEditorSaveData data, bool prettyPrint = false)
        {
#if ALE_JSON
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);

            LevelEditorWriter writer = new LevelEditorWriter(new JsonTextWriter(sw) { Formatting = prettyPrint ? Formatting.Indented : Formatting.None });
            Serialize(data, writer);

            writer.Dispose();
            sw.Dispose();

            return sb.ToString();
#else
            return string.Empty;
#endif
        }

        public static LevelEditorSaveData DeserializeBinary(byte[] bytes)
        {
            MemoryStream stream = new MemoryStream(bytes);
            BinaryReader reader = new BinaryReader(stream);

            LevelEditorSaveData data = Deserialize(new LevelEditorReader(reader));

            reader.Dispose();
            stream.Dispose();

            return data;
        }

#if !ALE_JSON
        [Obsolete("SerializeJson can only be used with the Json.Net package. Install 'com.unity.nuget-newtonsoft-json' package to use Json.")]
#endif
        public static LevelEditorSaveData DeserializeJson(string json)
        {
#if ALE_JSON
            JsonTextReader reader = new JsonTextReader(new StringReader(json));

            LevelEditorSaveData data = Deserialize(new LevelEditorReader(reader));
            ((IDisposable)reader).Dispose();

            return data;
#else
            return new LevelEditorSaveData();
#endif
        }

        private static void Serialize(LevelEditorSaveData data, LevelEditorWriter writer)
        {
            Dictionary<string, int> objectPalette = new Dictionary<string, int>();
            int nextID = 0;
            for (int i = 0; i < data.objects.Count; i++)
            {
                if (!objectPalette.ContainsKey(data.objects[i].id))
                {
                    objectPalette.Add(data.objects[i].id, nextID);
                    nextID++;
                }
            }

            Dictionary<string, int> typePalette = new Dictionary<string, int>();
            nextID = 0;
            for (int i = 0; i < data.objects.Count; i++)
            {
                for (int j = 0; j < data.objects[i].components.Length; j++)
                {
                    LevelEditorComponentData comp = data.objects[i].components[j];

                    if (!typePalette.ContainsKey(comp.type))
                    {
                        typePalette.Add(comp.type, nextID);
                        nextID++;
                    }

                    for (int k = 0; k < comp.properties.Length; k++)
                    {
                        LevelEditorPropertyData prop = comp.properties[k];
                        if (!typePalette.ContainsKey(prop.typeName))
                        {
                            typePalette.Add(prop.typeName, nextID);
                            nextID++;
                        }
                    }
                }
            }

            foreach (KeyValuePair<string, LevelEditorCustomData> customData in data.customData)
            {
                if (!typePalette.ContainsKey(customData.Value.type))
                {
                    typePalette.Add(customData.Value.type, nextID);
                    nextID++;
                }
            }

            writer.WriteStartObject();

            writer.Write(data.name, "name");

            if (writer.IsJson)
            {
                writer.WriteStartObject("objectPalette");
                foreach (KeyValuePair<string, int> obj in objectPalette)
                {
                    writer.Write(obj.Value, obj.Key);
                }
                writer.WriteEndObject();
            }
            else
            {
                writer.WriteStartArray(objectPalette.Count, "objectPalette");
                // Write all the object IDs and their type ID.
                foreach (KeyValuePair<string, int> obj in objectPalette)
                {
                    writer.Write(obj.Key, "key");
                    writer.Write(obj.Value, "value");
                }
                writer.WriteEndArray();
            }

            if (writer.IsJson)
            {
                writer.WriteStartObject("typePalette");
                foreach (KeyValuePair<string, int> obj in typePalette)
                {
                    writer.Write(obj.Value, obj.Key);
                }
                writer.WriteEndObject();
            }
            else
            {
                writer.WriteStartArray(typePalette.Count, "typePalette"); // Write how many types there are.
                foreach (KeyValuePair<string, int> type in typePalette)
                {
                    writer.WriteStartObject();

                    writer.Write(type.Key, "key");
                    writer.Write(type.Value, "value");

                    writer.WriteEndObject();
                }

                writer.WriteEndArray();
            }

            writer.WriteStartArray(data.objects.Count, "objects"); // Write how many objects there are in the level.
            for (int i = 0; i < data.objects.Count; i++)
            {
                LevelEditorObjectData obj = data.objects[i];

                writer.WriteStartObject();

                writer.Write(objectPalette[obj.id], "id"); // Use the palette to save space.
                writer.Write(obj.instanceId, "instanceID");
                writer.Write(obj.name, "name");
                writer.Write(obj.active, "active");
                writer.WriteStartArray(obj.components.Length, "components"); // Write how many components there are on the object.
                for (int j = 0; j < obj.components.Length; j++)
                {
                    LevelEditorComponentData comp = obj.components[j];

                    writer.WriteStartObject();

                    writer.Write(typePalette[comp.type], "type"); // Use the palette to save space.
                    writer.WriteStartArray(comp.properties.Length, "properties"); // Write how many properties there are on the object.
                    for (int k = 0; k < comp.properties.Length; k++)
                    {
                        LevelEditorPropertyData prop = comp.properties[k];

                        writer.WriteStartObject();

                        writer.Write(prop.id, "id"); // Write property name.
                        writer.Write(typePalette[prop.typeName], "type"); // Write type name.
                        writer.Write(prop.isArray, "isArray"); // Write if it's an array.
                        if (prop.isArray)
                        {
                            throw new NotImplementedException($"Can not serialize {comp.type}, {prop.id}. Array support is not yet supported. Sorry!");

                            //object[] valueArray = (object[])prop.value;

                            //writer.Write(valueArray.Length); // Write array length.

                            //if (TryGetFormatter(prop.typeName, allFormatters, out BaseFormatter formatter))
                            //{
                            //    for (int v = 0; v < valueArray.Length; v++)
                            //    {
                            //        formatter.Serialize(valueArray[i], writer);
                            //    }
                            //}
                        }
                        else
                        {
                            if (writers.TryGetValue(prop.typeName, out Action<LevelEditorWriter, object, string> typeWriter))
                            {
                                typeWriter.Invoke(writer, prop.value, "value");
                            }
                            else
                            {
                                Debug.LogWarning($"No writer for type '{prop.typeName}'.");
                            }
                        }
                        writer.WriteEndObject(); // Property
                    }
                    writer.WriteEndArray(); // Properties
                    writer.WriteEndObject(); // Component
                }
                writer.WriteEndArray(); // Components
                writer.WriteEndObject(); // Object
            }

            writer.WriteEndArray();

            writer.WriteEndObject(); // Data
        }

        private static LevelEditorSaveData Deserialize(LevelEditorReader reader)
        {
            LevelEditorSaveData data = new LevelEditorSaveData();

            reader.ReadObjectStart();
            data.name = reader.ReadString();

            // The palette is reversed compared to the serialize method due to when reading you only get the int ID 
            // so it's much easier to map int to string.
            Dictionary<int, string> objectPalette = new Dictionary<int, string>();
            Dictionary<int, string> typePalette = new Dictionary<int, string>();

            ReadPalette(reader, objectPalette);
            ReadPalette(reader, typePalette);

            data.objects = new List<LevelEditorObjectData>();

            reader.ReadArray(true, (i) =>
            {
                int id = reader.ReadInt();
                int instanceID = reader.ReadInt();
                string name = reader.ReadString();
                bool active = reader.ReadBool();

                LevelEditorObjectData obj = new LevelEditorObjectData
                {
                    id = objectPalette[id],
                    instanceId = instanceID,
                    name = name,
                    active = active
                };

                List<LevelEditorComponentData> components = new List<LevelEditorComponentData>();

                reader.ReadArray(true, (j) =>
                {
                    LevelEditorComponentData comp = new LevelEditorComponentData();

                    List<LevelEditorPropertyData> properties = new List<LevelEditorPropertyData>();

                    int type = reader.ReadInt();
                    comp.type = typePalette[type];
                    reader.ReadArray(true, (k) =>
                    {
                        int propertyId = reader.ReadInt();
                        int propertyType = reader.ReadInt();
                        bool isArray = reader.ReadBool();

                        LevelEditorPropertyData prop = new LevelEditorPropertyData
                        {
                            id = propertyId,
                            typeName = typePalette[propertyType],
                            isArray = isArray
                        };

                        if (isArray)
                        {

                        }
                        else
                        {
                            if (readers.TryGetValue(prop.typeName, out Func<LevelEditorReader, object> typeReader))
                            {
                                prop.value = typeReader.Invoke(reader);
                            }
                            else
                            {
                                Debug.LogWarning($"No reader for type '{prop.typeName}'.");
                            }
                        }

                        properties.Add(prop);
                    });

                    comp.properties = properties.ToArray();
                    components.Add(comp);
                });

                obj.components = components.ToArray();
                data.objects.Add(obj);
            });

            reader.ReadObjectEnd(); // Data end

            return data;
        }

        private static void ReadPalette(LevelEditorReader reader, Dictionary<int, string> palette)
        {
            if (reader.IsJson)
            {
#if ALE_JSON
                reader.Json.Read();
                reader.Json.Read();
                Assert.AreEqual(reader.Json.TokenType, JsonToken.StartObject);
                while (reader.Json.TokenType != JsonToken.EndObject)
                {
                    reader.Json.Read();
                    if (reader.Json.TokenType == JsonToken.EndObject)
                    {
                        break;
                    }

                    if (reader.Json.Value is string key)
                    {
                        int value = (int)reader.Json.ReadAsInt32();
                        palette.Add(value, key);
                    }
                }
#endif
            }
            else
            {
                reader.ReadArray(false, (i) =>
                {
                    string stringId = reader.ReadString();
                    int intId = reader.ReadInt();
                    palette.Add(intId, stringId);
                });
            }
        }
    }
}
