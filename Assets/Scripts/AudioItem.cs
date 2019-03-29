using Assets.Scripts.Data;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Assets.Scripts
{
    public class AudioItem : MonoBehaviour, IPointerDownHandler
    {
        public UnityEvent<bool> SelectItem { get; set; }

        public AudioClipData ClipData { get; set; }
        public TextMeshProUGUI ItemLabel;
        public Button PlayPauseButton;
        public Image ItemBackground;
        public Image PlayIcon;
        public Image PauseIcon;

        public Color ItemIdle;
        public Color ItemSelected;

        private AudioSource _speaker;

        private void Awake()
        {
            ItemLabel.text = ClipData.ClipName;
            _speaker = GetComponent<AudioSource>();

            PlayPauseButton.onClick.AddListener(PlayPauseClip);
            PlayPauseButton.gameObject.SetActive(false);

            SelectItem.AddListener(SetSelected);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            SelectionManager.Instance.Select(ClipData);
            SetSelected();
        }

        private void SetSelected(bool selected = true)
        {
            if (selected)
            {
                PlayPauseButton.gameObject.SetActive(true);
                ItemBackground.color = ItemSelected;
            }
            else
            {
                PlayPauseButton.gameObject.SetActive(false);
                ItemBackground.color = ItemIdle;
            }
        }

        private void PlayPauseClip()
        {
            if (ClipData == null) return;
            if (_speaker.isPlaying)
            {
                _speaker.Pause();
                PlayIcon.gameObject.SetActive(true);
                PauseIcon.gameObject.SetActive(false);
            }
            else
            {
                _speaker.PlayOneShot(ClipData.Clip);
                PlayIcon.gameObject.SetActive(false);
                PauseIcon.gameObject.SetActive(true);
            }
        }
    }
}
