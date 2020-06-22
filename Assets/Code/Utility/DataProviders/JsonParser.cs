using System;
using UnityEngine;

namespace Code.Utility.DataProviders
{
    public class JsonParser
    {
        public string GetJsonText(string pathToText)
        {
            var jsonText = Resources.Load<TextAsset>(pathToText);
            
            if (jsonText != null) return jsonText.text;
            throw new NullReferenceException($"There is no file {pathToText} in Resources folder");
        }

        public T GetJsonDataObject<T>(string jsonPath)
        {
            var jsonText = GetJsonText(jsonPath);
            var jsonObject = JsonUtility.FromJson<T>(jsonText);
            if (jsonText != null) return jsonObject;
            throw new NullReferenceException($"Couldn't create object of type {nameof(T)} from {jsonPath}");
        }
    }
}