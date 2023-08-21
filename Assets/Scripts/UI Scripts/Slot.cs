using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{

    public Item item; //획득한 아이템   
    public int itemCount; //획득한 아이템의 개수
    public Image itemImage; // 아이템 이미지

    [SerializeField]
    private Text text_Count;
    [SerializeField]
    private GameObject go_CountImage;

    private ItemEffectDatabase theItemEffectDarabase;

    [SerializeField]
    private RectTransform baseRect; //인벤토리 영역
    [SerializeField]
    private RectTransform quickSlotBaseRect; //퀵슬롯의 영역.

    private InputNumber theInputNumber;
    


    void Start() 
    {
        theItemEffectDarabase = FindObjectOfType<ItemEffectDatabase>();
        theInputNumber = FindObjectOfType<InputNumber>();
    }

    private void SetColor(float _alpha)  //이미지 투명도 조절
    {
        Color color = itemImage.color;
        color.a = _alpha;
        itemImage.color = color;

    }


    public void AddItem(Item _item, int _count = 1) //아이템 획득
    {
        item = _item;
        itemCount= _count;
        itemImage.sprite = item.itemImage;
        if(item.itemType != Item.ItemType.Equipment)
        {
            go_CountImage.SetActive(true);
            text_Count.text = itemCount.ToString();
        }
        else
        {
            text_Count.text = "0";
            go_CountImage.SetActive(false);
        }
       

        SetColor(1);
    }

    public void SetSlotCount(int _count) //아이템 갯수 조정
    {
        itemCount= _count;
        text_Count.text = itemCount.ToString();

        if(itemCount <= 0)
        {
            ClearSlot();
        }

    }


    private void ClearSlot() //슬롯 초기화
    {
        item = null;
        itemCount = 0;
        itemImage.sprite = null;
        SetColor(0);

       
        text_Count.text = "0";
        go_CountImage.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Right)
        {
            if(item != null)
            {
              
                theItemEffectDarabase.UseItem(item);

                if(item.itemType == Item.ItemType.Used)
                {
                    SetSlotCount(-1);
                }
       
            }
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if(item != null)
        {
            DragSlot.instance.dragSlot = this;
            DragSlot.instance.DragSetImage(itemImage);
            DragSlot.instance.transform.position = eventData.position; //??
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(item != null)
        {
            DragSlot.instance.transform.position = eventData.position;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {

        if(!((DragSlot.instance.transform.localPosition.x > baseRect.rect.xMin && DragSlot.instance.transform.localPosition.x < baseRect.rect.xMax 
            && DragSlot.instance.transform.localPosition.y > baseRect.rect.yMin && DragSlot.instance.transform.localPosition.y < baseRect.rect.yMax)
           ||
           (DragSlot.instance.transform.localPosition.x >quickSlotBaseRect.rect.xMin && DragSlot.instance.transform.localPosition.x < quickSlotBaseRect.rect.xMax 
           && DragSlot.instance.transform.localPosition.y > quickSlotBaseRect.transform.localPosition.y - quickSlotBaseRect.rect.yMax && DragSlot.instance.transform.localPosition.y < quickSlotBaseRect.transform.localPosition.y - quickSlotBaseRect.rect.yMin)))
        {
            if(DragSlot.instance.dragSlot != null)
            {
                theInputNumber.Call();
            }
        }
        else
        {
            DragSlot.instance.SetColor(0);
            DragSlot.instance.dragSlot = null;
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        if(DragSlot.instance.dragSlot != null)
           ChangeSlot();
    }

    private void ChangeSlot()
    {
        Item _tempItem = item;
        int _tempItemCount = itemCount;

        AddItem(DragSlot.instance.dragSlot.item, DragSlot.instance.dragSlot.itemCount);

        if(_tempItem != null)
        {
            DragSlot.instance.dragSlot.AddItem(_tempItem, _tempItemCount);
        }
        else
        {
            DragSlot.instance.dragSlot.ClearSlot();
        }
    }

    //마우스가 슬롯에 들어갈떄 발동
    public void OnPointerEnter(PointerEventData eventData)
    {
        if(item != null)
        {
            theItemEffectDarabase.ShowToolTip(item, transform.position);
        }
      
    }

    //슬롯에서 빠저나갈떄 발동
    public void OnPointerExit(PointerEventData eventData)
    {
        theItemEffectDarabase.HideToolTip();
    }
}
