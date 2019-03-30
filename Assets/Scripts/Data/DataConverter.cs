using Assets.Scripts.Singleton;
using UnityEngine;

namespace Assets.Scripts.Data
{
    public class DataConverter : Singleton<DataConverter>
    {
        public string DataToString(AudioClipData data)
        {
            string s = JsonUtility.ToJson(data);

            print(s);

            return s;
        }

        public AudioClipData StringToData(string data)
        {
            AudioClipData clipData = JsonUtility.FromJson<AudioClipData>(data);
            return clipData;
        }
    }
}
