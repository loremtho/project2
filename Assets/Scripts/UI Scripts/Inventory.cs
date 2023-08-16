using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Inventory : MonoBehaviour
{
    public static bool inventoryActivated = false;

    [SerializeField] //ÇÊ¿äÇÑ ÄÄÆ÷³ÍÆ®
    private GameObject go_InventoryBase;

    [SerializeField]
    private GameObject go_SlotsParent;

    private Slot[] slots; //½½·Ôµé

    
    // Start is called before the first frame update
    void Start()
    {
        slots = go_SlotsParent.GetComponentsInChildren<Slot>();
    }

    // Update is called once per frame
    void Update()
    {
        TryOpenInventory();
    }

    private void TryOpenInventory()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            inventoryActivated = !inventoryActivated;

            if (inventoryActivated)
            {
                OpenInventory();
            }
            else
            {
                CloseInventory();
            }
        }
   
    }

    private void OpenInventory()
    {
        go_InventoryBase.SetActive(true);
    }

    private void CloseInventory()
    {
        go_InventoryBase.SetActive(false);
    }

    public void Acquireltem(Item _item, int _count = 1)
    {
        if(Item.ItemType.Equipment != _item.itemType)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].item != null)
                {
                    if (slots[i].item.itemName == _item.itemName)
                    {
                        slots[i].SetSlotCoint(_count);
                        return;
                    }
                }
            }
        }
    

        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null )
            {
                slots[i].AddItem(_item, _count);
                return;
            }

        }
    }

}
