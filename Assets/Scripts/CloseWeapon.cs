using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseWeapon : MonoBehaviour
{
    // Start is called before the first frame update
    public string closeWeaponName;  //��������.

    public bool isHand;
    public bool isAxe;
    public bool isPickaxe;

    public float range; //���� ����
    public int damage; //���ݷ�.
    public float workSpeed; // �۾��ӵ�.
    public float attackDelay; //���� ������
    public float attackDelatA; //���� Ȱ��ȭ ����.
    public float attackDelatB; //���� ��Ȱ��ȭ ����.

    public Animator anim; //�ִϸ��̼�


 
}
