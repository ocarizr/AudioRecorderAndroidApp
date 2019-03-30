using Assets.Scripts.Data;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class ItemList : MonoBehaviour
    {
        public GameObject ItemPrefab;

        //private AudioItem _item;
        private AudioClipData _data;

        public List<AudioItem> Items { get; private set; }

        private void Awake()
        {
            Items = new List<AudioItem>();
            //_item = ItemPrefab.GetComponent<AudioItem>();
        }

        public void AddNewItem(AudioClipData data)
        {
            DataManager.Instance.SaveRecordedAudio(data);
            _data = data;
            AddItem();
        }

        public void AddFromDatabase(AudioClipData data)
        {
            _data = data;
            AddItem();
        }

        private void AddItem()
        {
            Instantiate(ItemPrefab, transform);
            ItemPrefab.GetComponent<AudioItem>().ClipData = _data;
            Items.Add(ItemPrefab.GetComponent<AudioItem>());
        }
    }
}
