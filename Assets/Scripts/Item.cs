using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "New Item/item")]
public class Item : ScriptableObject
{
    // Start is called before the first frame update
    public string itemName; //아이템 이름
    public ItemType itemType; //아이템의 유형
    public Sprite itemImage; //아이템 이미지
    public GameObject itemPrefad; //아이템의 프리펩

    public string weaponType; //무기 유형.

    public enum ItemType
    {
        Equipment,
        Used,
        Ingredient,
        ETC 
    }


}
