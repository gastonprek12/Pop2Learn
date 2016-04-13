using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
#if !UNITY_IOS
using System.Reflection;
#endif
using UnityEngine;
using System;
#if UNITY_WP8 && !UNITY_EDITOR
using UnityEngine.Windows;
#endif
#if NETFX_CORE
using System.Runtime.Serialization;
#endif

namespace Bigfoot
{
    public static class Serializer
    {

        public static byte[] SerializeObject<_T>(_T objectToSerialize)
        //same as above, but should technically work anyway
        {
            MemoryStream memStr = new MemoryStream();

#if UNITY_WP8 && !UNITY_EDITOR
        DataContractSerializer dcs = new DataContractSerializer(typeof(_T));

        dcs.WriteObject(memStr, objectToSerialize);
#else
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(memStr, objectToSerialize);
#endif

            memStr.Position = 0;

            return memStr.ToArray();
        }

        public static _T DeserializeObject<_T>(byte[] dataStream)
        {
            MemoryStream stream = new MemoryStream(dataStream);
            stream.Position = 0;

#if UNITY_WP8 && !UNITY_EDITOR
        DataContractSerializer dcs = new DataContractSerializer(typeof(_T));
        return (_T)dcs.ReadObject(stream);
#else

            BinaryFormatter bf = new BinaryFormatter();
            return (_T)bf.Deserialize(stream);
#endif
        }

        public static void SaveSerializableObjectToFile<_T>(_T serializableObjectToSave, string path)
        {
//#if !UNITY_WEBPLAYER && !UNITY_IOS && !UNITY_IPHONE && !UNITY_WP8
            File.WriteAllBytes(path, SerializeObject<_T>(serializableObjectToSave));
/*#elif UNITY_WP8 && !UNITY_EDITOR
        UnityEngine.Windows.File.WriteAllBytes(path, SerializeObject<_T>(serializableObjectToSave));
#else
        var b = new BinaryFormatter();
        var m = new MemoryStream();
        b.Serialize(m, serializableObjectToSave);
        PlayerPrefs.SetString(path,
            Convert.ToBase64String(
                m.GetBuffer()
            )
        );
#endif*/
        }

        public static _T LoadSerializableObjectFromFile<_T>(string path)
        {
//#if !UNITY_WEBPLAYER && !UNITY_IOS && !UNITY_IPHONE && !UNITY_WP8
           
            return DeserializeObject<_T>(File.ReadAllBytes(path));
            
/*#elif UNITY_WP8 && !UNITY_EDITOR
        if (UnityEngine.Windows.File.Exists(path))
		{
			return DeserializeObject<_T>(UnityEngine.Windows.File.ReadAllBytes(path));
		}         
#else
        var data = PlayerPrefs.GetString(path);
        if(!String.IsNullOrEmpty(data))
        {
            var b = new BinaryFormatter();
            var m = new MemoryStream(Convert.FromBase64String(data));
            return (_T)b.Deserialize(m);
        }
#endif
            return default(_T);*/
        }
    }
}