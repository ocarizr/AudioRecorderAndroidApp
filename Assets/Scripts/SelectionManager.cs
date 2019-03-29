using Assets.Scripts.Singleton;
using JetBrains.Annotations;

namespace Assets.Scripts
{
    public class SelectionManager : Singleton<SelectionManager>
    {
        [CanBeNull] public object Selected { get; private set; }

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);
        }

        public void Select(object data)
        {
            if (Selected != null)
            {
                AudioItem item = ApplicationManager.Instance.ItemList.Items.Find(obj => obj.ClipData == Selected);
                item.SelectItem.Invoke(false);
            }
            Selected = data;
        }

        public void Clear()
        {
            Selected = default;
        }
    }
}
