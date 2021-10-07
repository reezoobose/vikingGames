using dynamicscroll;
using DynamicScroll;
using TMPro;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

namespace Inventory
{
    public class InventoryItem : DynamicScrollObject<InventoryItemData>
    {
        public Image background;
        public Image icon;
        public TextMeshProUGUI cellName;
        public Button button;
        public SpriteAtlas atlas;
        private InventoryItem _referenceOfItem;

        private void Awake()
        {
            _referenceOfItem = GetComponent<InventoryItem>();
        }
        public override void UpdateScrollObject(InventoryItemData item, int index)
        {
            base.UpdateScrollObject(item, index);
            //reset color.
            background.color = Color.yellow;
            //Set name of the cell .
            _referenceOfItem.cellName.text = item.Name;
            //Get imag name .
            InventoryManager.InventoryManagerReference.GetImageName(item.IconIndex);
            //Set image from atlas .
            var fetchedImageFomAtlas = atlas.GetSprite(InventoryManager.InventoryManagerReference.GetImageName(item.IconIndex));
            icon.sprite = fetchedImageFomAtlas;
            //Call back object.
            var args = new InventoryItemCallbackArgs(item,this);
            //Button onclick .
            button.onClick.AddListener(() => { 
                button.onClick.RemoveAllListeners();
                InventoryItemCallback.OnItemClicked?.Invoke(null,args);
            });

        }

        public override void SetPositionInViewport(Vector2 position, Vector2 distanceFromCenter)
        {
            base.SetPositionInViewport(position, distanceFromCenter);
        }
    }
        
}
