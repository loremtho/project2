using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionController : MonoBehaviour
{
    [SerializeField]
    private float range; //습득 가능한 최대 거리

    private bool pickupActivated = false; //아이템 습득 가능할 시 True

    private bool lookComputer = false; //컴퓨터를 바라볼 경우 true


    private RaycastHit hitinfo; // 충돌체 정보 저장

    //아이템 레이어에만 반응하도록 레이어 마스크를 설정
    [SerializeField]
    private LayerMask layerMask;

    //필요한 컴포넌트
    [SerializeField]
    private Text actionText;
    [SerializeField]
    private Inventory theinventory;
    [SerializeField]
    private ComputerKit theComputer;


    void Update()
    {
        CheckItem();
        TryAction();
    }

    private void TryAction()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            CheckItem();
            CanPickUp();
            CanComputerPowerOn();
        }
    }

    private void CheckItem()
    {
        if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hitinfo, range, layerMask))
        {
            if(hitinfo.transform.tag == "Item")
            {
                ItemlnfoAppear();
            }
            else if(hitinfo.transform.tag == "Computer")
            {
                ComputerlnfoAppear();
            }
        }
        else
        {
            lnfoDisappear();
        }
    }

    private void CanPickUp()
    {
        if(pickupActivated)
        {
            if(hitinfo.transform != null)
            {
                Debug.Log(hitinfo.transform.GetComponent<ItemPickUp>().item.itemName + " ŉ�� ");
                theinventory.Acquireltem(hitinfo.transform.GetComponent<ItemPickUp>().item );
                Destroy(hitinfo.transform.gameObject);
                lnfoDisappear();
            }
        }
    }
    private void ItemlnfoAppear()
    {
        pickupActivated = true;
        actionText.gameObject.SetActive(true);
        actionText.text = hitinfo.transform.GetComponent<ItemPickUp>().item.itemName + "획득" + "<color=yellow>" + "(E)" + "</color>";
    }

    private void CanComputerPowerOn()
    {
        if(lookComputer)
        {
            if(hitinfo.transform != null)
            {
                if(!hitinfo.transform.GetComponent<ComputerKit>().isPowerOn)
                {
                    hitinfo.transform.GetComponent<ComputerKit>().PowerOn();
                    lnfoDisappear();
                }
            }
        }
    }

    private void ComputerlnfoAppear()
    {
        if(!hitinfo.transform.GetComponent<ComputerKit>().isPowerOn)
        {
            lookComputer = true;
            actionText.gameObject.SetActive(true);
            actionText.text =  " 컴퓨터 가동 " + "<color=yellow>" + "(E)" + "</color>";
        }
        
    }

    private void lnfoDisappear()
    {
        pickupActivated = false;
        lookComputer = false;
        actionText.gameObject.SetActive(false);
    }
}
