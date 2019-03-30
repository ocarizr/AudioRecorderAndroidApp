using System;
using UnityEngine;

namespace Assets.Scripts.Data
{
    [Serializable]
    public class AudioClipData
    {
        private AudioClip _clip;

        [SerializeField] public string ClipName = "";
        [SerializeField] public float[] ClipData;
        [SerializeField] public int Channels;
        [SerializeField] public int Frequency;

        public AudioClipData() { }

        public AudioClipData(string clipName, AudioClip clip)
        {
            ClipName = clipName;
            _clip = clip;
            ClipData = new float[clip.samples];
            _clip.GetData(ClipData, 0);
            Channels = _clip.channels;
            Frequency = _clip.frequency;
        }

        public AudioClip GetClip()
        {
            AudioClip clip = AudioClip.Create(ClipName, ClipData.Length, Channels, Frequency, false);
            clip.SetData(ClipData, 0);

            return clip;
        }
    }
}
