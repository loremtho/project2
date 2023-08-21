using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "New Item/item")]
public class Item : ScriptableObject
{
    // Start is called before the first frame update
    public string itemName; //������ �̸�
    public ItemType itemType; //�������� ����
    public Sprite itemImage; //������ �̹���
    public GameObject itemPrefad; //�������� ������

    public string weaponType; //���� ����.

    public enum ItemType
    {
        Equipment,
        Used,
        Ingredient,
        ETC 
    }


}
