using UnityEngine;

namespace Kidbook.Models
{
    [System.Serializable]
    public abstract class Model<T> where T: Model<T>
    {
        public static T CreateFromJSON(string plainText)
        {
            return JsonUtility.FromJson<T>(plainText);
        }
    }
}

