using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickSlotController : MonoBehaviour
{
    [SerializeField] private Slot[] quickSlots; //�����Ե�.
    [SerializeField] private Transform tf_parent; //�������� �θ� ��ü.

    private int selectedSlot; //���õ� ������. (0~7) = 8��.

    //�ʿ��� ������Ʈ
    [SerializeField]
    private GameObject go_SelectedImage; //���õ� �������� �̹���.
    [SerializeField]
    private WeaponManager theWeaponManager;

    // Start is called before the first frame update
    void Start()
    {
        quickSlots = tf_parent.GetComponentsInChildren<Slot>();
        selectedSlot= 0;
    }

    // Update is called once per frame
    void Update()
    {
        TryInputNumber();
    }
    
    private void TryInputNumber()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            ChangeSlot(0);
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            ChangeSlot(1);
        else if (Input.GetKeyDown(KeyCode.Alpha3))
            ChangeSlot(2);
        else if (Input.GetKeyDown(KeyCode.Alpha4))
            ChangeSlot(3);
        else if (Input.GetKeyDown(KeyCode.Alpha5))
            ChangeSlot(4);
        else if (Input.GetKeyDown(KeyCode.Alpha6))
            ChangeSlot(5);
        else if (Input.GetKeyDown(KeyCode.Alpha7))
            ChangeSlot(6);
        else if (Input.GetKeyDown(KeyCode.Alpha8))
            ChangeSlot(7);
    }

    public void IsActivatedQuickSlot(int _num)
    {

        if(selectedSlot == _num)
        {
            Execute();
            return;
        }
        if(DragSlot.instance != null)
        {
            if(DragSlot.instance.dragSlot!= null)
            {
                /*
                if (DragSlot.instance.dragSlot.GetQuickSlotNumber() == selectedSlot) //���� ����?
                {
                    Execute();
                    return;
                }
                */
            }    
      
        }
  
    }
    

    private void ChangeSlot(int _num)
    {
        SelectedSlot(_num); 

        Execute();
    }

    private void SelectedSlot(int _num)
    {
        //���õ� ����
        selectedSlot = _num;

        //���õ� �������� �̹��� �̵�
        go_SelectedImage.transform.position = quickSlots[selectedSlot].transform.position;
    }
    private void Execute()
    {
        if (quickSlots[selectedSlot].item != null)
        {
            if (quickSlots[selectedSlot].item.itemType == Item.ItemType.Equipment)
            {
                StartCoroutine(theWeaponManager.ChangeWeaponCoroutine(quickSlots[selectedSlot].item.weaponType, quickSlots[selectedSlot].item.itemName));
            }
            else if(quickSlots[selectedSlot].item.itemType == Item.ItemType.Used)
            {
                StartCoroutine(theWeaponManager.ChangeWeaponCoroutine("HAND", "�Ǽ�"));
            }
            else
            {
                StartCoroutine(theWeaponManager.ChangeWeaponCoroutine("HAND", "�Ǽ�"));
            }
        }
        else
        {
            StartCoroutine(theWeaponManager.ChangeWeaponCoroutine("HAND", "�Ǽ�"));
        }
    }
}

