using UnityEngine;

namespace Assets.Scripts.Data
{
    public class AudioClipData
    {
        public string ClipName { get; set; }
        public AudioClip Clip { get; set; }

        public AudioClipData() { }

        public AudioClipData(string clipName, AudioClip clip)
        {
            ClipName = clipName;
            Clip = clip;
        }
    }
}
