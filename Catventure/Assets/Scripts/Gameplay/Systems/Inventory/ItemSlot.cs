using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Gameplay.Systems.Inventory
{
    public class ItemSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [HideInInspector]
        public ItemStack itemStack;
        public Sprite defaultImage;
        public Image icon;
        public Text valueText;
        private bool _isSlot, _isSet;
        private Inventory _inventory; // reference Inventory script instance

        private void Start()
        {
            icon = GetComponent<Image>();
            valueText = transform.GetComponentInChildren<Text>();
            
            _inventory = FindObjectOfType<Inventory>(); // find Inventory-script
            if (_inventory == null)
            {
                Debug.LogError("Inventory not found!");
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _isSlot = true;
            _inventory.ShowTooltip(this); // show tooltip on mouse hover enter
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _isSlot = false;
            _inventory.HideTooltip(); // hide tooltip on mouse hover leave
        }

        private void Update()
        {
            if (itemStack == null && _isSet)
            {
                Debug.LogWarning("ItemStack is null in ItemSlot: " + gameObject.name);
                _isSet = false;
            }
            
            if (_isSlot)
            {
                if (Input.GetButtonDown("Fire1"))
                {
                    if (itemStack != null && itemStack.count > 0)
                    {
                        Inventory.dragStack = itemStack;
                        itemStack = null;
                    }
                }
                else if (Input.GetButtonUp("Fire1"))
                {
                    var itemDrag = Inventory.dragStack;
                    if (itemDrag != null)
                    {
                        if (itemStack == null)
                        {
                            itemStack = itemDrag;
                            Inventory.dragStack = null;
                        }
                        else if (itemStack.count <= 0)
                        {
                            itemStack = itemDrag;
                            Inventory.dragStack = null;
                        }
                        else
                        {
                            if (itemDrag.GetItem().id == itemStack.GetItem().id)
                            {
                                var i = Inventory.dragStack.count;
                                Inventory.dragStack = itemStack.AddValue(i);
                            }
                        }
                    }
                }
            }

            if (itemStack != null && itemStack.count > 0)
            {
                if (itemStack.GetItem().icon is not null && !_isSet)
                {
                    icon.sprite = itemStack.GetItem().icon;
                    _isSet = true;
                }

                if (!valueText.text.Equals(itemStack.count + ""))
                {
                    valueText.text = itemStack.count + "";
                }
            }
            else
            {
                if (!_isSet) return;
                icon.sprite = defaultImage;
                valueText.text = "";
                _isSet = false;
            }
        }
    }
}
