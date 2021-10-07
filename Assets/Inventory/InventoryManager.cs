using TMPro;
using UnityEngine;
using UnityEngine.UI;
using dynamicscroll;
using DynamicScroll;

namespace Inventory
{
    public class InventoryManager : MonoBehaviour
    {
        public GameObject inventoryItemPrefab;
        public GameObject container;
        [Multiline][Space(20f)]
        public string itemJson;
        public int itemGenerateScale = 10;
        public Sprite[] icons;
        [Space(20f)]
        public Image selectedItemImage;
        public TextMeshProUGUI selectedItemName;
        public TextMeshProUGUI selectedItemDescription;
        public TextMeshProUGUI selectedItemStat;
        [Space(20f)]
        public DynamicScrollRect verticalScroll;
        private DynamicScroll<InventoryItemData, InventoryItem> _dynamicScroll;
        public static InventoryManager InventoryManagerReference;
        private InventoryItem _currentItem ;
        private InventoryItem _previousItem ;


        /// <summary>
        /// Awake the instance .
        /// </summary>
        private void Awake()
        {
            if (InventoryManagerReference != null) return;
            InventoryManagerReference = this;
        }


        private void Start()
        {
            Init();
        }

        /// <summary>
        /// Initialize the containt .
        /// </summary>
        private  void Init()
        {
            //creating the object .
            _dynamicScroll = new DynamicScroll<InventoryItemData, InventoryItem> {spacing = 0f, centralizeOnStop = true};
            //Hold all the items extracted from json in an temp array .
            var itemDataArray = GenerateItemDataList(itemJson, itemGenerateScale);
            //Let dynamic scroll initiate the objects for you .
            _dynamicScroll.Initiate(verticalScroll, itemDataArray, 0, inventoryItemPrefab);
            //Attach with the global event.
            InventoryItemCallback.OnItemClicked += (sender, callbackArgs) => InventoryItemOnClick(callbackArgs.InventoryItemObject, callbackArgs.InventoryItemData);
            //Get the item.
            var item = _dynamicScroll.objectPool[0].GetComponent<InventoryItem>();
            //Call back object.
            var args = new InventoryItemCallbackArgs(itemDataArray[0],item);
            //Button onclick .
            InventoryItemCallback.OnItemClicked?.Invoke(null,args);
        }
        
        /// <summary>
        /// Get image name from the array .
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public string GetImageName(int index)
        {
            return icons[index].name;
        }

        private static InventoryItemData[] GenerateItemDataList(string json, int scale) 
        {
            var itemDataList = JsonUtility.FromJson<InventoryItems>(json).ItemDatas;
            var finalItemDataList = new InventoryItemData[itemDataList.Length * scale];
            for (var i = 0; i < itemDataList.Length; i++) {
                for (var j = 0; j < scale; j++) {
                    finalItemDataList[i + j*itemDataList.Length] = itemDataList[i];
                }
            }
            return finalItemDataList;
        }

        private void InventoryItemOnClick(InventoryItem itemClicked, InventoryItemData itemData)
        {
            _previousItem = _currentItem;
            _currentItem = itemClicked;
            if(_previousItem!= null)_previousItem.background.color = Color.yellow;
            _currentItem.background.color= Color.magenta;
            selectedItemImage.sprite = icons[itemData.IconIndex];
            selectedItemDescription.text = itemData.Description;
            selectedItemStat.text = itemData.Stat.ToString();
            selectedItemName.text = itemData.Name;
        }


    }
}
