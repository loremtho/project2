using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    // Start is called before the first frame update
    public Item item; //흭득한 아이템
    public int itemCount; //흭득한 아이템 개수
    public Image itemImage; // 아이템의 이미지

    [SerializeField]
    private Text text_Count;
    [SerializeField]
    private GameObject go_CountImage;

    private void SetColor(float _alpha)  //이미지 투명도 조절
    {
        Color color = itemImage.color;
        color.a = _alpha;
        itemImage.color = color;

    }


    public void AddItem(Item _item, int _count = 1) //아이템 흭득
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

    public void SetSlotCoint(int _count) //아이템 개수 조정
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
}
