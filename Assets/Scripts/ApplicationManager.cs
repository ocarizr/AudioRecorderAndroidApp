using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using TMPro;
using Assets.Scripts.Data;
using Assets.Scripts.Singleton;

namespace Assets.Scripts
{
    public class ApplicationManager : Singleton<ApplicationManager>
    {
        private const int MaxClipTime = 300;

        public Button AudioRecordButton;
        public Sprite RecordIcon;
        public Sprite StopIcon;
        public TMP_InputField Input;

        public ItemList ItemList;

        private string _clipName;
        private AudioClip _recordedClip;
        private float _startTime;

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);

            AudioRecordButton.onClick.AddListener(RecordFunction);
            Input.onEndEdit.AddListener((value) => _clipName = value);
        }

        private void Update()
        {
            if (Microphone.IsRecording("") && Time.time - _startTime >= MaxClipTime)
            {
                RecordFunction();
                _startTime = 0;
            }
        }

        private void RecordFunction()
        {
            if (!string.IsNullOrEmpty(_clipName) && DataManager.Instance.Items.All(item => item.ClipName != _clipName))
            {
                if (Microphone.IsRecording(""))
                {
                    Microphone.End("");
                    var adjustClip = AudioClip.Create(_clipName,
                        (int) (Time.time - _startTime) * _recordedClip.frequency, _recordedClip.channels,
                        _recordedClip.frequency, false);
                    var data = new float[adjustClip.samples];
                    _recordedClip.GetData(data, 0);
                    adjustClip.SetData(data, 0);
                    _recordedClip = adjustClip;
                    ItemList.AddNewItem(new AudioClipData(_clipName, _recordedClip));
                    AudioRecordButton.GetComponent<Image>().sprite = RecordIcon;
                }
                else
                {
                    SelectionManager.Instance.Clear();
                    _startTime = Time.time;
                    Microphone.GetDeviceCaps("", out int minFreq, out int maxFreq);
                    _recordedClip = Microphone.Start("", false, MaxClipTime, minFreq);
                    AudioRecordButton.GetComponent<Image>().sprite = StopIcon;
                }
            }
        }
    }
}
